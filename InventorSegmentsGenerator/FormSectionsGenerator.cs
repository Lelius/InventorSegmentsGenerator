using System.Windows.Forms;
using System;

namespace InventorSegmentsGenerator
{
    public partial class FormSectionsGenerator : Form
    {
        CompositeSection compositeSection;

         public FormSectionsGenerator()
        {
            InitializeComponent();

            compositeSection = new CompositeSection();
            checkBoxLeftBodyKit.Checked = false;
            checkBoxRightBodyKit.Checked = false;
            textBoxNumberSubsections.Text = compositeSection.compositeSubsections.Count.ToString();
        }


        private void displayAllTab()
        {
            tabControlSubsections.TabPages.Clear();

            if (checkBoxLeftBodyKit.Checked == true)
            {
                tabControlSubsections.TabPages.Add("Левый обвес");
            }

            int result;
            if (Int32.TryParse(textBoxNumberSubsections.Text, out result))
            {
                if (result >= 0 && result <= 120)
                {
                    //compositeSection.compositeSubsections.Count = result;
                    for (int i = 0; i < result; i++)
                    {
                        tabControlSubsections.TabPages.Add("Подсекция " + (i + 1));
                    }
                }
            }

            if (checkBoxRightBodyKit.Checked == true)
            {
                tabControlSubsections.TabPages.Add("Правый обвес");
            }
        }


        private void checkBoxLeftBodyKit_CheckedChanged(object sender, System.EventArgs e)
        {
            displayAllTab();
        }


        private void checkBoxRightBodyKit_CheckedChanged(object sender, System.EventArgs e)
        {
            displayAllTab();
        }

        private void textBoxNumberSubsections_TextChanged(object sender, System.EventArgs e)
        {
            displayAllTab();
        }

    }
}
