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
    public partial class ListaAlumnos : Form
    {

        private string _nombre;
        private int _id;
        private string deleteEstudent = "0";

        private string deleteEstudentName = "";

        public ListaAlumnos(string nombre, int id)
        {
            InitializeComponent();
            _nombre = nombre;
            _id = id;
            label1.Text = "Lista Estudiantes " + _nombre + " (" + _id + ")";
        }

        private void ListaAlumnos_Load(object sender, EventArgs e)
        {
            label4.Text = "0";
            label5.Text = "0";

            DataView dv;
            DateTime thisDay = DateTime.Today;
            var day = thisDay.ToString("d");
            ModelDataBase.executeQuery($"SELECT * FROM asistencias WHERE date = '{day}' AND curso_id = " + _id);
            ModelDataBase.setDataAdpater("asistencias");
            dv = ModelDataBase.getDataSet();
            DataTable dt = dv.ToTable();
            int dtrows = dt.Rows.Count;
            string idAsistencia = "";
            if (dtrows > 0)
            {
                idAsistencia = Convert.ToString(dt.Rows[dtrows - 1]["id"]);

                ModelDataBase.executeQuery("SELECT e.id, e.nombre, c.nombre as curso, a.estado  " +
               "FROM estudiantes as e, cursos as c, asistenciaest as a " +
               "WHERE e.curso_id = " + _id + " AND e.curso_id = c.id AND e.id = a.estudiante_id AND a.asistencias_id = " + idAsistencia);
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

            if (dtrows > 0)
            {
                ModelDataBase.executeQuery($"SELECT * FROM asistenciaest WHERE asistencias_id = {idAsistencia} AND estado = 'si' ");
                ModelDataBase.setDataAdpater("asis_ok");
                DataView div = ModelDataBase.getDataSet();
                label4.Text = div.Count + "";
                ModelDataBase.executeQuery($"SELECT * FROM asistenciaest WHERE asistencias_id = {idAsistencia} AND estado = 'no' ");
                ModelDataBase.setDataAdpater("asis_not");
                div = ModelDataBase.getDataSet();
                label5.Text = div.Count + "";
            }

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
                label4.Text = asistencia + "";
                label5.Text = noAsistencia + "";
                DateTime thisDay = DateTime.Today;
                var day = thisDay.ToString("d");
                ModelDataBase.executeQuery($"SELECT * FROM asistencias WHERE date = '{thisDay}' AND curso_id = {_id}");
                ModelDataBase.setDataAdpater("asistencias");
                dv = ModelDataBase.getDataSet();
                DataTable dt = dv.ToTable();
                int dtrows = dt.Rows.Count;

                if (dtrows == 0)
                {
                    int lastId = 0;
                    string sql = "INSERT INTO asistencias output INSERTED.ID VALUES ('" + day + "'," + _id + ")";
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
                        sql = $"INSERT INTO asistenciaest VALUES ({ids[i]},{lastId},'{estados[i]}')";

                        con = new SqlConnection(ModelDataBase.con);
                        cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        try
                        {
                            int a = cmd.ExecuteNonQuery();
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

        private void button3_Click(object sender, EventArgs e)
        {
            MenuCurso lista = new MenuCurso(_nombre, _id);
            lista.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgregarEstudiante estudiante = new AgregarEstudiante(_nombre, _id);
            estudiante.Show();
            Hide();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                deleteEstudent = Convert.ToString(selectedRow.Cells["id"].Value);
                deleteEstudentName = Convert.ToString(selectedRow.Cells["nombre"].Value);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show($"Estudiante: {deleteEstudentName}", "Eliminar estudiante",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes;
            if (confirm)
            {
                string sql = $"DELETE FROM estudiantes WHERE id = {deleteEstudent} ";

                SqlConnection con = new SqlConnection(ModelDataBase.con);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int a = cmd.ExecuteNonQuery();
                    MessageBox.Show("Eliminado");
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

                sql = $"DELETE FROM asistenciaest WHERE estudiante_id = {deleteEstudent} ";

                con = new SqlConnection(ModelDataBase.con);
                cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int a = cmd.ExecuteNonQuery();
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
                MenuCurso lista = new MenuCurso(_nombre, _id);
                lista.Show();
                Hide();

            }
        }
    }
}


