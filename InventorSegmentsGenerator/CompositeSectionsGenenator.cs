using System.Windows.Forms;
using Inventor;
using System.IO;
using System;
using System.Collections.Generic;

namespace InventorSegmentsGenerator
{
    class CompositeSectionsGenenator
    {
        private Inventor.Application invApp;
        private Inventor.ApplicationEvents invAppEvents;
        private string m_ClientId = "{43457a9b-2327-48bc-9c57-be7407696fb3}";
        ButtonDefinition buttonSectionsGenerator;
        ButtonIcons buttonIcons;
        FormSectionsGenerator mainFormGenerator;
        List<double> distanceToHoles = new List<double>();
        CompositeSection compositeSection;

        public CompositeSectionsGenenator(Inventor.Application invApp)
        {
            this.invApp = invApp;
            this.invAppEvents = invApp.ApplicationEvents;
            buttonIcons = new ButtonIcons(InvAddIn.Properties.Resources.Секция_16х16, InvAddIn.Properties.Resources.Секция_32х32);
        }

        public void MainGenerator()
        {
            //Создание кнопки Ribbon "Генератор секций"
            createButtonOnRibbon();

            buttonSectionsGenerator.OnExecute += ButtonSectionsGenerator_OnExecute;
        }

        private void ButtonSectionsGenerator_OnExecute(NameValueMap Context)
        {
            string projectPath = System.Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Desktop\\Section\\");
            string projectName = "Section";
            DesignProject oProject;
            AssemblyDocument oAssDoc;
            Matrix oPositionMatrix;

            //oProject = createAndAcivateProject(m_inventorApplication, projectName, projectPath);
            //oAssDoc = m_inventorApplication.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject) as AssemblyDocument;
            //oAssDoc.FullFileName = projectPath + "Секция.iam";
            //oPositionMatrix = m_inventorApplication.TransientGeometry.CreateMatrix();

            //PillarPartA48 pillarPart = new PillarPartA48(m_inventorApplication);
            //pillarPart.createPillarPart();

            //pillarPart.oDoc.FullFileName = projectPath + "СтойкаЛевая1.ipt";
            //if (!System.IO.File.Exists(pillarPart.oDoc.FullFileName))
            //    pillarPart.oPartDoc.Save();
            //ComponentOccurrence pillarPartLeft1 = oAssDoc.ComponentDefinition.Occurrences.Add(pillarPart.oDoc.FullFileName, oPositionMatrix);

            compositeSection = new CompositeSection(150);
            CrossbarPartA38 crossbarPartA38 = new CrossbarPartA38(invApp, 15);
            

            mainFormGenerator = new FormSectionsGenerator(compositeSection);
            mainFormGenerator.Show();
        }

        #region Создание и активация проекта
        private DesignProject createAndAcivateProject(Inventor.Application invApp, string projectName, string projectPath)
        {
            string projectFullPath = projectPath + projectName + ".ipj";
            DesignProjectManager oDesignProjectManager = invApp.DesignProjectManager;
            DesignProject oProject;

            if (projectName == ""| projectPath == "")
            {
                return null;
            }

            if (invApp.Documents.Count > 0)
            {
                MessageBox.Show("Для изменения активного проекта все текущие документы должны быть закрыты.");
                return null;
            }

            foreach (DesignProject oPr in oDesignProjectManager.DesignProjects)
            {
                if (oPr.Name == projectName)
                {
                    if (oPr.FullFileName == projectFullPath)
                    {
                        oPr.Activate();
                        return oPr;
                    }
                    else
                    {
                        MessageBox.Show(oPr.FullFileName + " - " + projectFullPath);
                        MessageBox.Show("Другой проект с данным именем уже существует!");
                        return null;
                    }
                }
            }

            if (System.IO.File.Exists(projectFullPath) == true)
            {
                oProject = invApp.DesignProjectManager.DesignProjects.AddExisting(projectFullPath);
                oProject.Activate();
                oDesignProjectManager = invApp.DesignProjectManager;
                return oProject;
            }
            else if (System.IO.File.Exists(projectFullPath) == false)
            {
                if (!Directory.Exists(projectPath))
                    Directory.CreateDirectory(projectPath);
                oProject = invApp.DesignProjectManager.DesignProjects.Add(MultiUserModeEnum.kSingleUserMode, projectName, projectPath);
                oProject.Activate();
                oDesignProjectManager = invApp.DesignProjectManager;
                return oProject;
            }
            else
                return null;
        }
        #endregion


        #region Создание кнопки Ribbon "Генератор секций"
        public void createButtonOnRibbon()
        {
            UserInterfaceManager userInterfaceManager = invApp.UserInterfaceManager;
            ControlDefinitions controlDefinitions = invApp.CommandManager.ControlDefinitions;
            buttonSectionsGenerator = controlDefinitions.AddButtonDefinition("Генератор секций", "id_SectionsGenerator", CommandTypesEnum.kNonShapeEditCmdType, m_ClientId, "", "", buttonIcons.IconStandart, buttonIcons.IconLarge);

            Ribbon zeroRibbon = userInterfaceManager.Ribbons["ZeroDoc"];
            RibbonTab startedTab = zeroRibbon.RibbonTabs["id_GetStarted"];

            //проверка на существование панели с кнопкой
            foreach (RibbonPanel ribbonPanel in startedTab.RibbonPanels)
            {
                if (ribbonPanel.InternalName == "id_PanelPerileFences")
                    return;
            }

            RibbonPanel newPanel = startedTab.RibbonPanels.Add("Перильные ограждения", "id_PanelPerileFences", m_ClientId);
            newPanel.CommandControls.AddButton(buttonSectionsGenerator, true);
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
