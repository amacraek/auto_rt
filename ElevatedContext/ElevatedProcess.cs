using System;
using System.Threading;


namespace ElevatedProcess
{
    class ElevatedProcess
    {
        private static PAExecPipeClient ElevatedPipeClient;
        private static PAExecWrapper PAExec;
        private static bool shouldKill = false;

        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                EventHandler<string> stringFromPAExec = new EventHandler<string>(StringFromPAExecHandler);
                EventHandler<string> stringFromServer = new EventHandler<string>(StringFromServerHandler);
                EventHandler<EventArgs> killProcess = new EventHandler<EventArgs>(KillProcessHandler);

                string paexec_EXE = args[0];
                string pipeName = args[1];
                string launchCommand = args[2];

                PAExec = new PAExecWrapper(paexec_EXE, stringFromPAExec);
                PAExec.Launch(launchCommand);

                ElevatedPipeClient = new PAExecPipeClient(stringFromServer, pipeName, killProcess);

                while (!shouldKill)
                {
                    Thread.Sleep(3000);
                }
            }
            else
            {
                throw new ApplicationException("ElevatedProcess.exe must be launched with three command-line parameters: [path to paexec.exe] [username] [command to launch paexec with]");
            }


        }

        /// <summary>
        /// Passes a string from PAExec to the server.
        /// </summary>
        private static void StringFromPAExecHandler(object o, string e)
        {
            ElevatedPipeClient.WriteToServer(e);
        }

        /// <summary>
        /// Passes a string from the server to PAExec.
        /// </summary>
        private static void StringFromServerHandler(object o, string e)
        {
            if (e.Contains("**KILLELEVATEDACCOUNT"))
            {
                PAExec.KillWrapper();
                ElevatedPipeClient.KillClient();
                shouldKill = true;
            }
            else
            {
                PAExec.WriteToPAExec(e);
            }
        }

        /// <summary>
        /// Kills ElevatedProcess.exe
        /// </summary>
        private static void KillProcessHandler(object o, EventArgs e)
        {
            PAExec.KillWrapper();
            ElevatedPipeClient.KillClient();
            shouldKill = true;
        }

        // For named pipes: stack overflow 16432813, 13806153, 1179195 
    }
}
