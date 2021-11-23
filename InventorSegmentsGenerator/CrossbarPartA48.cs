using System;
using Inventor;
using System.Collections.Generic;

namespace InventorSegmentsGenerator
{
    class CrossbarPartA48 : ProfileA48
    {
        private double lengthCrossbar;
        private double basicAngle;
        public double BasicAngle
        {
            get { return basicAngle; }
            set { basicAngle = value; }
        }
        public Dictionary<int, double> distanceToHoles;
        public double holeDiametr;

        protected Point2d[] oPointsNear;
        protected SketchLine[] oLinesNear;
        protected PlanarSketch oSketchSpikes;

        public CrossbarPartA48(Inventor.Application InvApp, double lengthCrossbar) : 
            base (InvApp, lengthCrossbar + 10, "Стеклопластик", "Оранжевый")                    // + 100 мм на случай наклонной перекладины
        {
            this.lengthCrossbar = lengthCrossbar;
            BasicAngle = 0;
            distanceToHoles = null;
            holeDiametr = 2.25;
        }


        public void createCrossbarPart()
        {
            createProfile();
            BasicAngle = 25;
            createSpikes();
        }


        private void createSpikes()
        {
            Point oPointOne = oTransGeo.CreatePoint(0, 0, -5);
            Point oPointTwo = oTransGeo.CreatePoint(4.8, 0, -5);
            WorkPoint oWorkPointOne = oCompDef.WorkPoints.AddFixed(oPointOne, true);
            WorkPoint oWorkPointTwo = oCompDef.WorkPoints.AddFixed(oPointTwo, true);
            WorkAxis oWorkAxisCrossbar = oCompDef.WorkAxes.AddByTwoPoints(oWorkPointOne, oWorkPointTwo, true);
            WorkPlane oWorkPlaneCrossbar = oCompDef.WorkPlanes.AddByLinePlaneAndAngle(oWorkAxisCrossbar, oCompDef.WorkPlanes["Плоскость XZ"], BasicAngle, true);

            oSketchSpikes = oCompDef.Sketches.AddWithOrientation(oWorkPlaneCrossbar, oWorkAxisCrossbar, true, true, oWorkPointOne);

            oPointsNear = new Point2d[10];
            oPointsNear[0] = oTransGeo.CreatePoint2d(0, -10);
            oPointsNear[1] = oTransGeo.CreatePoint2d(4.8, -10);
            oPointsNear[2] = oTransGeo.CreatePoint2d(4.8, 2.7);
            oPointsNear[3] = oTransGeo.CreatePoint2d(4.3, 2.7);
            oPointsNear[4] = oTransGeo.CreatePoint2d(4.3, 0.5);
            oPointsNear[5] = oTransGeo.CreatePoint2d(3.8, 0);
            oPointsNear[6] = oTransGeo.CreatePoint2d(1, 0);
            oPointsNear[7] = oTransGeo.CreatePoint2d(0.5, 0.5);
            oPointsNear[8] = oTransGeo.CreatePoint2d(0.5, 2.7);
            oPointsNear[9] = oTransGeo.CreatePoint2d(0, 2.7);

            oLinesNear = new SketchLine[10];
            oLinesNear[0] = oSketchSpikes.SketchLines.AddByTwoPoints(oPointsNear[0], oPointsNear[1]);
            oLinesNear[1] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[0].EndSketchPoint, oPointsNear[2]);
            oLinesNear[2] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[1].EndSketchPoint, oPointsNear[3]);
            oLinesNear[2] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[1].EndSketchPoint, oPointsNear[3]);
            oLinesNear[3] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[2].EndSketchPoint, oPointsNear[4]);
            oLinesNear[4] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[3].EndSketchPoint, oPointsNear[5]);
            oLinesNear[5] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[4].EndSketchPoint, oPointsNear[6]);
            oLinesNear[6] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[5].EndSketchPoint, oPointsNear[7]);
            oLinesNear[7] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[6].EndSketchPoint, oPointsNear[8]);
            oLinesNear[8] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[7].EndSketchPoint, oPointsNear[9]);
            oLinesNear[9] = oSketchSpikes.SketchLines.AddByTwoPoints(oLinesNear[8].EndSketchPoint, oLinesNear[0].StartSketchPoint);

            oProfile = oSketchSpikes.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
            oExtrudeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
        }
    }
}
