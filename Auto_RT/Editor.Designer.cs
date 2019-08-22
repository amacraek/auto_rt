namespace Auto_RT
{
    partial class EditorForm
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
            this.AtomicTreeView = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.AtomicListBox = new System.Windows.Forms.ListBox();
            this.AtomicViewTabs = new System.Windows.Forms.TabControl();
            this.treetabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.AutosaveCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveTreeButton = new System.Windows.Forms.Button();
            this.YAMLtabPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveYAMLButton = new System.Windows.Forms.Button();
            this.RawYAMLTextbox = new System.Windows.Forms.TextBox();
            this.AtomicsPathTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AtomicViewTabs.SuspendLayout();
            this.treetabPage.SuspendLayout();
            this.YAMLtabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // AtomicTreeView
            // 
            this.AtomicTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.AtomicTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.AtomicTreeView.Font = new System.Drawing.Font("Courier New", 8.75F, System.Drawing.FontStyle.Bold);
            this.AtomicTreeView.ForeColor = System.Drawing.Color.MidnightBlue;
            this.AtomicTreeView.Indent = 15;
            this.AtomicTreeView.LabelEdit = true;
            this.AtomicTreeView.Location = new System.Drawing.Point(3, 3);
            this.AtomicTreeView.Name = "AtomicTreeView";
            this.AtomicTreeView.Size = new System.Drawing.Size(463, 268);
            this.AtomicTreeView.TabIndex = 0;
            this.AtomicTreeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.AtomicTreeView_BeforeLabelEdit);
            this.AtomicTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.AtomicTreeView_AfterLabelEdit);
            this.AtomicTreeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.AtomicTreeView_DrawNode);
            this.AtomicTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.AtomicTreeView_NodeMouseDoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(233, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load Atomics";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadAtomics_Click);
            // 
            // AtomicListBox
            // 
            this.AtomicListBox.FormattingEnabled = true;
            this.AtomicListBox.HorizontalScrollbar = true;
            this.AtomicListBox.Location = new System.Drawing.Point(8, 30);
            this.AtomicListBox.Name = "AtomicListBox";
            this.AtomicListBox.Size = new System.Drawing.Size(233, 238);
            this.AtomicListBox.Sorted = true;
            this.AtomicListBox.TabIndex = 2;
            this.AtomicListBox.SelectedIndexChanged += new System.EventHandler(this.AtomicListBox_SelectedIndexChanged);
            // 
            // AtomicViewTabs
            // 
            this.AtomicViewTabs.Controls.Add(this.treetabPage);
            this.AtomicViewTabs.Controls.Add(this.YAMLtabPage);
            this.AtomicViewTabs.Location = new System.Drawing.Point(247, 4);
            this.AtomicViewTabs.Name = "AtomicViewTabs";
            this.AtomicViewTabs.SelectedIndex = 0;
            this.AtomicViewTabs.Size = new System.Drawing.Size(477, 329);
            this.AtomicViewTabs.TabIndex = 4;
            this.AtomicViewTabs.SelectedIndexChanged += new System.EventHandler(this.AtomicViewTabs_SelectedIndexChanged);
            // 
            // treetabPage
            // 
            this.treetabPage.Controls.Add(this.label1);
            this.treetabPage.Controls.Add(this.AutosaveCheckbox);
            this.treetabPage.Controls.Add(this.AtomicTreeView);
            this.treetabPage.Controls.Add(this.SaveTreeButton);
            this.treetabPage.Location = new System.Drawing.Point(4, 22);
            this.treetabPage.Name = "treetabPage";
            this.treetabPage.Padding = new System.Windows.Forms.Padding(3);
            this.treetabPage.Size = new System.Drawing.Size(469, 303);
            this.treetabPage.TabIndex = 0;
            this.treetabPage.Text = "Tree view";
            this.treetabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "(changing tabs will save your changes)";
            // 
            // AutosaveCheckbox
            // 
            this.AutosaveCheckbox.AutoSize = true;
            this.AutosaveCheckbox.Checked = global::Auto_RT.Properties.Settings.Default.editor_Autosave;
            this.AutosaveCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Auto_RT.Properties.Settings.Default, "editor_Autosave", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AutosaveCheckbox.Location = new System.Drawing.Point(87, 281);
            this.AutosaveCheckbox.Name = "AutosaveCheckbox";
            this.AutosaveCheckbox.Size = new System.Drawing.Size(77, 17);
            this.AutosaveCheckbox.TabIndex = 6;
            this.AutosaveCheckbox.Text = "Autosave?";
            this.AutosaveCheckbox.UseVisualStyleBackColor = true;
            // 
            // SaveTreeButton
            // 
            this.SaveTreeButton.Location = new System.Drawing.Point(6, 277);
            this.SaveTreeButton.Name = "SaveTreeButton";
            this.SaveTreeButton.Size = new System.Drawing.Size(75, 23);
            this.SaveTreeButton.TabIndex = 5;
            this.SaveTreeButton.Text = "Save";
            this.SaveTreeButton.UseVisualStyleBackColor = true;
            this.SaveTreeButton.Click += new System.EventHandler(this.SaveTreeButton_click);
            // 
            // YAMLtabPage
            // 
            this.YAMLtabPage.Controls.Add(this.label2);
            this.YAMLtabPage.Controls.Add(this.SaveYAMLButton);
            this.YAMLtabPage.Controls.Add(this.RawYAMLTextbox);
            this.YAMLtabPage.Location = new System.Drawing.Point(4, 22);
            this.YAMLtabPage.Name = "YAMLtabPage";
            this.YAMLtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.YAMLtabPage.Size = new System.Drawing.Size(469, 303);
            this.YAMLtabPage.TabIndex = 1;
            this.YAMLtabPage.Text = "Raw YAML";
            this.YAMLtabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(changing tabs will save your changes)";
            // 
            // SaveYAMLButton
            // 
            this.SaveYAMLButton.Location = new System.Drawing.Point(6, 277);
            this.SaveYAMLButton.Name = "SaveYAMLButton";
            this.SaveYAMLButton.Size = new System.Drawing.Size(75, 23);
            this.SaveYAMLButton.TabIndex = 5;
            this.SaveYAMLButton.Text = "Save";
            this.SaveYAMLButton.UseVisualStyleBackColor = true;
            this.SaveYAMLButton.Click += new System.EventHandler(this.SaveYAMLButton_Click);
            // 
            // RawYAMLTextbox
            // 
            this.RawYAMLTextbox.AcceptsReturn = true;
            this.RawYAMLTextbox.AcceptsTab = true;
            this.RawYAMLTextbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.RawYAMLTextbox.Font = new System.Drawing.Font("Courier New", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawYAMLTextbox.Location = new System.Drawing.Point(3, 3);
            this.RawYAMLTextbox.Multiline = true;
            this.RawYAMLTextbox.Name = "RawYAMLTextbox";
            this.RawYAMLTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.RawYAMLTextbox.Size = new System.Drawing.Size(463, 268);
            this.RawYAMLTextbox.TabIndex = 0;
            this.RawYAMLTextbox.WordWrap = false;
            this.RawYAMLTextbox.TextChanged += new System.EventHandler(this.RawYAMLTextbox_TextChanged);
            // 
            // AtomicsPathTextbox
            // 
            this.AtomicsPathTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Auto_RT.Properties.Settings.Default, "main_AtomicPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AtomicsPathTextbox.Location = new System.Drawing.Point(82, 277);
            this.AtomicsPathTextbox.Name = "AtomicsPathTextbox";
            this.AtomicsPathTextbox.Size = new System.Drawing.Size(159, 20);
            this.AtomicsPathTextbox.TabIndex = 3;
            this.AtomicsPathTextbox.Text = global::Auto_RT.Properties.Settings.Default.main_AtomicPath;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Atomics path:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select an atomic to edit.";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 337);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AtomicViewTabs);
            this.Controls.Add(this.AtomicsPathTextbox);
            this.Controls.Add(this.AtomicListBox);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "EditorForm";
            this.ShowIcon = false;
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.AtomicViewTabs.ResumeLayout(false);
            this.treetabPage.ResumeLayout(false);
            this.treetabPage.PerformLayout();
            this.YAMLtabPage.ResumeLayout(false);
            this.YAMLtabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView AtomicTreeView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox AtomicListBox;
        private System.Windows.Forms.TextBox AtomicsPathTextbox;
        private System.Windows.Forms.TabControl AtomicViewTabs;
        private System.Windows.Forms.TabPage treetabPage;
        private System.Windows.Forms.TabPage YAMLtabPage;
        private System.Windows.Forms.TextBox RawYAMLTextbox;
        private System.Windows.Forms.Button SaveTreeButton;
        private System.Windows.Forms.CheckBox AutosaveCheckbox;
        private System.Windows.Forms.Button SaveYAMLButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}