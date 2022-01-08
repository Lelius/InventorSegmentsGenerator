using System.Windows.Forms;
using Inventor;
using System.IO;
using System;
using System.Collections.Generic;

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
        List<double> distanceToHoles = new List<double>();

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
            string projectPath = System.Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Desktop\\Section\\");
            string projectName = "Section";
            DesignProject oProject;

            oProject = createAndAcivateProject(m_inventorApplication, projectName, projectPath);

            //AssemblyDocument oAssDoc = m_inventorApplication.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, "") as AssemblyDocument; 
            //Matrix oPositionMatrix = m_inventorApplication.TransientGeometry.CreateMatrix();

            //PillarPartA48 pillarPartA48 = new PillarPartA48(m_inventorApplication);
            //pillarPartA48.createPillarPart();
            //ProfileB30x7_3 profileB30X7_3 = new ProfileB30x7_3(m_inventorApplication, 20);
            //profileB30X7_3.createProfile();

            PillarPartA48 pillarPart = new PillarPartA48(m_inventorApplication);
            pillarPart.BasicAngle = 25;
            pillarPart.createPillarPart();

            distanceToHoles.Add(7);
            distanceToHoles.Add(13);

            CrossbarPartA48 crossbarPartA48 = new CrossbarPartA48(m_inventorApplication, 20);
            crossbarPartA48.BasicAngle = 25;
            crossbarPartA48.distanceToHoles = this.distanceToHoles;
            crossbarPartA48.createCrossbarPart();

            CrossbarPartA38 crossbarPartA38 = new CrossbarPartA38(m_inventorApplication, 20);
            crossbarPartA38.BasicAngle = 25;
            crossbarPartA38.distanceToHoles = this.distanceToHoles;
            crossbarPartA38.createCrossbarPart();
            //AssemblyDocument oAssDoc = (AssemblyDocument)m_inventorApplication.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, "", true);
            //Matrix oPositionMatrix = m_inventorApplication.TransientGeometry.CreateMatrix();
            //ProfileA38 a38 = new ProfileA38(m_inventorApplication, 15, "Стеклопластик", "Оранжевый");
            //a38.createProfile();
            //a38.oDoc.DisplayName = "Швеллер А38";



            //m_mainForm = new FormSectionsGenerator();
            //m_mainForm.Show();
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
