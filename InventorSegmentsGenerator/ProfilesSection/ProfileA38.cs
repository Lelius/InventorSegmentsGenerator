using Inventor;
using System;

namespace InventorSegmentsGenerator
{
    ///<summary>
    ///Класс, создающий деталь Швеллер А38
    ///</summary>
    ///<param name = "m_inventorApplication" > Это главный Inventor.Application объект</param>
    ///<param name = "lengthProfile" > Длина формируемой детали (в см)</param>
    ///<param name = "materialProfile" > Материал детали (необязательно)</param>
    ///<param name = "colorProfile" > Цвет детали (необязательно)</param>
    ///<returns></returns>
    ///<remarks></remarks>
    public class ProfileA38 : ProfileFrameSection
    {
        private double lengthProfile;
        private string materialProfile = "";
        private string colorProfile = "";
        private protected PlanarSketch oSketch;
        private protected Point2d[] point;
        private protected SketchLine[] oLine;
        private protected Profile oProfile;
        private protected ExtrudeDefinition oExtrudeDef;
        private protected ExtrudeFeature oExtrude;
        private EdgeCollection oEdges;
        private FilletFeature oFillet;
        private Material oMaterial;
        private RenderStyle oRenderStyle;

        public ProfileA38(Inventor.Application m_inventorApplication, double lengthProfile, string materialProfile = "", string colorProfile = "")
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
            point[1] = oTransGeo.CreatePoint2d(0, 2.5);
            point[2] = oTransGeo.CreatePoint2d(0.4, 2.5);
            point[3] = oTransGeo.CreatePoint2d(0.4, 0.4);
            point[4] = oTransGeo.CreatePoint2d(3.4, 0.4);
            point[5] = oTransGeo.CreatePoint2d(3.4, 2.5);
            point[6] = oTransGeo.CreatePoint2d(3.8, 2.5);
            point[7] = oTransGeo.CreatePoint2d(3.8, 0);

            oLine = new SketchLine[8];
            oLine[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLine[1] = oSketch.SketchLines.AddByTwoPoints(oLine[0].EndSketchPoint, point[2]);
            oLine[2] = oSketch.SketchLines.AddByTwoPoints(oLine[1].EndSketchPoint, point[3]);
            oLine[3] = oSketch.SketchLines.AddByTwoPoints(oLine[2].EndSketchPoint, point[4]);
            oLine[4] = oSketch.SketchLines.AddByTwoPoints(oLine[3].EndSketchPoint, point[5]);
            oLine[5] = oSketch.SketchLines.AddByTwoPoints(oLine[4].EndSketchPoint, point[6]);
            oLine[6] = oSketch.SketchLines.AddByTwoPoints(oLine[5].EndSketchPoint, point[7]);
            oLine[7] = oSketch.SketchLines.AddByTwoPoints(oLine[6].EndSketchPoint, oLine[0].StartSketchPoint);

            oProfile = oSketch.Profiles.AddForSolid();
            oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            oEdges = oInvApp.TransientObjects.CreateEdgeCollection();
            for (int i = 1; i <= oLine.Length; i++)
            {
                foreach (Edge oEdge in (oExtrude.SideFaces[i].Edges))
                {
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[2]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[3]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[4]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[5]))
                    {
                        oEdges.Add(oEdge);
                    }
                }
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
