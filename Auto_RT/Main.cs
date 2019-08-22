using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_RT
{
    public partial class Main : Form
    {
        private static PAExecService paexecService;
        private static AtomicRTdotNET atomicRT;        
        private bool shouldSendCommands = false;
        private EventHandler<string> handleStringFromClient;
        EditorForm editor = new EditorForm();
        About aboutForm = new About();

        public Main()
        {            
            InitializeComponent();
            handleStringFromClient = new EventHandler<string>(StringFromClientHandler);        
            atomicRT = new AtomicRTdotNET();
            paexecService = new PAExecService(handleStringFromClient);
        }

        #region Auto_RT Methods

        /// <summary>
        /// Begins the process of executing the commands that are "checked" in CmdQueueListView. <para/>
        /// Commands are sent to the remote computer via the PAExecService instance, and displayed in real time.
        /// </summary>
        private async void StartCmdQueue()
        {
            // because this is an asynchronous method, all access to the GUI thread (and its controls) must be through invokes.
            int numUnchecked = (int)CmdQueueListView.Invoke(new Func<int>(() => CmdQueueListView.Items.Count - CmdQueueListView.CheckedItems.Count));

            
            while (shouldSendCommands && CmdQueueListView.Items.Count > numUnchecked)
            {
                // is the item at top (index 0) of the queue checked?
                bool isChecked = (bool)CmdQueueListView.Invoke(new Func<bool>(() => CmdQueueListView.Items[0].Checked));

                if (isChecked)
                {                    
                    CheckIfConsoleAllowsInput:
                    string cmdText = (string)CmdOutput.Invoke(new Func<string>(() => CmdOutput.Text));
                    string endOfConsoleText = cmdText.Substring(cmdText.Length - Math.Min(10, cmdText.Length));
                    if (!endOfConsoleText.Contains('>'))
                    {
                        if (!shouldSendCommands) { break; } // stop endless loops if console dies
                        await Task.Delay(1000);
                        goto CheckIfConsoleAllowsInput; // check again after waiting                   
                    }

                    // send next command to the paexecService, remove from queue, then start over
                    string nextCmd = (string)CmdQueueListView.Invoke(new Func<string>(() => CmdQueueListView.Items[0].SubItems[1].Text.ToString()));
                    paexecService.WriteToClient(nextCmd.TrimEnd());
                    CmdQueueListView.Invoke(new Action(() => CmdQueueListView.Items.RemoveAt(0)));
                    await Task.Delay(500);
                    await Task.Run(() => StartCmdQueue());
                }
                else
                {
                    // if the command isn't checked, then move it to bottom of command queue
                    CmdQueueListView.Invoke(new Action(() => CmdQueueListView.Items.Add((ListViewItem)CmdQueueListView.Items[0].Clone())));
                    CmdQueueListView.Invoke(new Action(() => CmdQueueListView.Items.RemoveAt(0)));
                }
            }
            shouldSendCommands = false;
            StopExecutingButton.Invoke(new Action(() => StopExecutingButton.Enabled = false));
        }

        #endregion

        #region Big buttons

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            // write to console
            CmdOutput.Invoke(new Action(() => CmdOutput.AppendText("Launching pipes...\r\n")));
            
            // save auth mode in application settings
            Properties.Settings.Default.authMode = authTabControl.SelectedTab.Name;

            // start service, authenticating to target machine as specified
            if (Properties.Settings.Default.authMode == "elevated")
            {
                paexecService.StartService(PAExecEXELocation.Text, TestComputer.Text, AdminUsername.Text, 
                    ElevatedAccountEXELocation.Text);
            }
            else
            {
                paexecService.StartService(PAExecEXELocation.Text, TestComputer.Text, AdminUsername.Text,
                    userAuthTextbox.Text, passAuthTextbox.Text, ElevatedAccountEXELocation.Text);
            }
            
            // enable/disable buttons on gui
            KillButton.Enabled = paexecService.IsRunning;
            ConnectButton.Enabled = !paexecService.IsRunning;
            ExecuteButton.Enabled = (CmdQueueListView.Items.Count > 0);
        }

        private void LoadCommandsButton_click(object sender, EventArgs e)
        {
            // populate the CmdQueueListView with executors of the selected techniques
            var checkedTechniques = TechniqueListView.CheckedItems;            
            CmdQueueListView.BeginUpdate();
            CmdQueueListView.Clear();
            CmdQueueListView.Columns.Add("Technique");
            CmdQueueListView.Columns.Add("Command");
            CmdQueueListView.View = View.Details;
            foreach (ListViewItem item in checkedTechniques)
            {
                string technique = item.SubItems[0].Text.ToString(); // e.g. "T1017"
                var thisAtomic = atomicRT.GetAtomicByTechnique(technique);
                List<string> executors = atomicRT.BuildWindowsExecutors(thisAtomic); // assembles the commands
                foreach (string executor in executors)
                {
                    // add each command to the listview
                    CmdQueueListView.Items.Add(new ListViewItem(new[] { technique, executor }) { Checked = true });
                }
            }
            CmdQueueListView.Columns[0].Width = -2;
            CmdQueueListView.Columns[1].Width = -2;
            CmdQueueListView.EndUpdate();
            ExecuteButton.Enabled = KillButton.Enabled;
        }        

        private void KillButton_Click(object sender, EventArgs e)
        {
            // sends kill message to paexecService service
            paexecService.KillPipes();
            shouldSendCommands = false;
            CmdOutput.Invoke(new Action(() => CmdOutput.AppendText("\r\nKilling pipes...\r\n")));
            CmdOutput.Invoke(new Action(() => CmdOutput.AppendText("\r\nPipes killed.\r\n")));
            KillButton.Enabled = false;
            ConnectButton.Enabled = true;
            ExecuteButton.Enabled = false;
            
        }
        #endregion

        #region "Browse" dialog buttons

        // all of these just open a browse dialog and then put the result into a textbox
        
        private void BrowseElevatedAccountButton_Click(object sender, EventArgs e)
        {
            DialogResult accountResult = ElevatedAccountBrowseDialog.ShowDialog();
            if (accountResult == DialogResult.OK || accountResult == DialogResult.Yes)
            {
                ElevatedAccountEXELocation.Text = ElevatedAccountBrowseDialog.FileName;
            }
        }

        private void BrowsePAExecEXEButton_Click(object sender, EventArgs e)
        {
            DialogResult paExecResult = PAExecEXEBrowseDialog.ShowDialog();
            if (paExecResult == DialogResult.OK || paExecResult == DialogResult.Yes)
            {
                PAExecEXELocation.Text = PAExecEXEBrowseDialog.FileName;
            }
        }

        private void AtomicRTBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult atomicRTResult = AtomicRTBrowseDialog.ShowDialog();
            if (atomicRTResult == DialogResult.OK || atomicRTResult == DialogResult.Yes)
            {
                AtomicRTPathTextbox.Text = AtomicRTBrowseDialog.SelectedPath;
            }
        }
        #endregion

        #region Atomic RT controls

        private void LoadAtomicRTButton_Click(object sender, EventArgs e)
        {
            // msdn 4054d980-18f4-4857-bb47-0917ccdf4f4d

            atomicRT.AtomicsPath = AtomicRTPathTextbox.Text;
            Properties.Settings.Default.main_AtomicPath = AtomicRTPathTextbox.Text;
            // the following line will parse the "windows-index.md" file in the supplied folder for the atomics
            Dictionary<string, string> keyValues = atomicRT.ParseWindowsIndex();

            if (keyValues != null)
            {
                // if windows-index.md parses without errors, then populate the technique listview. 
                TechniqueListView.BeginUpdate();
                TechniqueListView.Clear();
                TechniqueListView.Columns.Add("Technique");
                TechniqueListView.Columns.Add("Description");
                TechniqueListView.View = View.Details;
                int i = 0;
                while (i < keyValues.Count)
                {
                    TechniqueListView.Items.Add(new ListViewItem(new[] { keyValues.Keys.ElementAt(i), keyValues.Values.ElementAt(i) }) { Checked = true });
                    i++;
                }
                TechniqueListView.Columns[0].Width = -2;
                TechniqueListView.Columns[1].Width = -2;
                TechniqueListView.EndUpdate();
                LoadCommandsButton.Enabled = true;
            }
        }

        private void EditorButton_click(object sender, EventArgs e)
        {
            // open the editor form
            Properties.Settings.Default.main_AtomicPath = AtomicRTPathTextbox.Text;            
            if (editor.IsDisposed)
            {
                editor = new EditorForm();
            }
            editor.Show();
        }

        private void CheckAllTechniqueButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in TechniqueListView.Items)
            {
                item.Checked = true;
            }
        }

        private void UncheckAllTechniqueButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in TechniqueListView.Items)
            {
                item.Checked = false;
            }
        }

        private void AtomicRTPathTextbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AtomicRTPathTextbox.Text))
            {
                LoadAtomicRTButton.Enabled = false;
            }
            else
            {
                LoadAtomicRTButton.Enabled = true;
            }
        }
        #endregion

        #region Command queue controls
                
        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            // begins process of executing the selected commands
            shouldSendCommands = true;
            StopExecutingButton.Enabled = true;
            await Task.Run(() => StartCmdQueue());
        }                

        private void SelectAllCmdButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in CmdQueueListView.Items)
            {
                item.Checked = true;
            }
        }

        private void DeselectAllCmdButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in CmdQueueListView.Items)
            {
                item.Checked = false;
            }
        }

        private void StopExecutingButton_Click(object sender, EventArgs e)
        {
            shouldSendCommands = false;
            StopExecutingButton.Enabled = false;
        }

        #endregion  

        #region Form load/close methods

        private void Main_Load(object sender, EventArgs e)
        {
            // the config from the previous session will be restored if this isn't the first run
            try
            {
                if (Properties.Settings.Default.FirstRun == false)
                {                    
                    if (Properties.Settings.Default.main_AtomicPath != null && System.IO.Directory.Exists(Properties.Settings.Default.main_AtomicPath))
                    {
                        AtomicRTPathTextbox.Text = Properties.Settings.Default.main_AtomicPath;
                        LoadAtomicRTButton_Click(sender, e);
                    }
                    try
                    {
                        authTabControl.SelectTab(Properties.Settings.Default.authMode);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                else
                {
                    
                    string paExecFileLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\paexec.exe";
                    if (System.IO.File.Exists(paExecFileLocation))
                    {
                        PAExecEXELocation.Text = paExecFileLocation;
                    }

                    string elevatedAccountFileLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ElevatedProcess.exe";
                    if (System.IO.File.Exists(elevatedAccountFileLocation))
                    {
                        ElevatedAccountEXELocation.Text = elevatedAccountFileLocation;
                    }
                }
            }
            catch
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save();
                TechniqueListView.BeginUpdate();
                TechniqueListView.Items.Clear();
                TechniqueListView.EndUpdate();
            }

            // enable/disable buttons that rely on atomicRT path
            if (!string.IsNullOrEmpty(AtomicRTPathTextbox.Text.ToString()))
            {
                LoadAtomicRTButton.Enabled = true;
                LoadCommandsButton.Enabled = true;
                atomicRT.AtomicsPath = AtomicRTPathTextbox.Text;
            }

            // enable/disable connect button based on required values
            ConnectButton.Enabled = !string.IsNullOrEmpty(AdminUsername.Text.ToString()) && !string.IsNullOrEmpty(PAExecEXELocation.Text.ToString()) &&
                !string.IsNullOrEmpty(TestComputer.Text.ToString()) && !string.IsNullOrEmpty(ElevatedAccountEXELocation.Text.ToString());
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // kill paexec service upon close
            try
            {
                if (paexecService.IsRunning)
                {
                    paexecService.KillPipes();
                }
            }
            catch
            {
                // do nothing
            }
            Properties.Settings.Default.authMode = authTabControl.SelectedTab.Name;
            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.Save();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Control "changed" event methods

        // these methods determine the appearance of the GUI in response to changes

        private void AdminUsername_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = !string.IsNullOrEmpty(AdminUsername.Text.ToString()) && !string.IsNullOrEmpty(PAExecEXELocation.Text.ToString()) &&
                !string.IsNullOrEmpty(TestComputer.Text.ToString()) && !string.IsNullOrEmpty(ElevatedAccountEXELocation.Text.ToString()) ;
        }

        private void ElevatedAccountEXELocation_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = !string.IsNullOrEmpty(AdminUsername.Text.ToString()) && !string.IsNullOrEmpty(PAExecEXELocation.Text.ToString()) &&
                !string.IsNullOrEmpty(TestComputer.Text.ToString()) && !string.IsNullOrEmpty(ElevatedAccountEXELocation.Text.ToString()) ;
        }

        private void TestComputer_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = !string.IsNullOrEmpty(AdminUsername.Text.ToString()) && !string.IsNullOrEmpty(PAExecEXELocation.Text.ToString()) &&
                !string.IsNullOrEmpty(TestComputer.Text.ToString()) && !string.IsNullOrEmpty(ElevatedAccountEXELocation.Text.ToString()) ;
        }        

        private void PAExecEXELocation_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = !string.IsNullOrEmpty(AdminUsername.Text.ToString()) && !string.IsNullOrEmpty(PAExecEXELocation.Text.ToString()) &&
                !string.IsNullOrEmpty(TestComputer.Text.ToString()) && !string.IsNullOrEmpty(ElevatedAccountEXELocation.Text.ToString()) ;
        }        

        private void LaunchButton_EnabledChanged(object sender, EventArgs e)
        {
            if(ExecuteButton.Enabled)
            {
                ExecuteButton.BackColor = System.Drawing.Color.FromArgb(233, 92, 92);
            }
            else
            {
                ExecuteButton.BackColor = System.Drawing.Color.FromArgb(170, 170, 170);
            }
        }

        private void AuthTabControl_Selected(object sender, TabControlEventArgs e)
        {
            Properties.Settings.Default.authMode = authTabControl.SelectedTab.Name;
            Properties.Settings.Default.Save();
        }

        private void CmdOutput_TextChanged(object sender, EventArgs e)
        {
            bool isrunning = paexecService.IsRunning;
            KillButton.Invoke(new Action(() => KillButton.Enabled = isrunning));
            ConnectButton.Invoke(new Action(() => ConnectButton.Enabled = isrunning)); ;
            
        }

        #endregion

        #region Handler methods

        private static void StringToClientHandler(object o, string e)
        {
            paexecService.WriteToClient(e);
        }

        private void StringFromClientHandler(object o, string e)
        {
            if (e.StartsWith("echo **HEARTBEAT"))
            {
                // do not print to cmd
            }
            else
            {
                CmdOutput.Invoke(new Action(() => CmdOutput.AppendText(e.ToString())));
            }
            
        }


        #endregion

        #region MenuStrip
        private void getAtomicRedTeamGithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/redcanaryco/atomic-red-team");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutForm.IsDisposed)
            {
                aboutForm = new About();
            }
            aboutForm.Show();

        }

        #endregion

        
    }


}
