using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, создающий профиль Прутка круглого В(Диаметр).
    /// </summary>
    public class ProfileV_D : ProfileFrameSection
    {
        private double diameterProfile;
        private double lengthProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d pointCenter;
        private SketchCircle oCircleOutside;
        private Profile oProfile;
        private ExtrudeDefinition oExtrudeDef;
        private ExtrudeFeature oExtrude;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        /// <summary>
        /// Класс, создающий профиль Прутка круглого В(Диаметр).
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application.</param>
        /// <param name="diameterProfile">Диаметр создаваемого профиля (см).</param>
        /// <param name="lengthProfile">Длина создаваемого профиля (см).</param>
        /// <param name="materialProfile">Материал создаваемого профиля.</param>
        /// <param name="colorProfile">Цвет создаваемого профиля.</param>
        public ProfileV_D(Inventor.Application invApp, double diameterProfile, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(invApp)
        {
            this.diameterProfile = diameterProfile;
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        /// <summary>
        /// Создание профиля прутка круглого.
        /// </summary>
        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            pointCenter = oTransGeo.CreatePoint2d(0, 0);
            oCircleOutside = oSketch.SketchCircles.AddByCenterRadius(pointCenter, (diameterProfile / 2));

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
