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
            ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];

            Point oPointOne = oTransGeo.CreatePoint(0, 0, -5);
            Point oPointTwo = oTransGeo.CreatePoint(4.8, 0, -5);
            WorkPoint oWorkPointOne = oCompDef.WorkPoints.AddFixed(oPointOne, true);
            WorkPoint oWorkPointTwo = oCompDef.WorkPoints.AddFixed(oPointTwo, true);
            WorkAxis oWorkAxisCrossbar = oCompDef.WorkAxes.AddByTwoPoints(oWorkPointOne, oWorkPointTwo, true);
            WorkPlane oWorkPlaneCrossbar = oCompDef.WorkPlanes.AddByLinePlaneAndAngle(oWorkAxisCrossbar, oCompDef.WorkPlanes["Плоскость XZ"], BasicAngle, true);

            oSketch = oCompDef.Sketches.AddWithOrientation(oWorkPlaneCrossbar, oWorkAxisCrossbar, true , false, oWorkPointOne);
        }
    }
}
