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
		private const string XmlNodeTag = "MenuItem";
		private const string XmlNodeTextAtt = "ID";
		private const string XmlNodeTagAtt = "OnClick";
        private const string XmlConfigFile = "TeklaToolbar.xml";

        public string strMacrosFolder = "";
        public string strModelingMacrosFolder = "";
        public string strTeklaToolbarFolder = "";
        Model model = new Model();

		public TreeViewSerializer()
		{
		}

		public void SerializeTreeView(TreeView treeView) 
		{
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            XmlWriter writer = XmlTextWriter.Create(XmlConfigFile, settings);
			writer.WriteStartDocument();
			writer.WriteStartElement("root");
			SaveNodes(treeView.Nodes, writer);
			writer.WriteEndElement();
			writer.Close();
		}

		private void SaveNodes(TreeNodeCollection nodesCollection, XmlWriter textWriter)
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

        private void SetAttributeValue(TreeNode node, string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt) node.Text = value;
            else if (propertyName == XmlNodeTagAtt) node.Tag = value;
        }

        public void PopulateMenu(MenuStrip menuStrip)
        {
            try
            {
                model.GetAdvancedOption("XS_MACRO_DIRECTORY", ref strMacrosFolder);
                strModelingMacrosFolder = strMacrosFolder + @"\modeling\";
                strTeklaToolbarFolder = strMacrosFolder + @"\modeling\TeklaToolbar\";
                DirectoryInfo MacrosFolder = new DirectoryInfo(strTeklaToolbarFolder);

                menuStrip.Items.Clear();
                AddViewsToMenu(menuStrip);
                LoadDynamicMenu(menuStrip);

                //menuStrip1 = new System.Windows.Forms.MenuStrip();
                //menuStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //menuStrip1.Location = new System.Drawing.Point(0, 0);
                //menuStrip1.Name = "menuStrip1";
                //menuStrip1.Size = new System.Drawing.Size(100, 24);
                //menuStrip1.TabIndex = 0;
                //menuStrip1.Text = "menuStrip1";
                
                //Controls.Add(this.menuStrip1);
                //MainMenuStrip = this.menuStrip1;
            }
            finally
            {
                int width = 0;
                foreach (ToolStripMenuItem t in menuStrip.Items) width = width + t.Width;
                TeklaToolbarForm.ActiveForm.Width = width + 30;
            }
        }

        private void AddViewsToMenu(MenuStrip menuStrip)
        {
            ToolStripMenuItem myViewsMenuItem = new ToolStripMenuItem("My Views");
            string strUsername = Environment.UserName;
            Tekla.Structures.Model.UI.ModelViewEnumerator modelViewsEnum = Tekla.Structures.Model.UI.ViewHandler.GetPermanentViews();

            System.Collections.ArrayList arrayViewNames = new System.Collections.ArrayList();
            System.Collections.ArrayList arrayViews = new System.Collections.ArrayList();
            while (modelViewsEnum.MoveNext())
            {
                Tekla.Structures.Model.UI.View currentView = modelViewsEnum.Current;
                if (currentView.Name.Contains(strUsername))
                {
                    arrayViewNames.Add(currentView.Name);
                    arrayViews.Add(currentView);
                }
            }

            arrayViewNames.Sort();

            foreach (string viewName in arrayViewNames)
            {
                foreach (Tekla.Structures.Model.UI.View view in arrayViews)
                {
                    if (viewName == view.Name)
                    {
                        ToolStripMenuItem SavedView = new ToolStripMenuItem(view.Name);
                        SavedView.Tag = view;
                        SavedView.Click += new EventHandler(SavedView_Click);
                        myViewsMenuItem.DropDownItems.Add(SavedView);
                    }
                }
            }

            if (modelViewsEnum.Count > 0) menuStrip.Items.Add(myViewsMenuItem);
        }

        public void LoadDynamicMenu(MenuStrip menuStrip)
        {
            XmlTextReader xmlReader = new XmlTextReader("TeklaToolbar.xml");
            XmlDocument document = new XmlDocument();
            document.Load(xmlReader);

            XmlElement element = document.DocumentElement;

            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();

                menuItem.Text = node.Attributes[0].Value;

                menuStrip.Items.Add(menuItem);
                GenerateMenusFromXML(node, (ToolStripMenuItem)menuStrip.Items[menuStrip.Items.Count - 1]);
            }
        }

        private void GenerateMenusFromXML(XmlNode rootNode, ToolStripMenuItem menuItem)
        {
            ToolStripItem item = null;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Attributes[0].Value == "-") menuItem.DropDownItems.Add(new ToolStripSeparator());

                else
                {
                    item = new ToolStripMenuItem();
                    item.Text = node.Attributes[0].Value;
                    if (node.Attributes["OnClick"] != null)
                    {
                        if (node.Attributes["OnClick"].Value != "Folder")
                        {
                            item.Tag = node.Attributes["OnClick"].Value;
                            item.Click += new EventHandler(menuItem_Click);
                        }
                    }

                    menuItem.DropDownItems.Add(item);
                    GenerateMenusFromXML(node, (ToolStripMenuItem)menuItem.DropDownItems[menuItem.DropDownItems.Count - 1]);
                }
            }
        }

        void SavedView_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem SavedView = sender as ToolStripMenuItem;
                Tekla.Structures.Model.UI.ViewHandler.ShowView(SavedView.Tag as Tekla.Structures.Model.UI.View);
            }
            catch { }
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
