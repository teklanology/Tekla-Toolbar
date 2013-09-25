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
            ModelInfo modelInfo = model.GetInfo();
            string drawingsFolderPath = modelInfo.ModelPath + @"\drawings\";
            DrawingHandler drawingHandler = new DrawingHandler();
            DrawingEnumerator drawingEnum = drawingHandler.GetDrawingSelector().GetSelected();
            if (drawingEnum.GetSize() == 1)
            {
                while (drawingEnum.MoveNext())
                {
                    System.Reflection.PropertyInfo propertyInfo = drawingEnum.Current.GetType().GetProperty("Identifier", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    object value = propertyInfo.GetValue(drawingEnum.Current, null);

                    Identifier Identifier = (Identifier)value;
                    Beam tempBeam = new Beam();
                    tempBeam.Identifier = Identifier;

                    string drawingFile = "";
                    bool result = tempBeam.GetReportProperty("DRAWING_PLOT_FILE", ref drawingFile);
                    System.IO.FileInfo file = new System.IO.FileInfo(drawingsFolderPath + drawingFile);
                    if (file.Exists)
                        System.Diagnostics.Process.Start("Explorer.exe", @"/select, " + file.FullName);
                }
            }
        }
    }
}
