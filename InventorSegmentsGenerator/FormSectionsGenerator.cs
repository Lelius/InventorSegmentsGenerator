using System.Windows.Forms;

namespace InventorSegmentsGenerator
{
    public partial class FormSectionsGenerator : Form
    {
         public FormSectionsGenerator()
        {
            InitializeComponent();

            CompositeSection m_compositeSection = new CompositeSection();
            comboBoxChannel.SelectedIndex = 1;
            textBoxLengthSection.Text = m_compositeSection.Length.ToString();
        
        }
    }
}
