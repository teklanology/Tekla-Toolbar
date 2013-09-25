using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                akit.ValueChange("main_frame", "depth_position_om", "3");
                akit.CommandStart("ail_create_basic_view", "", "main_frame");

                Model model = new Model();
                TransformationPlane transformationplane = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());
                Tekla.Structures.Model.UI.Picker picker = new Tekla.Structures.Model.UI.Picker();
                Tekla.Structures.Geometry3d.Point point = picker.PickPoint();
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(transformationplane);
                
                akit.ValueChange("Modelling create view", "v1_coordinate", point.Z.ToString("F02"));
                akit.PushButton("v1_create", "Modelling create view");
                //akit.PushButton("v1_create_cancel", "Modelling create view");
            }
            catch { }
        }
    }
}
