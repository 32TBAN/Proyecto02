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
    public partial class Recepcion : Form
    {
        ProductoresEntidad productoresEntidad = new ProductoresEntidad();
        RecepcionEntidad recepcionEntidad = new RecepcionEntidad();
        public Recepcion()
        {
            InitializeComponent();
        }
        private void Productos_Load(object sender, EventArgs e)
        {
            CargarComponentes();
        }

        private void CargarComponentes()
        {
            CargarBodegas();
            CargarProductos();
        }

        private void CargarProductos()
        {
            guna2ComboBox_Producto.DataSource = ProductoNegocio.DevolverProductos();
            guna2ComboBox_Producto.DisplayMember = "Nombre";
        }

        private void CargarBodegas()
        {
            var listaBodegas = BodegaNegocio.DevolverBodegas();
            guna2ComboBox_Bodega.DataSource = listaBodegas;
            guna2ComboBox_Bodega.DisplayMember = "Num";

            DefinirUbicacion(((BodegaEntidad) guna2ComboBox_Bodega.SelectedValue).Num);
        }

        private void DefinirUbicacion(int selectedValue)
        {
            label_Ubicacion.Text = BodegaNegocio.BuscarBodega(selectedValue).Ubicacion;
        }

        #region Botones
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            GuardarDatosProductor();
        }
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            guna2TextBox_Cedula.Text = "";
            guna2TextBox_Nombre.Text = "";
            guna2TextBox_Direccion.Text = "";
            guna2TextBox_Telefono.Text = "";
            guna2TextBox_TelefonoAdi.Text = "";
            guna2TextBox_Email.Text = "";
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            GuardarDatosProductor();
        }
        #endregion

        #region Funciones
        private void GuardarDatosProductor()
        {
            if (ComprobarDatos())
            {
                productoresEntidad.Cedula = guna2TextBox_Cedula.Text;
                productoresEntidad.Nombre = guna2TextBox_Nombre.Text;
                productoresEntidad.Direccion = guna2TextBox_Direccion.Text;
                productoresEntidad.Telefono = guna2TextBox_Telefono.Text;
                productoresEntidad.TelefonoAdicional = guna2TextBox_TelefonoAdi.Text;
                productoresEntidad.Email = guna2TextBox_Email.Text;

                productoresEntidad = ProductoresNegocio.Guardar(productoresEntidad);
                if (productoresEntidad != null)
                {
                    MessageBox.Show("Se han guardado los datos.");
                    guna2TextBox_CedulaProductor.Text = productoresEntidad.Cedula;
                    ComenzarRecepcion();
                }
                else
                {
                    MessageBox.Show("A ocurido un error al guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ComenzarRecepcion()
        {
            recepcionEntidad.IdUsuario = 1;
            recepcionEntidad.IdProductor = productoresEntidad.Id;
            recepcionEntidad.FechaRecepcion = DateTime.UtcNow;
            recepcionEntidad.Total = 0;

            recepcionEntidad = RecepcionNegocio.ComenzarRecepcion(recepcionEntidad);
            if (recepcionEntidad != null)
            {
                MessageBox.Show("Ahorra puede comprar los productos");
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error en la recepcion");
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


        #endregion

        private void guna2TextBox_Cedula_TextChanged(object sender, EventArgs e)
        {
            CargarDatosExistentes();
        }

        private void CargarDatosExistentes()
        {
            productoresEntidad = ProductoresNegocio.DevolverProductorCedula(guna2TextBox_Cedula.Text);

            if (productoresEntidad != null)
            {
                guna2TextBox_Nombre.Text = productoresEntidad.Nombre;
                guna2TextBox_Direccion.Text = productoresEntidad.Direccion;
                guna2TextBox_Telefono.Text = productoresEntidad.Telefono;
                guna2TextBox_TelefonoAdi.Text = productoresEntidad.TelefonoAdicional;
                guna2TextBox_Email.Text = productoresEntidad.Email;

                guna2TextBox_CedulaProductor.Text = productoresEntidad.Cedula;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            AgregarProducto agregarProducto = new AgregarProducto();
            agregarProducto.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AgregarBodega agregarBodega = new AgregarBodega();
            agregarBodega.Show();
        }

        private void guna2ComboBox_Bodega_SelectedValueChanged(object sender, EventArgs e)
        {
            DefinirUbicacion(((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
