using System;
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
    public partial class ListaDesayunos : Form
    {
        private string _nombre;
        private int _id;

        public ListaDesayunos(string nombre, int id)
        {
            InitializeComponent();
            _nombre = nombre;
            _id = id;
            label1.Text = "Lista Desayunos " + _nombre + " (" + _id + ")";
        }

        private void ListaDesayunos_Load(object sender, EventArgs e)
        {
            DataView dv;
            DateTime thisDay = DateTime.Today;
            var day = thisDay.ToString("d");
            ModelDataBase.executeQuery($"SELECT * FROM comidas WHERE fecha = '{day}' AND curso_id = " + _id + " AND tipo = 1");
            ModelDataBase.setDataAdpater("comidas");
            dv = ModelDataBase.getDataSet();
            DataTable dt = dv.ToTable();
            int dtrows = dt.Rows.Count;
            string idComida = "";
            if (dtrows > 0)
            {
                idComida = Convert.ToString(dt.Rows[dtrows - 1]["id"]);

                ModelDataBase.executeQuery("SELECT e.id, e.nombre, c.nombre as curso, a.estado  " +
               "FROM estudiantes as e, cursos as c, comidaest as a " +
               "WHERE e.curso_id = " + _id + " AND e.curso_id = c.id AND e.id = a.estudiante_id AND a.comida_id = " + idComida+" AND a.tipo_id = 1");
                ModelDataBase.setDataAdpater("estudiantes");
                dv = ModelDataBase.getDataSet();
                dt = dv.ToTable();
                if (dv.Count != 0) dataGridView1.DataSource = dv;
                dtrows = dt.Rows.Count;
            }
            else
            {
                string sql = $"SELECT e.id, e.nombre, c.nombre as curso, e.estado FROM estudiantes as e, cursos as c WHERE e.curso_id = {_id} AND e.curso_id = c.id";
                //MessageBox.Show(sql);
                ModelDataBase.executeQuery(sql);
                ModelDataBase.setDataAdpater("estudiantes_p");
                DataView dvi = ModelDataBase.getDataSet();
                DataTable dti = dvi.ToTable();
                if (dvi.Count != 0) dataGridView1.DataSource = dvi;
                dtrows = dt.Rows.Count;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MenuCurso lista = new MenuCurso(_nombre, _id);
            lista.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataView dv;
            int asistencia = 0;
            int noAsistencia = 0;
            int fila = 0;
            int rows = dataGridView1.Rows.Count - 1;
            int[] ids = new int[rows];
            string[] estados = new string[rows];
            for (fila = 0; fila < rows; fila++)
            {
                for (int col = 0; col < dataGridView1.Rows[fila].Cells.Count; col++)
                {
                    string valor = dataGridView1.Rows[fila].Cells[col].Value.ToString();
                    if (col == 0) ids[fila] = Convert.ToInt32(valor);
                    if (col == 3)
                    {
                        if (valor == "")
                        {
                            MessageBox.Show("Por favor, llene el campo vacio");
                            return;
                        }
                        else
                        {
                            estados[fila] = valor;
                            if (valor == "no") noAsistencia++;
                            else asistencia++;
                        }
                    }
                }
            }
            if (fila > 0)
            {
    
                DateTime thisDay = DateTime.Today;
                var day = thisDay.ToString("d");
                string sql = $"SELECT * FROM comidas WHERE fecha = '{day}' AND curso_id = {_id}  AND tipo = 1";
                ModelDataBase.executeQuery(sql);                
                ModelDataBase.setDataAdpater("comidas");
                dv = ModelDataBase.getDataSet();
                DataTable dt = dv.ToTable();
                int dtrows = dt.Rows.Count;

                if (dtrows == 0)
                {
                    int lastId = 0;
                    sql = "INSERT INTO comidas output INSERTED.ID VALUES ('" + day + "'," + _id + ",1)";
                    SqlConnection con = new SqlConnection(ModelDataBase.con);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    try
                    {
                        int a = cmd.ExecuteNonQuery();
                        lastId = (int)cmd.ExecuteScalar();
                        //MessageBox.Show("Registro ingresado correctamente !");
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
                    bool ok = false;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        sql = $"INSERT INTO comidaest VALUES ({ids[i]},{lastId},'{estados[i]}',1)";

                        con = new SqlConnection(ModelDataBase.con);
                        cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        try
                        {
                            int a = (int)cmd.ExecuteNonQuery();
                            ok = true;
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
                    if (ok) MessageBox.Show("Llamado correctamente !");

                }
                else
                {
                    MessageBox.Show("Ya has llamdo a lista !");
                }
            }
        }
    }
}
