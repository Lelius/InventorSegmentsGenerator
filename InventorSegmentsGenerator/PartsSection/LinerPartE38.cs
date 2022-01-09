﻿using System;
using Inventor;

namespace InventorSegmentsGenerator
{
    public class LinerPartE38 : ProfileE38
    {
        private double lengthLiner;
        private double basicAngle;
        public double BasicAngle
        {
            get { return basicAngle; }
            set { basicAngle = value; }
        }

        protected Point2d[] points;
        protected SketchLine[] sketchLines;
        protected PlanarSketch oSketchStartStopSides;
        protected Profile oProfileLiner;

        public LinerPartE38(Inventor.Application m_inventorApplication, double lengthLiner, string materialProfile = "", string colorProfile = "") :
            base(m_inventorApplication, lengthLiner + 10, materialProfile, colorProfile)
        {
            this.lengthLiner = lengthLiner;
            BasicAngle = 0;
        }


        public void createLiner()
        {
            createProfile();
            createStartStopSides();
        }


        private void createStartStopSides()
        {
            oSketchStartStopSides = oCompDef.Sketches.AddWithOrientation(oCompDef.WorkPlanes["Плоскость YZ"], oCompDef.WorkAxes["Ось Z"], false, true, oCompDef.WorkPoints[1], false);

            double delta = 2.6 * Math.Tan(BasicAngle * Math.PI / 180);
            points = new Point2d[4];
            points[0] = oTransGeo.CreatePoint2d(5, 0);
            points[1] = oTransGeo.CreatePoint2d((5 + delta), 2.6);
            points[2] = oTransGeo.CreatePoint2d((5 + lengthLiner), 2.6);
            points[3] = oTransGeo.CreatePoint2d((5 + lengthLiner), 0);

            sketchLines = new SketchLine[4];
            sketchLines[0] = oSketchStartStopSides.SketchLines.AddByTwoPoints(points[0], points[1]);
            sketchLines[1] = oSketchStartStopSides.SketchLines.AddByTwoPoints(sketchLines[0].EndSketchPoint, points[2]);
            sketchLines[2] = oSketchStartStopSides.SketchLines.AddByTwoPoints(sketchLines[1].EndSketchPoint, points[3]);
            sketchLines[3] = oSketchStartStopSides.SketchLines.AddByTwoPoints(sketchLines[2].EndSketchPoint, sketchLines[0].StartSketchPoint);

            oProfileLiner = oSketchStartStopSides.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfileLiner, PartFeatureOperationEnum.kIntersectOperation);
            oExtrudeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
        }
    }
}
