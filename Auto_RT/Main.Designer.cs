namespace Auto_RT
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.CmdOutput = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.KillButton = new System.Windows.Forms.Button();
            this.ElevatedAccountLabel = new System.Windows.Forms.Label();
            this.ElevatedAccountGroup = new System.Windows.Forms.GroupBox();
            this.BrowseElevatedAccountButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ElevatedAccountEXELocation = new System.Windows.Forms.TextBox();
            this.AdminUsername = new System.Windows.Forms.TextBox();
            this.TestComputerLabel = new System.Windows.Forms.Label();
            this.PAExecLabel = new System.Windows.Forms.Label();
            this.ElevatedAccountBrowseDialog = new System.Windows.Forms.OpenFileDialog();
            this.PAExecEXEBrowseDialog = new System.Windows.Forms.OpenFileDialog();
            this.LoadAtomicRTButton = new System.Windows.Forms.Button();
            this.TechniqueListView = new System.Windows.Forms.ListView();
            this.Technique = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UncheckAllTechniqueButton = new System.Windows.Forms.Button();
            this.CheckAllTechniqueButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AtomicRTBrowseButton = new System.Windows.Forms.Button();
            this.AtomicRTPathTextbox = new System.Windows.Forms.TextBox();
            this.PathToRTLabel = new System.Windows.Forms.Label();
            this.AtomicRTBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.LoadCommandsButton = new System.Windows.Forms.Button();
            this.CmdQueueListView = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectAllCmdButton = new System.Windows.Forms.Button();
            this.DeselectAllCmdButton = new System.Windows.Forms.Button();
            this.StopExecutingButton = new System.Windows.Forms.Button();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.getAtomicRedTeamGithubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.authTabControl = new System.Windows.Forms.TabControl();
            this.elevated = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.TestComputer = new System.Windows.Forms.TextBox();
            this.userpass = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.passAuthTextbox = new System.Windows.Forms.TextBox();
            this.userlabel = new System.Windows.Forms.Label();
            this.userAuthTextbox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PAExecEXELocation = new System.Windows.Forms.TextBox();
            this.ElevatedAccountGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.authTabControl.SuspendLayout();
            this.elevated.SuspendLayout();
            this.userpass.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmdOutput
            // 
            this.CmdOutput.BackColor = System.Drawing.Color.Black;
            this.CmdOutput.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdOutput.ForeColor = System.Drawing.Color.White;
            this.CmdOutput.Location = new System.Drawing.Point(4, 512);
            this.CmdOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmdOutput.Multiline = true;
            this.CmdOutput.Name = "CmdOutput";
            this.CmdOutput.ReadOnly = true;
            this.CmdOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CmdOutput.Size = new System.Drawing.Size(1063, 211);
            this.CmdOutput.TabIndex = 0;
            this.CmdOutput.TextChanged += new System.EventHandler(this.CmdOutput_TextChanged);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.ConnectButton.Location = new System.Drawing.Point(595, 250);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(152, 68);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect Client";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // KillButton
            // 
            this.KillButton.Enabled = false;
            this.KillButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KillButton.ForeColor = System.Drawing.Color.DarkRed;
            this.KillButton.Location = new System.Drawing.Point(915, 250);
            this.KillButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.KillButton.Name = "KillButton";
            this.KillButton.Size = new System.Drawing.Size(152, 68);
            this.KillButton.TabIndex = 2;
            this.KillButton.Text = "Disconnect Client";
            this.KillButton.UseVisualStyleBackColor = true;
            this.KillButton.Click += new System.EventHandler(this.KillButton_Click);
            // 
            // ElevatedAccountLabel
            // 
            this.ElevatedAccountLabel.AutoSize = true;
            this.ElevatedAccountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElevatedAccountLabel.Location = new System.Drawing.Point(64, 25);
            this.ElevatedAccountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ElevatedAccountLabel.Name = "ElevatedAccountLabel";
            this.ElevatedAccountLabel.Size = new System.Drawing.Size(77, 17);
            this.ElevatedAccountLabel.TabIndex = 4;
            this.ElevatedAccountLabel.Text = "Username:";
            this.ElevatedAccountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ElevatedAccountGroup
            // 
            this.ElevatedAccountGroup.Controls.Add(this.BrowseElevatedAccountButton);
            this.ElevatedAccountGroup.Controls.Add(this.label1);
            this.ElevatedAccountGroup.Controls.Add(this.ElevatedAccountEXELocation);
            this.ElevatedAccountGroup.Controls.Add(this.AdminUsername);
            this.ElevatedAccountGroup.Controls.Add(this.ElevatedAccountLabel);
            this.ElevatedAccountGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElevatedAccountGroup.Location = new System.Drawing.Point(595, 28);
            this.ElevatedAccountGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ElevatedAccountGroup.Name = "ElevatedAccountGroup";
            this.ElevatedAccountGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ElevatedAccountGroup.Size = new System.Drawing.Size(473, 92);
            this.ElevatedAccountGroup.TabIndex = 5;
            this.ElevatedAccountGroup.TabStop = false;
            this.ElevatedAccountGroup.Text = "Elevated Account Configuration";
            // 
            // BrowseElevatedAccountButton
            // 
            this.BrowseElevatedAccountButton.Location = new System.Drawing.Point(428, 52);
            this.BrowseElevatedAccountButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BrowseElevatedAccountButton.Name = "BrowseElevatedAccountButton";
            this.BrowseElevatedAccountButton.Size = new System.Drawing.Size(36, 28);
            this.BrowseElevatedAccountButton.TabIndex = 7;
            this.BrowseElevatedAccountButton.Text = "...";
            this.BrowseElevatedAccountButton.UseVisualStyleBackColor = true;
            this.BrowseElevatedAccountButton.Click += new System.EventHandler(this.BrowseElevatedAccountButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Path to executable:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ElevatedAccountEXELocation
            // 
            this.ElevatedAccountEXELocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_EAPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ElevatedAccountEXELocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElevatedAccountEXELocation.Location = new System.Drawing.Point(152, 54);
            this.ElevatedAccountEXELocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ElevatedAccountEXELocation.Name = "ElevatedAccountEXELocation";
            this.ElevatedAccountEXELocation.Size = new System.Drawing.Size(269, 23);
            this.ElevatedAccountEXELocation.TabIndex = 5;
            this.ElevatedAccountEXELocation.Text = global::Auto_RT.Properties.Settings.Default.main_EAPath;
            this.ElevatedAccountEXELocation.TextChanged += new System.EventHandler(this.ElevatedAccountEXELocation_TextChanged);
            // 
            // AdminUsername
            // 
            this.AdminUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.AdminUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AdminUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdminUsername.Location = new System.Drawing.Point(149, 21);
            this.AdminUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AdminUsername.Name = "AdminUsername";
            this.AdminUsername.Size = new System.Drawing.Size(313, 23);
            this.AdminUsername.TabIndex = 3;
            this.AdminUsername.Text = global::Auto_RT.Properties.Settings.Default.main_Username;
            this.AdminUsername.TextChanged += new System.EventHandler(this.AdminUsername_TextChanged);
            // 
            // TestComputerLabel
            // 
            this.TestComputerLabel.AutoSize = true;
            this.TestComputerLabel.Location = new System.Drawing.Point(7, 10);
            this.TestComputerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TestComputerLabel.Name = "TestComputerLabel";
            this.TestComputerLabel.Size = new System.Drawing.Size(111, 17);
            this.TestComputerLabel.TabIndex = 9;
            this.TestComputerLabel.Text = "Target machine:";
            this.TestComputerLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PAExecLabel
            // 
            this.PAExecLabel.AutoSize = true;
            this.PAExecLabel.Location = new System.Drawing.Point(600, 226);
            this.PAExecLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PAExecLabel.Name = "PAExecLabel";
            this.PAExecLabel.Size = new System.Drawing.Size(132, 17);
            this.PAExecLabel.TabIndex = 9;
            this.PAExecLabel.Text = "Path to paexec.exe:";
            this.PAExecLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ElevatedAccountBrowseDialog
            // 
            this.ElevatedAccountBrowseDialog.Filter = "ElevatedProcess.exe|ElevatedProcess.exe";
            this.ElevatedAccountBrowseDialog.RestoreDirectory = true;
            // 
            // PAExecEXEBrowseDialog
            // 
            this.PAExecEXEBrowseDialog.Filter = "paexec.exe|paexec.exe";
            this.PAExecEXEBrowseDialog.RestoreDirectory = true;
            // 
            // LoadAtomicRTButton
            // 
            this.LoadAtomicRTButton.Enabled = false;
            this.LoadAtomicRTButton.Location = new System.Drawing.Point(429, 222);
            this.LoadAtomicRTButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadAtomicRTButton.Name = "LoadAtomicRTButton";
            this.LoadAtomicRTButton.Size = new System.Drawing.Size(144, 28);
            this.LoadAtomicRTButton.TabIndex = 7;
            this.LoadAtomicRTButton.Text = "Load AtomicRT";
            this.LoadAtomicRTButton.UseVisualStyleBackColor = true;
            this.LoadAtomicRTButton.Click += new System.EventHandler(this.LoadAtomicRTButton_Click);
            // 
            // TechniqueListView
            // 
            this.TechniqueListView.CheckBoxes = true;
            this.TechniqueListView.FullRowSelect = true;
            this.TechniqueListView.GridLines = true;
            this.TechniqueListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TechniqueListView.HideSelection = false;
            this.TechniqueListView.Location = new System.Drawing.Point(12, 23);
            this.TechniqueListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TechniqueListView.Name = "TechniqueListView";
            this.TechniqueListView.ShowGroups = false;
            this.TechniqueListView.Size = new System.Drawing.Size(453, 190);
            this.TechniqueListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TechniqueListView.TabIndex = 10;
            this.TechniqueListView.UseCompatibleStateImageBehavior = false;
            this.TechniqueListView.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UncheckAllTechniqueButton);
            this.groupBox1.Controls.Add(this.CheckAllTechniqueButton);
            this.groupBox1.Controls.Add(this.EditButton);
            this.groupBox1.Controls.Add(this.AtomicRTBrowseButton);
            this.groupBox1.Controls.Add(this.AtomicRTPathTextbox);
            this.groupBox1.Controls.Add(this.PathToRTLabel);
            this.groupBox1.Controls.Add(this.TechniqueListView);
            this.groupBox1.Controls.Add(this.LoadAtomicRTButton);
            this.groupBox1.Location = new System.Drawing.Point(4, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(581, 271);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Atomic Red Team Configuration";
            // 
            // UncheckAllTechniqueButton
            // 
            this.UncheckAllTechniqueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.UncheckAllTechniqueButton.Location = new System.Drawing.Point(475, 71);
            this.UncheckAllTechniqueButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UncheckAllTechniqueButton.Name = "UncheckAllTechniqueButton";
            this.UncheckAllTechniqueButton.Size = new System.Drawing.Size(99, 46);
            this.UncheckAllTechniqueButton.TabIndex = 18;
            this.UncheckAllTechniqueButton.Text = "Uncheck all";
            this.UncheckAllTechniqueButton.UseVisualStyleBackColor = true;
            this.UncheckAllTechniqueButton.Click += new System.EventHandler(this.UncheckAllTechniqueButton_Click);
            // 
            // CheckAllTechniqueButton
            // 
            this.CheckAllTechniqueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.CheckAllTechniqueButton.Location = new System.Drawing.Point(475, 21);
            this.CheckAllTechniqueButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckAllTechniqueButton.Name = "CheckAllTechniqueButton";
            this.CheckAllTechniqueButton.Size = new System.Drawing.Size(99, 46);
            this.CheckAllTechniqueButton.TabIndex = 17;
            this.CheckAllTechniqueButton.Text = "Check all";
            this.CheckAllTechniqueButton.UseVisualStyleBackColor = true;
            this.CheckAllTechniqueButton.Click += new System.EventHandler(this.CheckAllTechniqueButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(475, 122);
            this.EditButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(99, 92);
            this.EditButton.TabIndex = 16;
            this.EditButton.Text = "Open editor";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditorButton_click);
            // 
            // AtomicRTBrowseButton
            // 
            this.AtomicRTBrowseButton.Location = new System.Drawing.Point(385, 222);
            this.AtomicRTBrowseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AtomicRTBrowseButton.Name = "AtomicRTBrowseButton";
            this.AtomicRTBrowseButton.Size = new System.Drawing.Size(36, 28);
            this.AtomicRTBrowseButton.TabIndex = 8;
            this.AtomicRTBrowseButton.Text = "...";
            this.AtomicRTBrowseButton.UseVisualStyleBackColor = true;
            this.AtomicRTBrowseButton.Click += new System.EventHandler(this.AtomicRTBrowseButton_Click);
            // 
            // AtomicRTPathTextbox
            // 
            this.AtomicRTPathTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_AtomicPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AtomicRTPathTextbox.Location = new System.Drawing.Point(123, 224);
            this.AtomicRTPathTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AtomicRTPathTextbox.Name = "AtomicRTPathTextbox";
            this.AtomicRTPathTextbox.Size = new System.Drawing.Size(253, 22);
            this.AtomicRTPathTextbox.TabIndex = 12;
            this.AtomicRTPathTextbox.Text = global::Auto_RT.Properties.Settings.Default.main_AtomicPath;
            this.AtomicRTPathTextbox.WordWrap = false;
            this.AtomicRTPathTextbox.TextChanged += new System.EventHandler(this.AtomicRTPathTextbox_TextChanged);
            // 
            // PathToRTLabel
            // 
            this.PathToRTLabel.AutoSize = true;
            this.PathToRTLabel.Location = new System.Drawing.Point(8, 228);
            this.PathToRTLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PathToRTLabel.Name = "PathToRTLabel";
            this.PathToRTLabel.Size = new System.Drawing.Size(102, 17);
            this.PathToRTLabel.TabIndex = 11;
            this.PathToRTLabel.Text = "\"atomics\" path:";
            // 
            // AtomicRTBrowseDialog
            // 
            this.AtomicRTBrowseDialog.ShowNewFolderButton = false;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.ExecuteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExecuteButton.Enabled = false;
            this.ExecuteButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.ExecuteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.ExecuteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExecuteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteButton.Location = new System.Drawing.Point(897, 325);
            this.ExecuteButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(172, 138);
            this.ExecuteButton.TabIndex = 12;
            this.ExecuteButton.Text = "Start Executing Selected Commands";
            this.ExecuteButton.UseVisualStyleBackColor = false;
            this.ExecuteButton.EnabledChanged += new System.EventHandler(this.LaunchButton_EnabledChanged);
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // LoadCommandsButton
            // 
            this.LoadCommandsButton.Enabled = false;
            this.LoadCommandsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadCommandsButton.ForeColor = System.Drawing.Color.Chocolate;
            this.LoadCommandsButton.Location = new System.Drawing.Point(755, 251);
            this.LoadCommandsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadCommandsButton.Name = "LoadCommandsButton";
            this.LoadCommandsButton.Size = new System.Drawing.Size(152, 66);
            this.LoadCommandsButton.TabIndex = 13;
            this.LoadCommandsButton.Text = "Load Commands";
            this.LoadCommandsButton.UseVisualStyleBackColor = true;
            this.LoadCommandsButton.Click += new System.EventHandler(this.LoadCommandsButton_click);
            // 
            // CmdQueueListView
            // 
            this.CmdQueueListView.CheckBoxes = true;
            this.CmdQueueListView.HideSelection = false;
            this.CmdQueueListView.Location = new System.Drawing.Point(4, 325);
            this.CmdQueueListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmdQueueListView.Name = "CmdQueueListView";
            this.CmdQueueListView.Size = new System.Drawing.Size(795, 179);
            this.CmdQueueListView.TabIndex = 14;
            this.CmdQueueListView.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 303);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Command Queue:";
            // 
            // SelectAllCmdButton
            // 
            this.SelectAllCmdButton.Location = new System.Drawing.Point(808, 325);
            this.SelectAllCmdButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SelectAllCmdButton.Name = "SelectAllCmdButton";
            this.SelectAllCmdButton.Size = new System.Drawing.Size(81, 89);
            this.SelectAllCmdButton.TabIndex = 16;
            this.SelectAllCmdButton.Text = "Select all";
            this.SelectAllCmdButton.UseVisualStyleBackColor = true;
            this.SelectAllCmdButton.Click += new System.EventHandler(this.SelectAllCmdButton_Click);
            // 
            // DeselectAllCmdButton
            // 
            this.DeselectAllCmdButton.Location = new System.Drawing.Point(808, 416);
            this.DeselectAllCmdButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeselectAllCmdButton.Name = "DeselectAllCmdButton";
            this.DeselectAllCmdButton.Size = new System.Drawing.Size(81, 89);
            this.DeselectAllCmdButton.TabIndex = 17;
            this.DeselectAllCmdButton.Text = "Deselect all";
            this.DeselectAllCmdButton.UseVisualStyleBackColor = true;
            this.DeselectAllCmdButton.Click += new System.EventHandler(this.DeselectAllCmdButton_Click);
            // 
            // StopExecutingButton
            // 
            this.StopExecutingButton.Enabled = false;
            this.StopExecutingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopExecutingButton.Location = new System.Drawing.Point(897, 465);
            this.StopExecutingButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopExecutingButton.Name = "StopExecutingButton";
            this.StopExecutingButton.Size = new System.Drawing.Size(172, 39);
            this.StopExecutingButton.TabIndex = 18;
            this.StopExecutingButton.Text = "Stop executing";
            this.StopExecutingButton.UseVisualStyleBackColor = true;
            this.StopExecutingButton.Click += new System.EventHandler(this.StopExecutingButton_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 150);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getAtomicRedTeamGithubToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1072, 28);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "MainMenu";
            // 
            // getAtomicRedTeamGithubToolStripMenuItem
            // 
            this.getAtomicRedTeamGithubToolStripMenuItem.Name = "getAtomicRedTeamGithubToolStripMenuItem";
            this.getAtomicRedTeamGithubToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
            this.getAtomicRedTeamGithubToolStripMenuItem.Text = "Get Atomic Red Team (Github)";
            this.getAtomicRedTeamGithubToolStripMenuItem.Click += new System.EventHandler(this.getAtomicRedTeamGithubToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // authTabControl
            // 
            this.authTabControl.Controls.Add(this.elevated);
            this.authTabControl.Controls.Add(this.userpass);
            this.authTabControl.Location = new System.Drawing.Point(595, 116);
            this.authTabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.authTabControl.Name = "authTabControl";
            this.authTabControl.SelectedIndex = 0;
            this.authTabControl.Size = new System.Drawing.Size(475, 98);
            this.authTabControl.TabIndex = 20;
            this.authTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.AuthTabControl_Selected);
            // 
            // elevated
            // 
            this.elevated.Controls.Add(this.label5);
            this.elevated.Controls.Add(this.TestComputer);
            this.elevated.Controls.Add(this.TestComputerLabel);
            this.elevated.Location = new System.Drawing.Point(4, 25);
            this.elevated.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.elevated.Name = "elevated";
            this.elevated.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.elevated.Size = new System.Drawing.Size(467, 69);
            this.elevated.TabIndex = 0;
            this.elevated.Text = "Use elevated account for target";
            this.elevated.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(43, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(413, 34);
            this.label5.TabIndex = 21;
            this.label5.Text = "Connect to the target machine using the same elevated account\r\nused to launch Aut" +
    "o_RT\'s elevated process, as specified above.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TestComputer
            // 
            this.TestComputer.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TestComputer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_TestComp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TestComputer.Location = new System.Drawing.Point(119, 6);
            this.TestComputer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestComputer.Name = "TestComputer";
            this.TestComputer.Size = new System.Drawing.Size(331, 22);
            this.TestComputer.TabIndex = 8;
            this.TestComputer.Text = global::Auto_RT.Properties.Settings.Default.main_TestComp;
            this.TestComputer.TextChanged += new System.EventHandler(this.TestComputer_TextChanged);
            // 
            // userpass
            // 
            this.userpass.Controls.Add(this.label4);
            this.userpass.Controls.Add(this.passAuthTextbox);
            this.userpass.Controls.Add(this.userlabel);
            this.userpass.Controls.Add(this.userAuthTextbox);
            this.userpass.Controls.Add(this.textBox1);
            this.userpass.Controls.Add(this.label3);
            this.userpass.Location = new System.Drawing.Point(4, 25);
            this.userpass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userpass.Name = "userpass";
            this.userpass.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userpass.Size = new System.Drawing.Size(467, 69);
            this.userpass.TabIndex = 1;
            this.userpass.Text = "Enter credentials for target";
            this.userpass.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Pass:";
            // 
            // passAuthTextbox
            // 
            this.passAuthTextbox.Location = new System.Drawing.Point(305, 37);
            this.passAuthTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.passAuthTextbox.Name = "passAuthTextbox";
            this.passAuthTextbox.Size = new System.Drawing.Size(124, 22);
            this.passAuthTextbox.TabIndex = 22;
            // 
            // userlabel
            // 
            this.userlabel.AutoSize = true;
            this.userlabel.Location = new System.Drawing.Point(33, 41);
            this.userlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userlabel.Name = "userlabel";
            this.userlabel.Size = new System.Drawing.Size(77, 17);
            this.userlabel.TabIndex = 21;
            this.userlabel.Text = "Username:";
            // 
            // userAuthTextbox
            // 
            this.userAuthTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_remoteUser", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.userAuthTextbox.Location = new System.Drawing.Point(119, 37);
            this.userAuthTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userAuthTextbox.Name = "userAuthTextbox";
            this.userAuthTextbox.Size = new System.Drawing.Size(125, 22);
            this.userAuthTextbox.TabIndex = 21;
            this.userAuthTextbox.Text = global::Auto_RT.Properties.Settings.Default.main_remoteUser;
            // 
            // textBox1
            // 
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_TestComp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(119, 7);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(311, 22);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = global::Auto_RT.Properties.Settings.Default.main_TestComp;
            this.textBox1.TextChanged += new System.EventHandler(this.TestComputer_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Target machine:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PAExecEXELocation
            // 
            this.PAExecEXELocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_paexecPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PAExecEXELocation.Location = new System.Drawing.Point(744, 222);
            this.PAExecEXELocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PAExecEXELocation.Name = "PAExecEXELocation";
            this.PAExecEXELocation.Size = new System.Drawing.Size(321, 22);
            this.PAExecEXELocation.TabIndex = 8;
            this.PAExecEXELocation.Text = global::Auto_RT.Properties.Settings.Default.main_paexecPath;
            this.PAExecEXELocation.TextChanged += new System.EventHandler(this.PAExecEXELocation_TextChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 727);
            this.Controls.Add(this.authTabControl);
            this.Controls.Add(this.StopExecutingButton);
            this.Controls.Add(this.DeselectAllCmdButton);
            this.Controls.Add(this.PAExecEXELocation);
            this.Controls.Add(this.PAExecLabel);
            this.Controls.Add(this.SelectAllCmdButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CmdQueueListView);
            this.Controls.Add(this.LoadCommandsButton);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ElevatedAccountGroup);
            this.Controls.Add(this.KillButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.CmdOutput);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Auto_RT - v0.1b";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ElevatedAccountGroup.ResumeLayout(false);
            this.ElevatedAccountGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.authTabControl.ResumeLayout(false);
            this.elevated.ResumeLayout(false);
            this.elevated.PerformLayout();
            this.userpass.ResumeLayout(false);
            this.userpass.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox CmdOutput;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button KillButton;
        private System.Windows.Forms.TextBox AdminUsername;
        private System.Windows.Forms.Label ElevatedAccountLabel;
        private System.Windows.Forms.GroupBox ElevatedAccountGroup;
        private System.Windows.Forms.Button BrowseElevatedAccountButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ElevatedAccountEXELocation;
        private System.Windows.Forms.TextBox TestComputer;
        private System.Windows.Forms.Label TestComputerLabel;
        private System.Windows.Forms.TextBox PAExecEXELocation;
        private System.Windows.Forms.Label PAExecLabel;
        private System.Windows.Forms.OpenFileDialog ElevatedAccountBrowseDialog;
        private System.Windows.Forms.OpenFileDialog PAExecEXEBrowseDialog;
        private System.Windows.Forms.Button LoadAtomicRTButton;
        private System.Windows.Forms.ListView TechniqueListView;
        private System.Windows.Forms.ColumnHeader Technique;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox AtomicRTPathTextbox;
        private System.Windows.Forms.Label PathToRTLabel;
        private System.Windows.Forms.Button AtomicRTBrowseButton;
        private System.Windows.Forms.FolderBrowserDialog AtomicRTBrowseDialog;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.Button LoadCommandsButton;
        private System.Windows.Forms.ListView CmdQueueListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button UncheckAllTechniqueButton;
        private System.Windows.Forms.Button CheckAllTechniqueButton;
        private System.Windows.Forms.Button SelectAllCmdButton;
        private System.Windows.Forms.Button DeselectAllCmdButton;
        private System.Windows.Forms.Button StopExecutingButton;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem getAtomicRedTeamGithubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl authTabControl;
        private System.Windows.Forms.TabPage elevated;
        private System.Windows.Forms.TabPage userpass;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passAuthTextbox;
        private System.Windows.Forms.Label userlabel;
        private System.Windows.Forms.TextBox userAuthTextbox;
    }
}

