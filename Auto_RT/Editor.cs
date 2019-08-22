using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Auto_RT
{
  
    public partial class EditorForm : Form
    {
        private AtomicRTdotNET atomicRT;
        private string SelectedTechnique;
        private string SelectedTechniqueName;
        
        // might be implemented later: 
        private bool ShouldSaveTree = false; 
        private bool ShouldSaveYAML = false;

        public EditorForm()
        {
            InitializeComponent();
            atomicRT = new AtomicRTdotNET();
        }

        #region Methods

        private void SaveTree()
        {
            TreeNode[] nodeArray = AtomicTreeView.Nodes.Cast<TreeNode>().ToArray();
            string path = atomicRT.GetYAMLPathFromTechnique(SelectedTechnique);
            atomicRT.SaveAtomicAsYAML(path, atomicRT.TreeNodesToAtomic(nodeArray, SelectedTechnique, SelectedTechniqueName));
            SaveTreeButton.Text = "Save";
            ShouldSaveTree = false;
        }

        private void SaveYAML()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(atomicRT.GetYAMLPathFromTechnique(SelectedTechnique)))
            {
                sw.Write(RawYAMLTextbox.Text);
            }
            SaveYAMLButton.Text = "Save";
            ShouldSaveYAML = false;
        }

        private void UpdateTree()
        {
            try
            {
                var split = AtomicListBox.Items[AtomicListBox.SelectedIndex].ToString().Split(new string[1] { "\t" }, StringSplitOptions.None);
                SelectedTechnique = split[0];
                SelectedTechniqueName = split[1];
                AtomicTreeView.BeginUpdate();
                AtomicTreeView.Nodes.Clear();
                AtomicTreeView.Nodes.AddRange(atomicRT.AtomicToTreeNodes(atomicRT.GetAtomicByTechnique(SelectedTechnique)));
                AtomicTreeView.ExpandAll();
                AtomicTreeView.Nodes[0].EnsureVisible();
                AtomicTreeView.SelectedNode = AtomicTreeView.Nodes[0];
                AtomicTreeView.EndUpdate();
                SaveTreeButton.Text = "Save";
                ShouldSaveTree = false;
            }
            catch
            {

                // nothing
            }
            
        }

        private void UpdateYAML()
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(atomicRT.GetYAMLPathFromTechnique(SelectedTechnique), System.Text.Encoding.UTF8))
            {
                RawYAMLTextbox.Clear();
                while (!sr.EndOfStream) { RawYAMLTextbox.Text += (sr.ReadLine() + "\r\n"); }
            }
            SaveYAMLButton.Text = "Save";
            ShouldSaveYAML = false;
        }

        #endregion

        #region Form load/close

        private void Editor_Load(object sender, EventArgs e)
        {
            atomicRT.AtomicsPath = Properties.Settings.Default.main_AtomicPath;
            if (!string.IsNullOrWhiteSpace(atomicRT.AtomicsPath))
            {
                try
                { 
                    Dictionary<string, string> keyValues = atomicRT.ParseWindowsIndex();
                    AtomicListBox.BeginUpdate();
                    AtomicListBox.Items.Clear();
                    foreach (KeyValuePair<string, string> valuePair in keyValues)
                    {
                        AtomicListBox.Items.Add(string.Join("\t", valuePair.Key, valuePair.Value));
                    }
                    AtomicListBox.EndUpdate();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error loading atomics: \n\n" + err.ToString());
                    this.Close();
                }
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Atomics Listbox

        private void LoadAtomics_Click(object sender, EventArgs e)
        {
            atomicRT.AtomicsPath = AtomicsPathTextbox.Text;

            try
            {
                Dictionary<string, string> keyValues = atomicRT.ParseWindowsIndex();
                AtomicListBox.BeginUpdate();
                AtomicListBox.Items.Clear();
                foreach (KeyValuePair<string, string> valuePair in keyValues)
                {
                    AtomicListBox.Items.Add(string.Join("\t", valuePair.Key, valuePair.Value));
                }
                AtomicListBox.EndUpdate();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error loading atomics: \n\n" + err.ToString());
            }

        }
        
        private void AtomicListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTree();
            UpdateYAML();
        }

        #endregion
               
        #region Atomic TreeView 

        private void AtomicTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // we only want to edit nodes that are bottom-level (i.e. no child nodes)
            if (e.Node.Nodes.Count > 0)
            {
                e.CancelEdit = true;
            }
        }

        private void AtomicTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // we only want to edit nodes that are bottom-level (i.e. no child nodes)
            if (e.Node.Nodes.Count == 0)
            {
                e.Node.BeginEdit();
            }
        }

        private void AtomicTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            // found somewhere online. this allows us to colour the nodes differently.
            TreeNodeStates state = e.State;
            Font font = AtomicTreeView.Font;
            Color foreColor;
            Color backColor = AtomicTreeView.BackColor; // currently set to blue.

            if (e.Node.Nodes.Count == 0) // if the node has no children
            {
                Font regFont = new Font(font, FontStyle.Regular);
                foreColor = Color.Black;
                using (Brush background = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(background, e.Bounds);
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, regFont, e.Bounds, foreColor, TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
                }
            }
            else
            {
                foreColor = AtomicTreeView.ForeColor;
                using (Brush background = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(background, e.Bounds);
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, foreColor, TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
                }
            }
        }

        private void SaveTreeButton_click(object sender, EventArgs e)
        {
            SaveTree();
        }

        private void AtomicTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label) || e.Node.Text == e.Label)
            {
                e.CancelEdit = true; // don't edit if no valid changes
            }
            else
            {
                e.Node.Text = e.Label;
                if (AutosaveCheckbox.Checked)
                {
                    SaveTree();
                }
                else
                {
                    SaveTreeButton.Text = "Save *";
                    ShouldSaveTree = true;
                }
            }
        }

        #endregion

        #region Tab view 

        private void AtomicViewTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AtomicViewTabs.SelectedIndex == 0)
            {
                SaveYAML();
                UpdateTree();
            }
            else if (AtomicViewTabs.SelectedIndex == 1)
            {
                SaveTree();
                UpdateYAML();
            }
        }

        #endregion

        #region Raw YAML textbox

        private void RawYAMLTextbox_TextChanged(object sender, EventArgs e)
        {
            SaveYAMLButton.Text = "Save *";
            ShouldSaveYAML = true;
        }

        private void SaveYAMLButton_Click(object sender, EventArgs e)
        {
            SaveYAML();
        }

        #endregion

    }
}
