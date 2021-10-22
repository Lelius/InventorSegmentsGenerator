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
            ProfileA38 a38 = new ProfileA38(m_inventorApplication, 15, "Стеклопластик", "Оранжевый");
            ProfileA48 a48 = new ProfileA48(m_inventorApplication, 15, "Стеклопластик", "Оранжевый");
            ProfileE38 e38 = new ProfileE38(m_inventorApplication, 15, "Стеклопластик", "Оранжевый");
            ProfileG38x39 g38x39 = new ProfileG38x39(m_inventorApplication, 2.4, "Стеклопластик", "Оранжевый");
            a38.createProfile();
            a38.oDoc.DisplayName = "Швеллер А38";
            a48.createProfile();
            a48.oDoc.DisplayName = "Швеллер А48";
            e38.createProfile();
            e38.oDoc.DisplayName = "Вкладыш Е38";
            g38x39.createProfile();
            g38x39.oDoc.DisplayName = "Вкладыш Ж38х39";

            //m_mainForm = new FormSectionsGenerator();
            //m_mainForm.Show();
        }


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
