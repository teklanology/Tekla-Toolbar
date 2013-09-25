using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tekla.Technology.Akit;
using TS = Tekla.Structures;
using TSD = Tekla.Structures.Drawing;
using TSG = Tekla.Structures.Geometry3d;
using TSM = Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                TSM.Model model = new TSM.Model();
                TSD.DrawingHandler drawingHandler = new TSD.DrawingHandler();
                TSG.Vector UpDirection = new TSG.Vector(0.0, 0.0, 1.0);
                TSD.Size A3 = new TSD.Size(410, 287);

                TSM.TransformationPlane current = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TSM.TransformationPlane());

                TSM.ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
                while (modelObjectEnum.MoveNext())
                {
                    if (modelObjectEnum.Current is Tekla.Structures.Model.Part)
                    {
                        TSM.Part selectedPart = (TSM.Part)modelObjectEnum.Current;

                        string USER_FIELD_3 = "", USER_FIELD_4 = "";
                        selectedPart.GetUserProperty("USER_FIELD_3", ref USER_FIELD_3);
                        selectedPart.GetUserProperty("USER_FIELD_4", ref USER_FIELD_4);
                        USER_FIELD_4 = USER_FIELD_4.Replace("(?)", "");
                        selectedPart.SetUserProperty("USER_FIELD_4", USER_FIELD_4);

                        if (USER_FIELD_3 == "M")
                        {
                            TSD.Drawing gaDrawing = new TSD.GADrawing("BRAD-Mod-Ass", A3);
                            gaDrawing.Name = selectedPart.Name;
                            gaDrawing.Title1 = "SITEWORK";
                            gaDrawing.Title2 = USER_FIELD_3 + USER_FIELD_4;
                            gaDrawing.Title3 = "";
                            gaDrawing.Insert();
                            drawingHandler.SetActiveDrawing(gaDrawing, false);

                            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new Tekla.Structures.Model.TransformationPlane(selectedPart.GetCoordinateSystem()));
                            TSM.Solid tsolid = selectedPart.GetSolid();
                            TSG.Point tsMinPt = tsolid.MinimumPoint;
                            TSG.Point tsMaxPt = tsolid.MaximumPoint;
                            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new Tekla.Structures.Model.TransformationPlane());

                            if (selectedPart.Name.Contains("BEAM"))
                            {
                                TSG.CoordinateSystem ModelObjectCoordSys = selectedPart.GetCoordinateSystem();
                                TSG.CoordinateSystem PlanViewCoordSys = new TSG.CoordinateSystem();
                                PlanViewCoordSys.Origin = new TSG.Point(ModelObjectCoordSys.Origin);
                                PlanViewCoordSys.AxisX = new TSG.Vector(ModelObjectCoordSys.AxisX) * -1.0;
                                PlanViewCoordSys.AxisY = new TSG.Vector(ModelObjectCoordSys.AxisY);

                                TSG.Vector tempVector = (PlanViewCoordSys.AxisX.Cross(UpDirection));
                                if (tempVector == new TSG.Vector())
                                    tempVector = (ModelObjectCoordSys.AxisY.Cross(UpDirection));

                                PlanViewCoordSys.AxisX = tempVector.Cross(UpDirection);
                                PlanViewCoordSys.AxisY = tempVector;

                                TSM.Solid solid = selectedPart.GetSolid();

                                TSG.AABB aabbPlanView = new TSG.AABB();
                                aabbPlanView.MinPoint = new TSG.Point(-50, tsMinPt.Z - 50, tsMinPt.Y - 50);
                                aabbPlanView.MaxPoint = new TSG.Point(tsMaxPt.X + 50, tsMaxPt.Z + 50, tsMaxPt.Y + 50);

                                TSD.View PlanView = new TSD.View(gaDrawing.GetSheet(), PlanViewCoordSys, PlanViewCoordSys, aabbPlanView, "BRAD-Mod-Ass");
                                PlanView.Name = "TOP";
                                PlanView.Scale = 10;
                                PlanView.Attributes.Shortening.CutParts = true;
                                PlanView.Attributes.Shortening.MinimumLength = 1200;
                                PlanView.Attributes.Shortening.Offset = 0.5;
                                PlanView.Insert();
                                PlanView.Attributes.FixedViewPlacing = true;
                                PlanView.Origin = new TSG.Point(100, 200);
                                PlanView.Modify();

                                TSG.CoordinateSystem FrontViewCoordSys = (TSG.CoordinateSystem)PlanViewCoordSys;
                                FrontViewCoordSys.AxisX = tempVector.Cross(UpDirection).GetNormal();
                                FrontViewCoordSys.AxisY = UpDirection.GetNormal();

                                TSG.AABB aabbFrontView = new TSG.AABB();
                                aabbFrontView.MinPoint = new TSG.Point(-50, tsMinPt.Y - 50, tsMinPt.Z - 50);
                                aabbFrontView.MaxPoint = new TSG.Point(tsMaxPt.X + 50, tsMaxPt.Y + 50, tsMaxPt.Z + 50);

                                TSD.View FrontView = new TSD.View(gaDrawing.GetSheet(), FrontViewCoordSys, FrontViewCoordSys, aabbFrontView, "BRAD-Mod-Ass");
                                FrontView.Name = "FRONT";
                                FrontView.Scale = 10;
                                FrontView.Attributes.Shortening.CutParts = true;
                                FrontView.Attributes.Shortening.MinimumLength = 1200;
                                FrontView.Attributes.Shortening.Offset = 0.5;
                                FrontView.Insert();
                                FrontView.Attributes.FixedViewPlacing = true;
                                FrontView.Origin = new TSG.Point(100, (200 - FrontView.Height - 2));
                                FrontView.Modify();
                            }
                            if (selectedPart.Name.Contains("COLUMN"))
                            {
                                TSG.CoordinateSystem ModelObjectCoordSys = selectedPart.GetCoordinateSystem();
                                TSG.CoordinateSystem PlanViewCoordSys = new TSG.CoordinateSystem();
                                PlanViewCoordSys.Origin = new TSG.Point(ModelObjectCoordSys.Origin);
                                PlanViewCoordSys.AxisX = new TSG.Vector(ModelObjectCoordSys.AxisX);
                                PlanViewCoordSys.AxisY = new TSG.Vector(ModelObjectCoordSys.AxisY);

                                TSG.Vector tempVector = (PlanViewCoordSys.AxisX.Cross(UpDirection));
                                if (tempVector == new TSG.Vector())
                                    tempVector = (ModelObjectCoordSys.AxisY.Cross(UpDirection));

                                TSG.AABB aabbPlanView = new TSG.AABB();
                                aabbPlanView.MinPoint = new TSG.Point(-50, tsMinPt.Y - 50, tsMinPt.Z - 50);
                                aabbPlanView.MaxPoint = new TSG.Point(tsMaxPt.X + 50, tsMaxPt.Y + 50, tsMaxPt.Z + 50);

                                TSD.View PlanView = new TSD.View(gaDrawing.GetSheet(), PlanViewCoordSys, PlanViewCoordSys, aabbPlanView, "BRAD-Mod-Ass");
                                PlanView.Name = "TOP";
                                PlanView.Scale = 10;
                                PlanView.Attributes.Shortening.CutParts = true;
                                PlanView.Attributes.Shortening.MinimumLength = 1200;
                                PlanView.Attributes.Shortening.Offset = 0.5;
                                PlanView.Origin = new TSG.Point(100, 200);
                                PlanView.Insert();
                                PlanView.Attributes.FixedViewPlacing = true;
                                PlanView.Modify();

                                TSG.CoordinateSystem FrontViewCoordSys = (TSG.CoordinateSystem)PlanViewCoordSys;
                                FrontViewCoordSys.AxisY = new TSG.Vector(ModelObjectCoordSys.AxisY).Cross(UpDirection) * -1;

                                TSG.AABB aabbFrontView = new TSG.AABB();
                                aabbFrontView.MinPoint = new TSG.Point(-50, tsMinPt.Z - 50, tsMinPt.Y - 50);
                                aabbFrontView.MaxPoint = new TSG.Point(tsMaxPt.X + 50, tsMaxPt.Z + 50, tsMaxPt.Y + 50);

                                TSD.View FrontView = new TSD.View(gaDrawing.GetSheet(), FrontViewCoordSys, FrontViewCoordSys, aabbFrontView, "BRAD-Mod-Ass");
                                FrontView.Name = "FRONT";
                                FrontView.Scale = 10;
                                FrontView.Attributes.Shortening.CutParts = true;
                                FrontView.Attributes.Shortening.MinimumLength = 1200;
                                FrontView.Attributes.Shortening.Offset = 0.5;
                                FrontView.Origin = new TSG.Point(100, (200 - FrontView.Height - 30));
                                FrontView.Insert();
                                FrontView.Attributes.FixedViewPlacing = true;
                                FrontView.Modify();
                            }
                            drawingHandler.CloseActiveDrawing(true);
                        }
                    }
                }
                MessageBox.Show("Drawings Created");
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(current);
            }
            catch { }
        }
    }
}
