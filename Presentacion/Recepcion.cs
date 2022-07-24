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
        List<DetalleRecepcionEntidad> listaRecepcion = new List<DetalleRecepcionEntidad>();
        DetalleRecepcionEntidad detalleRecepcion = new DetalleRecepcionEntidad();
        public Recepcion()
        {
            InitializeComponent();
        }
        private void Productos_Load(object sender, EventArgs e)
        {
            CargarComponentes();
        }

        #region GuardarDatosProductor
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            GuardarDatosProductor();

        }
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            GuardarDatosProductor();
        }
        private void GuardarDatosProductor()
        {
            if (ComprobarDatos())
            {
                productoresEntidad = new ProductoresEntidad();
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
        #region Recepcion
        private void ComenzarRecepcion()
        {
            if (productoresEntidad != null)
            {
                recepcionEntidad = new RecepcionEntidad();
                recepcionEntidad.IdUsuario = 1;
                recepcionEntidad.IdProductor = productoresEntidad.Id;
                recepcionEntidad.FechaRecepcion = DateTime.UtcNow;
                recepcionEntidad.Total = Convert.ToSingle(guna2TextBox_Total.Text);
                recepcionEntidad = RecepcionNegocio.ComenzarRecepcion(recepcionEntidad);
                if (recepcionEntidad == null)
                {
                    MessageBox.Show("Error al realizar la recepcion");
                }
            }
        }
        #endregion
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
                ComenzarRecepcion();
            }
        }
        #endregion

        #region AgregacionProductos
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AgregarProductoDetalle();
        }

        private void AgregarProductoDetalle()
        {
            if (ControlDatosAgregarProducto())
            {
                listaRecepcion = RecepcionNegocio.DevolverProductosDetalle(recepcionEntidad.NumRec); 

                detalleRecepcion.NumRecepcion = recepcionEntidad.NumRec;
                detalleRecepcion.CodProducto = ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Id;
                detalleRecepcion.NombreProducto = ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Nombre;
                detalleRecepcion.NumBodega = ((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num;

                if (listaRecepcion == null)
                {
                    detalleRecepcion.Cantidad = (int)guna2NumericUpDown_Cantidad.Value;
                    detalleRecepcion.Subtotal = (detalleRecepcion.Cantidad * ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio);
                    detalleRecepcion = RecepcionNegocio.AgregarProductoDetalle(detalleRecepcion, true);
                }
                else
                {
                    bool encontrado = false;
                    foreach (var item in listaRecepcion)
                    {
                        if (item.CodProducto == detalleRecepcion.CodProducto)
                        {
                            detalleRecepcion.Cantidad = item.Cantidad + (int)guna2NumericUpDown_Cantidad.Value;
                            detalleRecepcion.Subtotal = (detalleRecepcion.Cantidad * ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio);
                            detalleRecepcion = RecepcionNegocio.AgregarProductoDetalle(detalleRecepcion, false);
                            encontrado = true;
                        }
                    }
                    if (!encontrado)
                    {
                        detalleRecepcion.Cantidad = (int)guna2NumericUpDown_Cantidad.Value;
                        detalleRecepcion.Subtotal = (detalleRecepcion.Cantidad * ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio);
                        detalleRecepcion = RecepcionNegocio.AgregarProductoDetalle(detalleRecepcion, true);
                    }
                }

                if (detalleRecepcion != null)
                {
                    listaRecepcion = RecepcionNegocio.DevolverProductosDetalle(recepcionEntidad.NumRec);
                    CargarDatosProductoDetalle();
                }
                else
                {
                    MessageBox.Show("Error al agregar producto");
                }
            }
        }
        private bool ControlDatosAgregarProducto()
        {
            if (guna2TextBox_CedulaProductor.Text == "")
            {
                MessageBox.Show("Debe ingresar los datos de un productor");
                return false;

            }
            else if (guna2NumericUpDown_Cantidad.Value.ToString() == "0")
            {
                MessageBox.Show("Escoja una cantidad valida");
                return false;
            }
            else
            {
                BodegaEntidad bodegaCap = BodegaNegocio.BuscarBodega(((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num);
                if (bodegaCap.Capacidad < guna2NumericUpDown_Cantidad.Value)
                {
                    MessageBox.Show("La capacidad de la bodega es: " + bodegaCap.Capacidad + "kg si necesita una cantidad mayor seleccione otra bodega");
                    return false;
                }
            }
       
            return true;
        }
        private void CargarDatosProductoDetalle()
        {
            dataGridView_ProductosDetalle.DataSource = null;
            dataGridView_ProductosDetalle.DataSource = listaRecepcion;
            dataGridView_ProductosDetalle.Columns["NumRecepcion"].Visible = false;
            dataGridView_ProductosDetalle.Columns["CodProducto"].Visible = false;

            float total = 0; 
            foreach (var item in listaRecepcion)
            {
                total += item.Subtotal;
            }
            guna2TextBox_Total.Text = total.ToString();
        }
        #endregion



        #region FinalizarCompra
        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            recepcionEntidad.Total = Convert.ToSingle(guna2TextBox_Total.Text);
            recepcionEntidad = RecepcionNegocio.ComenzarRecepcion(recepcionEntidad);
            if (recepcionEntidad != null)
            {
                MessageBox.Show("Se ha finalizado la recepcion");
                Nuevo();
            }
        }

        #endregion

        #region OtrasFunciones
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

            DefinirUbicacion(((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num);
        }

        private void DefinirUbicacion(int selectedValue)
        {
            label_Ubicacion.Text = BodegaNegocio.BuscarBodega(selectedValue).Ubicacion;
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            guna2TextBox_Cedula.Text = "";
            guna2TextBox_Nombre.Text = "";
            guna2TextBox_Direccion.Text = "";
            guna2TextBox_Telefono.Text = "";
            guna2TextBox_TelefonoAdi.Text = "";
            guna2TextBox_Email.Text = "";
            guna2TextBox_Total.Text = "0";
            guna2TextBox_CedulaProductor.Text = "";

            productoresEntidad = new ProductoresEntidad();
            listaRecepcion.Clear();
            recepcionEntidad = new RecepcionEntidad();
            dataGridView_ProductosDetalle.DataSource = null;
            CargarBodegas();
        }
        private void dataGridView_ProductosDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int productoEliminar = Convert.ToInt32(dataGridView_ProductosDetalle.Rows[e.RowIndex].Cells["CodProducto"].Value.ToString());
                EliminarProducto(productoEliminar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error no ha seleccionado una fila valida" +
                    " " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarProducto(int productoEliminar)
        {
            if (MessageBox.Show("Esta seguro de eliminar este producto", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                 == DialogResult.OK)
            {
                DetalleRecepcionEntidad recepcionEntidadBuscado = new DetalleRecepcionEntidad();
                foreach (var item in listaRecepcion)
                {
                    if (item.CodProducto == productoEliminar)
                    {
                        recepcionEntidadBuscado = item;
                        break;
                    }
                }
                listaRecepcion.Remove(recepcionEntidadBuscado);
                CargarDatosProductoDetalle();
            }
        }

        #endregion

        private void guna2TextBox_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

    }
}
