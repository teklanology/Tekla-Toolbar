using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
			DrawingHandler DrawingHandler = new DrawingHandler();
            Drawing Drawing = DrawingHandler.GetActiveDrawing();
            DrawingObjectEnumerator DrawingObjEnum = Drawing.GetSheet().GetAllObjects();
            ArrayList MarkArray = new ArrayList();
			ArrayList PartArray = new ArrayList();
            while (DrawingObjEnum.MoveNext())
            {
                if (DrawingObjEnum.Current is MarkBase)
                    MarkArray.Add(DrawingObjEnum.Current);
					
				if (DrawingObjEnum.Current is Tekla.Structures.Drawing.Part || DrawingObjEnum.Current is Tekla.Structures.Drawing.Bolt)
					PartArray.Add(DrawingObjEnum.Current);
            }
            DrawingHandler.GetDrawingObjectSelector().SelectObjects(MarkArray, true);

			// part mark properties
            akit.Callback("acmd_display_selected_drawing_object_dialog", "", "main_frame");
            akit.TabChange("pmark_dial", "Container_2", "gr_mark_general_tab");
            akit.PushButton("gr_pmark_place", "pmark_dial");
			akit.ValueChange("pmpl_dial", "text_placing_mode", "1");
            akit.PushButton("txpl_modify", "pmpl_dial");
            akit.PushButton("txpl_cancel", "pmpl_dial");
            akit.PushButton("pmark_cancel", "pmark_dial");
			
			// bolt mark properties
            akit.Callback("acmd_display_attr_dialog", "smark_dial", "main_frame");
            akit.TabChange("smark_dial", "Container_217", "gr_mark_general_tab");
            akit.PushButton("gr_smark_place", "smark_dial");
            akit.ValueChange("smpl_dial", "text_placing_mode", "1");
            akit.PushButton("txpl_modify", "smpl_dial");
            akit.PushButton("txpl_cancel", "smpl_dial");
            akit.PushButton("smark_cancel", "smark_dial");
			
			// connection mark properties
			akit.Callback("acmd_display_attr_dialog", "jmark_dial", "main_frame");
            akit.TabChange("jmark_dial", "Container_217", "gr_mark_general_tab");
            akit.PushButton("gr_jmark_place", "jmark_dial");
            akit.ValueChange("jmpl_dial", "text_placing_mode", "1");
            akit.PushButton("txpl_modify", "jmpl_dial");
            akit.PushButton("txpl_cancel", "jmpl_dial");
            akit.PushButton("jmark_cancel", "jmark_dial");
			
			DrawingHandler.GetDrawingObjectSelector().UnselectAllObjects();
			DrawingHandler.GetDrawingObjectSelector().SelectObjects(PartArray, true);
			akit.Callback("acmd_update_marks_selected", "", "main_frame");
        }
    }
}
