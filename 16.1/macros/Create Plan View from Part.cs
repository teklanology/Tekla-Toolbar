using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using System.Windows.Forms;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                Model model = new Model();
                TransformationPlane transformationplane = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());
                ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
                
                if (modelObjectEnum.GetSize() == 1)
                {
                    while (modelObjectEnum.MoveNext())
                    {
                        if (modelObjectEnum.Current is Tekla.Structures.Model.Part)
                        {
                            Tekla.Structures.Model.Part part = modelObjectEnum.Current as Tekla.Structures.Model.Part;
                            double level = 0; part.GetReportProperty("TOP_LEVEL_UNFORMATTED", ref level);
                            akit.CommandStart("ail_create_basic_view", "", "main_frame");
                            akit.ValueChange("Modelling create view", "v1_coordinate", level.ToString("F02"));
                            akit.PushButton("v1_create", "Modelling create view");              
                        }
                    }
                }

                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(transformationplane);
            }
            catch { }
        }
    }
}
