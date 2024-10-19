using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFT_BLE_expert_
{
    public partial class feedback : Form
    {
        public feedback()
        {
            InitializeComponent();
            progressBar1.BackColor = Color.LightCyan;
            progressBar1.ForeColor = Color.Pink;
            chart1.Series[0].Points.Clear();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        public void update_bar(int value)
        {
            progressBar1.Value = value;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void update_time_lab(int value)
        {
            label2.Text = value.ToString() + "秒";
        }

        public void update_status_lab(string status)
        {
            label4.Text = status;
        }

        public void update_line(int score, int system_time)
        {
            chart1.Series[0].Points.AddXY(system_time, score);
        }

        public void clear_line()
        {
            chart1.Series[0].Points.Clear();
        }
    }
}
