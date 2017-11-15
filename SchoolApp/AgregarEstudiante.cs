using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolApp
{
    public partial class AgregarEstudiante : Form
    {
        private int _id;
        private string _nombre;
        private static ArrayList listParameter;

        public AgregarEstudiante(string nombre,int id)
        {
            InitializeComponent();
            _id = id;
            _nombre = nombre;
        }

        private void AgregarEstudiante_Load(object sender, EventArgs e)
        {
            label5.Text = _id+"";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( textBox1.Text == "" || textBox2.Text =="" || textBox3.Text =="")
            {
                MessageBox.Show("Complata los campos");
                return;
            }
            string nombre = textBox1.Text + " " + textBox3.Text;
           
            string sql = $"INSERT INTO estudiantes(nombre,curso_id,estado) VALUES ('{nombre}',{_id},'')";

            SqlConnection con = new SqlConnection(ModelDataBase.con);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show("Registro ingresado correctamente !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                // Cierro la Conexión.
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListaAlumnos estudiante = new ListaAlumnos(_nombre,_id);
            estudiante.Show();
            Hide();
        }
    }
}
