using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Entidades;
namespace Presentacion
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            IniciarComponentes();
        }

        private void IniciarComponentes()
        {
            Componetes();
        }

        private void Componetes()
        {
            CargarValorGrafica(10);
            iconButton_InfoPersonal.Visible = false;
        }

        private void CargarValorGrafica(int valor)
        {
            chart_Grafica.DataSource = BodegaNegocio.ListaCantidadProductoBodega(valor);
            chart_Grafica.Series[0].XValueMember = "NumBodega";
            chart_Grafica.Series[0].YValueMembers = "Cantidad";
            chart_Grafica.DataBind();
        }
        #region Diseño
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCaprure();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsq, int wParam, int lParam);
        private void guna2CustomGradientPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCaprure();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
        #region FuncionBotones
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            guna2CustomGradientPanel_InfoPersonal.Visible = false;
        }

        public void AbrirFormularios<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panel_Contenedor.Controls.OfType<MiForm>().FirstOrDefault();

            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.Dock = DockStyle.Fill;
                panel_Contenedor.Controls.Add(formulario);
                panel_Contenedor.Tag = formulario;
                formulario.Show();
            }
            else
            {
                formulario.BringToFront();
            }
            guna2CustomGradientPanel_InfoPersonal.Visible = false;
            iconButton_InfoPersonal.Visible = true;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea cerrar sesion?", "Cerrar",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void iconButton_CerrarInfo_Click(object sender, EventArgs e)
        {
            guna2CustomGradientPanel_InfoPersonal.Visible = false;
            iconButton_InfoPersonal.Visible = true;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            guna2CustomGradientPanel_InfoPersonal.Visible = true;
            iconButton_InfoPersonal.Visible = false;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Recepcion>();
            label_Lugar.Text = "Ingreso de Productos";
            pictureBox_Referencia.Image = pictureBox_Producto.Image;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Almacen>();
            label_Lugar.Text = "Bodegas";
            pictureBox_Referencia.Image = pictureBox_Bodegas.Image;

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Venta>();
            label_Lugar.Text = "Venta";
            pictureBox_Referencia.Image = pictureBox_Ventas.Image;

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Reporte>();
            label_Lugar.Text = "Reportes";
            pictureBox_Referencia.Image = pictureBox_Reporte.Image;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label_Lugar.Text = "Bienvenid@";
            CerrarFormularios<Recepcion>();
            CerrarFormularios<Venta>();
            CerrarFormularios<Almacen>();
            CerrarFormularios<Reporte>();
            pictureBox_Referencia.Image = null;
            CargarValorGrafica(10);
        }

        public void CerrarFormularios<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panel_Contenedor.Controls.OfType<MiForm>().FirstOrDefault();

            if (formulario != null)
            {
                formulario.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Recepcion>();
            label_Lugar.Text = "Ingreso de Productos";
            pictureBox_Referencia.Image = pictureBox_Producto.Image;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Almacen>();
            label_Lugar.Text = "Bodegas";
            pictureBox_Referencia.Image = pictureBox_Bodegas.Image;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Venta>();
            label_Lugar.Text = "Venta";
            pictureBox_Referencia.Image = pictureBox_Ventas.Image;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Reporte>();
            label_Lugar.Text = "Reportes";
            pictureBox_Referencia.Image = pictureBox_Reporte.Image;

        }

        private void CambioarImagen(object img)
        {
            guna2Transition1.HideSync(pictureBox_Principal);
            pictureBox_Principal.Image = ((PictureBox)img).Image;
            guna2Transition1.ShowSync(pictureBox_Principal);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
            CargarValorGrafica(8);

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
            CargarValorGrafica(1);

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
            CargarValorGrafica(3);

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
            CargarValorGrafica(9);

        }
        #endregion

    }
}
