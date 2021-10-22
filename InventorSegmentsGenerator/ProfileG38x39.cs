using Inventor;
using System;

namespace InventorSegmentsGenerator
{
    public class ProfileG38x39 : ProfileFrameSection
    {
        private double lengthProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private PlanarSketch oSketch;
        private Point2d[] point;
        private Point2d pointCenter;
        private SketchLine[] oLines;
        private SketchCircle oCircle;
        private Profile oProfile;
        private ExtrudeDefinition oExtrudeDef;
        private ExtrudeFeature oExtrude;
        private EdgeCollection oEdges;
        private FilletFeature oFillet;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        public ProfileG38x39(Inventor.Application m_inventorApplication, double lengthProfile, string materialProfile = "", string colorProfile = "")
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
            point[1] = oTransGeo.CreatePoint2d(0, 3.8);
            point[2] = oTransGeo.CreatePoint2d(3.9, 3.8);
            point[3] = oTransGeo.CreatePoint2d(3.9, 0);
            pointCenter = oTransGeo.CreatePoint2d(1.95, 1.9);

            oLines = new SketchLine[4];
            oLines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, point[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, point[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, oLines[0].StartSketchPoint);
            oCircle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, 1.125);

            oProfile = oSketch.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);

            oEdges = oInvApp.TransientObjects.CreateEdgeCollection();

            foreach (Edge oEdge in (oExtrude.SideFaces[3].Edges))
            {
                if (Math.Abs(oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) - lengthProfile) < 0.0001)
                {
                    oEdges.Add(oEdge);
                }
            }
            foreach (Edge oEdge in (oExtrude.SideFaces[5].Edges))
            {
                if (Math.Abs(oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) - lengthProfile) < 0.0001)
                {
                    oEdges.Add(oEdge);
                }
            }
            if (oEdges.Count > 0)
            {
                oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.15);
            }

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
