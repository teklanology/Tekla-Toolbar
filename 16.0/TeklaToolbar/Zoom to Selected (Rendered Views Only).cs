using System.Collections;

using Tekla.Structures;
using Tekla.Structures.Drawing;
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

                ArrayList ModelObjectArray = new ArrayList();
                
                if (drawingHandler.GetActiveDrawing() != null)
                {
                    DrawingObjectEnumerator drawingObjectEnum = drawingHandler.GetDrawingObjectSelector().GetSelected();
                    if (drawingObjectEnum.GetSize() > 0)
					{
						while (drawingObjectEnum.MoveNext())
						{
							Tekla.Structures.Drawing.ModelObject dModelObject = (Tekla.Structures.Drawing.ModelObject)drawingObjectEnum.Current;
							ModelObjectArray.Add(model.SelectModelObject(dModelObject.ModelIdentifier));
						}
					}
					
					Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
					modelObjectSelector.Select(ModelObjectArray);
                }
				
				akit.Callback("acmdZoomToSelected", "", "main_frame");
			}
			catch { }
        }
    }
}
