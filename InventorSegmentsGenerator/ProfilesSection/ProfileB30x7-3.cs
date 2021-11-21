// TODO: объединить в один класс трубного профиля

using Inventor;

namespace InventorSegmentsGenerator
{
    class ProfileB30x7_3 : ProfileFrameSection
    {
        private double lengthProfile;
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

        public ProfileB30x7_3(Inventor.Application m_inventorApplication, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(m_inventorApplication)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            pointCenter = oTransGeo.CreatePoint2d(0, 0);
            oCircleOutside = oSketch.SketchCircles.AddByCenterRadius(pointCenter, 1.5);
            oCircleInside = oSketch.SketchCircles.AddByCenterRadius(pointCenter, 0.77);

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
