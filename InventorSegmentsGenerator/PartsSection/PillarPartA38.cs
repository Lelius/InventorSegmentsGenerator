using System;
using System.Collections.Generic;
using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, создающий деталь Стойка (швеллер А38).
    /// </summary>
    public class PillarPartA38 : ProfileA38
    {
        /// <summary>
        /// Высота боковой грани профиля А38.
        /// </summary>
        const double HeightProfile = 2.5;

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
        /// Высота Стойки (см).
        /// </summary>
        public double heightPillar;
        /// <summary>
        /// Количество отверстий под крепежные болты.
        /// </summary>
        public int numberOfHoles;
        /// <summary>
        /// Коллекция расстояний между отверстиями для крепежных болтов, начиная от верхней кромки Стойки,
        /// в формате <номер, расстояние (см)>.
        /// </summary>
        public Dictionary<int, double> distanceToHoles;
        public TypeOfFasteningEnum typeOfFastening;
        /// <summary>
        /// Диаметр отверстия под крепежный болт (см).
        /// </summary>
        public double holeBoltDiametr;
        /// <summary>
        /// Диаметр отверстия под арматуру при бетонировании нижнего конца Стойки в грунт (см).
        /// </summary>
        public double holeGroundDiametr;

        protected Face oFrontFace;

        /// <summary>
        /// Класс, создающий деталь Стойка (швеллер А38).
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application</param>
        public PillarPartA38(Inventor.Application invApp) : base(invApp, 120, "Стеклопластик", "Оранжевый")
        {
            heightPillar = 120;
            numberOfHoles = 3;
            distanceToHoles = new Dictionary<int, double> { { 1, 3.6 }, { 2, 45 }, { 3, 95 } };
            typeOfFastening = TypeOfFasteningEnum.Boot;
            holeBoltDiametr = 0.9;
            holeGroundDiametr = 0.52;
            basicAngle = 0;
        }

        /// <summary>
        /// Класс, создающий деталь Стойка (швеллер А38).
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application.</param>
        /// <param name="heightPillar">Высота Стойки (см).</param>
        /// <param name="materialProfile">Материал Стойки.</param>
        /// <param name="colorProfile">Цвет Стойки.</param>
        /// <param name="typeOfFastening">Вариант нижнего окончания Стойки.</param>
        public PillarPartA38(Inventor.Application invApp, double heightPillar, string materialProfile, string colorProfile, TypeOfFasteningEnum typeOfFastening) :
            base(invApp, heightPillar, materialProfile, colorProfile)

        {
            this.heightPillar = heightPillar;
            numberOfHoles = 3;
            distanceToHoles = new Dictionary<int, double> { { 1, 3.6 }, { 2, 45 }, { 3, 95 } };
            this.typeOfFastening = typeOfFastening;
            holeBoltDiametr = 0.9;
            holeGroundDiametr = 0.52;
            basicAngle = 0;
        }

        /// <summary>
        /// Создание детали Стойка.
        /// </summary>
        public void createPillarPart()
        {
            createProfile();
            createAngleCut();
            createBoltHoles();
            createGroundHoles();
        }


        /// <summary>
        /// Обработка нижнего конца Стойки 
        /// в соответствии с выбранным вариантом установки секции TypeOfFasteningEnum.
        /// </summary>
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

        /// <summary>
        /// Обрезка верхнего конца Стойки в соответствии со значением BasicAngle.
        /// </summary>
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
                        wP[1] = oTransGeo.CreatePoint2d(2.5, getCathe(BasicAngle));
                        wP[2] = oTransGeo.CreatePoint2d(2.5, 0);

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
                        wP[2] = oTransGeo.CreatePoint2d(2.5, 0);

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

        /// <summary>
        /// Создание отверстий в Стойке под крепежные болты.
        /// </summary>
        private void createBoltHoles()
        {
            ExtrudeFeature extrudeFeature = oCompDef.Features.ExtrudeFeatures["Выдавливание1"];

            foreach (Face oF in extrudeFeature.SideFaces)
            {
                Point wPoint = oF.PointOnFace;
                if (wPoint.Y == 0.0)
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


        /// <summary>
        /// Вычисление длины противолежащего катета по гипотенузе (высоте полки профиля) и прилежащему углу.
        /// </summary>
        /// <param name="angle">Значение прилежащего угла (град).</param>
        /// <returns>Длина противолежащего катета (см).</returns>
        static private protected double getCathe(double angle)
        {
            if (angle == 0)
                return 0;
            return (double)Math.Abs(HeightProfile * Math.Tan(angle * Math.PI / 180));
        }
    }
}
