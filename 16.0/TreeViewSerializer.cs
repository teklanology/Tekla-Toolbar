using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Text;

using Tekla.Structures;
using Tekla.Structures.Model;

namespace TeklaToolbar
{
	public class TreeViewSerializer
	{
		private const string XmlNodeTag = "node";
		private const string XmlNodeTextAtt = "text";
		private const string XmlNodeTagAtt = "tag";
        private const string XmlConfigFile = "MyTreeView.xml";

        public string strMacrosFolder = "";
        public string strModelingMacrosFolder = "";
        public string strTeklaToolbarFolder = "";
        Model model = new Model();

		public TreeViewSerializer()
		{
		}

		public void SerializeTreeView(TreeView treeView) 
		{
			XmlTextWriter textWriter = new XmlTextWriter(XmlConfigFile, System.Text.Encoding.ASCII);
			textWriter.WriteStartDocument();
			textWriter.WriteStartElement("TreeView");
			SaveNodes(treeView.Nodes, textWriter);
			textWriter.WriteEndElement();
			textWriter.Close();
		}

		private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
		{
			for(int i = 0; i < nodesCollection.Count; i++)
			{
				TreeNode node = nodesCollection[i];
				textWriter.WriteStartElement(XmlNodeTag);
				textWriter.WriteAttributeString(XmlNodeTextAtt, node.Text);
				if(node.Tag != null) textWriter.WriteAttributeString(XmlNodeTagAtt, node.Tag.ToString());
				
				if (node.Nodes.Count > 0) SaveNodes(node.Nodes, textWriter);
				textWriter.WriteEndElement();
			}
		}		

		public void DeserializeTreeView(TreeView treeView)
		{
			XmlTextReader reader = null;
			try
			{
				treeView.BeginUpdate();				
				reader = new XmlTextReader(XmlConfigFile);

				TreeNode parentNode = null;
				
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{						
						if (reader.Name == XmlNodeTag)
						{
							TreeNode newNode = new TreeNode();
							bool isEmptyElement = reader.IsEmptyElement;
							int attributeCount = reader.AttributeCount;
							if (attributeCount > 0)
							{
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									SetAttributeValue(newNode, reader.Name, reader.Value);
								}								
							}
                            if(parentNode != null) parentNode.Nodes.Add(newNode);
                            else treeView.Nodes.Add(newNode);

                            if (!isEmptyElement) parentNode = newNode;														
						}						                    
					}

					else if (reader.NodeType == XmlNodeType.EndElement)
					{
                        if (reader.Name == XmlNodeTag) parentNode = parentNode.Parent;
					}
					else if (reader.NodeType == XmlNodeType.XmlDeclaration)
					{ 
                        //Ignore Xml Declaration                    
					}
					else if (reader.NodeType == XmlNodeType.None) return;

                    else if (reader.NodeType == XmlNodeType.Text) parentNode.Nodes.Add(reader.Value);
				}
			}
			finally
			{
				treeView.EndUpdate();      
                reader.Close();	
			}
		}

        private void AddViewsToMenu(MenuStrip menuStrip)
        {
            ToolStripMenuItem myViewsMenuItem = new ToolStripMenuItem("My Views");
            string strUsername = Environment.UserName;
            Tekla.Structures.Model.UI.ModelViewEnumerator modelViewsEnum = Tekla.Structures.Model.UI.ViewHandler.GetPermanentViews();
            while (modelViewsEnum.MoveNext())
            {
                Tekla.Structures.Model.UI.View currentView = modelViewsEnum.Current;
                if (currentView.Name.Contains(strUsername))
                {
                    ToolStripMenuItem SavedView = new ToolStripMenuItem(currentView.Name);
                    SavedView.Tag = currentView;
                    SavedView.Click += new EventHandler(SavedView_Click);
                    myViewsMenuItem.DropDownItems.Add(SavedView);
                }
            }

            if (modelViewsEnum.Count > 0) menuStrip.Items.Add(myViewsMenuItem);
        }

        void SavedView_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem SavedView = sender as ToolStripMenuItem;
            Tekla.Structures.Model.UI.ViewHandler.ShowView(SavedView.Tag as Tekla.Structures.Model.UI.View);
        }

        public void PopulateMenu(MenuStrip menuStrip)
        {
            model.GetAdvancedOption("XS_MACRO_DIRECTORY", ref strMacrosFolder);
            strModelingMacrosFolder = strMacrosFolder + @"\modeling\";
            strTeklaToolbarFolder = strMacrosFolder + @"\modeling\TeklaToolbar\";
            DirectoryInfo MacrosFolder = new DirectoryInfo(strTeklaToolbarFolder);

            XmlTextReader reader = null;
            try
            {
                menuStrip.Items.Clear();
                AddViewsToMenu(menuStrip);
                reader = new XmlTextReader(XmlConfigFile);
                ToolStripMenuItem parentMenuItem = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            ToolStripMenuItem newMenuItem = new ToolStripMenuItem();
                            bool isEmptyElement = reader.IsEmptyElement;
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    MenuSetAttributeValue(newMenuItem, reader.Name, reader.Value);
                                }
                            }

                            if (parentMenuItem != null)
                            {
                                parentMenuItem.DropDownItems.Add(newMenuItem);
                            }
                            else 
                            {
                                menuStrip.Items.Add(newMenuItem); 
                            }

                            if (!isEmptyElement)
                            {
                                parentMenuItem = newMenuItem;
                            }
                        }
                    }
                }
            }
            finally
            {
                int width = 0;
                foreach (ToolStripMenuItem t in menuStrip.Items) width = width + t.Width;
                TeklaToolbarForm.ActiveForm.Width = width + 30;
                reader.Close();
            }
        }

		private void SetAttributeValue(TreeNode node, string propertyName, string value)
		{
			if (propertyName == XmlNodeTextAtt) node.Text = value;
            else if (propertyName == XmlNodeTagAtt) node.Tag = value;
		}

        private void MenuSetAttributeValue(ToolStripMenuItem menuItem, string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt) menuItem.Text = value;
            else if (propertyName == XmlNodeTagAtt)
            {
                menuItem.Tag = value;
                menuItem.Click += new EventHandler(menuItem_Click);
            }
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            FileInfo file = new FileInfo(selectedItem.Tag.ToString());
            if (file.Exists)
            {
                string strSelectedItem = file.FullName.Replace(strModelingMacrosFolder, "");
                Tekla.Structures.Model.Operations.Operation.RunMacro(strSelectedItem);
            }
        }
	}
}
