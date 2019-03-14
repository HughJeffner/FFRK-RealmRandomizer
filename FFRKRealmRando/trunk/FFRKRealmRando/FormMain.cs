using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKRealmRando
{
    public partial class FormMain : Form
    {

        Random rando = new Random();

        public FormMain()
        {
            InitializeComponent();

            //Init font
            string filename = "finalf.ttf";
            if (!File.Exists(filename)) File.WriteAllBytes(filename, Properties.Resources.finalf);
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(filename);
            Font ffFont = new Font(pfc.Families[0], 18);
            btnPull.Font = ffFont;
            btnReset.Font = ffFont;
            label1.Font = new Font(pfc.Families[0], 36);
            
        }

        private void btnPull_Click(object sender, EventArgs e)
        {

            timer1.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Reset
            btnReset_Click(sender, e);
            applySettings();
           
        }

        private void applySettings()
        {

            label1.Text = Properties.Settings.Default.LabelText;

            //Colors
            label1.ForeColor = Properties.Settings.Default.FontColor;
            flowLayoutPanel1.BackColor = Properties.Settings.Default.BackColor;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < checkedListBox1.Items.Count; x++)
            {
                checkedListBox1.Items[x] = new Realm(checkedListBox1.Items[x].ToString());
                checkedListBox1.SetItemChecked(x, x < 17);
            }
        }

        int initdelay = 10;
        int rate = 6;
        int end = 800;
        int delay = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            delay += delay/rate;
            timer1.Interval = delay;
            if (checkedListBox1.CheckedItems.Count == 0) return;
            int index = rando.Next(checkedListBox1.CheckedItems.Count);
            pictureBox1.Image = ((Realm)checkedListBox1.CheckedItems[index]).Image;
            pictureBox1.Refresh();
            if (timer1.Interval >= end)
            {
                checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(checkedListBox1.CheckedItems[index]), false);
                timer1.Enabled = false;
                timer1.Interval = initdelay;
                delay = initdelay;
                timer2.Enabled = true;
            }
        }

        int blinks = 0;
        int maxBlinks = 5;
        private void timer2_Tick(object sender, EventArgs e)
        {
             if (flowLayoutPanel1.BackColor == Properties.Settings.Default.BackColor)
            {
                flowLayoutPanel1.BackColor = Properties.Settings.Default.FlashColor;
                blinks += 1;
            }
            else
            {
                flowLayoutPanel1.BackColor = Properties.Settings.Default.BackColor;
            }
            if (blinks >= maxBlinks)
            {
                blinks = 0;
                flowLayoutPanel1.BackColor = Properties.Settings.Default.BackColor;
                timer2.Enabled = false;
            }
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            using (FormSettings formSettings = new FormSettings()){

                if (formSettings.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    applySettings();
                }

            }
        }
    }
}
