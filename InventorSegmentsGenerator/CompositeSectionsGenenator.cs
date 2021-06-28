using System.Windows.Forms;
using Inventor;

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
        CompositeSection m_compositeSection;

        public CompositeSectionsGenenator(Inventor.Application m_inventorApplication)
        {
            this.m_inventorApplication = m_inventorApplication;
            this.m_applicationEvents = m_inventorApplication.ApplicationEvents;
            m_buttonIcons = new ButtonIcons(InvAddIn.Properties.Resources.Секция_16х16, InvAddIn.Properties.Resources.Секция_32х32);
        }

        public void MainGenerator()
        {
            //testStartMainGenerator();

            //Создание кнопки Ribbon "Генератор секций"
            createButtonOnRibbon();

            m_buttonSectionsGenerator.OnExecute += M_buttonSectionsGenerator_OnExecute;
        }

        private void M_buttonSectionsGenerator_OnExecute(NameValueMap Context)
        {
            m_mainForm = new FormSectionsGenerator(ref m_compositeSection);
            m_mainForm.Show();
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


        #region List of all
        public void testStartMainGenerator()
        {
            MessageBox.Show("А вот и запуск основного метода!");

            Inventor.UserInterfaceManager userInterfaceManager = m_inventorApplication.UserInterfaceManager;
            Inventor.Ribbons ribbons = userInterfaceManager.Ribbons;
            MessageBox.Show(ribbons.Count.ToString());

            foreach (Inventor.Ribbon ribbon in userInterfaceManager.Ribbons)
            {
                string myEnumString = "";
                foreach (Inventor.RibbonTab tab in ribbon.RibbonTabs)
                {
                    myEnumString = myEnumString + tab.DisplayName + " - " + tab.InternalName + "\n";
                }
                MessageBox.Show(myEnumString);
            }
        }   //удалить как ненужный метод
        #endregion
    }
}
