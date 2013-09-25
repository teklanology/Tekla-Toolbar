using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;
using System.Collections;

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
                DrawingObjectEnumerator MyDrawingObjEnum = drawingHandler.GetDrawingObjectSelector().GetSelected();
                Tekla.Structures.Model.UI.ModelObjectSelector SelectObjects = new Tekla.Structures.Model.UI.ModelObjectSelector();
                Tekla.Structures.Drawing.Weld SelectedWeld = null;
                if (MyDrawingObjEnum.GetSize() == 1)
                {
                    while (MyDrawingObjEnum.MoveNext())
                    {
                        if (MyDrawingObjEnum.Current is Tekla.Structures.Drawing.Weld)
                        {
                            SelectedWeld = MyDrawingObjEnum.Current as Tekla.Structures.Drawing.Weld;
                            ArrayList temp = new ArrayList();
                            temp.Add(model.SelectModelObject(new Identifier(SelectedWeld.ModelIdentifier.ID)));
                            SelectObjects.Select(temp);
                            BaseWeld SelectedModelWeld = temp[0] as BaseWeld;

                            string strSizeAbove = SelectedModelWeld.SizeAbove.ToString();
                            string strTypeAbove = SelectedModelWeld.TypeAbove.ToString();
                            if (strTypeAbove == "WELD_TYPE_FILLET") strTypeAbove = "10"; // Fillet weld
                            if (strTypeAbove == "WELD_TYPE_SINGLE_BEVEL_BUTT_WITH_BROAD_ROOT_FACE") strTypeAbove = "6"; // W1 weld
                            if (strTypeAbove == "WELD_TYPE_NONE") strTypeAbove = "0"; // No weld
                            if (strTypeAbove == "WELD_TYPE_BEVEL_GROOVE_SINGLE_BEVEL_BUTT") strTypeAbove = "4"; // W6 weld
                            if (strTypeAbove == "WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT") strTypeAbove = "2"; // W3 weld
                            if (strTypeAbove == "WELD_TYPE_PLUG") strTypeAbove = "11"; // Plug weld
                            if (strTypeAbove == "WELD_TYPE_BEVEL_BACKING") strTypeAbove = "9"; // Seal weld
                            if (strTypeAbove == "WELD_TYPE_PARTIAL_PENETRATION_SINGLE_BEVEL_BUTT_PLUS_FILLET") strTypeAbove = "18"; // W1 weld + reinforced

                            string strContourAbove = SelectedModelWeld.ContourAbove.ToString();
                            if (strContourAbove == "WELD_CONTOUR_NONE") strContourAbove = "0";
                            if (strContourAbove == "WELD_CONTOUR_FLUSH") strContourAbove = "1";

                            string strSizeBelow = SelectedModelWeld.SizeBelow.ToString();
                            string strTypeBelow = SelectedModelWeld.TypeBelow.ToString();
                            if (strTypeBelow == "WELD_TYPE_FILLET") strTypeBelow = "10";
                            if (strTypeBelow == "WELD_TYPE_SINGLE_BEVEL_BUTT_WITH_BROAD_ROOT_FACE") strTypeBelow = "6";
                            if (strTypeBelow == "WELD_TYPE_NONE") strTypeBelow = "0"; // No weld
                            if (strTypeBelow == "WELD_TYPE_BEVEL_GROOVE_SINGLE_BEVEL_BUTT") strTypeBelow = "4"; // W6 weld
                            if (strTypeBelow == "WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT") strTypeBelow = "2"; // W3 weld
                            if (strTypeBelow == "WELD_TYPE_PLUG") strTypeBelow = "11"; // Plug weld
                            if (strTypeBelow == "WELD_TYPE_BEVEL_BACKING") strTypeBelow = "9"; // Seal weld
                            if (strTypeBelow == "WELD_TYPE_PARTIAL_PENETRATION_SINGLE_BEVEL_BUTT_PLUS_FILLET") strTypeBelow = "18"; // W1 weld + reinforced

                            string strContourBelow = SelectedModelWeld.ContourBelow.ToString();
                            if (strContourBelow == "WELD_CONTOUR_NONE") strContourBelow = "0";
                            if (strContourBelow == "WELD_CONTOUR_FLUSH") strContourBelow = "1";

                            string strAroundWeld = SelectedModelWeld.AroundWeld.ToString();
                            if (strAroundWeld == "False") strAroundWeld = "0";
                            if (strAroundWeld == "True") strAroundWeld = "1";

                            string strRefText = SelectedModelWeld.ReferenceText;

                            akit.PushButton("wld_cancel", "Weld Mark Properties");
                            akit.Callback("acmd_display_attr_dialog", "wld_dial", "main_frame");
                            akit.ValueChange("wld_dial", "gr_wld_get_menu", "standard");
                            akit.ValueChange("wld_dial", "w_size", strSizeBelow);
                            akit.ValueChange("wld_dial", "w_size2", strSizeAbove);
                            akit.ValueChange("wld_dial", "w_type", strTypeBelow);
                            akit.ValueChange("wld_dial", "w_type2", strTypeAbove);
                            akit.ValueChange("wld_dial", "w_ftype", strContourBelow);
                            akit.ValueChange("wld_dial", "w_ftype2", strContourAbove);
                            akit.ValueChange("wld_dial", "w_around", strAroundWeld);
                            akit.ValueChange("wld_dial", "w_wld", strRefText);
                            akit.PushButton("wld_apply", "wld_dial");
                            akit.CommandStart("ail_create_wld", "", "main_frame");
                        }
                    }
                    Tekla.Structures.Drawing.UI.DrawingObjectSelector drawingObjectSelector = drawingHandler.GetDrawingObjectSelector();
                    drawingObjectSelector.SelectObject(SelectedWeld);
                }
            }
            catch { }
        }
    }
}
