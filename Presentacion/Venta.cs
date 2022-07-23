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
        DetalleVentaEntidad detalleVentaEntidad = new DetalleVentaEntidad();
        List<DetalleVentaEntidad> listaDetalleVentas = new List<DetalleVentaEntidad>();
        public Venta()
        {
            InitializeComponent();
        }
        private void Venta_Load(object sender, EventArgs e)
        {
            ventaEntidad.Estado = true;
            CargarComponentes();
        }

        private void CargarComponentes()
        {
            CargarBodegas();
            CargarProductos();
        }

        private void CargarDataGrinview()
        {
            dataGridView_ProductosDetalle.DataSource = null;
            dataGridView_ProductosDetalle.DataSource = listaDetalleVentas;
            dataGridView_ProductosDetalle.Columns["NumVenta"].Visible = false;
            dataGridView_ProductosDetalle.Columns["CodProducto"].Visible = false;
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
                clienteEntidad = new ClienteEntidad();
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
            if (ventaEntidad.Estado)
            {
                ventaEntidad.IdUsuario = 1;
                ventaEntidad.IdCliente = clienteEntidad.Id;
                ventaEntidad.Total = Convert.ToSingle(label_Total.Text);
                ventaEntidad.Fecha = DateTime.UtcNow;
                ventaEntidad = VentaNegocio.ComenzarVenta(ventaEntidad);
            }
            else
            {
                ventaEntidad = new VentaEntidad();
                ventaEntidad.Estado = true;
                ventaEntidad.IdUsuario = 1;
                ventaEntidad.IdCliente = clienteEntidad.Id;
                ventaEntidad.Total = Convert.ToSingle(label_Total.Text);
                ventaEntidad.Fecha = DateTime.UtcNow;
                ventaEntidad = VentaNegocio.ComenzarVenta(ventaEntidad);
            }
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
                guna2TextBox_CedulaCliemte.Text = clienteEntidad.Cedula;
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
            listaDetalleVentas.Clear();
            dataGridView_ProductosDetalle.DataSource = null;
            label_Total.Text = "0";
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            GuardarDatosCliente();
        }

        private void guna2ComboBox_Bodega_SelectedValueChanged(object sender, EventArgs e)
        {
            DefinirUbicacion(((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AgregarProductoDetalle();
        }

        private void AgregarProductoDetalle()
        {
            if (ComprobacionDatosProducto())
            {
                listaDetalleVentas = VentaNegocio.DevolverDetalle(ventaEntidad.Num);

                detalleVentaEntidad.NumVenta = ventaEntidad.Num;
                detalleVentaEntidad.CodProducto = ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Id;
                detalleVentaEntidad.NumBodega = ((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num;

                if (listaDetalleVentas == null)
                {
                    detalleVentaEntidad.Cantidad = (int)guna2NumericUpDown_Cantidad.Value;
                    detalleVentaEntidad.Subtotal = (detalleVentaEntidad.Cantidad * ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio);
                    detalleVentaEntidad = VentaNegocio.AgregarDetalle(detalleVentaEntidad, true);
                }
                else
                {
                    bool encontrado = false;
                    foreach (var item in listaDetalleVentas)
                    {
                        if (item.CodProducto == detalleVentaEntidad.CodProducto)
                        {
                            detalleVentaEntidad.Cantidad = item.Cantidad+ (int)guna2NumericUpDown_Cantidad.Value;
                            detalleVentaEntidad.Subtotal = (detalleVentaEntidad.Cantidad * ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio);
                            detalleVentaEntidad = VentaNegocio.AgregarDetalle(detalleVentaEntidad, false);
                            encontrado = true;
                        }
                    }
                    if (!encontrado)
                    {
                        detalleVentaEntidad.Cantidad = (int)guna2NumericUpDown_Cantidad.Value;
                        detalleVentaEntidad.Subtotal = (detalleVentaEntidad.Cantidad * ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Precio);
                        detalleVentaEntidad = VentaNegocio.AgregarDetalle(detalleVentaEntidad, true);
                    }
                }

                if (detalleVentaEntidad != null)
                {
                    listaDetalleVentas = VentaNegocio.DevolverDetalle(ventaEntidad.Num);
                    CargarDataGrinview();
                    CargarTotal();
                }
                else
                {
                    MessageBox.Show("Error al agregar producto");
                }
            }
        }

        private void CargarTotal()
        {
            float total = 0;
            foreach (var item in listaDetalleVentas)
            {
                total += item.Subtotal;
            }
            label_Total.Text = total.ToString();
        }

        private bool ComprobacionDatosProducto()
        {
            if (guna2TextBox_CedulaCliemte.Text == "")
            {
                MessageBox.Show("Debe ingresar los datos de un cliente");
                return false;

            }
            else if (guna2NumericUpDown_Cantidad.Value.ToString() == "0")
            {
                MessageBox.Show("Escoja una cantidad valida");
                return false;
            }
            else {
                List<DetalleRecepcionEntidad> listaProductoCantidad = BodegaNegocio.ListaContenidoBodega(((BodegaEntidad)guna2ComboBox_Bodega.SelectedValue).Num);
                foreach (var item in listaProductoCantidad)
                {
                    if (item.NombreProducto == ((ProductosEntidad)guna2ComboBox_Producto.SelectedValue).Nombre)
                    {
                        if (item.Cantidad < guna2NumericUpDown_Cantidad.Value)
                        {
                            MessageBox.Show("El stock de la bodega es: " + item.Cantidad + "kg si necesita una cantidad mayor seleccione otra bodega");
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            ventaEntidad.Estado = false;
            ventaEntidad.Total = Convert.ToSingle(label_Total.Text);
            ventaEntidad = VentaNegocio.ComenzarVenta(ventaEntidad);
            if (ventaEntidad != null)
            {
                MessageBox.Show("Se ha finalizado la venta");
                Nuevo();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Nuevo();
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
                if (VentaNegocio.EliminarProductoDetalle(productoEliminar,ventaEntidad.Num))
                {
                    MessageBox.Show("Se ha eliminado");
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
        }
    }
}
