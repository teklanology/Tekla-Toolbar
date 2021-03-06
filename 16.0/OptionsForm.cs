using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Tekla.Structures;
using Tekla.Structures.Model;

namespace TeklaToolbar
{
    public partial class OptionsForm : Form
    {
        public string strMacrosFolder = "";
        public string strModelingMacrosFolder = "";
        public string strTeklaToolbarFolder = "";
        Model model = new Model();
        Properties.Settings settings = new global::TeklaToolbar.Properties.Settings();
        
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            string FileName = Application.StartupPath + "\\MyTreeView.xml";
            this.treeView1.Nodes.Clear();
            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.DeserializeTreeView(this.treeView1);

            model.GetAdvancedOption("XS_MACRO_DIRECTORY", ref strMacrosFolder);
            strModelingMacrosFolder = strMacrosFolder + @"\modeling\";
            strTeklaToolbarFolder = strMacrosFolder + @"\modeling\TeklaToolbar\";
            DirectoryInfo MacrosFolder = new DirectoryInfo(strTeklaToolbarFolder);

            if (MacrosFolder.Exists)
            {
                FileInfo[] files = MacrosFolder.GetFiles(".cs");

                foreach (FileInfo macro in MacrosFolder.GetFiles("*.cs"))
                {
                    ListViewItem listviewitem = new ListViewItem(macro.Name);
                    listviewitem.Tag = macro.FullName;
                    listView1.Items.Add(listviewitem);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = new TreeNode("New Node");
                tn.Tag = "Folder";
                treeView1.BeginUpdate();

                if (treeView1.SelectedNode == null) treeView1.Nodes.Add(tn);
                else if (treeView1.SelectedNode != null) treeView1.SelectedNode.Nodes.Add(tn);

                treeView1.EndUpdate();
                treeView1.SelectedNode = null;
            }
            catch { }
        }

        private void btnAddToMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag.ToString() == "Folder" && listView1.SelectedItems != null)
                {
                    ListViewItem listviewitem = listView1.SelectedItems[0];
                    TreeNode tn = new TreeNode(listviewitem.Text);
                    tn.Tag = listviewitem.Tag.ToString();
                    treeView1.BeginUpdate();
                    treeView1.SelectedNode.Nodes.Add(tn);
                    treeView1.EndUpdate();
                    treeView1.ExpandAll();
                    treeView1.SelectedNode = null;
                }
            }
            catch { }
        }

        private void btnRemoveFromMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode != null) treeView1.SelectedNode.Remove();
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string FileName = Application.StartupPath + "\\MyTreeView.xml";
            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.SerializeTreeView(this.treeView1);
            this.Close();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            Extensions.MoveNodeUp(treeView1.SelectedNode);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            Extensions.MoveNodeDown(treeView1.SelectedNode);
        }

        private void btnSeparator_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = new TreeNode("-Separator-");
                tn.Tag = "-Separator-";
                treeView1.BeginUpdate();

                if (treeView1.SelectedNode == null) treeView1.Nodes.Add(tn);
                else if (treeView1.SelectedNode != null) treeView1.SelectedNode.Nodes.Add(tn);

                treeView1.EndUpdate();
                treeView1.SelectedNode = null;
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = new TreeNode("New Node");
                tn.Tag = "Folder";
                treeView1.BeginUpdate();

                treeView1.Nodes.Add(tn);
                
                treeView1.EndUpdate();
                treeView1.SelectedNode = null;
            }
            catch { }
        }
    }
    public static class Extensions
    {
        public static void MoveNodeUp(TreeNode node)
        {
            try
            {
                TreeNode parent = node.Parent;
                TreeView view = node.TreeView; //
                if (parent != null)
                {
                    int index = parent.Nodes.IndexOf(node);
                    if (index > 0)
                    {
                        parent.Nodes.RemoveAt(index);
                        parent.Nodes.Insert(index - 1, node);
                        node.TreeView.SelectedNode = node;
                    }
                }
                else if (node.TreeView.Nodes.Contains(node)) //
                {
                    int index = view.Nodes.IndexOf(node);
                    if (index > 0)
                    {
                        view.Nodes.RemoveAt(index);
                        view.Nodes.Insert(index - 1, node);
                    }
                }
            }
            catch { }
        }

        public static void MoveNodeDown(TreeNode node)
        {
            try
            {
                TreeNode parent = node.Parent;
                TreeView view = node.TreeView; //
                if (parent != null)
                {
                    int index = parent.Nodes.IndexOf(node);
                    if (index < parent.Nodes.Count - 1)
                    {
                        parent.Nodes.RemoveAt(index);
                        parent.Nodes.Insert(index + 1, node);

                        node.TreeView.SelectedNode = node;
                    }
                }
                else if (node.TreeView.Nodes.Contains(node)) //
                {
                    int index = view.Nodes.IndexOf(node);
                    if (index < view.Nodes.Count - 1)
                    {
                        view.Nodes.RemoveAt(index);
                        view.Nodes.Insert(index + 1, node);
                    }
                }
            }
            catch { }
        }
    }
}