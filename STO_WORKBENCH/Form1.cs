using STO_WORKBENCH.SLRegistry;
using STO_WORKBENCH.PDFFiller;

namespace STO_WORKBENCH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            Form FormSort = new FormSort();
            FormSort.ShowDialog();
        }

        private void buttonSL_Click(object sender, EventArgs e)
        {
            Form SLRegistry = new FormSLRegistry();
            SLRegistry.ShowDialog();
        }

        private void buttonAutoSL_Click(object sender, EventArgs e)
        {
            Form FormPDFFiller = new FormPDFFiller();
            FormPDFFiller.ShowDialog();
        }
    }
}
