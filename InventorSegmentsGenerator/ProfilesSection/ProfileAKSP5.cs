using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, создающий профиль (приближенный) арматуры АКСП диаметром 5 мм.
    /// </summary>
    public class ProfileAKSP5 : ProfileFrameSection
    {
        private double lengthProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d pointCenter;
        private SketchCircle oCircle;
        private Profile oProfile;
        private ExtrudeDefinition oExtrudeDef;
        private ExtrudeFeature oExtrude;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        /// <summary>
        /// Класс, создающий профиль (приближенный) арматуры АКСП диаметром 5 мм.
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application.</param>
        /// <param name="lengthProfile">Длина создаваемого профиля (см).</param>
        /// <param name="materialProfile">Материал создаваемого профиля.</param>
        /// <param name="colorProfile">Цвет создаваемого профиля.</param>
        public ProfileAKSP5(Inventor.Application invApp, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(invApp)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        /// <summary>
        /// Создание профиля (приближенного) арматуры.
        /// </summary>
        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            pointCenter = oTransGeo.CreatePoint2d(0, 0);
            oCircle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, 0.25);

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
