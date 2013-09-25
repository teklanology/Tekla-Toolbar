// Created by Dale Nicholls
// A template written in Visual Studio to create graphical macros for use in Tekla Structures.
// http://dalenicholls.webs.com
// http://twitter.com/dalenicholls

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

        private System.Windows.Forms.Button GetParkMarks;
        private System.Windows.Forms.Button GetAssemblyMarks;
        private RichTextBox richTextBox1;
        private System.Windows.Forms.Button SelectPreviousSelection;
        private System.Windows.Forms.Button GetIdNumbers;

        static Tekla.Technology.Akit.IScript akit;

        public TeklaForm(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.GetParkMarks = new System.Windows.Forms.Button();
            this.GetAssemblyMarks = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SelectPreviousSelection = new System.Windows.Forms.Button();
            this.GetIdNumbers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GetParkMarks
            // 
            this.GetParkMarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GetParkMarks.Location = new System.Drawing.Point(180, 41);
            this.GetParkMarks.Name = "GetParkMarks";
            this.GetParkMarks.Size = new System.Drawing.Size(92, 23);
            this.GetParkMarks.TabIndex = 0;
            this.GetParkMarks.Text = "Part Marks";
            this.GetParkMarks.UseVisualStyleBackColor = true;
            this.GetParkMarks.Click += new System.EventHandler(this.GetParkMarks_Click);
            // 
            // GetAssemblyMarks
            // 
            this.GetAssemblyMarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GetAssemblyMarks.Location = new System.Drawing.Point(180, 70);
            this.GetAssemblyMarks.Name = "GetAssemblyMarks";
            this.GetAssemblyMarks.Size = new System.Drawing.Size(92, 23);
            this.GetAssemblyMarks.TabIndex = 1;
            this.GetAssemblyMarks.Text = "Assembly Marks";
            this.GetAssemblyMarks.UseVisualStyleBackColor = true;
            this.GetAssemblyMarks.Click += new System.EventHandler(this.GetAssemblyMarks_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(162, 129);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // SelectPreviousSelection
            // 
            this.SelectPreviousSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectPreviousSelection.Location = new System.Drawing.Point(180, 99);
            this.SelectPreviousSelection.Name = "SelectPreviousSelection";
            this.SelectPreviousSelection.Size = new System.Drawing.Size(92, 23);
            this.SelectPreviousSelection.TabIndex = 4;
            this.SelectPreviousSelection.Text = "Select Previous";
            this.SelectPreviousSelection.UseVisualStyleBackColor = true;
            this.SelectPreviousSelection.Click += new System.EventHandler(this.SelectPreviousSelection_Click);
            // 
            // GetIdNumbers
            // 
            this.GetIdNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GetIdNumbers.Location = new System.Drawing.Point(180, 12);
            this.GetIdNumbers.Name = "GetIdNumbers";
            this.GetIdNumbers.Size = new System.Drawing.Size(92, 23);
            this.GetIdNumbers.TabIndex = 5;
            this.GetIdNumbers.Text = "ID Numbers";
            this.GetIdNumbers.UseVisualStyleBackColor = true;
            this.GetIdNumbers.Click += new System.EventHandler(this.GetIdNumbers_Click);
            // 
            // TeklaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 153);
            this.Controls.Add(this.GetIdNumbers);
            this.Controls.Add(this.SelectPreviousSelection);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.GetAssemblyMarks);
            this.Controls.Add(this.GetParkMarks);
            this.Name = "TeklaForm";
            this.Text = "List Marks";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        private void GetParkMarks_Click(object sender, EventArgs e)
        {
            GetMarks("PART_POS");
        }

        DrawingHandler drawingHandler = new DrawingHandler();
        ArrayList DrawingObjectArray = new ArrayList();

        private void GetMarks(string MarkType)
        {
            Model model = new Model();
            Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            Drawing drawing = drawingHandler.GetActiveDrawing();
            DrawingObjectEnumerator drawingObjectEnumerator = drawingHandler.GetDrawingObjectSelector().GetSelected();
            ArrayList SelectedObjectArray = new ArrayList();
            ArrayList MarkArray = new ArrayList();
            
            while (drawingObjectEnumerator.MoveNext())
            {
                if (drawingObjectEnumerator.Current is Tekla.Structures.Drawing.Part)
                {
                    Tekla.Structures.Drawing.Part part = drawingObjectEnumerator.Current as Tekla.Structures.Drawing.Part;
                    SelectedObjectArray.Add(model.SelectModelObject(new Identifier(part.ModelIdentifier.ID)));
                    DrawingObjectArray.Add(part);
                }
            }
            modelObjectSelector.Select(SelectedObjectArray);
            Tekla.Structures.Model.ModelObjectEnumerator modelObjectEnumerator = model.GetModelObjectSelector().GetSelectedObjects();
            while (modelObjectEnumerator.MoveNext())
            {
                if (modelObjectEnumerator.Current is Tekla.Structures.Model.Part)
                {
                    Tekla.Structures.Model.Part modelPart = modelObjectEnumerator.Current as Tekla.Structures.Model.Part;
                    string mark = "";
                    try
                    {
                        modelPart.GetReportProperty(MarkType, ref mark);
                        if (!MarkArray.Contains(mark))
                            MarkArray.Add(mark);
                    }
                    catch { }
                }
            }
            MarkArray.Sort();
            string MarkList = "";
            foreach (string strMark in MarkArray)
            {
                MarkList = strMark + " " + MarkList;
            }
            Clipboard.SetDataObject(MarkList); richTextBox1.Text = MarkList;
            drawingHandler.GetDrawingObjectSelector().SelectObjects(DrawingObjectArray, false);

        }

        private void GetAssemblyMarks_Click(object sender, EventArgs e)
        {
            GetMarks("ASSEMBLY_POS");
        }

        private void SelectPreviousSelection_Click(object sender, EventArgs e)
        {
            drawingHandler.GetDrawingObjectSelector().SelectObjects(DrawingObjectArray, false);
        }

        private void GetIdNumbers_Click(object sender, EventArgs e)
        {
            Model model = new Model();
            Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            Drawing drawing = drawingHandler.GetActiveDrawing();
            DrawingObjectEnumerator drawingObjectEnumerator = drawingHandler.GetDrawingObjectSelector().GetSelected();
            ArrayList SelectedObjectArray = new ArrayList();
            ArrayList MarkArray = new ArrayList();

            while (drawingObjectEnumerator.MoveNext())
            {
                if (drawingObjectEnumerator.Current is Tekla.Structures.Drawing.Part)
                {
                    Tekla.Structures.Drawing.Part part = drawingObjectEnumerator.Current as Tekla.Structures.Drawing.Part;
                    SelectedObjectArray.Add(model.SelectModelObject(new Identifier(part.ModelIdentifier.ID)));
                    DrawingObjectArray.Add(part);
                }
                if (drawingObjectEnumerator.Current is Tekla.Structures.Drawing.Bolt)
                {
                    Tekla.Structures.Drawing.Bolt bolt = drawingObjectEnumerator.Current as Tekla.Structures.Drawing.Bolt;
                    SelectedObjectArray.Add(model.SelectModelObject(new Identifier(bolt.ModelIdentifier.ID)));
                    DrawingObjectArray.Add(bolt);
                }
            }
            modelObjectSelector.Select(SelectedObjectArray);
            Tekla.Structures.Model.ModelObjectEnumerator modelObjectEnumerator = model.GetModelObjectSelector().GetSelectedObjects();
            while (modelObjectEnumerator.MoveNext())
            {
                if (modelObjectEnumerator.Current is Tekla.Structures.Model.Part)
                {
                    Tekla.Structures.Model.Part modelPart = modelObjectEnumerator.Current as Tekla.Structures.Model.Part;
                    MarkArray.Add(modelPart.Identifier.ID.ToString());
                }
                if (modelObjectEnumerator.Current is Tekla.Structures.Model.BoltGroup)
                {
                    Tekla.Structures.Model.BoltGroup modelBolt = modelObjectEnumerator.Current as Tekla.Structures.Model.BoltGroup;
                    MarkArray.Add(modelBolt.Identifier.ID.ToString());
                }
            }
            MarkArray.Sort();
            string MarkList = "";
            foreach (string strMark in MarkArray)
            {
                MarkList = strMark + " " + MarkList;
            }
            Clipboard.SetDataObject(MarkList); richTextBox1.Text = MarkList;
            drawingHandler.GetDrawingObjectSelector().SelectObjects(DrawingObjectArray, false);
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