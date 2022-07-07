using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microcreditos
{
    public partial class PantallaCarga : Form
    {
        public PantallaCarga()
        {
            InitializeComponent();
        }

        //Variable segundos para calcular el tiempo de la pantalla de carga.
        int seconds = 0;

        private void PantallaCarga_Load(object sender, EventArgs e)
        {
            //Inicialización del timer cuando cargar el formulario.
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Conteo de segundos para la animacion de pantalla de carga.
            seconds++;

            if (seconds == 18)
            {
                timer1.Stop();
                Inicio i = new Inicio();
                this.Hide();
                i.ShowDialog();
                this.Close();
            }
        }
    }
}
