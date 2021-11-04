using System;
using System.Collections.Generic;
using Inventor;
using System.Windows.Forms;

namespace InventorSegmentsGenerator
{
    public class PillarPartA48 : ProfileA48
    {
        public enum TypeOfFastening { Boot, Ground, Rack }

        public double heightPillar;
        public double basicAngle;
        public int numberOfHoles;
        public Dictionary<int, double> distanceToHoles;
        public TypeOfFastening typeOfFastening;
        public double holeDiametr;

        protected Face oFrontFace;

        public PillarPartA48(Inventor.Application invApp) : base(invApp, 120, "Стеклопластик", "Оранжевый")
        {
            heightPillar = 120;
            basicAngle = 0;
            numberOfHoles = 3;
            distanceToHoles = new Dictionary<int, double> { { 1, 3.6}, { 2, 45}, { 3, 95} };
            typeOfFastening = TypeOfFastening.Boot;
            holeDiametr = 0.9;
        }

        public PillarPartA48(Inventor.Application invApp, double heightPillar, string materialProfile, string colorProfile, TypeOfFastening typeOfFastening) :
            base (invApp, heightPillar, materialProfile, colorProfile)

        {
            this.heightPillar = heightPillar;
            basicAngle = 0;
            numberOfHoles = 3;
            distanceToHoles = new Dictionary<int, double> { { 1, 3.6 }, { 2, 45 }, { 3, 95 } };
            this.typeOfFastening = typeOfFastening;
            holeDiametr = 0.9;
        }

        public void createPillarPart()
        {
            createProfile();
            createBoltHoles();
            basicAngle = -30;
            createAngleCut();
        }


        private void createAngleCut()
        {
            double cathe = Math.Abs(oLine[0].Length * Math.Tan(basicAngle * Math.PI / 180)); //угол в радианах

            if (Math.Abs(basicAngle) < 0.0000001)
            {
                return;
            }
            else if (basicAngle > 0)
            {
                ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];
                extrudeFeature.Definition.SetDistanceExtent(heightPillar + cathe, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

                foreach (Face oF in extrudeFeature.SideFaces)
                {
                    Point wPoint = oF.PointOnFace;
                    if (wPoint.X == point[6].X)
                    {
                        Point sketchPoint = oTransGeo.CreatePoint(point[6].X, 0, 0);
                        oSketch = oCompDef.Sketches.AddWithOrientation(oF, oF.Edges[1], true, true, oF.Edges[1].StartVertex);

                        Point2d[] wP = new Point2d[3];
                        SketchLine[] wL = new SketchLine[3];

                        wP[0] = oTransGeo.CreatePoint2d(0, 0);
                        wP[1] = oTransGeo.CreatePoint2d(3.2, cathe);
                        wP[2] = oTransGeo.CreatePoint2d(3.2, 0);

                        wL[0] = oSketch.SketchLines.AddByTwoPoints(wP[0], wP[1]);
                        wL[1] = oSketch.SketchLines.AddByTwoPoints(wL[0].EndSketchPoint, wP[2]);
                        wL[2] = oSketch.SketchLines.AddByTwoPoints(wL[1].EndSketchPoint, wL[0].StartSketchPoint);

                        oProfile = oSketch.Profiles.AddForSolid();
                        oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
                        oExtrudeDef.SetDistanceExtent(oLine[7].Length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                        oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
                    }
                }
            }
            else if (basicAngle < 0)
            {
                ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];

                foreach (Face oF in extrudeFeature.SideFaces)
                {
                    Point wPoint = oF.PointOnFace;
                    if (wPoint.X == point[6].X)
                    {
                        Point sketchPoint = oTransGeo.CreatePoint(point[6].X, 0, 0);
                        oSketch = oCompDef.Sketches.AddWithOrientation(oF, oF.Edges[1], true, true, oF.Edges[1].StartVertex);

                        Point2d[] wP = new Point2d[3];
                        SketchLine[] wL = new SketchLine[3];

                        wP[0] = oTransGeo.CreatePoint2d(0, 0);
                        wP[1] = oTransGeo.CreatePoint2d(0, cathe);
                        wP[2] = oTransGeo.CreatePoint2d(3.2, 0);

                        wL[0] = oSketch.SketchLines.AddByTwoPoints(wP[0], wP[1]);
                        wL[1] = oSketch.SketchLines.AddByTwoPoints(wL[0].EndSketchPoint, wP[2]);
                        wL[2] = oSketch.SketchLines.AddByTwoPoints(wL[1].EndSketchPoint, wL[0].StartSketchPoint);

                        oProfile = oSketch.Profiles.AddForSolid();
                        oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
                        oExtrudeDef.SetDistanceExtent(oLine[7].Length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                        oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
                    }
                }
            }
        }


        private void createBoltHoles()
        {
            foreach (Face oF in oExtrude.SideFaces)
            {
                Box2d box = oF.Evaluator.ParamRangeRect;

                if (box.MinPoint.X == point[0].X)
                    if (box.MinPoint.Y == point[0].Y)
                        if (box.MaxPoint.X == 4.8)
                            if (box.MaxPoint.Y == heightPillar)
                                oFrontFace = oF;
            }

            for (int i = 1; i <= distanceToHoles.Count; i++)
            {
                oSketch = oCompDef.Sketches.AddWithOrientation(oFrontFace, oCompDef.WorkAxes[1], true, true, oCompDef.WorkPoints[1]);
                Point2d pointCenter = oTransGeo.CreatePoint2d(2.4, 0 - distanceToHoles[i]);
                SketchCircle circle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, holeDiametr / 2);
                oProfile = oSketch.Profiles.AddForSolid();
                foreach (ProfilePath oProfPath in oProfile)
                {
                    if (oProfPath[1].SketchEntity.Equals(circle))
                    {
                        oProfPath.AddsMaterial = true;
                    }
                    else
                        oProfPath.Delete();
                }
                oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
                oExtrudeDef.SetDistanceExtent(0.5, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
            }
        }
    }
}
