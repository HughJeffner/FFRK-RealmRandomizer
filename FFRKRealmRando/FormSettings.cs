using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKRealmRando
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            txtLabelText.Text = Properties.Settings.Default.LabelText;
            numericUpDown1.Value = (decimal)Properties.Settings.Default.BorderSize;
            btnBorderColor.BackColor = Properties.Settings.Default.BorderColor;
            btnFontColor.BackColor = Properties.Settings.Default.FontColor;
            btnBackgroundColor.BackColor = Properties.Settings.Default.BackColor;
            btnFlashColor.BackColor = Properties.Settings.Default.FlashColor;
            fontDialog1.Font = Properties.Settings.Default.LabelFont;
            fontDialog1.FontMustExist = true;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LabelText = txtLabelText.Text;
            Properties.Settings.Default.BorderSize = (float)numericUpDown1.Value;
            Properties.Settings.Default.BorderColor = btnBorderColor.BackColor;
            Properties.Settings.Default.FontColor = btnFontColor.BackColor;
            Properties.Settings.Default.BackColor = btnBackgroundColor.BackColor;
            Properties.Settings.Default.FlashColor = btnFlashColor.BackColor;
            Properties.Settings.Default.LabelFont = fontDialog1.Font;
            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            colorDialog1.Color = b.BackColor;
            colorDialog1.ShowDialog(this);
            b.BackColor = colorDialog1.Color;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.ShowDialog(this);
            }
            catch
            {
                MessageBox.Show("Font not supported!");
            }
            
        }
               
    }
}
