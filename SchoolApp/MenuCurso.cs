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
    public partial class MenuCurso : Form
    {
        private string _nombre;
        private int _id;

        public MenuCurso(string nombre, int id)
        {
            InitializeComponent();
            _nombre = nombre;
            _id = id;
        }

        public MenuCurso()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListaAlumnos lista = new ListaAlumnos(_nombre,_id);
            lista.Show();
            Hide();
        }

        private void MenuCurso_Load(object sender, EventArgs e)
        {
            label1.Text = _nombre + _id;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MenuMaestro lista = new MenuMaestro();
            lista.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListaDesayunos lista = new ListaDesayunos(_nombre, _id);
            lista.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListaAlmuerzo lista = new ListaAlmuerzo(_nombre, _id);
            lista.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListaMerienda merienda = new ListaMerienda(_nombre,_id);
            merienda.Show();
            Hide();
        }
    }
}
