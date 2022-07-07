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
    public partial class Pagos : Form
    {
        //Conexión a la base de datos.
        SqlConnection conexion = new SqlConnection(@"Data Source = DESKTOP-M0J68FI\SQLEXPRESS; Initial Catalog = microcreditos; Integrated Security = True");

        DateTime fecha = DateTime.Today;

        double periodo;
        double cantidad;
        double res;
        double resD;

        public Pagos()
        {
            InitializeComponent();
        }

        //Al cargar el formulario se llenan los elementos a utilizar del mismo.
        private void Pagos_Load(object sender, EventArgs e)
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

        //Con la elección del CBX se manipula el DVG y  se toman datos de las tablas de la BD utilizada.
        private void cbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadPago = "select Nombre, Apaterno, Amaterno, Cantidad, Email, Fecha, Dia, Meses, Intereses from prestamos where Nombre = '" + cbNombre.Text + "'";
            SqlCommand com = new SqlCommand(cadPago, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvPago.DataSource = tabla;

            SqlCommand comando2 = new SqlCommand("select Cantidad from prestamos where Nombre = '" + cbNombre.Text + "'", conexion);
            conexion.Open();
            SqlDataReader r = comando2.ExecuteReader();
            while (r.Read())
            {
                lblCantidad.Text = (r["Cantidad"].ToString());
            }
            conexion.Close();

            SqlCommand comando3 = new SqlCommand("select Meses from prestamos where Nombre = '" + cbNombre.Text + "'", conexion);
            conexion.Open();
            SqlDataReader r2 = comando3.ExecuteReader();
            while (r2.Read())
            {
                lblPeriodo.Text = (r2["Meses"].ToString());
            }
            conexion.Close();

            lblFecha.Text = fecha.ToShortDateString();

        }

        //Registro y actualizacion en tablas deudores y prestamos segun el cliente seleccionado.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {            

            SqlCommand comando = new SqlCommand("select Meses from prestamos where Nombre = '" + cbNombre.Text + "'", conexion);
            conexion.Open();
            SqlDataReader r = comando.ExecuteReader();
            while (r.Read())
            {
                lblPeriodo.Text = (r["Meses"].ToString());
            }
            conexion.Close();

            periodo = Int32.Parse(lblPeriodo.Text);

            SqlCommand comando2 = new SqlCommand("select MontoD from deudores where Nombre = '" + cbNombre.Text + "'", conexion);
            conexion.Open();
            SqlDataReader r2 = comando2.ExecuteReader();
            while (r2.Read())
            {
                lblCantidad.Text = (r2["MontoD"].ToString());
            }
            conexion.Close();

            cantidad = Double.Parse(lblCantidad.Text);                       
            res = cantidad / periodo;

            if (Int32.Parse(txtPago.Text) >= res)
            {
                periodo --;
            }

            if (txtPago.Text != "")
            {
                resD = cantidad - Double.Parse(txtPago.Text);

                string cadSql = "update deudores set MontoP = '" + txtPago.Text + "' where Nombre = '" + cbNombre.Text + "'";
                conexion.Open();
                SqlCommand comando3 = new SqlCommand(cadSql, conexion);
                comando3.ExecuteNonQuery();
                conexion.Close();

                string cadSql2 = "update deudores set MontoD = " + resD + " where Nombre = '" + cbNombre.Text + "'";
                conexion.Open();
                SqlCommand comando4 = new SqlCommand(cadSql2, conexion);
                comando4.ExecuteNonQuery();
                conexion.Close();

                string cadSql3 = "insert into historial(Nombre, Periodo, Fecha, Estatus) values ('" + cbNombre.Text + "', " + periodo + ", '" + lblFecha.Text + "', '" + lblEstatus.Text + "')";
                conexion.Open();
                SqlCommand comando5 = new SqlCommand(cadSql3, conexion);
                comando5.ExecuteNonQuery();
                conexion.Close();
            }
            else
            {
                MessageBox.Show("El campo de pago debe completarse para concluir el registro", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string cadPago = "select Nombre, Periodo, Fecha, Estatus from historial";
            SqlCommand com = new SqlCommand(cadPago, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvPago.DataSource = tabla;
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
