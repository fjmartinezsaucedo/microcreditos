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
    public partial class Inicio : Form
    {
        //Conexión a base de datos.
        SqlConnection conexion = new SqlConnection(@"Data Source = DESKTOP-M0J68FI\SQLEXPRESS; Initial Catalog = microcreditos; Integrated Security = True");

        DateTime fecha = DateTime.Today;

        double res;        
        double montoF;        

        public Inicio()
        {
            InitializeComponent();
        }
        //Transición de pantallas del menú.
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar c = new Consultar();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }
        //Transición de pantallas del menú.
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Historial h = new Historial();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        //Metodo para el registro de prestamos respetando las restricciones del mismo.
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApaterno.Text != "" && txtAmaterno.Text != "" && txtCantidad.Text != "" && txtEmail.Text != "")
            {
                string cadSql = "insert into prestamos(Nombre, Apaterno, Amaterno, Cantidad, Telefono, Email, Fecha, Dia, Meses, Intereses) values ('" + txtNombre.Text + "', '" + txtApaterno.Text + "', '" + txtAmaterno.Text + "', " + txtCantidad.Text + ", " + txtTelefono.Text + ", '" + txtEmail.Text + "', '" + txtFecha.Text + "', " + txtDia.Text + ", " + txtMeses.Text + ", " + txtIntereses.Text + ")";
                conexion.Open();
                SqlCommand comando = new SqlCommand(cadSql, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();

                res = Int32.Parse(txtMeses.Text) * Int32.Parse(txtIntereses.Text);
                res = res + Int32.Parse(txtCantidad.Text);
                montoF = res;

                string cadSql2 = "insert into deudores(Nombre, Email, MontoD, MontoP, MontoF) values ('" + txtNombre.Text + "', '" + txtEmail.Text + "', "+ montoF +", " + 0 + ", '" + montoF + "')";
                conexion.Open();
                SqlCommand comando2 = new SqlCommand(cadSql2, conexion);
                comando2.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("Se agrego el registro correctamente", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombre.Clear();
                txtApaterno.Clear();
                txtAmaterno.Clear();
                txtCantidad.Clear();
                txtTelefono.Clear();
                txtEmail.Clear();
                txtFecha.Clear();
                txtDia.Clear();
                txtMeses.Clear();
                txtIntereses.Clear();

            }else
            {
                MessageBox.Show("Los campos deben llenarse obligatoriamente para completar el registro", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            txtFecha.Text = fecha.ToShortDateString();
            txtFecha.Enabled = false;
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            Pagos p = new Pagos();
            this.Hide();
            p.ShowDialog();
            this.Close();
        }

        private void btnAgradecimientos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Agradezco de antemano al equipo de desarrollo y recursos humanos por otorgarme la oportunidad de demostrar mis conocimientos.", "Saludos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
