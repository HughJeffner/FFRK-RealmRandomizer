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
using Transitions;

namespace FFRKRealmRando
{
    public partial class FormMain : Form
    {

        // Random number source
        Random rando = new Random();

        public FormMain()
        {
            InitializeComponent();

            //Extract UI font to local directory
            try
            {
                string filename = "finalf.ttf";
                if (!File.Exists(filename)) File.WriteAllBytes(filename, Properties.Resources.finalf);
            }
            catch { }
                 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            // Title
            this.Text += " " + Application.ProductVersion;
            
            // Restore previous window size
            this.Size = Properties.Settings.Default.FormSize;

            // Reset
            btnReset_Click(sender, e);
            applySettings();
        }

        private void applySettings()
        {
            // Update UI with current settings
            label1.Text = Properties.Settings.Default.LabelText;
            label1.ForeColor = Properties.Settings.Default.FontColor;
            label1.Font = Properties.Settings.Default.LabelFont;
            label1.BorderColor = Properties.Settings.Default.BorderColor;
            label1.BorderSize = Properties.Settings.Default.BorderSize;
            panel1.BackColor = Properties.Settings.Default.BackColor;

        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            // Start animation timer
            timer1.Start();
                       
        }

        private Image getNextImage()
        {
            // Get next realm
            int index = rando.Next(checkedListBox1.CheckedItems.Count);

            // Update with the image
            return ((Realm)checkedListBox1.CheckedItems[index]).Image;
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset realm checkbox to intial state
            for (int x = 0; x < checkedListBox1.Items.Count; x++)
            {
                checkedListBox1.Items[x] = new Realm(checkedListBox1.Items[x].ToString());
                checkedListBox1.SetItemChecked(x, x < 17);
            }
        }

        // Animation parameters
        int initdelay = 10;
        int rate = 6;
        int end = 800;
        int delay = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update timer interval
            delay += delay/rate;
            timer1.Interval = delay;

            // If no valid realms then just exit
            if (checkedListBox1.CheckedItems.Count == 0) return;

            // Get next realm
            int index = rando.Next(checkedListBox1.CheckedItems.Count);

            // Update with the image
            pictureBox1.Image = ((Realm)checkedListBox1.CheckedItems[index]).Image;
            pictureBox1.Refresh();

            // If we crossed the end threshold then stop
            if (timer1.Interval >= end)
            {
                checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(checkedListBox1.CheckedItems[index]), false);
                timer1.Enabled = false;
                timer1.Interval = initdelay;
                delay = initdelay;

                // Blink Animation
                Transition.run(panel1, "BackColor", Properties.Settings.Default.FlashColor, new TransitionType_Flash(4, 250));

                // Bounce Animation
                pictureBox1.BringToFront();
                Transition bounce = new Transition(new TransitionType_Bounce(500));
                bounce.add(pictureBox1, "Left", pictureBox1.Left - 25);
                bounce.add(pictureBox1, "Width", pictureBox1.Width + 50);
                bounce.add(pictureBox1, "Top", pictureBox1.Top - 25);
                bounce.add(pictureBox1, "Height", pictureBox1.Height + 50);
                bounce.run();
            }
        }

        // Click to show settings
        private void panel1_Click(object sender, EventArgs e)
        {
            using (FormSettings formSettings = new FormSettings()){

                if (formSettings.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    applySettings();
                }

            }
        }

        // Save window size
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Save();
        }
    }
}
