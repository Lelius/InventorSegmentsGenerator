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
            createHole();
        }


        private void createHole()
        {
            oFrontFace = oExtrude.SideFaces[4];

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
                oExtrudeDef.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
            }
        }
    }
}
