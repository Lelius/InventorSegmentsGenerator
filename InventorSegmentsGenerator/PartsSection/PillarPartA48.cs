using System;
using System.Collections.Generic;
using Inventor;
using System.Windows.Forms;

namespace InventorSegmentsGenerator
{
    public class PillarPartA48 : ProfileA48
    {
        const double HeightProfile = 3.2;

        private double basicAngle;
        public double BasicAngle
        {
            get { return basicAngle; }
            set { basicAngle = value; }
        }

        public double heightPillar;
        public int numberOfHoles;
        public Dictionary<int, double> distanceToHoles;
        public TypeOfFasteningEnum typeOfFastening;
        public double holeBoltDiametr;
        public double holeGroundDiametr;

        protected Face oFrontFace;

        public PillarPartA48(Inventor.Application invApp) : base(invApp, 120, "Стеклопластик", "Оранжевый")
        {
            heightPillar = 120;
            numberOfHoles = 3;
            distanceToHoles = new Dictionary<int, double> { { 1, 3.6}, { 2, 45}, { 3, 95} };
            typeOfFastening = TypeOfFasteningEnum.Boot;
            holeBoltDiametr = 0.9;
            holeGroundDiametr = 0.52;
            basicAngle = 0;
        }

        public PillarPartA48(Inventor.Application invApp, double heightPillar, string materialProfile, string colorProfile, TypeOfFasteningEnum typeOfFastening) :
            base (invApp, heightPillar, materialProfile, colorProfile)

        {
            this.heightPillar = heightPillar;
            numberOfHoles = 3;
            distanceToHoles = new Dictionary<int, double> { { 1, 3.6 }, { 2, 45 }, { 3, 95 } };
            this.typeOfFastening = typeOfFastening;
            holeBoltDiametr = 0.9;
            holeGroundDiametr = 0.52;
            basicAngle = 0;
        }

        public void createPillarPart()
        {
            createProfile();
            createAngleCut();
            createBoltHoles();
            createGroundHoles();
        }


        private void createGroundHoles()
        {
            switch (typeOfFastening)
            {
                case TypeOfFasteningEnum.Boot:
                    break;
                case TypeOfFasteningEnum.Ground:
                    {
                        ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];
                        foreach (Face oF in extrudeFeature.SideFaces)
                        {
                            Point wPoint = oF.PointOnFace;
                            if (wPoint.Y == point[7].Y & oF.SurfaceType == SurfaceTypeEnum.kPlaneSurface)
                            {
                                foreach (Edge oEdge in oF.Edges)
                                {
                                    if (oEdge.PointOnEdge.X == 0 & oEdge.PointOnEdge.Y == 0)
                                    {
                                        oSketch = oCompDef.Sketches.AddWithOrientation(oF, oCompDef.WorkAxes[3], true, false, oEdge.StopVertex);
                                        Point2d pointCenter = oTransGeo.CreatePoint2d((oLine[7].Length / 2), 10);
                                        SketchCircle circle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, holeGroundDiametr / 2);

                                        oProfile = oSketch.Profiles.AddForSolid();
                                        oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
                                        oExtrudeDef.SetDistanceExtent(oLine[0].Length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                                        oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
                                    }
                                }
                            }
                            if (wPoint.X == point[1].X & oF.SurfaceType == SurfaceTypeEnum.kPlaneSurface)
                            {
                                foreach (Edge oEdge in oF.Edges)
                                {
                                    if (oEdge.PointOnEdge.X == 0 & oEdge.PointOnEdge.Y == 0)
                                    {
                                        oSketch = oCompDef.Sketches.AddWithOrientation(oF, oCompDef.WorkAxes[3], false, false, oEdge.StopVertex);
                                        Point2d pointCenter = oTransGeo.CreatePoint2d((oLine[0].Length / 2), -5);
                                        SketchCircle circle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, holeGroundDiametr / 2);

                                        oProfile = oSketch.Profiles.AddForSolid();
                                        oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
                                        oExtrudeDef.SetDistanceExtent(oLine[7].Length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                                        oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case TypeOfFasteningEnum.Rack:
                    break;
                default:
                    return;
            }
        }

        private void createAngleCut()
        {
            if (Math.Abs(BasicAngle) < 0.0000001)
            {
                return;
            }
            else if (BasicAngle > 0)
            {
                ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];
                extrudeFeature.Definition.SetDistanceExtent(heightPillar + getCathe(BasicAngle), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

                foreach (Face oF in extrudeFeature.SideFaces)
                {
                    Point wPoint = oF.PointOnFace;
                    if (wPoint.X == point[6].X)
                    {
                        oSketch = oCompDef.Sketches.AddWithOrientation(oF, oF.Edges[1], true, true, oF.Edges[1].StartVertex);

                        Point2d[] wP = new Point2d[3];
                        SketchLine[] wL = new SketchLine[3];

                        wP[0] = oTransGeo.CreatePoint2d(0, 0);
                        wP[1] = oTransGeo.CreatePoint2d(3.2, getCathe(BasicAngle));
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
            else if (BasicAngle < 0)
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
                        wP[1] = oTransGeo.CreatePoint2d(0, getCathe(BasicAngle));
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
            ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];

            foreach (Face oF in extrudeFeature.SideFaces)
            {
                Point wPoint = oF.PointOnFace;
                if(wPoint.Y == 0.0)
                {
                    oFrontFace = oF;
                }
            }

            WorkPoint workPoint = oCompDef.WorkPoints[1];

            for (int i = 1; i <= distanceToHoles.Count; i++)
            {
                oSketch = oCompDef.Sketches.AddWithOrientation(oFrontFace, oCompDef.WorkAxes[1], true, true, oCompDef.WorkPoints[1]);
                Point2d pointCenter = oTransGeo.CreatePoint2d(oLine[7].Length / 2, 0 - distanceToHoles[i] - getCathe(BasicAngle));
                SketchCircle circle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, holeBoltDiametr / 2);
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


        static private protected double getCathe(double angle)
        {
            if (angle == 0)
                return 0;
            return (double)Math.Abs(HeightProfile * Math.Tan(angle * Math.PI / 180));
        }
    }
}
