using Inventor;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, создающий профиль Швеллер А30
    /// </summary>
    public class ProfileA30 : ProfileFrameSection
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

        /// <summary>
        /// Класс, создающий профиль Швеллер А38
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application.</param>
        /// <param name="lengthProfile">Длина создаваемого профиля (см).</param>
        /// <param name="materialProfile">Материал создаваемого профиля.</param>
        /// <param name="colorProfile">Цвет создаваемого профиля.</param>
        public ProfileA30(Inventor.Application invApp, double lengthProfile, string materialProfile = "", string colorProfile = "")
            : base(invApp)
        {
            this.lengthProfile = lengthProfile;
            this.materialProfile = materialProfile;
            this.colorProfile = colorProfile;
        }

        /// <summary>
        /// Создание профиля швеллера.
        /// </summary>
        public void createProfile()
        {
            oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            point = new Point2d[8];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 2.1);
            point[2] = oTransGeo.CreatePoint2d(0.3, 2.1);
            point[3] = oTransGeo.CreatePoint2d(0.3, 0.3);
            point[4] = oTransGeo.CreatePoint2d(2.7, 0.3);
            point[5] = oTransGeo.CreatePoint2d(2.7, 2.1);
            point[6] = oTransGeo.CreatePoint2d(3.0, 2.1);
            point[7] = oTransGeo.CreatePoint2d(3.0, 0);

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
            oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.2);
            oEdges = oInvApp.TransientObjects.CreateEdgeCollection();
            for (int i = 1; i <= oLine.Length; i++)
            {
                foreach (Edge oEdge in (oExtrude.SideFaces[i].Edges))
                {
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[0]))
                    {
                        oEdges.Add(oEdge);
                    }
                    if (isEdgeAndPoint2dOnStraight(oEdge, point[7]))
                    {
                        oEdges.Add(oEdge);
                    }
                }
            }
            oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.3);


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
