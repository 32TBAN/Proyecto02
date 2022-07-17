using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;
namespace Presentacion
{
    public partial class Venta : Form
    {
        ClienteEntidad clienteEntidad = new ClienteEntidad();
        VentaEntidad ventaEntidad = new VentaEntidad();
        public Venta()
        {
            InitializeComponent();
        }

        private void guna2TextBox_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            GuardarDatosCliente();
        }

        private void GuardarDatosCliente()
        {
            if (ComprobacionDatos())
            {
                clienteEntidad.Cedula = guna2TextBox_Cedula.Text;
                clienteEntidad.Nombre = guna2TextBox_Nombre.Text;
                clienteEntidad.Celular = guna2TextBox_Celular.Text;
                clienteEntidad.Email = guna2TextBox_Email.Text;

                clienteEntidad = ClienteNegocio.Guardar(clienteEntidad);
                if (clienteEntidad != null)
                {
                    MessageBox.Show("Se han guardado los datos.");
                    guna2TextBox_CedulaCliemte.Text = clienteEntidad.Cedula;
                    ComenzarVenta();
                }
                else
                {
                    MessageBox.Show("A ocurido un error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ComenzarVenta()
        {
            ventaEntidad = new VentaEntidad();
            ventaEntidad.IdUsuario = 1;
            ventaEntidad.IdCliente = clienteEntidad.Id;
            ventaEntidad.Total = Convert.ToSingle(label_Total.Text);
            ventaEntidad.Fecha = DateTime.UtcNow;
        }

        private bool ComprobacionDatos()
        {
            if (guna2TextBox_Cedula.Text == "" || guna2TextBox_Nombre.Text == "")
            {
                MessageBox.Show("Datos incompletos asegurese de tener: Cedula y Nombre");
                return false;
            }
            return true;
        }

        private void guna2TextBox_Cedula_TextChanged(object sender, EventArgs e)
        {
            CargarDatosExistentes();
        }

        private void CargarDatosExistentes()
        {
            clienteEntidad = ClienteNegocio.DevolverClienteCedula(guna2TextBox_Cedula.Text);

            if (clienteEntidad != null)
            {
                guna2TextBox_Nombre.Text = clienteEntidad.Nombre;
                guna2TextBox_Celular.Text = clienteEntidad.Celular;
                guna2TextBox_Email.Text = clienteEntidad.Email;
                guna2TextBox_CedulaCliemte.Text = clienteEntidad.Celular;
                ComenzarVenta();
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            guna2TextBox_Cedula.Text = "";
            guna2TextBox_Nombre.Text = "";
            guna2TextBox_Celular.Text = "";
            guna2TextBox_Email.Text = "";
            guna2TextBox_CedulaCliemte.Text = "";
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            GuardarDatosCliente();
        }
    }
}
