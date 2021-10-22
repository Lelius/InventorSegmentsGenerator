using Inventor;
using System;

namespace InventorSegmentsGenerator
{
    public class ProfileA48 : ProfileFrameSection
    {
        private double lengthProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d[] point;
        private SketchLine[] oLines;
        private Profile oProfile;
        private ExtrudeDefinition oExtrudeDef;
        private ExtrudeFeature oExtrude;
        private EdgeCollection oEdges;
        private FilletFeature oFillet;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        public ProfileA48(Inventor.Application m_inventorApplication, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(m_inventorApplication)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            point = new Point2d[8];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 3.2);
            point[2] = oTransGeo.CreatePoint2d(0.5, 3.2);
            point[3] = oTransGeo.CreatePoint2d(0.5, 0.5);
            point[4] = oTransGeo.CreatePoint2d(4.3, 0.5);
            point[5] = oTransGeo.CreatePoint2d(4.3, 3.2);
            point[6] = oTransGeo.CreatePoint2d(4.8, 3.2);
            point[7] = oTransGeo.CreatePoint2d(4.8, 0);

            oLines = new SketchLine[8];
            oLines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, point[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, point[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, point[4]);
            oLines[4] = oSketch.SketchLines.AddByTwoPoints(oLines[3].EndSketchPoint, point[5]);
            oLines[5] = oSketch.SketchLines.AddByTwoPoints(oLines[4].EndSketchPoint, point[6]);
            oLines[6] = oSketch.SketchLines.AddByTwoPoints(oLines[5].EndSketchPoint, point[7]);
            oLines[7] = oSketch.SketchLines.AddByTwoPoints(oLines[6].EndSketchPoint, oLines[0].StartSketchPoint);

            oProfile = oSketch.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);

            oEdges = oInvApp.TransientObjects.CreateEdgeCollection();
            foreach (Edge oEdge in (oExtrude.SideFaces[4].Edges))
            {
                if (Math.Abs(oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) - lengthProfile) < 0.0001)
                    oEdges.Add(oEdge);
            }
            foreach (Edge oEdge in (oExtrude.SideFaces[6].Edges))
            {
                if (Math.Abs(oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) - lengthProfile) < 0.0001)
                    oEdges.Add(oEdge);
            }
            oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.15);

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
