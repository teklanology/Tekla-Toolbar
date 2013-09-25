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
            Model model = new Model();
            DrawingHandler drawingHandler = new DrawingHandler();
            DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();

            if (drawingHandler.GetActiveDrawing() == null)
            {
                while (drawingEnum.MoveNext())
                {
                    if (drawingEnum.Current is AssemblyDrawing)
                    {
                        AssemblyDrawing assemblyDrawing = drawingEnum.Current as AssemblyDrawing;
                        drawingHandler.SetActiveDrawing(assemblyDrawing, false);
                        DrawingObjectEnumerator drawingObjectEnum = drawingHandler.GetActiveDrawing().GetSheet().GetAllObjects();
                        while (drawingObjectEnum.MoveNext())
                        {
                            if (drawingObjectEnum.Current is Tekla.Structures.Drawing.Part)
                            {
                                Tekla.Structures.Drawing.Part part = drawingObjectEnum.Current as Tekla.Structures.Drawing.Part;
                                ArrayList array = new ArrayList();
                                array.Add(model.SelectModelObject(new Tekla.Structures.Identifier(part.ModelIdentifier.ID)));
                                Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
                                modelObjectSelector.Select(array);
                                ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
                                while (modelObjectEnum.MoveNext())
                                {
                                    if (modelObjectEnum.Current is Tekla.Structures.Model.Part)
                                    {
                                        Tekla.Structures.Model.Part mpart = modelObjectEnum.Current as Tekla.Structures.Model.Part;
                                        Tekla.Structures.Model.Assembly assembly = mpart.GetAssembly();
                                        Tekla.Structures.Model.Part mainPart = (Tekla.Structures.Model.Part)assembly.GetMainPart();
                                        assemblyDrawing.Name = mainPart.Name;
                                        assemblyDrawing.Modify();
                                        assemblyDrawing.CommitChanges();
                                    }
                                }
                                modelObjectSelector.Select(new ArrayList());
                            }
                        }
                        drawingHandler.CloseActiveDrawing();
                    }
                }
            }
        }
    }
}
