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
    public partial class AgregarProducto : Form
    {
        ProductosEntidad productosEntidad = new ProductosEntidad();
        public AgregarProducto()
        {
            InitializeComponent();
        }
        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            CargarComponentes();
        }
        private void CargarComponentes()
        {
            dataGridView1.DataSource = ProductoNegocio.DevolverProductos();
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_NombreProducto.Text != "" || !guna2NumericUpDown_Precio.Value.Equals(0))
            {
                productosEntidad.Nombre = guna2TextBox_NombreProducto.Text;
                productosEntidad.Precio = (float) guna2NumericUpDown_Precio.Value;

                productosEntidad = ProductoNegocio.Guardar(productosEntidad);
                if (productosEntidad != null)
                {
                    MessageBox.Show("Se ha guardado");
                    CargarComponentes();
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                }
            }
            else
            {
                MessageBox.Show("Datos no validos");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            EliminarProducto(id);
        }

        private void EliminarProducto(int id)
        {
            productosEntidad = ProductoNegocio.BuscarProducto(id);
            if (MessageBox.Show("Desea eliminar producto:\n" + productosEntidad.Id.ToString() + " - " +
                productosEntidad.Nombre.ToString() + " - " + productosEntidad.Precio.ToString(), "Eliminar producto"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ProductoNegocio.EliminarProducto(id))
                {
                    MessageBox.Show("Se ha eliminado");
                    CargarComponentes();
                }
                else
                {
                    MessageBox.Show("No se ha podido eliminar");
                }
            }
        }

    }
}
