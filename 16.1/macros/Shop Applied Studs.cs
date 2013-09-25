// Created by Dale Nicholls
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
    public class StudCreator : System.Windows.Forms.Form
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

        private TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private TextBox textBox3;
        private System.Windows.Forms.CheckBox checkBox1;

        static Tekla.Technology.Akit.IScript akit;

        public StudCreator(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(77, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(101, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "150";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Offset";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(99, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Insert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Spacing (X)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(77, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(101, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "95";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Centres (Y)";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(77, 64);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(101, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "0";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 90);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Staggered";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // StudCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 146);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StudCreator";
            this.Text = "Stud Creator";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        Model model = new Model();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TransformationPlane currentTransformationPlane = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());

                Tekla.Structures.Model.UI.Picker picker = new Tekla.Structures.Model.UI.Picker();
                Tekla.Structures.Model.ModelObject modelObject = picker.PickObject(Tekla.Structures.Model.UI.Picker.PickObjectEnum.PICK_ONE_PART);
                ArrayList array = new  ArrayList();
                array = picker.PickPoints(Tekla.Structures.Model.UI.Picker.PickPointEnum.PICK_TWO_POINTS, "");
                Tekla.Structures.Model.Part beam = (Tekla.Structures.Model.Part)modelObject;
              
                Phase phase = new Phase();
                beam.GetPhase(out phase);
                Tekla.Structures.Geometry3d.Point point1 = (Tekla.Structures.Geometry3d.Point)array[0];
                Tekla.Structures.Geometry3d.Point point2 = (Tekla.Structures.Geometry3d.Point)array[1];

                double offset = 0; double spacing = 0; double centres = 0;
                double.TryParse(textBox1.Text, out offset);
                double.TryParse(textBox2.Text, out spacing);
                double.TryParse(textBox3.Text, out centres);

                Tekla.Structures.Geometry3d.LineSegment lineSegment = new Tekla.Structures.Geometry3d.LineSegment(point1, point2);
                double count = (lineSegment.Length() - offset - 15) / spacing;
                count = Math.Truncate(count);

                if (checkBox1.Checked)
                {
                    ArrayList PartDblRepPropNames = new ArrayList();
                    PartDblRepPropNames.Add("PROFILE.WIDTH");
                    Hashtable dblProps = new Hashtable();
                    beam.GetDoubleReportProperties(PartDblRepPropNames, ref dblProps);
                    double length = (double)dblProps["PROFILE.WIDTH"];

                    if (length < 171)
                        MessageBox.Show("Warning: Flange to small");

                    BoltXYList staggeredBolt = new BoltXYList();
                    staggeredBolt.FirstPosition = point1;
                    staggeredBolt.SecondPosition = point2;
                    staggeredBolt.PartToBoltTo = beam;
                    staggeredBolt.PartToBeBolted = beam;

                    staggeredBolt.BoltSize = 19.05;
                    staggeredBolt.Tolerance = 2;
                    staggeredBolt.BoltStandard = "NELSON";
                    staggeredBolt.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
                    staggeredBolt.CutLength = 100;

                    staggeredBolt.Length = 100;
                    staggeredBolt.ExtraLength = 100;
                    staggeredBolt.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;

                    staggeredBolt.Position.Depth = Position.DepthEnum.MIDDLE;
                    staggeredBolt.Position.Plane = Position.PlaneEnum.MIDDLE;
                    staggeredBolt.Position.Rotation = Position.RotationEnum.FRONT;

                    staggeredBolt.Bolt = true;
                    staggeredBolt.Washer1 = staggeredBolt.Washer2 = staggeredBolt.Washer3 = staggeredBolt.Nut1 = staggeredBolt.Nut2 = false;

                    staggeredBolt.Hole1 = staggeredBolt.Hole2 = staggeredBolt.Hole3 = staggeredBolt.Hole4 = staggeredBolt.Hole5 = false;

                    staggeredBolt.StartPointOffset.Dx = offset;
                    staggeredBolt.AddBoltDistX(0);
                    staggeredBolt.AddBoltDistY(centres / 2);

                    int side = 1;
                    for (int i = 1; i <= count; i++)
                    {
                        side = side * -1;
                        staggeredBolt.AddBoltDistX(i * spacing);
                        staggeredBolt.AddBoltDistY(side * (centres / 2));
                    }

                    staggeredBolt.Insert();
                    staggeredBolt.SetPhase(phase);
                    staggeredBolt.Modify();
                }

                if (!checkBox1.Checked)
                {
                    BoltArray bolt = new BoltArray();
                    bolt.FirstPosition = point1;
                    bolt.SecondPosition = point2;
                    bolt.PartToBoltTo = beam;
                    bolt.PartToBeBolted = beam;

                    bolt.BoltSize = 19.05;
                    bolt.Tolerance = 2;
                    bolt.BoltStandard = "NELSON";
                    bolt.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
                    bolt.CutLength = 100;

                    bolt.Length = 100;
                    bolt.ExtraLength = 100;
                    bolt.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;

                    bolt.Position.Depth = Position.DepthEnum.MIDDLE;
                    bolt.Position.Plane = Position.PlaneEnum.MIDDLE;
                    bolt.Position.Rotation = Position.RotationEnum.FRONT;

                    bolt.Bolt = true;
                    bolt.Washer1 = bolt.Washer2 = bolt.Washer3 = bolt.Nut1 = bolt.Nut2 = false;

                    bolt.Hole1 = bolt.Hole2 = bolt.Hole3 = bolt.Hole4 = bolt.Hole5 = false;

                    bolt.StartPointOffset.Dx = offset;
                    bolt.AddBoltDistX(0);
                    bolt.AddBoltDistY(centres);

                    for (int i = 1; i <= count; i++)
                    {
                        bolt.AddBoltDistX(spacing);
                    }

                    bolt.Insert();
                    bolt.SetPhase(phase);
                    bolt.Modify();
                }
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTransformationPlane);
                model.CommitChanges();
            }
            catch { }
        }
    }

    public class Script
    {
        static Tekla.Technology.Akit.IScript akit;

        public static void Run(Tekla.Technology.Akit.IScript RunMe)
        {
            akit = RunMe;
            Application.Run(new StudCreator(akit));
        }
    }
}