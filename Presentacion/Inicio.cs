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
using System.IO;

namespace Presentacion
{
    public partial class Inicio : Form
    {
        public UsuarioEntidad usuarioEntidad { get; set; }
        public Form formAbierto = null;
        public void CargarDatosUsuario()
        {
            guna2TextBox_Nombre.Text = usuarioEntidad.Nickname;
            guna2TextBox_Email.Text = usuarioEntidad.Email;
            guna2TextBox_Cedula.Text = usuarioEntidad.Cedula;
            guna2TextBox_Privilegio.Text = usuarioEntidad.Tipo;
            label_Lugar.Text += " "+usuarioEntidad.Nombre;

            if (usuarioEntidad.Tipo == "A")
            {
                guna2GradientButton_Agregar.Visible = true;
            }
            if (usuarioEntidad.FotoPerfil != null)
            {
                guna2CirclePictureBox_FotoPerfil.Image = CargarImgen(usuarioEntidad.FotoPerfil);
                pictureBox_Referencia.Image = guna2CirclePictureBox_FotoPerfil.Image;
            }
        }

        private Image CargarImgen(byte[] fotoPerfil)
        {
            MemoryStream img = new MemoryStream(fotoPerfil);
            return Image.FromStream(img);
        }

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
            guna2GradientButton_Agregar.Visible = false;
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

        public void AbrirFormularios(Form form)
        {
            if (formAbierto != null)
            {
                formAbierto.Close();
            }
            formAbierto = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel_Contenedor.Controls.Add(form);
            panel_Contenedor.Tag = form;
            form.BringToFront();
            form.Show();
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
            Recepcion recepcion = new Recepcion(usuarioEntidad);
            AbrirFormularios(recepcion);
            label_Lugar.Text = "Ingreso de Productos";
            pictureBox_Referencia.Image = pictureBox_Producto.Image;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Almacen almacen = new Almacen(usuarioEntidad);
            AbrirFormularios(almacen);
            label_Lugar.Text = "Bodegas";
            pictureBox_Referencia.Image = pictureBox_Bodegas.Image;

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Venta venta = new Venta(usuarioEntidad);
            AbrirFormularios(venta);
            label_Lugar.Text = "Venta";
            pictureBox_Referencia.Image = pictureBox_Ventas.Image;

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            Reporte reporte = new Reporte();
            AbrirFormularios(reporte);
            label_Lugar.Text = "Reportes";
            pictureBox_Referencia.Image = pictureBox_Reporte.Image;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label_Lugar.Text = "Bienvenid@ "+usuarioEntidad.Nombre;
            CerrarFormularios();
            pictureBox_Referencia.Image = CargarImgen(usuarioEntidad.FotoPerfil);
            CargarValorGrafica(10);
        }

        public void CerrarFormularios()
        {
            if (formAbierto != null)
            {
                formAbierto.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Recepcion recepcion = new Recepcion(usuarioEntidad);
            AbrirFormularios(recepcion);
            label_Lugar.Text = "Ingreso de Productos";
            pictureBox_Referencia.Image = pictureBox_Producto.Image;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Almacen almacen = new Almacen(usuarioEntidad);
            AbrirFormularios(almacen);
            label_Lugar.Text = "Bodegas";
            pictureBox_Referencia.Image = pictureBox_Bodegas.Image;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Venta venta = new Venta(usuarioEntidad);
            AbrirFormularios(venta);
            label_Lugar.Text = "Venta";
            pictureBox_Referencia.Image = pictureBox_Ventas.Image;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Reporte reporte = new Reporte();
            AbrirFormularios(reporte);
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

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_Nombre.Text != "" || guna2TextBox_Email.Text != "")
            {
                UsuarioEntidad usuarioEntidadActualizar = usuarioEntidad;
                usuarioEntidadActualizar.Nickname = guna2TextBox_Nombre.Text;
                usuarioEntidadActualizar.Email = guna2TextBox_Email.Text;
                usuarioEntidadActualizar.FotoPerfil = CargarImagen();

                usuarioEntidadActualizar = UsuarioNegocio.Guardar(usuarioEntidadActualizar);
                if (usuarioEntidadActualizar != null)
                {
                    MessageBox.Show("Se ha guardado");
                    usuarioEntidad = usuarioEntidadActualizar;
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                    UsuarioEntidad usuarioEntidadBuscado = UsuarioNegocio.BuscarUsuarioNickname(guna2TextBox_Nombre.Text);
                    if (usuarioEntidadBuscado != null && usuarioEntidadBuscado.Nickname == guna2TextBox_Nombre.Text)
                    {
                        MessageBox.Show("Ese nickname ya existe");
                        guna2TextBox_Nombre.Text = "";
                    }
                    usuarioEntidadActualizar = usuarioEntidad;
                }
            }
        }
        private byte[] CargarImagen()
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            guna2CirclePictureBox_FotoPerfil.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }
        private void guna2GradientButton_Agregar_Click(object sender, EventArgs e)
        {
            AgregarUsuario agregarUsuario = new AgregarUsuario();
            agregarUsuario.Show();
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            DialogResult dr = of.ShowDialog();
            if (dr == DialogResult.OK)
            {
                guna2CirclePictureBox_FotoPerfil.Image = Image.FromFile(of.FileName);
            }
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            guna2TextBox_Cedula.Focus();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            guna2TextBox_Email.Focus();
        }
    }
}
