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
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace Tekla.Technology.Akit.UserScript
{
    public class CopyDrawingObjects : System.Windows.Forms.Form
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

        private System.Windows.Forms.Button CopyObjects;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button PasteObjects;

        static Tekla.Technology.Akit.IScript akit;

        public CopyDrawingObjects(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.CopyObjects = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PasteObjects = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CopyObjects
            // 
            this.CopyObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyObjects.Location = new System.Drawing.Point(146, 12);
            this.CopyObjects.Name = "CopyObjects";
            this.CopyObjects.Size = new System.Drawing.Size(98, 23);
            this.CopyObjects.TabIndex = 0;
            this.CopyObjects.Text = "Copy Objects";
            this.CopyObjects.UseVisualStyleBackColor = true;
            this.CopyObjects.Click += new System.EventHandler(this.CopyObjects_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 78);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(256, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // PasteObjects
            // 
            this.PasteObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasteObjects.Location = new System.Drawing.Point(146, 41);
            this.PasteObjects.Name = "PasteObjects";
            this.PasteObjects.Size = new System.Drawing.Size(98, 23);
            this.PasteObjects.TabIndex = 3;
            this.PasteObjects.Text = "Paste Objects";
            this.PasteObjects.UseVisualStyleBackColor = true;
            this.PasteObjects.Click += new System.EventHandler(this.PasteObjects_Click);
            // 
            // CopyDrawingObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 100);
            this.Controls.Add(this.PasteObjects);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CopyObjects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CopyDrawingObjects";
            this.Text = "CopyDrawingObjects";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TeklaForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        Model model = new Model();
        DrawingHandler drawingHandler = new DrawingHandler();
        public DrawingObjectEnumerator selectedObjects;
        public Tekla.Structures.Drawing.View v;
        public ArrayList selectedPartsArray = new ArrayList();
        public ArrayList selectedObjectsArray = new ArrayList();
        public ArrayList selectedViewsArray = new ArrayList();

        private void TeklaForm_Load(object sender, EventArgs e)
        {

        }

        private void CopyObjects_Click(object sender, EventArgs e)
        {
            try
            {
                selectedObjects = drawingHandler.GetDrawingObjectSelector().GetSelected();
                
                selectedObjectsArray.Clear();
                selectedPartsArray.Clear();
                selectedViewsArray.Clear();

                foreach (DrawingObject drawingObject in selectedObjects)
                {
                    if (drawingObject is Tekla.Structures.Drawing.Part)
                    {
                        Tekla.Structures.Drawing.Part drawingPart = (Tekla.Structures.Drawing.Part)drawingObject;
                        selectedPartsArray.Add(drawingPart.ModelIdentifier);
                        v = (Tekla.Structures.Drawing.View)drawingPart.GetView();
                    }
                    if (drawingObject is Tekla.Structures.Drawing.View)
                        selectedViewsArray.Add(drawingObject);
                    if (drawingObject is Tekla.Structures.Drawing.GraphicObject)
                        selectedObjectsArray.Add(drawingObject);
                    if (drawingObject is Tekla.Structures.Drawing.Text)
                        selectedObjectsArray.Add(drawingObject);
                }
                int count = selectedObjectsArray.Count + selectedPartsArray.Count + selectedViewsArray.Count;
                statusLabel.Text = count.ToString() + " objects copied";
            }
            catch { }
        }

        private void PasteObjects_Click(object sender, EventArgs e)
        {
            try
            {
                Drawing drawing = drawingHandler.GetActiveDrawing();

                if (selectedPartsArray.Count > 0)
                {
                    Tekla.Structures.Drawing.View view = new Tekla.Structures.Drawing.View(drawing.GetSheet(), v.ViewCoordinateSystem, v.DisplayCoordinateSystem, selectedPartsArray);
                    view.Insert();
                }

                if (selectedViewsArray.Count > 0)
                {
                    foreach (Tekla.Structures.Drawing.View oView in selectedViewsArray)
                    {
                        Tekla.Structures.Drawing.View view = new Tekla.Structures.Drawing.View(drawing.GetSheet(), oView.ViewCoordinateSystem, oView.DisplayCoordinateSystem, oView.RestrictionBox);
                        view = oView;
                        view.Insert();
                    }
                }

                if (selectedObjectsArray.Count > 0)
                {
                    Tekla.Structures.Drawing.UI.Picker picker = drawingHandler.GetPicker();
                    Tekla.Structures.Geometry3d.Point point = null;
                    ViewBase selectedView = null;
                    picker.PickPoint("Pick View", out point, out selectedView);

                    foreach (DrawingObject drawingObject in selectedObjectsArray)
                    {
                        if (drawingObject is Tekla.Structures.Drawing.Text)
                        {
                            Tekla.Structures.Drawing.Text oText = (Tekla.Structures.Drawing.Text)drawingObject;
                            Tekla.Structures.Drawing.Text text = new Tekla.Structures.Drawing.Text(selectedView, oText.InsertionPoint, oText.TextString, oText.Attributes);

                            if (oText.Placing is Tekla.Structures.Drawing.LeaderLinePlacing)
                            {
                                Tekla.Structures.Drawing.LeaderLinePlacing l = (Tekla.Structures.Drawing.LeaderLinePlacing)oText.Placing;
                                text = new Tekla.Structures.Drawing.Text(selectedView, oText.InsertionPoint, oText.TextString, new LeaderLinePlacing(l.StartPoint), oText.Attributes);
                                text.Insert();
                            }
                            else
                            {
                                text = new Tekla.Structures.Drawing.Text(selectedView, oText.InsertionPoint, oText.TextString, oText.Attributes);
                                text.Insert();
                            }
                        }

                        if (drawingObject is Tekla.Structures.Drawing.Line)
                        {
                            Tekla.Structures.Drawing.Line oLine = (Tekla.Structures.Drawing.Line)drawingObject;
                            Tekla.Structures.Drawing.Line line = new Tekla.Structures.Drawing.Line(selectedView, oLine.StartPoint, oLine.EndPoint, oLine.Bulge, oLine.Attributes);
                            line.Insert();
                        }

                        if (drawingObject is Tekla.Structures.Drawing.Arc)
                        {
                            Tekla.Structures.Drawing.Arc oArc = (Tekla.Structures.Drawing.Arc)drawingObject;
                            Tekla.Structures.Drawing.Arc arc = new Arc(selectedView, oArc.StartPoint, oArc.EndPoint, oArc.Radius, oArc.Attributes);
                            arc.Insert();
                        }

                        if (drawingObject is Tekla.Structures.Drawing.Polyline)
                        {
                            Tekla.Structures.Drawing.Polyline oPolyline = (Tekla.Structures.Drawing.Polyline)drawingObject;
                            Tekla.Structures.Drawing.Polyline polyline = new Polyline(selectedView, oPolyline.Points, oPolyline.Attributes);
                            polyline.Bulge = oPolyline.Bulge;
                            polyline.Insert();
                        }

                        if (drawingObject is Tekla.Structures.Drawing.Rectangle)
                        {
                            Tekla.Structures.Drawing.Rectangle oRectangle = (Tekla.Structures.Drawing.Rectangle)drawingObject;
                            Tekla.Structures.Drawing.Rectangle rectangle = new Tekla.Structures.Drawing.Rectangle(selectedView, oRectangle.StartPoint, oRectangle.Width, oRectangle.Height, oRectangle.Attributes);
                            rectangle.Angle = oRectangle.Angle;
                            rectangle.Insert();
                        }

                        if (drawingObject is Tekla.Structures.Drawing.Circle)
                        {
                            Tekla.Structures.Drawing.Circle oCircle = (Tekla.Structures.Drawing.Circle)drawingObject;
                            Tekla.Structures.Drawing.Circle circle = new Circle(selectedView, oCircle.CenterPoint, oCircle.Radius, oCircle.Attributes);
                            circle.Insert();
                        }

                        if (drawingObject is Tekla.Structures.Drawing.Polygon)
                        {
                            Tekla.Structures.Drawing.Polygon oPolygon = (Tekla.Structures.Drawing.Polygon)drawingObject;
                            Tekla.Structures.Drawing.Polygon polygon = new Tekla.Structures.Drawing.Polygon(selectedView, oPolygon.Points, oPolygon.Attributes); ;
                            polygon.Bulge = oPolygon.Bulge;
                            polygon.Insert();
                        }
                    }
                }
                statusLabel.Text = "Objects pasted in drawing " + drawing.Mark.Substring(1, drawing.Mark.Length - 2);
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }
    }

    public class Script
    {
        static Tekla.Technology.Akit.IScript akit;

        public static void Run(Tekla.Technology.Akit.IScript RunMe)
        {
            akit = RunMe;
            Application.Run(new CopyDrawingObjects(akit));
        }
    }
}