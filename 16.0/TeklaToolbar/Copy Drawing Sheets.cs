// Created by Dale Nicholls | http://dalenicholls.webs.com | http://twitter.com/dalenicholls
// collects copies the main drawing dg file to other drawing sheet dg files

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Tekla.Technology.Akit;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
    public class CopyDrawingSheets : System.Windows.Forms.Form
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
        private System.Windows.Forms.Button PickMainDrawing;
        private System.Windows.Forms.Button PickExtraSheets;
        private System.Windows.Forms.Button CopyViews;
        private TextBox MainDrawingNumber;
        private ListBox ExtraSheetsList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RemoveMaterialBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CreateExtraSheets;
        private TextBox RequiredSheets;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;

        static Tekla.Technology.Akit.IScript akit;

        public CopyDrawingSheets(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.PickMainDrawing = new System.Windows.Forms.Button();
            this.PickExtraSheets = new System.Windows.Forms.Button();
            this.CopyViews = new System.Windows.Forms.Button();
            this.MainDrawingNumber = new System.Windows.Forms.TextBox();
            this.ExtraSheetsList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RemoveMaterialBox = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.CreateExtraSheets = new System.Windows.Forms.Button();
            this.RequiredSheets = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PickMainDrawing
            // 
            this.PickMainDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PickMainDrawing.Location = new System.Drawing.Point(321, 12);
            this.PickMainDrawing.Name = "PickMainDrawing";
            this.PickMainDrawing.Size = new System.Drawing.Size(106, 23);
            this.PickMainDrawing.TabIndex = 1;
            this.PickMainDrawing.Text = "Pick Main Drawing";
            this.PickMainDrawing.UseVisualStyleBackColor = true;
            this.PickMainDrawing.Click += new System.EventHandler(this.PickMainDrawing_Click);
            // 
            // PickExtraSheets
            // 
            this.PickExtraSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PickExtraSheets.Location = new System.Drawing.Point(321, 70);
            this.PickExtraSheets.Name = "PickExtraSheets";
            this.PickExtraSheets.Size = new System.Drawing.Size(106, 23);
            this.PickExtraSheets.TabIndex = 2;
            this.PickExtraSheets.Text = "Pick Extra Sheets";
            this.PickExtraSheets.UseVisualStyleBackColor = true;
            this.PickExtraSheets.Click += new System.EventHandler(this.PickExtraSheets_Click);
            // 
            // CopyViews
            // 
            this.CopyViews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyViews.Location = new System.Drawing.Point(321, 99);
            this.CopyViews.Name = "CopyViews";
            this.CopyViews.Size = new System.Drawing.Size(106, 23);
            this.CopyViews.TabIndex = 3;
            this.CopyViews.Text = "Copy Views";
            this.CopyViews.UseVisualStyleBackColor = true;
            this.CopyViews.Click += new System.EventHandler(this.CopyViews_Click);
            // 
            // MainDrawingNumber
            // 
            this.MainDrawingNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainDrawingNumber.Location = new System.Drawing.Point(12, 14);
            this.MainDrawingNumber.Name = "MainDrawingNumber";
            this.MainDrawingNumber.Size = new System.Drawing.Size(281, 20);
            this.MainDrawingNumber.TabIndex = 4;
            // 
            // ExtraSheetsList
            // 
            this.ExtraSheetsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtraSheetsList.FormattingEnabled = true;
            this.ExtraSheetsList.Location = new System.Drawing.Point(12, 75);
            this.ExtraSheetsList.Name = "ExtraSheetsList";
            this.ExtraSheetsList.Size = new System.Drawing.Size(281, 212);
            this.ExtraSheetsList.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(321, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Select Objects";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "1.";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "3.";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "4.";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "6.";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "5.";
            // 
            // RemoveMaterialBox
            // 
            this.RemoveMaterialBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveMaterialBox.Location = new System.Drawing.Point(321, 128);
            this.RemoveMaterialBox.Name = "RemoveMaterialBox";
            this.RemoveMaterialBox.Size = new System.Drawing.Size(106, 23);
            this.RemoveMaterialBox.TabIndex = 12;
            this.RemoveMaterialBox.Text = "Remove Parts List";
            this.RemoveMaterialBox.UseVisualStyleBackColor = true;
            this.RemoveMaterialBox.Click += new System.EventHandler(this.RemoveMaterialBox_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "2.";
            // 
            // CreateExtraSheets
            // 
            this.CreateExtraSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateExtraSheets.Location = new System.Drawing.Point(321, 41);
            this.CreateExtraSheets.Name = "CreateExtraSheets";
            this.CreateExtraSheets.Size = new System.Drawing.Size(106, 23);
            this.CreateExtraSheets.TabIndex = 14;
            this.CreateExtraSheets.Text = "Create Sheets";
            this.CreateExtraSheets.UseVisualStyleBackColor = true;
            this.CreateExtraSheets.Click += new System.EventHandler(this.CreateExtraSheets_Click);
            // 
            // RequiredSheets
            // 
            this.RequiredSheets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RequiredSheets.Location = new System.Drawing.Point(135, 43);
            this.RequiredSheets.Name = "RequiredSheets";
            this.RequiredSheets.Size = new System.Drawing.Size(158, 20);
            this.RequiredSheets.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(321, 186);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Select Objects";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Sheets Req\'d (inc. orig)";
            // 
            // CopyDrawingSheets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 304);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.RequiredSheets);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CreateExtraSheets);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RemoveMaterialBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ExtraSheetsList);
            this.Controls.Add(this.MainDrawingNumber);
            this.Controls.Add(this.CopyViews);
            this.Controls.Add(this.PickExtraSheets);
            this.Controls.Add(this.PickMainDrawing);
            this.Name = "CopyDrawingSheets";
            this.Text = "Copy Drawing Sheets";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public string MainDrawing;
        ArrayList ExtraSheets = new ArrayList();

        private void PickMainDrawing_Click(object sender, EventArgs e)
        {
            Model model = new Model();
            ModelInfo modelInfo = model.GetInfo();
            string drawingFolderPath = modelInfo.ModelPath + @"\drawings\";

            DrawingHandler drawingHandler = new DrawingHandler();
            DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();
            if (drawingEnum.GetSize() == 1)
            {
                while (drawingEnum.MoveNext())
                {
                    if (drawingEnum.Current is AssemblyDrawing)
                    {
                        PropertyInfo pi = drawingEnum.Current.GetType().GetProperty("Identifier", BindingFlags.Instance | BindingFlags.NonPublic);
                        object value = pi.GetValue(drawingEnum.Current, null);
                        Identifier Identifier = (Identifier)value;
                        Beam temporary = new Beam();
                        temporary.Identifier = Identifier;

                        string dgFileName = "";
                        bool dg = temporary.GetReportProperty("DRAWING_PLOT_FILE", ref dgFileName);

                        FileInfo dgFile = new FileInfo(drawingFolderPath + dgFileName);
                        if (dgFile.Exists)
                        {
                            MainDrawing = dgFile.FullName;
                            MainDrawingNumber.Text = drawingEnum.Current.Mark;
                        }
                    }
                    else
                        MessageBox.Show("Pick ONE Assembly Drawing");
                }
                CreateExtraSheets.Enabled = true;
            }
            else
                MessageBox.Show("Pick ONE Assembly Drawing");
        }

        private void PickExtraSheets_Click(object sender, EventArgs e)
        {
            ExtraSheets.Clear();
            ExtraSheetsList.Items.Clear();
            bool error = false;
            Model model = new Model();
            ModelInfo modelInfo = model.GetInfo();
            string drawingFolderPath = modelInfo.ModelPath + @"\drawings\";

            DrawingHandler drawingHandler = new DrawingHandler();
            DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();
            if (drawingEnum.GetSize() > 0)
            {
                while (drawingEnum.MoveNext())
                {
                    if (drawingEnum.Current is AssemblyDrawing)
                    {
                        PropertyInfo pi = drawingEnum.Current.GetType().GetProperty("Identifier", BindingFlags.Instance | BindingFlags.NonPublic);
                        object value = pi.GetValue(drawingEnum.Current, null);
                        Identifier Identifier = (Identifier)value;
                        Beam temporary = new Beam();
                        temporary.Identifier = Identifier;

                        string dgFileName = "";
                        bool dg = temporary.GetReportProperty("DRAWING_PLOT_FILE", ref dgFileName);

                        FileInfo dgFile = new FileInfo(drawingFolderPath + dgFileName);
                        if (dgFile.Exists)
                        {
                            ExtraSheets.Add(dgFile.FullName);
                            ExtraSheetsList.Items.Add(drawingEnum.Current.Mark);
                        }
                    }
                    else
                        error = true;
                }
                CopyViews.Enabled = true;
                if (error == true)
                {
                    MessageBox.Show("Pick Assembly Drawings Only");
                    ExtraSheets.Clear();
                    ExtraSheetsList.Items.Clear();
                    CopyViews.Enabled = false;
                }
                
            }
        }

        private void CopyViews_Click(object sender, EventArgs e)
        {
            foreach (string dgFileName in ExtraSheets)
            {
                FileInfo dgFile = new FileInfo(dgFileName);
                if (dgFile.Exists)
                    System.IO.File.Delete(dgFile.FullName);
                File.Copy(MainDrawing, dgFile.FullName);    
            }
            MainDrawing = MainDrawingNumber.Text = "";
            ExtraSheets.Clear(); ExtraSheetsList.Items.Clear();
            PickExtraSheets.Enabled = CopyViews.Enabled = false;
            MessageBox.Show("Copying Complete");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // select objects
            ArrayList array = new ArrayList();
            DrawingHandler drawingHandler = new DrawingHandler();
            Drawing drawing = drawingHandler.GetActiveDrawing();
            ContainerView containerView = drawing.GetSheet();
            DrawingObjectEnumerator drawingObjEnum = containerView.GetObjects();
            foreach (DrawingObject dobj in drawingObjEnum)
            {
                array.Add(dobj);
            }
            drawingHandler.GetDrawingObjectSelector().SelectObjects(array, false);
        }

        private void RemoveMaterialBox_Click(object sender, EventArgs e)
        {
            akit.Callback("acmd_display_dr_attr", "", "main_frame");
            akit.PushButton("gr_adraw_layout", "adraw_dial");
            akit.ValueChange("adl_dial", "gr_dl_sheet", "0");
            akit.ValueChange("adl_dial", "gr_dl_sheet", "1097464");
            akit.PushButton("dl_modify", "adl_dial");
        }

        private void CreateExtraSheets_Click(object sender, EventArgs e)
        {
            akit.Callback("acmd_display_attr_dialog", "adraw_dial", "main_frame"); // open drawing settings dialog
            akit.ValueChange("adraw_dial", "gr_adraw_get_menu", "KWP-blank"); // select blank attributes
            akit.PushButton("gr_adraw_get", "adraw_dial"); // load attributes

            Model model = new Model();
            Tekla.Structures.Model.UI.ModelObjectSelector mObjSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            DrawingHandler drawingHandler = new DrawingHandler();
            DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();
            if (drawingEnum.GetSize() > 0)
            {
                while (drawingEnum.MoveNext())
                {
                    if (drawingEnum.Current is AssemblyDrawing)
                    {
                        AssemblyDrawing assemblyDrawing = drawingEnum.Current as AssemblyDrawing;
                        akit.ValueChange("adraw_dial", "gr_adraw_description", assemblyDrawing.Name); // change name
                        akit.PushButton("dia_draw_select_parts", "Drawing_selection"); // select parts in drawing
                        int count = 0;
                        bool result = int.TryParse(RequiredSheets.Text, out count);
                        for (int i = 2; i <= count; i++)
                        {
                            akit.ValueChange("adraw_dial", "txtFldSheetNumber", i.ToString()); // change sheet number
                            akit.PushButton("gr_adraw_apply", "adraw_dial"); // apply settings
                            akit.Callback("acmd_create_dim_assembly_drawings", "", "main_frame"); // create assembly drawing for selected part
                        }

                        mObjSelector.Select(new ArrayList());
                    }
                }
            }
            PickExtraSheets.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawingHandler dh = new DrawingHandler();
            Tekla.Structures.Geometry3d.Point point = null;
            Tekla.Structures.Drawing.ViewBase view = null;
            dh.GetPicker().PickPoint("hi", out point, out view);
            Tekla.Structures.Geometry3d.Point endpoint = new Tekla.Structures.Geometry3d.Point();
            endpoint.X = 1142 + point.X;
            endpoint.Y = 815 + point.Y;
            Tekla.Structures.Drawing.Rectangle rectangle = new Tekla.Structures.Drawing.Rectangle(view, point, endpoint);
            rectangle.Insert();
            Tekla.Structures.Geometry3d.Point tpoint = new Tekla.Structures.Geometry3d.Point(endpoint.X, point.Y);
            Tekla.Structures.Geometry3d.Point tendpoint = new Tekla.Structures.Geometry3d.Point();
            tendpoint.X = tpoint.X - 128;
            tendpoint.Y = tpoint.Y + 59;
            Tekla.Structures.Drawing.Rectangle titlebox = new Tekla.Structures.Drawing.Rectangle(view, tpoint, tendpoint);
            titlebox.Insert();

        }
    }

    public class Script
    {
        static Tekla.Technology.Akit.IScript akit;

        public static void Run(Tekla.Technology.Akit.IScript RunMe)
        {
            akit = RunMe;
            Application.Run(new CopyDrawingSheets(akit));
        }
    }
}