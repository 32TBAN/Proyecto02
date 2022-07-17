using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Entidades;
namespace Presentacion
{
    public partial class Almacen : Form
    {
        public Almacen()
        {
            InitializeComponent();
        }

        private void Almacen_Load(object sender, EventArgs e)
        {
            CargarComponentes();
        }

        private void CargarComponentes()
        {
            dataGridView_Bodegas.DataSource = BodegaNegocio.DevolverBodegas();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AgregarBodega agregarBodega = new AgregarBodega();
            agregarBodega.Show();
            agregarBodega.FormClosed += CerrarMenuBodega;
        }
        private void CerrarMenuBodega(object sender, FormClosedEventArgs e)
        {
            CargarComponentes();
        }
        private void dataGridView_Bodegas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var numBodega = Convert.ToInt32(dataGridView_Bodegas.Rows[e.RowIndex].Cells["Num"].Value.ToString());
                CargarContenidoBodega(numBodega);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void CargarContenidoBodega(int numBodega)
        {

            dataGridView_Contenido.DataSource = BodegaNegocio.ListaContenidoBodega(numBodega);
            dataGridView_Contenido.Columns["NumRecepcion"].Visible = false;
            dataGridView_Contenido.Columns["CodProducto"].Visible = false;
            dataGridView_Contenido.Columns["Subtotal"].Visible = false;
            dataGridView_Contenido.Columns["NumBodega"].Visible = false;
        }
    }
}
