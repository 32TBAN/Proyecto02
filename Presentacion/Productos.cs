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
        int capacidadBodega = 0;
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
                DetalleRecepcionEntidad detalleRecepcion = new DetalleRecepcionEntidad();
                detalleRecepcion.NumRecepcion = recepcionEntidad.NumRec;
                detalleRecepcion.CodProducto = ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Id;
                detalleRecepcion.NombreProducto = ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Nombre;
                detalleRecepcion.Cantidad = Convert.ToInt32(guna2NumericUpDown_Cantidad.Value);
                detalleRecepcion.Subtotal = ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio *
                    detalleRecepcion.Cantidad;
                detalleRecepcion.NumBodega = ((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num;

                capacidadBodega -= detalleRecepcion.Cantidad;

                bool aumento = false;
                foreach (var item in listaRecepcion)
                {
                    if (item.CodProducto == detalleRecepcion.CodProducto)
                    {
                        var cantidadAnterior = item.Cantidad;
                        item.Cantidad += detalleRecepcion.Cantidad;
                        item.Subtotal = (item.Subtotal*item.Cantidad)/cantidadAnterior;
                        aumento = true;
                    }
                }
                if (!aumento)
                    listaRecepcion.Add(detalleRecepcion);

                CargarDatosProductoDetalle();
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
            else if (capacidadBodega < guna2NumericUpDown_Cantidad.Value)
            {
                MessageBox.Show("La capacidad de la bodega es: " +capacidadBodega+"kg si necesita una cantidad mayor seleccione otra bodega");
                return false;
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

        #region Recepcion
        private void ComenzarRecepcion()
        {
            recepcionEntidad = new RecepcionEntidad();
            recepcionEntidad.IdUsuario = 1;
            recepcionEntidad.IdProductor = productoresEntidad.Id;
            recepcionEntidad.FechaRecepcion = DateTime.UtcNow;
        }
        #endregion

        #region FinalizarCompra
        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (ComprobarDatosEnviar())
            {
                FinalizarCompraProductos();
                ComprobarCapacidadBodegas();
                Nuevo();
            }
        }

        private bool ComprobarDatosEnviar()
        {   
            if (productoresEntidad == null)
            {
                MessageBox.Show("Error en los datos del producto");
                return false;
            }
            else if (listaRecepcion.Count() == 0)
            {
                MessageBox.Show("Error no ha ingresado ningun producto");
                return false;
            }
            else if (recepcionEntidad == null)
            {
                MessageBox.Show("Error al realizar la recepcion");
                return false;
            }
            else if (ComprobarCapacidadBodegas())
            {
                MessageBox.Show("Error no se ha finalizado la compra");
                return false;
            }
            return true;
        }

        private bool ComprobarCapacidadBodegas()
        {
            foreach (var item in listaRecepcion)
            {
              BodegaEntidad bodegaEntidad = BodegaNegocio.BuscarBodega(item.NumBodega);
                if (item.Cantidad > bodegaEntidad.Capacidad)
                {
                    MessageBox.Show("Se ha superado la capacidad de la bodega num: "+ item.NumBodega +
                        "\nContacte con el administrador");
                    return true;
                }
            }
            return false;
        }

        private void FinalizarCompraProductos()
        {
            CargarDatosRecepcion();
            CargarDatosProductos();
        }
        private void CargarDatosRecepcion()
        {
            recepcionEntidad.Total = Convert.ToSingle(guna2TextBox_Total.Text);
            recepcionEntidad = RecepcionNegocio.ComenzarRecepcion(recepcionEntidad);
            if (recepcionEntidad == null)
            {
                MessageBox.Show("Error al realizar la recepcion");
            }
        }
        private void CargarDatosProductos()
        {
            foreach (var item in listaRecepcion)
            {
                item.NumRecepcion = recepcionEntidad.NumRec;
            }
            if (RecepcionNegocio.AgregarProductoDetalle(listaRecepcion))
            {
                MessageBox.Show("Se ha agregado producto");
            }
            else
            {
                MessageBox.Show("Error al agregar producto");
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
            capacidadBodega = ((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Capacidad;
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
            capacidadBodega = ((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Capacidad;
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

            productoresEntidad = null;
            listaRecepcion.Clear();
            recepcionEntidad = null;
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
                        capacidadBodega += item.Cantidad;
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
    }
}
