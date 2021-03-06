using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, создающий профиль Прямоугольник пустотелый Ж38х39.
    /// </summary>
    public class ProfileG38x39 : ProfileFrameSection
    {
        public double lengthProfile;
        public string materialProfile = "";
        public string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d[] point;
        private Point2d pointCenter;
        private SketchLine[] oLine;
        private SketchCircle oCircle;
        protected Profile oProfile;
        protected ExtrudeDefinition oExtrudeDef;
        protected ExtrudeFeature oExtrude;
        private EdgeCollection oEdges;
        private FilletFeature oFillet;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        /// <summary>
        /// Класс, создающий профиль Прямоугольник пустотелый Ж38х39.
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application.</param>
        /// <param name="lengthProfile">Длина создаваемого профиля (см).</param>
        /// <param name="materialProfile">Материал создаваемого профиля.</param>
        /// <param name="colorProfile">Цвет создаваемого профиля.</param>
        public ProfileG38x39(Inventor.Application invApp, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(invApp)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        /// <summary>
        /// Создание профиля прямоугольника пустотелого.
        /// </summary>
        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            point = new Point2d[4];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 3.8);
            point[2] = oTransGeo.CreatePoint2d(3.9, 3.8);
            point[3] = oTransGeo.CreatePoint2d(3.9, 0);
            pointCenter = oTransGeo.CreatePoint2d(1.95, 1.9);

            oLine = new SketchLine[4];
            oLine[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLine[1] = oSketch.SketchLines.AddByTwoPoints(oLine[0].EndSketchPoint, point[2]);
            oLine[2] = oSketch.SketchLines.AddByTwoPoints(oLine[1].EndSketchPoint, point[3]);
            oLine[3] = oSketch.SketchLines.AddByTwoPoints(oLine[2].EndSketchPoint, oLine[0].StartSketchPoint);
            oCircle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, 1.125);

            oProfile = oSketch.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);

            oEdges = oInvApp.TransientObjects.CreateEdgeCollection();
            for (int i = 1; i <= oLine.Length; i++)
            {
                foreach (Edge oEdge in (oExtrude.SideFaces[i].Edges))
                {
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[0]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[1]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[2]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[3]))
                    {
                        oEdges.Add(oEdge);
                    }
                }
            }
            oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.2);

            oMaterial = oPartDoc.ComponentDefinition.Material;
            foreach (Material mat in oPartDoc.Materials)
            {
                if (mat.Name == materialProfile)
                {
                    oMaterial = mat;
                }
            }
            oPartDoc.ComponentDefinition.Material = oMaterial;

            oRenderStyle = oPartDoc.ComponentDefinition.Material.RenderStyle;
            foreach (RenderStyle style in oPartDoc.RenderStyles)
            {
                if (style.Name == colorProfile)
                {
                    oRenderStyle = style;
                }
            }
            oPartDoc.ComponentDefinition.Material.RenderStyle = oRenderStyle;
        }
    }
}
