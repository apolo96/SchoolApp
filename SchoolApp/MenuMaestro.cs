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
    public partial class MenuMaestro : Form
    {
        public MenuMaestro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            navigation(sender,1);
        }

        private void navigation(object s, int id)
        {
            Button btn = s as Button;
            var curso = btn.Text;
            if (curso == "Primero") id = 1;
            if (curso == "Segundo") id = 2;
            if (curso == "Tercero") id = 3;
            if (curso == "Cuarto") id = 4;
            if (curso == "Quinto") id = 5;
            if (curso == "Prejardin") id = 6;
            if (curso == "Jardin") id = 7;
            if (curso == "Transicion") id = 8;
            MenuCurso Menu = new MenuCurso(curso, id);           
            Menu.Show();
            Hide();
        }

        private void MenuMaestro_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            navigation(sender,1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            navigation(sender, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            navigation(sender, 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            navigation(sender, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            navigation(sender, 1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            navigation(sender, 1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            navigation(sender, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 lista = new Form1();
            lista.Show();
            Hide();
        }
    }
}
