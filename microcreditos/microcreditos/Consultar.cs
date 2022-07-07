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

namespace microcreditos
{
    public partial class Consultar : Form
    {
        //Conexión a la base de datos.
        SqlConnection conexion = new SqlConnection(@"Data Source = DESKTOP-M0J68FI\SQLEXPRESS; Initial Catalog = microcreditos; Integrated Security = True");

        public Consultar()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Inicio i = new Inicio();
            this.Hide();
            i.ShowDialog();
            this.Close();
        }

        //Al cargar formulario se llenan los objetos con información incluyendno el DGV.
        private void Consultar_Load(object sender, EventArgs e)
        {
            string cadConsulta = "select Nombre, Email, MontoD, MontoP, MontoF from deudores";
            SqlCommand com = new SqlCommand(cadConsulta, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvDeudores.DataSource = tabla;

            SqlCommand comando = new SqlCommand("select Nombre from prestamos", conexion);
            conexion.Open();
            SqlDataReader r = comando.ExecuteReader();
            while (r.Read())
            {
                cbNombre.Items.Add(r["Nombre"].ToString());
            }
            conexion.Close();

            SqlCommand comando2 = new SqlCommand("select Email from prestamos", conexion);
            conexion.Open();
            SqlDataReader r2 = comando2.ExecuteReader();
            while (r2.Read())
            {
                cbEmail.Items.Add(r2["Email"].ToString());
            }
            conexion.Close();
        }

        //Manipulacion del DGV con los CBX del formulario.
        private void cbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadConsulta = "select Nombre, Email, MontoD, MontoP, MontoF from deudores where Nombre = '" + cbNombre.Text + "'";
            SqlCommand com = new SqlCommand(cadConsulta, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvDeudores.DataSource = tabla;
        }

        private void cbEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadConsulta = "select Nombre, Email, MontoD, MontoP, MontoF from deudores where Email = '" + cbEmail.Text + "'";
            SqlCommand com = new SqlCommand(cadConsulta, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvDeudores.DataSource = tabla;
        }
    }
}
