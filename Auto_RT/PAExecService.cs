using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace Auto_RT
{
    /// <summary>
    /// Encapsulates a secure named pipe server for interprocess communication with an elevated command window in a different user context.
    /// <para/>This allows for interactive control of paexec.exe from within the local user context, after prompting for elevated credentials.
    /// </summary>
    public class PAExecService
    {
        private NamedPipeServerStream server;
        private readonly int MaxLen = 4096;
        private readonly object threadLock = new object();
        private ConcurrentQueue<string> PendingMessages = new ConcurrentQueue<string>();
        private Process clientProcess;
        private readonly string pipeUser;
        private System.Timers.Timer heartbeatTimer;

        public bool ShouldRun { get; private set; }
        public bool IsRunning { get; private set; } = false;
        public bool HeartbeatReceived { get; private set; }

        public event EventHandler<string> ClientMsgReceivedHandler; // event handler to allow for delegated output handling 

        /// <summary>
        /// Default constructor for PAExecService. Requires an event handler to be handle the string data read by the server.
        /// </summary>
        /// <param name="msgReceivedHandler">
        /// This is invoked when the server receives data from the pipe.
        /// <para/>Can be used (e.g.) to print received messages to console.
        /// </param>
        /// 
        public PAExecService(EventHandler<string> msgReceivedHandler)
        {
            ClientMsgReceivedHandler += msgReceivedHandler;
            pipeUser = WindowsIdentity.GetCurrent().Name;
    }

        /// <summary>
        /// Launches the paexec.exe service. Calling this method will prompt for elevated credentials because the program runs in two different user contexts.
        /// </summary>
        /// <param name="paexec_EXE">The full path to the "paexec.exe" binary.</param>
        /// <param name="testComputer">The name of the test computer to launch paexec.exe on."</param>
        /// <param name="elevatedUsername">
        /// The username of the elevated account that will be used to launch paexec. Should include the account domain.
        /// <para/>E.g.    <paramref name="elevatedUsername"/> = @"domain\\administrator.account"
        /// </param>
        /// <param name="elevatedContext_EXE">The full path to the ElevatedProcess.exe binary.</param>
        public bool StartService(string paexec_EXE, string testComputer, string elevatedUsername, string elevatedContext_EXE)
        {
            string pipeName = Guid.NewGuid().ToString();
            try
            {
                // configure pipe security
                PipeSecurity pipeSecurity = new PipeSecurity();
                pipeSecurity.AddAccessRule(new PipeAccessRule(elevatedUsername, PipeAccessRights.FullControl, AccessControlType.Allow));
                pipeSecurity.AddAccessRule(new PipeAccessRule(pipeUser, PipeAccessRights.FullControl, AccessControlType.Allow));
                // launch server first
                server = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 2, PipeTransmissionMode.Message,
                    PipeOptions.Asynchronous, MaxLen, MaxLen, pipeSecurity)
                { ReadMode = PipeTransmissionMode.Message };
                // this is the admin/elevated process that runs paexec.exe and acts as a pipe client
                clientProcess = new Process 
                {
                    StartInfo = new ProcessStartInfo(elevatedContext_EXE)
                    { Verb = "RunAs", Arguments = string.Join("", "\"", paexec_EXE, "\" ", pipeName, " \"\\\\", testComputer.Trim(), " -h cmd\"") }
                };

                ClientMsgReceivedHandler?.Invoke(this, "Prompting for elevated credentials needed to launch paexec.exe wrapper.\r\n");
                clientProcess.Start(); // will prompt for credentials, due to "RunAs" verb
                Thread.Sleep(500);

                // Async await for connection. if shut down flag is set, it will stop
                ShouldRun = true;
                serverconnect:
                while (ShouldRun)
                {
                    try
                    {
                        IAsyncResult asyncResult = server.BeginWaitForConnection(HandleConnection, server);
                        int waitResult = WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
                        if (waitResult == 0)
                        {
                            server.EndWaitForConnection(asyncResult);
                            IsRunning = true;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        ClientMsgReceivedHandler?.Invoke(this, "Server connection error. Retrying... \r\n\r\n");
                        IsRunning = false;
                        server.Close();
                        continue;
                    }

                }
                
                // after connection is made, await messages over the pipe.
                try
                {
                    new Task(ReadFromClientTask).Start();
                    new Task(WriteToClientTask).Start();
                    StartHeartbeatTimer(60);
                    ClientMsgReceivedHandler?.Invoke(this, "Connected to paexec.exe wrapper. Redirecting output...\r\n\r\n");
                    return true;
                }
                catch (Exception)
                {
                    server.Close();
                    IsRunning = false;
                    ClientMsgReceivedHandler?.Invoke(this, "Pipe exception. Resetting server... \r\n");
                    Thread.Sleep(1500);
                    goto serverconnect;
                }               
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error launching the paexec.exe pipe service. \r\n\r\n" + e.ToString());
                if(server.IsConnected) { server.Close(); }
                return false;
            }
        }

        public bool StartService(string paexec_EXE, string testComputer, string elevatedUsername, string remoteUser, string remotePw, string elevatedContext_EXE)
        {
            string pipeName = Guid.NewGuid().ToString();
            try
            {
                // configure pipe security
                PipeSecurity pipeSecurity = new PipeSecurity();
                pipeSecurity.AddAccessRule(new PipeAccessRule(elevatedUsername, PipeAccessRights.FullControl, AccessControlType.Allow));
                pipeSecurity.AddAccessRule(new PipeAccessRule(pipeUser, PipeAccessRights.FullControl, AccessControlType.Allow));
                // launch server first
                server = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 2, PipeTransmissionMode.Message,
                    PipeOptions.Asynchronous, MaxLen, MaxLen, pipeSecurity)
                { ReadMode = PipeTransmissionMode.Message };
                // this is the admin/elevated process that wraps paexec.exe and acts as a pipe client
                clientProcess = new Process
                {
                    StartInfo = new ProcessStartInfo(elevatedContext_EXE)
                    { Verb = "RunAs", Arguments = string.Join("", "\"", paexec_EXE, "\" ", pipeName, " \"\\\\", testComputer.Trim(), " -u ", remoteUser, " -p ", remotePw, " -h cmd\"") }
                };

                ClientMsgReceivedHandler?.Invoke(this, "Prompting for elevated credentials needed to launch paexec.exe wrapper.\r\n");
                clientProcess.Start(); // will prompt for credentials, due to "RunAs" verb
                Thread.Sleep(500);

                // Async await for connection. if shut down flag is set, it will stop
                ShouldRun = true;
            serverconnect:
                while (ShouldRun)
                {
                    try
                    {
                        IAsyncResult asyncResult = server.BeginWaitForConnection(HandleConnection, server);
                        int waitResult = WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
                        if (waitResult == 0)
                        {
                            server.EndWaitForConnection(asyncResult);
                            IsRunning = true;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        ClientMsgReceivedHandler?.Invoke(this, "Server connection error. Retrying... \r\n\r\n");
                        IsRunning = false;
                        server.Close();
                        continue;
                    }

                }

                // after connection is made, await messages over the pipe.
                try
                {
                    new Task(ReadFromClientTask).Start();
                    new Task(WriteToClientTask).Start();
                    StartHeartbeatTimer(30);
                    ClientMsgReceivedHandler?.Invoke(this, "Connected to paexec.exe wrapper. Redirecting output...\r\n\r\n");
                    return true;
                }
                catch (Exception)
                {
                    server.Close();
                    IsRunning = false;
                    ClientMsgReceivedHandler?.Invoke(this, "Pipe exception. Resetting server... \r\n");
                    Thread.Sleep(1500);
                    goto serverconnect;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error launching the paexec.exe pipe service. \r\n\r\n" + e.ToString());
                if (server.IsConnected) { server.Close(); }
                return false;
            }
        }

        /// <summary>
        /// Call this method to kill the pipe system. This will kill the client and the server.
        /// </summary>
        public void KillPipes()
        {
            ShouldRun = false;
            WriteToClient("**KILLELEVATEDACCOUNT");
            Thread.Sleep(500);
            IsRunning = false;
            server.Close();
            heartbeatTimer.Stop();
        }

        /// <summary>
        /// Write a message (asynchronously) to the pipe, via the server stream.
        /// </summary>
        /// <param name="msg">The message to write to the pipe.</param>
        public void WriteToClient(string msg)
        {
            if (msg == null)
            {
                return;
            }

            lock (threadLock)
            {
                PendingMessages.Enqueue(msg);
            }
        }

        /// <summary>
        /// Handles messages received from the client. 
        /// </summary>
        protected virtual void OnClientMsgReceived(string e)
        {
            string trimString = e.TrimEnd('\0');
            if (trimString.StartsWith("**HEARTBEAT"))
            {           
                HeartbeatReceived = true;
            }
            else
            {
                ClientMsgReceivedHandler?.Invoke(this, trimString);
            }            
        }


        /// <summary>
        /// Async method to write pending data to the standard input stream of the command line.
        /// </summary>
        private async void WriteToClientTask()
        {
            while (ShouldRun)
            {
                Thread.Sleep(1);
                if (!PendingMessages.IsEmpty) // AND if it is a good time to write to client
                {
                    byte[] msgByte = new byte[MaxLen];
                    string peekMsg = string.Empty;
                    PendingMessages.TryPeek(out peekMsg);
                    byte[] strBuf = Encoding.UTF8.GetBytes(peekMsg);
                    Buffer.BlockCopy(strBuf, 0, msgByte, 0, strBuf.Length);
                    await server.WriteAsync(msgByte, 0, MaxLen);
                    Thread.Sleep(1);
                    await server.FlushAsync();
                    lock (threadLock)
                    {
                        PendingMessages.TryDequeue(out var result);
                    }
                }
            }
            KillPipes();
        }

        /// <summary>
        /// Async method to read the output stream of the command line. 
        /// </summary>
        private async void ReadFromClientTask()
        {
            while (ShouldRun)
            {
                byte[] buffer = new byte[MaxLen];                
                await server.ReadAsync(buffer, 0, MaxLen);
                OnClientMsgReceived(Encoding.UTF8.GetString(buffer));
                Thread.Sleep(1);
            }
            KillPipes();
        }

        /// <summary>
        /// Handles async result of connecting to client. Pauses thread for 1000ms while connection validates.
        /// </summary>
        private void HandleConnection(IAsyncResult asyncResult)
        {
            Thread.Sleep(1000);
        }

        private void StartHeartbeatTimer(int second)
        {
            heartbeatTimer = new System.Timers.Timer(1000 * second);
            heartbeatTimer.Elapsed += CheckHeartbeat;
            heartbeatTimer.AutoReset = true;
            heartbeatTimer.Enabled = true;
            HeartbeatReceived = true;
        }

        private void CheckHeartbeat(Object source, ElapsedEventArgs e)
        {
            if(!HeartbeatReceived)
            {
                ClientMsgReceivedHandler?.Invoke(this, "\t\theartbeat timeout");
            }
            else
            {
                WriteToClient("echo **HEARTBEAT");
                HeartbeatReceived = false;
            }            
        }
    }


}
