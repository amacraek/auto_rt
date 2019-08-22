using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatedProcess
{
    /// <summary>
    /// Wrapper for paexec.exe command line. Used to execute and interact with paexec.exe from .NET environment.
    /// </summary>
    public class PAExecWrapper
    {
        // based on stack overflow 21848271        
        private readonly Process cmd = new Process();
        private readonly object threadLock = new object();
        private string pendingWriteData;
        private readonly string exeLocation;
        public bool Running { get; private set; }
        public event EventHandler<string> ErrorTextReceived;
        public event EventHandler ProcessExited;
        public event EventHandler<string> ReceivedPAExecMsg;

        /// <summary>
        /// Constructor for PAExecWrapper. This does not initiate the paexec.exe process.
        /// <para/>To initiate paexec.exe, use PAExecWrapper.Launch(@"//server ... [commands here]")
        /// </summary>
        /// <param name="paexec_EXE">Full directory to paexec.exe binary.</param>
        /// <param name="paexecMsgHandler">Event handler for the paexec.exe standard output stream.
        /// <para/>Can be used (e.g.) to print paexec.exe output to console.</param>
        public PAExecWrapper(string paexec_EXE, EventHandler<string> paexecMsgHandler)
        {
            // wrapper fields
            ReceivedPAExecMsg += paexecMsgHandler;
            ErrorTextReceived += paexecMsgHandler;
            exeLocation = paexec_EXE;
            // process start info for cmd.exe
            cmd.StartInfo.Verb = "RunAs";
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.EnableRaisingEvents = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            cmd.Exited += ProcessExited;
        }

        /// <summary>
        /// Launches a wrapped cmd.exe and runs the starting command "[path to paexec.exe] [arguments]".
        /// <para/>Arguments are required to launch the wrapped executable.
        /// </summary>
        /// <param name="arg">Arguments with which paexec.exe are launched. Precede this with an '@' to escape special characters.
        /// <para/>E.g. wrapper.Launch(@"\\thiscomputer -h cmd")</param>
        public void Launch(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new InvalidOperationException(
                    "Cannot launch paexec.exe without input arguments.");
            }

            if (Running)
            {
                throw new InvalidOperationException(
                    "Unable to launch new process, because a command-line process is still running on this PAExecWrapper instance.");
            }

            // start cmd 
            cmd.Start();
            Running = true;

            // start async i/o stream redirection
            new Task(ReadFromPAExecTask).Start();
            new Task(WriteToPAExecTask).Start();
            new Task(ReadErrorPAExecTask).Start();

            // launch paexec.exe with given arguments
            string launchString = string.Join(" ", exeLocation, arg);
            WriteToPAExec(launchString);
        }

        public void KillWrapper()
        {
            WriteToPAExec("exit");
            Thread.Sleep(250);
            cmd.Kill();
            Thread.Sleep(500);
            Running = false;
        }

        /// <summary>
        /// Write a command to the paexec.exe wrapper.
        /// </summary>
        /// <param name="command">The command to write.</param>
        public void WriteToPAExec(string command)
        {
            if (command == null)
            {
                return;
            }

            lock (threadLock)
            {
                pendingWriteData = command;
            }
        }

        protected virtual void OnErrorTextReceived(string e)
        {
            ErrorTextReceived?.Invoke(this, e);
        }

        protected virtual void OnProcessExited(object sender, EventArgs eventArgs)
        {
            ProcessExited?.Invoke(sender, eventArgs);
        }

        protected virtual void OnPAExecMsgReceived(string e)
        {
            ReceivedPAExecMsg?.Invoke(this, e);
        }

        /// <summary>
        /// Async method to read the output stream of the command line. 
        /// </summary>
        private async void ReadFromPAExecTask()
        {
            var Standard = new StringBuilder();
            var buff = new char[4096];
            int length;

            while (cmd.HasExited == false && Running)
            {
                // this might be memory inefficient
                Standard.Clear();
                length = await cmd.StandardOutput.ReadAsync(buff, 0, buff.Length);
                Standard.Append(buff.SubArray(0, length));
                string test = Standard.ToString();
                OnPAExecMsgReceived(Standard.ToString());
                Thread.Sleep(1);
            }

            Running = false;
        }

        /// <summary>
        /// Async method to read the output error stream of the command line.
        /// </summary>
        private async void ReadErrorPAExecTask()
        {
            var sb = new StringBuilder();

            while (cmd.HasExited == false && Running)
            {
                sb.Clear();
                var buff = new char[4096];
                int length = await cmd.StandardError.ReadAsync(buff, 0, buff.Length);
                sb.Append(buff.SubArray(0, length));
                OnErrorTextReceived(sb.ToString());
                Thread.Sleep(1);
            }
            Running = false;
        }

        /// <summary>
        /// Async method to write pending data to the standard input stream of the command line.
        /// </summary>
        private async void WriteToPAExecTask()
        {
            while (cmd.HasExited == false && Running)
            {
                Thread.Sleep(1);
                if (pendingWriteData != null)
                {
                    await cmd.StandardInput.WriteLineAsync(pendingWriteData);
                    Thread.Sleep(1);
                    await cmd.StandardInput.FlushAsync();
                    lock (threadLock)
                    {
                        pendingWriteData = null;
                    }
                }
            }
            Running = false;
        }
    }

    public static class CharArrayExtensions
    {
        public static char[] SubArray(this char[] input, int startIndex, int length)
        {
            List<char> result = new List<char>();
            for (int i = startIndex; i < length; i++)
            {
                result.Add(input[i]);
            }
            return result.ToArray();
        }
    }
}
