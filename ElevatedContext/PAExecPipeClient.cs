using System;
using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ElevatedProcess
{
    /// <summary>
    /// Encapsulates a secure named pipe client for interprocess communication with a pipe server.
    /// <para/>This allows for interactive control of paexec.exe from the pipe server.
    /// </summary>
    class PAExecPipeClient
    {
        public static NamedPipeClientStream client;
        public event EventHandler<string> ServerMsgReceivedHandler;
        public event EventHandler<EventArgs> killProcessHandler;
        public bool Running { get; private set; }
        private readonly object threadLock = new object();
        private readonly int MaxLen = 4096;
        private ConcurrentQueue<byte[]> pendingWriteData = new ConcurrentQueue<byte[]>();
        private System.Timers.Timer heartbeatTimer;
        private bool HeartbeatReceived;
        

        /// <summary>
        /// Constructor for the pipe client. Requires an event handler for receiving messages from the server.
        /// </summary>
        /// <param name="pipeName">The name of the pipe. By default this is a guid generated outside the instance of this class.</param>
        /// <param name="serverMsgHandler">This is invoked when the client receives a message from the server.
        /// <para/>This should point to a method that writes to the paexec.exe input stream. 
        /// This way the pipe server can send commands through the pipe.</param>
        public PAExecPipeClient(EventHandler<string> serverMsgHandler, string pipeName, EventHandler<EventArgs> killProcess)
        {
            // event called when the client reads a message from the server. 
            ServerMsgReceivedHandler += serverMsgHandler;
            // event called when process should be killed out
            killProcessHandler += killProcess;

            Running = false;
            client = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.Impersonation);

            client.Connect();

            var t1 = new Thread(StartStream);
            t1.Start();
        }

        private void StartStream()
        {
            Running = true;
            StartReadingAsync(client);
            StartWritingAsync(client);
            StartHeartbeatTimer(30);
        }

        public void KillClient()
        {
            client.Close();
            Running = false;
        }

        /// <summary>
        /// Synchronously writes a message to the pipe, which can be read by the server.
        /// </summary>
        /// <param name="message">The message to send through the pipe.</param>
        /// <returns>True if message sent.</returns>
        public bool WriteToServer(string message)
        {
            try
            {
                byte[] msgByte = Encoding.UTF8.GetBytes(message);
                if (msgByte.Length > MaxLen)
                {
                    return false;
                }
                else
                {
                    var msgBuf = new byte[MaxLen];
                    Buffer.BlockCopy(msgByte, 0, msgBuf, 0, msgByte.Length);
                    pendingWriteData.Enqueue(msgBuf);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Asynchronously start reading data from the pipe server.
        /// </summary>
        /// <param name="pipe">The pipe client stream to read from.</param>
        private async Task StartReadingAsync(NamedPipeClientStream pipe)
        {
            byte[] inputBuffer = new byte[MaxLen];
            await pipe.ReadAsync(inputBuffer, 0, MaxLen).ContinueWith(async c =>
            {
                OnServerMsgReceived(Encoding.UTF8.GetString(inputBuffer));
                if (Running)
                {
                    await StartReadingAsync(pipe); // read the next data <-- 
                }
            });
        }

        private async Task StartWritingAsync(NamedPipeClientStream pipe)
        {
            while (Running)
            {
                Thread.Sleep(1);
                if (pendingWriteData.Count > 0)
                {
                    byte[] buf = new byte[MaxLen];
                    bool result = pendingWriteData.TryPeek(out buf);
                    int retryCount = 0;

                    while (!result && retryCount < 6)
                    {
                        Thread.Sleep(500);
                        result = pendingWriteData.TryPeek(out buf);
                        retryCount += 1;
                    }

                    await pipe.WriteAsync(buf, 0, MaxLen);
                    Thread.Sleep(1);
                    await pipe.FlushAsync();
                    lock (threadLock)
                    {
                        pendingWriteData.TryDequeue(out buf);
                    }
                }
            }
            Running = false;
        }

        protected virtual void OnServerMsgReceived(string e)
        {
            string trimmed = e.TrimEnd('\0');
            if (trimmed.StartsWith("echo **HEARTBEAT"))
            {
                HeartbeatReceived = true;
            }
            ServerMsgReceivedHandler?.Invoke(this, e.TrimEnd('\0'));
        }

        private void StartHeartbeatTimer(int second)
        {
            heartbeatTimer = new System.Timers.Timer(3 * 1000 * second);
            heartbeatTimer.Elapsed += HeartbeatTimerElapsed;
            heartbeatTimer.AutoReset = false;
            heartbeatTimer.Enabled = true;
            HeartbeatReceived = false;
        }

        private void HeartbeatTimerElapsed(Object source, ElapsedEventArgs e)
        {
            if (!HeartbeatReceived)
            {
                //WriteToServer("Heartbeat timeout... killing elevated process\r\n");
                killProcessHandler?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                HeartbeatReceived = false;
                heartbeatTimer.Start();
            }
        }


    }

}