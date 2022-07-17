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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Metodos metodos = new Metodos();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarDatos();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarDatos();
        }

        public void actualizarDatos()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = metodos.getTabla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = metodos.getPendientes();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        metodos.altaNube(fila[1].ToString(), fila[3].ToString());
                        metodos.marcarSubidos(int.Parse(fila[4].ToString()));
                    }
                }
                actualizarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
