using System.Windows.Forms;
using Inventor;
using System;

namespace InventorSegmentsGenerator
{
    class CompositeSectionsGenenator
    {
        Inventor.Application m_inventorApplication;
        Inventor.ApplicationEvents m_applicationEvents;
        string m_ClientId = "{43457a9b-2327-48bc-9c57-be7407696fb3}";
        ButtonDefinition m_buttonSectionsGenerator;
        ButtonIcons m_buttonIcons;
        FormSectionsGenerator m_mainForm;

        public CompositeSectionsGenenator(Inventor.Application m_inventorApplication)
        {
            this.m_inventorApplication = m_inventorApplication;
            this.m_applicationEvents = m_inventorApplication.ApplicationEvents;
            m_buttonIcons = new ButtonIcons(InvAddIn.Properties.Resources.Секция_16х16, InvAddIn.Properties.Resources.Секция_32х32);
        }

        public void MainGenerator()
        {
            //Создание кнопки Ribbon "Генератор секций"
            createButtonOnRibbon();

            m_buttonSectionsGenerator.OnExecute += M_buttonSectionsGenerator_OnExecute;
        }

        private void M_buttonSectionsGenerator_OnExecute(NameValueMap Context)
        {
            //(Channel48(20, "Стеклопластик", "Оранжевый")).DisplayName = "Стойка";

            //(Channel38(20, "Стеклопластик", "Оранжевый")).DisplayName = "Стойка";

            //(ProfileG38x39(2.4, "Стеклопластик", "Оранжевый")).DisplayName = "Вкладыш";

            (ProfileE38(10, "Стеклопластик", "Оранжевый")).DisplayName = "Вкладыш";

            //m_mainForm = new FormSectionsGenerator();
            //m_mainForm.Show();
        }


        #region Деталь Е38
        private Document ProfileE38(double lengthProfile, string materialProfile = "", string colorProfile = "")
        {
            Document oDoc = m_inventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, "", true);
            PartDocument oPartDoc = oDoc as PartDocument;
            PartComponentDefinition oCompDef = oPartDoc.ComponentDefinition;
            PlanarSketch oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;

            TransientGeometry oTransGeo = m_inventorApplication.TransientGeometry;

            Point2d[] point = new Point2d[4];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 2.5);
            point[2] = oTransGeo.CreatePoint2d(3.8, 2.5);
            point[3] = oTransGeo.CreatePoint2d(3.8, 0);

            SketchLine[] oLines = new SketchLine[4];
            oLines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, point[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, point[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, oLines[0].StartSketchPoint);

            Profile oProfile = oSketch.Profiles.AddForSolid();
            ExtrudeDefinition oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            EdgeCollection oEdges = m_inventorApplication.TransientObjects.CreateEdgeCollection();

            foreach (Edge oEdge in (oExtrude.SideFaces[3].Edges))
            {
                if (Math.Abs(oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) - lengthProfile) < 0.0001)
                {
                    oEdges.Add(oEdge);
                }
            }
            if (oEdges.Count > 0)
            {
                FilletFeature oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.15);
            }

            Material oMaterial = oPartDoc.ComponentDefinition.Material;
            foreach (Material mat in oPartDoc.Materials)
            {
                if (mat.Name == materialProfile)
                {
                    oMaterial = mat;
                }
            }
            oPartDoc.ComponentDefinition.Material = oMaterial;

            RenderStyle oRenderStyle = oPartDoc.ComponentDefinition.Material.RenderStyle;
            foreach (RenderStyle style in oPartDoc.RenderStyles)
            {
                if (style.Name == colorProfile)
                {
                    oRenderStyle = style;
                }
            }
            oPartDoc.ComponentDefinition.Material.RenderStyle = oRenderStyle;

            return oDoc;
        }
        #endregion


        #region Деталь Ж38х39
        private Document ProfileG38x39(double lengthProfile, string materialProfile = "", string colorProfile = "")
        {
            Document oDoc = m_inventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, "", true);
            PartDocument oPartDoc = oDoc as PartDocument;
            PartComponentDefinition oCompDef = oPartDoc.ComponentDefinition;
            PlanarSketch oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;

            TransientGeometry oTransGeo = m_inventorApplication.TransientGeometry;

            Point2d[] point = new Point2d[4];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 3.8);
            point[2] = oTransGeo.CreatePoint2d(3.9, 3.8);
            point[3] = oTransGeo.CreatePoint2d(3.9, 0);
            Point2d pointCenter = oTransGeo.CreatePoint2d(1.95, 1.9);

            SketchLine[] oLines = new SketchLine[4];
            oLines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, point[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, point[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, oLines[0].StartSketchPoint);
            SketchCircle oCircle = oSketch.SketchCircles.AddByCenterRadius(pointCenter, 1.125);

            Profile oProfile = oSketch.Profiles.AddForSolid();
            ExtrudeDefinition oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthProfile, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            EdgeCollection oEdges = m_inventorApplication.TransientObjects.CreateEdgeCollection();

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
                FilletFeature oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.15);
            }

            Material oMaterial = oPartDoc.ComponentDefinition.Material;
            foreach (Material mat in oPartDoc.Materials)
            {
                if (mat.Name == materialProfile)
                {
                    oMaterial = mat;
                }
            }
            oPartDoc.ComponentDefinition.Material = oMaterial;

            RenderStyle oRenderStyle = oPartDoc.ComponentDefinition.Material.RenderStyle;
            foreach (RenderStyle style in oPartDoc.RenderStyles)
            {
                if (style.Name == colorProfile)
                {
                    oRenderStyle = style;
                }
            }
            oPartDoc.ComponentDefinition.Material.RenderStyle = oRenderStyle;

            return oDoc;
        }
        #endregion


        #region Деталь 38-й швеллер
        private Document Channel38(double lengthChannel, string materialChannel = "", string colorChannel = "")
        {
            Document oDoc = m_inventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, "", true);
            PartDocument oPartDoc = oDoc as PartDocument;
            PartComponentDefinition oCompDef = oPartDoc.ComponentDefinition;
            PlanarSketch oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;

            TransientGeometry oTransGeo = m_inventorApplication.TransientGeometry;

            Point2d[] point = new Point2d[8];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 2.5);
            point[2] = oTransGeo.CreatePoint2d(0.4, 2.5);
            point[3] = oTransGeo.CreatePoint2d(0.4, 0.4);
            point[4] = oTransGeo.CreatePoint2d(3.4, 0.4);
            point[5] = oTransGeo.CreatePoint2d(3.4, 2.5);
            point[6] = oTransGeo.CreatePoint2d(3.8, 2.5);
            point[7] = oTransGeo.CreatePoint2d(3.8, 0);

            SketchLine[] oLines = new SketchLine[8];
            oLines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, point[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, point[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, point[4]);
            oLines[4] = oSketch.SketchLines.AddByTwoPoints(oLines[3].EndSketchPoint, point[5]);
            oLines[5] = oSketch.SketchLines.AddByTwoPoints(oLines[4].EndSketchPoint, point[6]);
            oLines[6] = oSketch.SketchLines.AddByTwoPoints(oLines[5].EndSketchPoint, point[7]);
            oLines[7] = oSketch.SketchLines.AddByTwoPoints(oLines[6].EndSketchPoint, oLines[0].StartSketchPoint);

            Profile oProfile = oSketch.Profiles.AddForSolid();
            ExtrudeDefinition oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthChannel, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            EdgeCollection oEdges = m_inventorApplication.TransientObjects.CreateEdgeCollection();
            foreach (Edge oEdge in (oExtrude.SideFaces[4].Edges))
            {
                if (oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) == lengthChannel)
                    oEdges.Add(oEdge);
            }
            foreach (Edge oEdge in (oExtrude.SideFaces[6].Edges))
            {
                if (oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) == lengthChannel)
                    oEdges.Add(oEdge);
            }
            FilletFeature oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.15);

            Material oMaterial = oPartDoc.ComponentDefinition.Material;
            foreach (Material mat in oPartDoc.Materials)
            {
                if (mat.Name == materialChannel)
                {
                    oMaterial = mat;
                }
            }
            oPartDoc.ComponentDefinition.Material = oMaterial;

            RenderStyle oRenderStyle = oPartDoc.ComponentDefinition.Material.RenderStyle;
            foreach (RenderStyle style in oPartDoc.RenderStyles)
            {
                if (style.Name == colorChannel)
                {
                    oRenderStyle = style;
                }
            }
            oPartDoc.ComponentDefinition.Material.RenderStyle = oRenderStyle;

            return oDoc;
        }
        #endregion


        #region Деталь 48-й швеллер
        private Document Channel48(double lengthChannel, string materialChannel = "", string colorChannel = "")
        {
            Document oDoc = m_inventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, "", true);
            PartDocument oPartDoc = oDoc as PartDocument;
            PartComponentDefinition oCompDef = oPartDoc.ComponentDefinition;
            PlanarSketch oSketch = oCompDef.Sketches.Add(oPartDoc.ComponentDefinition.WorkPlanes[3]);

            oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;

            TransientGeometry oTransGeo = m_inventorApplication.TransientGeometry;

            Point2d[] point = new Point2d[8];
            point[0] = oTransGeo.CreatePoint2d(0, 0);
            point[1] = oTransGeo.CreatePoint2d(0, 3.2);
            point[2] = oTransGeo.CreatePoint2d(0.5, 3.2);
            point[3] = oTransGeo.CreatePoint2d(0.5, 0.5);
            point[4] = oTransGeo.CreatePoint2d(4.3, 0.5);
            point[5] = oTransGeo.CreatePoint2d(4.3, 3.2);
            point[6] = oTransGeo.CreatePoint2d(4.8, 3.2);
            point[7] = oTransGeo.CreatePoint2d(4.8, 0);

            SketchLine[] oLines = new SketchLine[8];
            oLines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, point[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, point[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, point[4]);
            oLines[4] = oSketch.SketchLines.AddByTwoPoints(oLines[3].EndSketchPoint, point[5]);
            oLines[5] = oSketch.SketchLines.AddByTwoPoints(oLines[4].EndSketchPoint, point[6]);
            oLines[6] = oSketch.SketchLines.AddByTwoPoints(oLines[5].EndSketchPoint, point[7]);
            oLines[7] = oSketch.SketchLines.AddByTwoPoints(oLines[6].EndSketchPoint, oLines[0].StartSketchPoint);

            Profile oProfile = oSketch.Profiles.AddForSolid();
            ExtrudeDefinition oExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudeDef.SetDistanceExtent(lengthChannel, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature oExtrude = oCompDef.Features.ExtrudeFeatures.Add(oExtrudeDef);


            EdgeCollection oEdges = m_inventorApplication.TransientObjects.CreateEdgeCollection();
            foreach (Edge oEdge in (oExtrude.SideFaces[4].Edges))
            {
                if (oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) == lengthChannel)
                    oEdges.Add(oEdge);
            }
            foreach (Edge oEdge in (oExtrude.SideFaces[6].Edges))
            {
                if (oEdge.StartVertex.Point.DistanceTo(oEdge.StopVertex.Point) == lengthChannel)
                    oEdges.Add(oEdge);
            }
            FilletFeature oFillet = oCompDef.Features.FilletFeatures.AddSimple(oEdges, 0.15);

            Material oMaterial = oPartDoc.ComponentDefinition.Material;
            foreach (Material mat in oPartDoc.Materials)
            {
                if (mat.Name == materialChannel)
                {
                    oMaterial = mat;
                }
            }
            oPartDoc.ComponentDefinition.Material = oMaterial;

            RenderStyle oRenderStyle = oPartDoc.ComponentDefinition.Material.RenderStyle;
            foreach (RenderStyle style in oPartDoc.RenderStyles)
            {
                if (style.Name == colorChannel)
                {
                    oRenderStyle = style;
                }
            }
            oPartDoc.ComponentDefinition.Material.RenderStyle = oRenderStyle;

            return oDoc;
        }
        #endregion


        #region Создание кнопки Ribbon "Генератор секций"
        public void createButtonOnRibbon()
        {
            UserInterfaceManager userInterfaceManager = m_inventorApplication.UserInterfaceManager;
            ControlDefinitions controlDefinitions = m_inventorApplication.CommandManager.ControlDefinitions;
            m_buttonSectionsGenerator = controlDefinitions.AddButtonDefinition("Генератор секций", "id_SectionsGenerator", CommandTypesEnum.kNonShapeEditCmdType, m_ClientId, "", "", m_buttonIcons.IconStandart, m_buttonIcons.IconLarge);

            Ribbon zeroRibbon = userInterfaceManager.Ribbons["ZeroDoc"];
            RibbonTab startedTab = zeroRibbon.RibbonTabs["id_GetStarted"];

            //проверка на существование панели с кнопкой
            foreach (RibbonPanel ribbonPanel in startedTab.RibbonPanels)
            {
                if (ribbonPanel.InternalName == "id_PanelPerileFences")
                    return;
            }

            RibbonPanel newPanel = startedTab.RibbonPanels.Add("Перильные ограждения", "id_PanelPerileFences", m_ClientId);
            newPanel.CommandControls.AddButton(m_buttonSectionsGenerator, true);
        }


        private class PathToRibbon
        {

        }


        private class ButtonIcons
        {
            public readonly stdole.IPictureDisp IconStandart;
            public readonly stdole.IPictureDisp IconLarge;

            public ButtonIcons(System.Drawing.Image PictureStandart, System.Drawing.Image PictureLarge)
            {
                IconStandart = ImageConvertor.ConvertImageToPictureDisp(PictureStandart);
                IconLarge = ImageConvertor.ConvertImageToPictureDisp(PictureLarge);
            }
        }


        private class ImageConvertor : System.Windows.Forms.AxHost
        {
            ImageConvertor() : base("") { }

            public static stdole.IPictureDisp ConvertImageToPictureDisp(System.Drawing.Image image)
            {
                if (null == image)
                    return null;
                return GetIPictureDispFromPicture(image) as stdole.IPictureDisp;
            }
        }
        #endregion

    }
}
