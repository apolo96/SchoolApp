using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MenuMaestro Menu = new MenuMaestro();
            Menu.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuCocinero Menu = new MenuCocinero();
            Menu.Show();
            Hide();
        }
    }
}
