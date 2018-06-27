using System;
using System.Windows.Forms;

namespace AvroViewerGui
{
    public partial class GoToLineForm : Form
    {
        private int StartValue { get; }
        private int EndValue { get; }

        public string LineNumber { get; set; }

        public GoToLineForm(int startValue, int endValue)
        {
            InitializeComponent();

            StartValue = startValue;
            EndValue = endValue;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            labelLineNumberRange.Text = $"&Line number ({StartValue} - {EndValue}):";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxLineNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(textBoxLineNumber.Text))
            {
                LineNumber = textBoxLineNumber.Text;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBoxLineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Make sure only numbers can be enterd and reacts on ENTER key
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
