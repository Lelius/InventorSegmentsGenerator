using System.Windows.Forms;
using System;

namespace InventorSegmentsGenerator
{
    public partial class FormSectionsGenerator : Form
    {
        private CompositeSection compositeSection;

        public FormSectionsGenerator()
        {
            InitializeComponent();

            compositeSection = new CompositeSection();

            int index;
            if (compositeSection.TypeOfMainChannel == ProfileTypeEnum.A38)
            {
                index = comboBoxChannel.FindString("А38");
            }
            else if (compositeSection.TypeOfMainChannel == ProfileTypeEnum.A48)
            {
                index = comboBoxChannel.FindString("А38");
            }
            else
                index = 0;
            comboBoxChannel.SelectedIndex = index;

            numericUpDownLengthSection.Value = (decimal)compositeSection.LengthSection;
            numericUpDownBasicAngle.Value = (decimal)compositeSection.BasicAngle;
            numericUpDownNumberSubsections.Value = (decimal)compositeSection.NumberSubsections;
            checkBoxLeftBodyKit.Checked = compositeSection.LeftBodyKit;
            checkBoxRightBodyKit.Checked = compositeSection.RightBodyKit;
            numericUpDownHeightSideStand.Value = (decimal)compositeSection.HeightSideStand;
            numericUpDownHeightTopCrossbarRail.Value = (decimal)compositeSection.HeightTopCrossbarRail;
            numericUpDownHeightBottomCrossbarRail.Value = (decimal)compositeSection.HeightBottomCrossbarRail;
            numericUpDownHeightGroundRail.Value = (decimal)compositeSection.HeightGroundRail;
        }

        public FormSectionsGenerator(CompositeSection compositeSectionParent)
        {
            InitializeComponent();

            compositeSection = compositeSectionParent;

            int index;
            if (compositeSection.TypeOfMainChannel == ProfileTypeEnum.A38)
            {
                index = comboBoxChannel.FindString("А38");
            }
            else if (compositeSection.TypeOfMainChannel == ProfileTypeEnum.A48)
            {
                index = comboBoxChannel.FindString("А38");
            }
            else
                index = 0;
            comboBoxChannel.SelectedIndex = index;

            numericUpDownLengthSection.Value = (decimal)compositeSection.LengthSection;
            numericUpDownBasicAngle.Value = (decimal)compositeSection.BasicAngle;
            numericUpDownNumberSubsections.Value = (decimal)compositeSection.NumberSubsections;
            checkBoxLeftBodyKit.Checked = compositeSection.LeftBodyKit;
            checkBoxRightBodyKit.Checked = compositeSection.RightBodyKit;
            numericUpDownHeightSideStand.Value = (decimal)compositeSection.HeightSideStand;
            numericUpDownHeightTopCrossbarRail.Value = (decimal)compositeSection.HeightTopCrossbarRail;
            numericUpDownHeightBottomCrossbarRail.Value = (decimal)compositeSection.HeightBottomCrossbarRail;
            numericUpDownHeightGroundRail.Value = (decimal)compositeSection.HeightGroundRail;
        }
    }
}
