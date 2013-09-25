using System.Collections;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            Model model = new Model();
            ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
            Tekla.Structures.Model.UI.Picker picker = new Tekla.Structures.Model.UI.Picker();

            if (modelObjectEnum.GetSize() == 0) modelObjectEnum = picker.PickObjects(Tekla.Structures.Model.UI.Picker.PickObjectsEnum.PICK_N_PARTS);

            ArrayList arrayPoints = picker.PickPoints(Tekla.Structures.Model.UI.Picker.PickPointEnum.PICK_TWO_POINTS);
            Point point1 = (Tekla.Structures.Geometry3d.Point)arrayPoints[0];
            Point point2 = (Tekla.Structures.Geometry3d.Point)arrayPoints[1];
            Line line = new Tekla.Structures.Geometry3d.Line(point1, point2);

            while (modelObjectEnum.MoveNext())
            {
                if (modelObjectEnum.Current is Beam)
                {
                    Beam beam = (Beam)modelObjectEnum.Current;
                    Line line2 = new Line(beam.StartPoint, beam.EndPoint);
                    Point intersection = Intersection.LineToLine(line, line2).Point1;
                    Tekla.Structures.Model.Operations.Operation.Split(beam, intersection);
                }
            }
            model.CommitChanges();
        }
    }
}
