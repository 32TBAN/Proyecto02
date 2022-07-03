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
            iconButton_InfoPersonal.Visible = false;
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
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea cerrar sesion?","Cerrar",
                MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question) == DialogResult.Yes)
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
            AbrirFormularios<Productos>();
            label_Lugar.Text = "Ingreso de Productos";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Almacen>();
            label_Lugar.Text = "Bodegas";
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Venta>();
            label_Lugar.Text = "Venta";
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            AbrirFormularios<Reporte>();
            label_Lugar.Text = "Reportes";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label_Lugar.Text = "Bienvenid@";
        }

        public void CerrarFoemularios<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panel_Contenedor.Controls.OfType<MiForm>().FirstOrDefault();

            if (formulario != null)
            {
                formulario = new MiForm();
                panel_Contenedor.Controls.Remove(formulario);
                panel_Contenedor.Tag = formulario;
                formulario.Show();
            }

        }
    }
}
