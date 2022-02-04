using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Создание профиля круглой трубы.
    /// </summary>
    class ProfileB_DxS : ProfileFrameSection
    {
        private double lengthProfile;
        private double diameterProfile;
        private double wallThicknessProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d pointCenter;
        private SketchCircle oCircleOutside;
        private SketchCircle oCircleInside;
        private Profile oProfile;
        private ExtrudeDefinition oExtrudeDef;
        private ExtrudeFeature oExtrude;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        /// <summary>
        /// Создание профиля круглой трубы.
        /// </summary>
        /// <param name="m_inventorApplication">Главный объект Inventor.Application</param>
        /// <param name="diameterProfile">Внешний диаметр трубы (см).</param>
        /// <param name="wallThicknessProfile">Толщина стенки трубы (см).</param>
        /// <param name="lengthProfile">Длина создаваемого профиля трубы (см).</param>
        /// <param name="materialProfile">Материал создаваемого профиля трубы.</param>
        /// <param name="colorProfile">Цвет создаваемого профиля трубы.</param>
        public ProfileB_DxS(Inventor.Application m_inventorApplication, double diameterProfile, double wallThicknessProfile, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(m_inventorApplication)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
            this.diameterProfile = diameterProfile;
            this.wallThicknessProfile = wallThicknessProfile;
        }

        /// <summary>
        /// Непосредственное создание профиля трубы.
        /// </summary>
        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            pointCenter = oTransGeo.CreatePoint2d(0, 0);
            oCircleOutside = oSketch.SketchCircles.AddByCenterRadius(pointCenter, (diameterProfile / 2));
            oCircleInside = oSketch.SketchCircles.AddByCenterRadius(pointCenter, ((diameterProfile / 2) - wallThicknessProfile));

            oProfile = oSketch.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);

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
