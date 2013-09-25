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

                ArrayList ModelObjectArray = new ArrayList();
                
                if (drawingHandler.GetActiveDrawing() != null)
                {
                    DrawingObjectEnumerator drawingObjectEnum = drawingHandler.GetDrawingObjectSelector().GetSelected();
                    while (drawingObjectEnum.MoveNext())
                    {
                        Tekla.Structures.Drawing.ModelObject dModelObject = (Tekla.Structures.Drawing.ModelObject)drawingObjectEnum.Current;
                        ModelObjectArray.Add(model.SelectModelObject(dModelObject.ModelIdentifier));
                    }
                }
                else
                {
                    ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
                    while (modelObjectEnum.MoveNext())
                    {   
                        Tekla.Structures.Model.ModelObject modelObject = (Tekla.Structures.Model.ModelObject)modelObjectEnum.Current;
                        ModelObjectArray.Add(model.SelectModelObject(modelObject.Identifier));
                    }
                }

                Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
                modelObjectSelector.Select(ModelObjectArray);
                akit.Callback("acmdCreateViewBySelectedObjectsExtrema", "", "main_frame");
                akit.Callback("acmd_interrupt", "", "main_frame");
            }
            catch { }
        }
    }
}
