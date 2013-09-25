using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            Model model = new Model();
            //Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            DrawingHandler dh = new DrawingHandler();
            DrawingObjectEnumerator doe = dh.GetDrawingObjectSelector().GetSelected();
            while (doe.MoveNext())
            {
                string CONN_CODE_END1 = "", CONN_CODE_END2 = "";
                if (doe.Current is Tekla.Structures.Drawing.Part)
                {
                    Tekla.Structures.Drawing.Part dPart = (Tekla.Structures.Drawing.Part)doe.Current;
                    Tekla.Structures.Model.Part mPart = (Tekla.Structures.Model.Part)model.SelectModelObject((Identifier)dPart.ModelIdentifier);
                    if (mPart is Beam)
                    {
                        Beam beam = (Beam)mPart;
                        ViewBase view = dPart.GetView();
                        mPart.GetUserProperty("CONN_CODE_END1", ref CONN_CODE_END1);
                        mPart.GetUserProperty("CONN_CODE_END2", ref CONN_CODE_END2);

                        if (CONN_CODE_END1 != "")
                        {
                            Tekla.Structures.Drawing.Text text = new Tekla.Structures.Drawing.Text(view, beam.StartPoint + new Point(200, 200), CONN_CODE_END1, new LeaderLinePlacing(beam.StartPoint));
                            text.Insert();
                        }
                        if (CONN_CODE_END2 != "")
                        {
                            Tekla.Structures.Drawing.Text text = new Tekla.Structures.Drawing.Text(view, beam.EndPoint + new Point(200, 200), CONN_CODE_END2, new LeaderLinePlacing(beam.EndPoint));
                            text.Insert();
                        }
                    }
                }
            }
        }
    }
}
