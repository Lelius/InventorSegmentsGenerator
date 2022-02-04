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

            TabPage tabPage1 = new TabPage("Tab 1");
            TabPage tabPage2 = new TabPage("Tab 2");
            tabControlSubsections.Controls.Add(tabPage1);
            tabControlSubsections.Controls.Add(tabPage2);

            UserControl userControl = new UserControl();
            TableLayoutPanel tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel5.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel5.Dock = DockStyle.Fill;
            userControl.Controls.Add(tableLayoutPanel5);
            tabPage1.Controls.Add(userControl);

            tableLayoutPanel5.RowCount = 8;
            tableLayoutPanel5.ColumnCount = 2;
            ColumnStyle columnStyle = new ColumnStyle();
            columnStyle.SizeType = SizeType.Percent;
            columnStyle.Width = 70;
            tableLayoutPanel5.ColumnStyles.Add(columnStyle);
            
        }
    }
}
