using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKRealmRando
{
    public partial class Form1 : Form
    {

        Random rando = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPull_Click(object sender, EventArgs e)
        {

            timer1.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            btnReset_Click(sender, e);
            
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
             if (flowLayoutPanel1.BackColor == Color.Lime)
            {
                flowLayoutPanel1.BackColor = Color.DarkGray;
                blinks += 1;
            }
            else
            {
                flowLayoutPanel1.BackColor = Color.Lime;
            }
            if (blinks >= maxBlinks)
            {
                blinks = 0;
                flowLayoutPanel1.BackColor = Color.Lime;
                timer2.Enabled = false;
            }
        }
    }
}
