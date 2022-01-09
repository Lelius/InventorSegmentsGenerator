using System;
using Inventor;

namespace InventorSegmentsGenerator
{
    class ProfileE29 : ProfileFrameSection
    {
        private double lengthProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d[] point;
        private SketchLine[] oLine;
        private Profile oProfile;
        private ExtrudeDefinition oExtrudeDef;
        private ExtrudeFeature oExtrude;
        private EdgeCollection oEdges;
        private FilletFeature oFillet;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        public ProfileE29(Inventor.Application m_inventorApplication, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(m_inventorApplication)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);
            point = new Point2d[4];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 2.1);
            point[2] = oTransGeo.CreatePoint2d(2.9, 2.1);
            point[3] = oTransGeo.CreatePoint2d(2.9, 0);

            oLine = new SketchLine[4];
            oLine[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLine[1] = oSketch.SketchLines.AddByTwoPoints(oLine[0].EndSketchPoint, point[2]);
            oLine[2] = oSketch.SketchLines.AddByTwoPoints(oLine[1].EndSketchPoint, point[3]);
            oLine[3] = oSketch.SketchLines.AddByTwoPoints(oLine[2].EndSketchPoint, oLine[0].StartSketchPoint);

            oProfile = oSketch.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            oEdges = oInvApp.TransientObjects.CreateEdgeCollection();
            for (int i = 1; i <= oLine.Length; i++)
            {
                foreach (Edge oEdge in (oExtrude.SideFaces[i].Edges))
                {
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[1]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[2]))
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
