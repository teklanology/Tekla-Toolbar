using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tekla.Technology.Akit;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
    public class TeklaForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private System.Windows.Forms.Button button1;
        private ComboBox cmbPrinter;
        private ComboBox cmbSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        static Tekla.Technology.Akit.IScript akit;

        public TeklaForm(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(105, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(55, 12);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(125, 21);
            this.cmbPrinter.TabIndex = 4;
            this.cmbPrinter.Text = "PDF";
            // 
            // cmbSize
            // 
            this.cmbSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(55, 39);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(125, 21);
            this.cmbSize.TabIndex = 5;
            this.cmbSize.Text = "Auto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Printer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Size";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(105, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Sort";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(12, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Settings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TeklaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 129);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSize);
            this.Controls.Add(this.cmbPrinter);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TeklaForm";
            this.ShowInTaskbar = false;
            this.Text = "Print Drawings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TeklaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        Model model = new Model();
        DrawingHandler drawingHandler = new DrawingHandler();

        ArrayList printerNames = new ArrayList();
        ArrayList printerScales = new ArrayList();
        ArrayList printsizeNames = new ArrayList();
        ArrayList printsizeDimensions = new ArrayList();
        ArrayList papersizeNames = new ArrayList();
        ArrayList papersizeDimensions = new ArrayList();
        ArrayList papersizeAutoPrintsizeNames = new ArrayList();
        ArrayList papersizeAutoScales = new ArrayList();
        string plotfileDirectory;
        string printerSettingsPath;

        private void TeklaForm_Load(object sender, EventArgs e)
        {
            printerSettingsPath = model.GetInfo().ModelPath + @"\PrintSettings.ini";
            System.IO.FileInfo printerSettingsFile = new System.IO.FileInfo(printerSettingsPath);
            if (!printerSettingsFile.Exists)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(printerSettingsPath))
                {
                    sw.WriteLine("printer=PDF:1");
                    sw.WriteLine("printer=copier:Auto");
                    sw.WriteLine("printer=epson:Auto");
                    sw.WriteLine("printsize=Auto:0,0");
                    sw.WriteLine("printsize=A3:410,287");
                    sw.WriteLine("printsize=A4:280,195");
                    sw.WriteLine("papersize=A0:1152,825:A3:0.35");
                    sw.WriteLine("papersize=A1:825,560:A3:0.49");
                    sw.WriteLine("papersize=A2:580,405:A4:0.49");
                    sw.WriteLine("papersize=A3:405,280:A4:0.70");
                    sw.WriteLine("papersize=A4:280,195:A4:1.00");
                    sw.WriteLine(@"plotfiledirectory=C:\pdf-plots\");
                }
                System.Diagnostics.Process.Start(printerSettingsPath);
                MessageBox.Show("Modify settings if necessary and\nrestart to load settings");
                Application.Exit();
            }
            else
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(printerSettingsPath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] propertyLine = line.Split(new char[] { '=' });
                        string propertyName = propertyLine[0];
                        string propertyValue = propertyLine[1];
                        if (propertyName == "printer")
                        {
                            string[] printerSettings = propertyValue.Split(new char[] { ':' });
                            string printerName = printerSettings[0];
                            string printerScale = printerSettings[1];
                            printerNames.Add(printerName);
                            printerScales.Add(printerScale);
                            cmbPrinter.Items.Add(printerName);
                        }

                        if (propertyName == "printsize")
                        {
                            string[] printsizeProperties = propertyValue.Split(new char[] { ':' });
                            string printsizeName = printsizeProperties[0];
                            string[] printsizeWidthHeight = printsizeProperties[1].Split(new char[] { ',' });
                            double printsizeWidth = double.Parse(printsizeWidthHeight[0]);
                            double printsizeHeight = double.Parse(printsizeWidthHeight[1]);
                            Tekla.Structures.Drawing.Size printsizeDimension = new Tekla.Structures.Drawing.Size(printsizeWidth, printsizeHeight);
                            printsizeNames.Add(printsizeName);
                            printsizeDimensions.Add(printsizeDimension);
                            cmbSize.Items.Add(printsizeName);
                        }

                        if (propertyName == "papersize")
                        {
                            string[] papersizeProperties = propertyValue.Split(new char[] { ':' });
                            string papersizeName = papersizeProperties[0];
                            string[] papersizeWidthHeight = papersizeProperties[1].Split(new char[] { ',' });
                            double papersizeWidth = double.Parse(papersizeWidthHeight[0]);
                            double papersizeHeight = double.Parse(papersizeWidthHeight[1]);
                            Tekla.Structures.Drawing.Size papersizeDimension = new Tekla.Structures.Drawing.Size(papersizeWidth, papersizeHeight);
                            string papersizeAutoPrintsizeName = papersizeProperties[2];
                            double papersizeAutoScale = double.Parse(papersizeProperties[3]);
                            papersizeNames.Add(papersizeName);
                            papersizeDimensions.Add(papersizeDimension);
                            papersizeAutoPrintsizeNames.Add(papersizeAutoPrintsizeName);
                            papersizeAutoScales.Add(papersizeAutoScale);
                        }
                        if (propertyName == "plotfiledirectory") plotfileDirectory = propertyValue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string printer = cmbPrinter.Text;
                string size = "";
                double scale = 1;

                PrintAttributes printAttributes = new PrintAttributes();
                printAttributes.ScalingType = DotPrintScalingType.Scale;
                printAttributes.PrintToMultipleSheet = false;
                printAttributes.NumberOfCopies = 1;
                printAttributes.Orientation = DotPrintOrientationType.Auto;
                printAttributes.PrintArea = DotPrintAreaType.EntireDrawing;

                DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();
                while (drawingEnum.MoveNext())
                {
                    Drawing drawing = (Drawing)drawingEnum.Current;

                    for (int i = 0; i < printerNames.Count; i++)
                    {
                        string printerName = printerNames[i].ToString();
                        if (printerName == cmbPrinter.Text)
                        {
                            double printerScale;
                            bool printerScaleBool = double.TryParse(printerScales[i].ToString(), out printerScale);

                            if (printerScaleBool)
                            {
                                for (int c = 0; c < papersizeDimensions.Count; c++)
                                {
                                    Tekla.Structures.Drawing.Size m = (Tekla.Structures.Drawing.Size)papersizeDimensions[c];
                                    if (drawing.Layout.SheetSize.Height == m.Height && drawing.Layout.SheetSize.Width == m.Width)
                                        size = (string)papersizeNames[c];
                                }

                                scale = printerScale;
                            }
                            else
                            {
                                if (cmbSize.Text != "Auto")
                                {
                                    for (int c = 0; c < printsizeNames.Count; c++)
                                    {
                                        if (cmbSize.Text == printsizeNames[c].ToString())
                                        {
                                            size = printsizeNames[c].ToString();
                                            Tekla.Structures.Drawing.Size n = (Tekla.Structures.Drawing.Size)printsizeDimensions[c];
                                            scale = n.Width / drawing.Layout.SheetSize.Width;
                                            scale = double.Parse(scale.ToString("F2"));
                                        }
                                    }
                                }

                                else if (cmbSize.Text == "Auto")
                                {
                                    for (int c = 0; c < papersizeNames.Count; c++)
                                    {
                                        Tekla.Structures.Drawing.Size m = (Tekla.Structures.Drawing.Size)papersizeDimensions[c];
                                        if (drawing.Layout.SheetSize.Height == m.Height && drawing.Layout.SheetSize.Width == m.Width)
                                        {
                                            size = papersizeAutoPrintsizeNames[c].ToString();
                                            scale = double.Parse(papersizeAutoScales[c].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    printAttributes.Scale = scale;
                    printAttributes.PrinterInstance = printer + "-" + size;
                    drawingHandler.PrintDrawing(drawing, printAttributes);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {               
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(plotfileDirectory);
                if (dir.Exists)
                {
                    string datetime = DateTime.Now.ToString().Replace(':', '-').Replace('/', '-');
                    System.IO.DirectoryInfo datefolder = new System.IO.DirectoryInfo(plotfileDirectory + @"\" + datetime + @"\");
                    datefolder.Create();
                    System.Diagnostics.Process.Start(datefolder.FullName);
                    DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();
                    while (drawingEnum.MoveNext())
                    {
                        Drawing drawing = (Drawing)drawingEnum.Current;

                        System.Reflection.PropertyInfo propertyInfo = drawing.GetType().GetProperty("Identifier", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                        object value = propertyInfo.GetValue(drawing, null);

                        Identifier Identifier = (Identifier)value;
                        Beam fakebeam = new Beam();
                        fakebeam.Identifier = Identifier;
                        string RevisionMark = "";
                        bool rev = fakebeam.GetReportProperty("REVISION.LAST_MARK", ref RevisionMark);
                        string drawingMark = "";
                        if (drawing is SinglePartDrawing || drawing is AssemblyDrawing)
                            drawingMark = drawing.Mark.Replace("[", "").Replace(".", "").Replace("]", "") + RevisionMark;
                        if (drawing is GADrawing)
                        {
                            if (RevisionMark.Length > 0)
                                RevisionMark = "-" + RevisionMark;
                            drawingMark = drawing.Title2 + RevisionMark;
                        }
                        System.IO.FileInfo file = new System.IO.FileInfo(dir.FullName + @"\" + drawingMark + ".pdf");
                        if (file.Exists)
                        {
                            if (drawing is SinglePartDrawing)
                            {
                                System.IO.DirectoryInfo singlePartDrawingFolder = new System.IO.DirectoryInfo(datefolder.FullName + @"\FIT\");
                                singlePartDrawingFolder.Create();
                                file.MoveTo(singlePartDrawingFolder.FullName + file.Name);
                            }
                            if (drawing is AssemblyDrawing)
                            {
                                System.IO.DirectoryInfo assemblyDrawingFolder = new System.IO.DirectoryInfo(datefolder.FullName + @"\ASS\");
                                assemblyDrawingFolder.Create();

                                for (int i = 0; i < papersizeNames.Count; i++)
                                {
                                    Tekla.Structures.Drawing.Size m = (Tekla.Structures.Drawing.Size)papersizeDimensions[i];
                                    if (drawing.Layout.SheetSize.Height == m.Height && drawing.Layout.SheetSize.Width == m.Width)
                                    {
                                        System.IO.DirectoryInfo assemblyDrawingSizeFolder = new System.IO.DirectoryInfo(datefolder.FullName + @"\ASS\" + papersizeNames[i].ToString() + @"\");
                                        assemblyDrawingSizeFolder.Create();
                                        file.MoveTo(assemblyDrawingSizeFolder.FullName + file.Name);
                                    }
                                }
                            }
                            if (drawing is GADrawing)
                            {
                                System.IO.DirectoryInfo gaDrawingFolder = new System.IO.DirectoryInfo(datefolder.FullName + @"\GAS\");
                                gaDrawingFolder.Create();
                                file.MoveTo(gaDrawingFolder.FullName + file.Name);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(printerSettingsPath);
        }
    }

    public class Script
    {
        static Tekla.Technology.Akit.IScript akit;

        public static void Run(Tekla.Technology.Akit.IScript RunMe)
        {
            akit = RunMe;
            Application.Run(new TeklaForm(akit));
        }
    }
}