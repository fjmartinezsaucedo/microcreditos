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
    public partial class Historial : Form
    {
        //Conexión a la base de datos.
        SqlConnection conexion = new SqlConnection(@"Data Source = DESKTOP-M0J68FI\SQLEXPRESS; Initial Catalog = microcreditos; Integrated Security = True");

        public Historial()
        {
            InitializeComponent();
        }

        //Cuando carga el formulario se llena de información el CBX
        private void Historial_Load(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("select Nombre from prestamos", conexion);
            conexion.Open();
            SqlDataReader r = comando.ExecuteReader();
            while (r.Read())
            {
                cbNombre.Items.Add(r["Nombre"].ToString());
            }
            conexion.Close();
        }

        //El DGV se manipula segun el cliente seleccionado y la informacion sobre el en la BD
        private void cbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadPago = "select Nombre, Periodo, Fecha, Estatus from historial where Nombre = '" + cbNombre.Text + "'";
            SqlCommand com = new SqlCommand(cadPago, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvHistorial.DataSource = tabla;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Inicio i = new Inicio();
            this.Hide();
            i.ShowDialog();
            this.Close();
        }
    }
}
