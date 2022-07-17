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
    public partial class AgregarBodega : Form
    {
        BodegaEntidad bodegaEntidad = new BodegaEntidad();
        public AgregarBodega()
        {
            InitializeComponent();
        }
        private void AgregarBodega_Load(object sender, EventArgs e)
        {
            CargarComponentes();
        }

        private void CargarComponentes()
        {
            dataGridView1.DataSource = BodegaNegocio.DevolverBodegas();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
           if (guna2TextBox_Bodega.Text != "" || !guna2NumericUpDown_Capacidad.Value.Equals(0))
            {
                bodegaEntidad.Ubicacion = guna2TextBox_Bodega.Text;
                bodegaEntidad.Capacidad = Convert.ToInt32(guna2NumericUpDown_Capacidad.Value);

                bodegaEntidad = BodegaNegocio.Guardar(bodegaEntidad);
                if (bodegaEntidad != null)
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
                MessageBox.Show("Datos Invalidos");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var num = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Num"].Value.ToString());
                BuscarBodega(num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BuscarBodega(int num)
        {
            bodegaEntidad = BodegaNegocio.BuscarBodega(num);
            guna2TextBox_Bodega.Text = bodegaEntidad.Ubicacion;
            guna2NumericUpDown_Capacidad.Value = bodegaEntidad.Capacidad;
        }

        private void EliminarBodega(int num)
        {
            bodegaEntidad = BodegaNegocio.BuscarBodega(num);
            if (MessageBox.Show("Desea eliminar bodega:\n"+bodegaEntidad.Num.ToString()+" - "+
                bodegaEntidad.Ubicacion.ToString()+" - "+bodegaEntidad.Capacidad.ToString(),"Eliminar bodega"
                ,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (BodegaNegocio.EliminarBodega(num))
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

        private void guna2Button_Elimianr_Click(object sender, EventArgs e)
        {
            if (bodegaEntidad.Num != 0)
            {
                EliminarBodega(bodegaEntidad.Num);
            }
            else
            {
                MessageBox.Show("Seleccione una bodega");
            }
        }
    }
}
