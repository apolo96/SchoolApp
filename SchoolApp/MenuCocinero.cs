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
    public partial class MenuCocinero : Form
    {
        public MenuCocinero()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            Hide();
        }

        private void MenuCocinero_Load(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            var day = thisDay.ToString("d");
            label7.Text = "Día, " + day;
            //ModelDataBase.executeQuery($"SELECT * FROM comidas WHERE fecha = '{day}' AND tipo = 1");
            //ModelDataBase.setDataAdpater("comidas");
            //DataView dv = ModelDataBase.getDataSet();
            //DataTable dt = dv.ToTable();
            //int dtrows = dt.Rows.Count;
            //string idComida = "";
            //if (dtrows > 0)
            //{
            //    idComida = Convert.ToString(dt.Rows[dtrows - 1]["id"]);

            ModelDataBase.executeQuery($"SELECT * FROM comidaest WHERE  estado = 'si' AND tipo_id = 1");
                ModelDataBase.setDataAdpater("asis_ok");
                DataView div = ModelDataBase.getDataSet();
                label4.Text = div.Count + "";
            //}

            //ModelDataBase.executeQuery($"SELECT * FROM comidas WHERE fecha = '{day}' AND tipo = 2");
            //ModelDataBase.setDataAdpater("comidas");
            //DataView dv1 = ModelDataBase.getDataSet();
            //DataTable dt1 = dv1.ToTable();
            //int dtrows1 = dt1.Rows.Count;
            //string idComida1 = "";
            //if (dtrows1 > 0)
            //{
                //idComida1 = Convert.ToString(dt1.Rows[dtrows - 1]["id"]);

                ModelDataBase.executeQuery($"SELECT * FROM comidaest WHERE estado = 'si' AND tipo_id = 2");
                ModelDataBase.setDataAdpater("asis_ok");
                DataView div1 = ModelDataBase.getDataSet();
                label5.Text = div1.Count + "";
            //}

            //ModelDataBase.executeQuery($"SELECT * FROM comidas WHERE fecha = '{day}' AND tipo = 3");
            //ModelDataBase.setDataAdpater("comidas");
            //DataView dv2 = ModelDataBase.getDataSet();
            //DataTable dt2 = dv2.ToTable();
            //int dtrows2 = dt2.Rows.Count;
            //string idComida2 = "";
            //if (dtrows1 > 0)
            //{
            //    idComida2 = Convert.ToString(dt1.Rows[dtrows - 1]["id"]);

                ModelDataBase.executeQuery($"SELECT * FROM comidaest WHERE estado = 'si' AND tipo_id = 3");
                ModelDataBase.setDataAdpater("asis_ok");
                DataView div2 = ModelDataBase.getDataSet();
                label6.Text = div2.Count + "";
            //}

        }
    }
}
