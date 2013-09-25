using System.Collections;
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
                Model model = new Model();
                DrawingHandler drawingHandler = new DrawingHandler();
                DrawingObjectEnumerator drawingObjectEnum = drawingHandler.GetDrawingObjectSelector().GetSelected();
                ArrayList ModelObjectArray = new ArrayList();

                if (drawingObjectEnum.GetSize() > 0)
                {
                    while (drawingObjectEnum.MoveNext())
                    {
                        if (drawingObjectEnum.Current is Tekla.Structures.Drawing.Part)
                        {
                            Tekla.Structures.Drawing.Part part = drawingObjectEnum.Current as Tekla.Structures.Drawing.Part;
                            ModelObjectArray.Add(model.SelectModelObject(new Identifier(part.ModelIdentifier.ID)));
                        }
                        if (drawingObjectEnum.Current is Tekla.Structures.Drawing.Bolt)
                        {
                            Tekla.Structures.Drawing.Bolt bolt = drawingObjectEnum.Current as Tekla.Structures.Drawing.Bolt;
                            ModelObjectArray.Add(model.SelectModelObject(new Identifier(bolt.ModelIdentifier.ID)));
                        }
                    }
                }

                Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
                modelObjectSelector.Select(ModelObjectArray);
                akit.Callback("acmd_display_selected_object_dialog", "", "main_frame");
            }
            catch { }
        }
    }
}
