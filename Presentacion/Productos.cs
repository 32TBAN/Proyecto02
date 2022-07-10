using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            GuardarDatosProductor();
        }

        private void GuardarDatosProductor()
        {
            if (ComprobarDatos())
            {

            }
        }

        private bool ComprobarDatos()
        {
            if (guna2TextBox_Cedula.Text == "" || guna2TextBox_Nombre.Text == "" ||
                guna2TextBox_Direccion.Text == "")
            {
                MessageBox.Show("Datos incompletos asegurese de tener: Cedula, Nombre y Direccion");
                return false;
            }
            else if (guna2TextBox_Telefono.Text == "" && guna2TextBox_Email.Text == "")
            {
                MessageBox.Show("Debe haber una forma de contactar al productor email o telefono");
                return false;
            }
            return true;
        }
    }
}
