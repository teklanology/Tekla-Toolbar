using System;
using System.Windows.Forms;
using Tekla.Technology.Akit;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                Model model = new Model();
                DrawingHandler drawingHandler = new DrawingHandler();
                Drawing drawing = drawingHandler.GetActiveDrawing();

                if (drawing != null)
                {
                    Tekla.Structures.Drawing.StraightDimension sd = null;
                    Tekla.Structures.Drawing.Bolt bolt = null;
                    DrawingObjectEnumerator drawingObjectEnum = drawingHandler.GetDrawingObjectSelector().GetSelected();
                    while (drawingObjectEnum.MoveNext())
                    {
                        if (drawingObjectEnum.Current is Tekla.Structures.Drawing.StraightDimension)
                            sd = (Tekla.Structures.Drawing.StraightDimension)drawingObjectEnum.Current;
                        if (drawingObjectEnum.Current is Tekla.Structures.Drawing.Bolt)
                            bolt = (Tekla.Structures.Drawing.Bolt)drawingObjectEnum.Current;
                    }

                    if (bolt == null)
                    {
                        Tekla.Structures.Drawing.UI.Picker picker = drawingHandler.GetPicker();
                        DrawingObject drawingObject = null;
                        Tekla.Structures.Drawing.ViewBase viewBase = null;
                        Tekla.Structures.Geometry3d.Point pickedPoint = null;
                        Type[] TypeFilter = new Type[] { typeof(Tekla.Structures.Drawing.Bolt) };

                        picker.PickObject("pick bolt", TypeFilter, out drawingObject, out viewBase, out pickedPoint);
                        bolt = (Tekla.Structures.Drawing.Bolt)drawingObject;
                    }

                    Tekla.Structures.Model.BoltGroup mBolt = (Tekla.Structures.Model.BoltGroup)model.SelectModelObject((Identifier)bolt.ModelIdentifier);
                    string note = "";
                    if (mBolt is BoltArray)
                    {
                        BoltArray boltArray = (BoltArray)mBolt;

                        if (boltArray.GetBoltDistYCount() == 1 && boltArray.GetBoltDistY(0) == 0)
                        {
                            note = (mBolt.BoltPositions.Count - 1).ToString() + " No SPACES @ " + boltArray.GetBoltDistX(1).ToString() + " = ";
                        }

                        if (boltArray.GetBoltDistY(0) > 0)
                        {
                            
                        }                        
                    }
                    else if (mBolt is BoltXYList)
                    {
                        BoltXYList boltXYList = (BoltXYList)mBolt;
                        note = (boltXYList.BoltPositions.Count - 1).ToString() + " STAGGERED No SPACES @ " + (boltXYList.GetBoltDistX(1)).ToString() + " = ";
                    }

                    Tekla.Structures.Drawing.UI.DrawingObjectSelector drawingObjectSelector = drawingHandler.GetDrawingObjectSelector();
                    drawingObjectSelector.SelectObject(sd);

                    akit.ValueChange("main_frame", "gr_sel_dimension", "1");
                    akit.Callback("acmd_display_attr_dialog", "dim_dial", "main_frame");
                    akit.PushButton("dim_set", "dim_dial");
                    akit.TabChange("dim_dial", "tabWndDimAttrib", "tabMarks");
                    akit.PushButton("dim_on_off", "dim_dial");
                    akit.ValueChange("dim_dial", "txtFldPrefix", note);
                    akit.PushButton("dim_modify", "dim_dial");
                    akit.PushButton("dim_cancel", "dim_dial");
                }
            }
            catch { }
        }
    }
}
