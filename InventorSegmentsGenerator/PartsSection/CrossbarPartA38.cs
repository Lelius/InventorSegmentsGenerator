using System;
using System.Collections.Generic;
using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, создающий деталь Перекладина (профиль А38).
    /// </summary>
    class CrossbarPartA38 : ProfileA38
    {
        private double lengthCrossbar;
        private double basicAngle;
        /// <summary>
        /// Основной угол наклона секции.
        /// </summary>
        public double BasicAngle
        {
            get { return basicAngle; }
            set { basicAngle = value; }
        }
        /// <summary>
        /// Список расстояний от боковой (левой) стойки до отверстий для прутков-труб (см).
        /// </summary>
        public List<double> distanceToHoles;
        /// <summary>
        /// Диаметр отверстия для вертикального прутка-трубы (см).
        /// </summary>
        public double holeDiametr;

        protected Point2d[] oPointsNear;
        protected SketchLine[] oLinesNear;
        protected PlanarSketch oSketchSpikesNear;

        protected Point2d[] oPointsFar;
        protected SketchLine[] oLinesFar;
        protected PlanarSketch oSketchSpikesFar;

        protected PlanarSketch oSketchHoles;

        /// <summary>
        /// Класс, создающий деталь Перекладина (профиль А38).
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application.</param>
        /// <param name="lengthCrossbar">Длина детали Перекладина (см).</param>
        public CrossbarPartA38(Inventor.Application invApp, double lengthCrossbar) :
            base(invApp, lengthCrossbar + 10, "Стеклопластик", "Оранжевый")                    // + 100 мм на случай наклонной перекладины
        {
            this.lengthCrossbar = lengthCrossbar;
            BasicAngle = 0;
            distanceToHoles = new List<double>();
            holeDiametr = 1.55;
        }


        /// <summary>
        /// Создает деталь Перекладина.
        /// </summary>
        public void createCrossbarPart()
        {
            createProfile();
            createSpikes();
            createHoles();
        }


        /// <summary>
        /// Создает отверстия для вертикальных прутков-труб.
        /// </summary>
        private void createHoles()
        {
            WorkPoint oWorkDeltaOriginPoint = null;

            foreach (Face oF in oCompDef.Features.ExtrudeFeatures[2].Faces)
            {
                foreach (Vertex oVer in oF.Vertices)
                {
                    if (oVer.Point.X == 0 & oVer.Point.Y == 0)
                    {
                        oWorkDeltaOriginPoint = oCompDef.WorkPoints.AddFixed(oVer.Point, true);
                    }
                }
            }

            if (oWorkDeltaOriginPoint == null)
                return;
            if (distanceToHoles.Count < 1)
                return;

            oSketchHoles = oCompDef.Sketches.AddWithOrientation(oCompDef.WorkPlanes["Плоскость XZ"], oCompDef.WorkAxes["Ось Z"], true, true, oWorkDeltaOriginPoint, false);
            foreach (double dist in distanceToHoles)
            {
                Point2d oPoint = oTransGeo.CreatePoint2d((-dist), 1.9);
                SketchCircle oCircle = oSketchHoles.SketchCircles.AddByCenterRadius(oPoint, holeDiametr / 2.0);
            }
            oProfile = oSketchHoles.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
            oExtrudeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
        }

        /// <summary>
        /// Создает соединительные шипы на концах профиля.
        /// </summary>
        private void createSpikes()
        {
            Point oPointOne = oTransGeo.CreatePoint(0, 0, -5);
            Point oPointTwo = oTransGeo.CreatePoint(3.8, 0, -5);
            WorkPoint oWorkPointOne = oCompDef.WorkPoints.AddFixed(oPointOne, true);
            WorkPoint oWorkPointTwo = oCompDef.WorkPoints.AddFixed(oPointTwo, true);
            WorkAxis oWorkAxisCrossbar = oCompDef.WorkAxes.AddByTwoPoints(oWorkPointOne, oWorkPointTwo, true);
            WorkPlane oWorkPlaneCrossbar = oCompDef.WorkPlanes.AddByLinePlaneAndAngle(oWorkAxisCrossbar, oCompDef.WorkPlanes["Плоскость XZ"], "-" + BasicAngle.ToString() + " deg", true);

            oSketchSpikesNear = oCompDef.Sketches.AddWithOrientation(oWorkPlaneCrossbar, oWorkAxisCrossbar, true, true, oWorkPointOne);

            oPointsNear = new Point2d[10];
            oPointsNear[0] = oTransGeo.CreatePoint2d(0, -10);
            oPointsNear[1] = oTransGeo.CreatePoint2d(3.8, -10);
            oPointsNear[2] = oTransGeo.CreatePoint2d(3.8, 2.1);
            oPointsNear[3] = oTransGeo.CreatePoint2d(3.4, 2.1);
            oPointsNear[4] = oTransGeo.CreatePoint2d(3.4, 0.5);
            oPointsNear[5] = oTransGeo.CreatePoint2d(2.9, 0);
            oPointsNear[6] = oTransGeo.CreatePoint2d(0.9, 0);
            oPointsNear[7] = oTransGeo.CreatePoint2d(0.4, 0.5);
            oPointsNear[8] = oTransGeo.CreatePoint2d(0.4, 2.1);
            oPointsNear[9] = oTransGeo.CreatePoint2d(0, 2.1);

            oLinesNear = new SketchLine[10];
            oLinesNear[0] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oPointsNear[0], oPointsNear[1]);
            oLinesNear[1] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[0].EndSketchPoint, oPointsNear[2]);
            oLinesNear[2] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[1].EndSketchPoint, oPointsNear[3]);
            oLinesNear[2] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[1].EndSketchPoint, oPointsNear[3]);
            oLinesNear[3] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[2].EndSketchPoint, oPointsNear[4]);
            oLinesNear[4] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[3].EndSketchPoint, oPointsNear[5]);
            oLinesNear[5] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[4].EndSketchPoint, oPointsNear[6]);
            oLinesNear[6] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[5].EndSketchPoint, oPointsNear[7]);
            oLinesNear[7] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[6].EndSketchPoint, oPointsNear[8]);
            oLinesNear[8] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[7].EndSketchPoint, oPointsNear[9]);
            oLinesNear[9] = oSketchSpikesNear.SketchLines.AddByTwoPoints(oLinesNear[8].EndSketchPoint, oLinesNear[0].StartSketchPoint);

            oProfile = oSketchSpikesNear.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
            oExtrudeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            oSketchSpikesFar = oCompDef.Sketches.AddWithOrientation(oWorkPlaneCrossbar, oWorkAxisCrossbar, true, true, oWorkPointOne);

            oPointsFar = new Point2d[10];
            double lengthCrossbarProection = lengthCrossbar * Math.Cos(BasicAngle * Math.PI / 180);
            oPointsFar[0] = oTransGeo.CreatePoint2d(0, lengthCrossbar + 10);
            oPointsFar[1] = oTransGeo.CreatePoint2d(3.8, lengthCrossbar + 10);
            oPointsFar[2] = oTransGeo.CreatePoint2d(3.8, lengthCrossbarProection - 2.1);
            oPointsFar[3] = oTransGeo.CreatePoint2d(3.4, lengthCrossbarProection - 2.1);
            oPointsFar[4] = oTransGeo.CreatePoint2d(3.4, lengthCrossbarProection - 0.5);
            oPointsFar[5] = oTransGeo.CreatePoint2d(2.9, lengthCrossbarProection);
            oPointsFar[6] = oTransGeo.CreatePoint2d(0.9, lengthCrossbarProection);
            oPointsFar[7] = oTransGeo.CreatePoint2d(0.4, lengthCrossbarProection - 0.5);
            oPointsFar[8] = oTransGeo.CreatePoint2d(0.4, lengthCrossbarProection - 2.1);
            oPointsFar[9] = oTransGeo.CreatePoint2d(0, lengthCrossbarProection - 2.1);

            oLinesFar = new SketchLine[10];
            oLinesFar[0] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oPointsFar[0], oPointsFar[1]);
            oLinesFar[1] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[0].EndSketchPoint, oPointsFar[2]);
            oLinesFar[2] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[1].EndSketchPoint, oPointsFar[3]);
            oLinesFar[2] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[1].EndSketchPoint, oPointsFar[3]);
            oLinesFar[3] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[2].EndSketchPoint, oPointsFar[4]);
            oLinesFar[4] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[3].EndSketchPoint, oPointsFar[5]);
            oLinesFar[5] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[4].EndSketchPoint, oPointsFar[6]);
            oLinesFar[6] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[5].EndSketchPoint, oPointsFar[7]);
            oLinesFar[7] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[6].EndSketchPoint, oPointsFar[8]);
            oLinesFar[8] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[7].EndSketchPoint, oPointsFar[9]);
            oLinesFar[9] = oSketchSpikesFar.SketchLines.AddByTwoPoints(oLinesFar[8].EndSketchPoint, oLinesFar[0].StartSketchPoint);

            oSketchSpikesFar.AddByProjectingEntity(oLinesNear[3]);

            oProfile = oSketchSpikesFar.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kCutOperation);
            oExtrudeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);
        }
    }
}
