using System.Windows.Forms;

namespace InventorSegmentsGenerator
{
    public partial class FormSectionsGenerator : Form
    {
        public FormSectionsGenerator()
        {
            InitializeComponent();
        }

        public FormSectionsGenerator(ref CompositeSection m_compositeSection)
        {
            InitializeComponent();
            comboBoxChannel.SelectedIndex = 1;
            textBoxLengthSection.Text = m_compositeSection.Length.ToString();
        }
    }
}
