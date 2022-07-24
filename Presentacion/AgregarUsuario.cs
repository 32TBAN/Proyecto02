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
using System.IO;
namespace Presentacion
{
    public partial class AgregarUsuario : Form
    {
        UsuarioEntidad usuarioEntidad = new UsuarioEntidad();
        public AgregarUsuario()
        {
            InitializeComponent();
            dataGridView_Contenido.DataSource = UsuarioNegocio.ListaUsuarios();
        }

        private void guna2GradientButton_Agregar_Click(object sender, EventArgs e)
        {
            if (ControlDatos())
            {
                usuarioEntidad.Cedula = guna2TextBox_Cedula.Text;
                usuarioEntidad.Nickname = guna2TextBox_Nickname.Text;
                usuarioEntidad.Nombre = guna2TextBox_Nombre.Text;
                usuarioEntidad.Email = guna2TextBox_Email.Text;
                usuarioEntidad.FotoPerfil = CargarImagen();
                if (guna2ComboBox_Privi.Text == "Administrador")
                {
                    usuarioEntidad.Tipo = "A";
                }
                else
                {
                    usuarioEntidad.Tipo = "N";
                }

                usuarioEntidad = UsuarioNegocio.Guardar(usuarioEntidad);
                if (usuarioEntidad != null)
                {
                    MessageBox.Show("Se ha guardado");
                    dataGridView_Contenido.DataSource = UsuarioNegocio.ListaUsuarios();
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                    UsuarioEntidad usuarioEntidadBuscado = UsuarioNegocio.BuscarUsuarioNickname(guna2TextBox_Nickname.Text);
                    if (usuarioEntidadBuscado != null && usuarioEntidadBuscado.Nickname == guna2TextBox_Nickname.Text)
                    {
                        MessageBox.Show("Ese nickname ya existe");
                        guna2TextBox_Nickname.Text = "";
                    }
                    usuarioEntidadBuscado = UsuarioNegocio.BuscarUsuario(guna2TextBox_Cedula.Text);
                    if (usuarioEntidadBuscado != null && usuarioEntidadBuscado.Cedula == guna2TextBox_Cedula.Text)
                    {
                        MessageBox.Show("El usuario ya existe");
                        guna2TextBox_Cedula.Text = "";
                    }
                    usuarioEntidad = new UsuarioEntidad();
                }
                
            }
        }

        private byte[] CargarImagen()
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            pictureBox_Foto.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }

        private bool ControlDatos()
        {
            if (guna2TextBox_Cedula.Text == "" || guna2TextBox_Nickname.Text == "" || guna2TextBox_Nombre.Text == ""||
                guna2TextBox_Email.Text == "")
            {
                MessageBox.Show("Faltan datos");
                return false;
            }
            return true;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            DialogResult dr =  of.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox_Foto.Image = Image.FromFile(of.FileName);
            }
        }

        private void dataGridView_Contenido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var cedula = dataGridView_Contenido.Rows[e.RowIndex].Cells["Cedula"].Value.ToString();
                CargarDatosUsuario(cedula);
            }
            catch (Exception)
            {
                MessageBox.Show("Escoja una fila correcta");
            }
        }

        private void CargarDatosUsuario(string cedula)
        {
            usuarioEntidad = UsuarioNegocio.BuscarUsuario(cedula);
            if (usuarioEntidad != null)
            {
                guna2TextBox_Cedula.Text = usuarioEntidad.Cedula;
                guna2TextBox_Nickname.Text = usuarioEntidad.Nickname;
                guna2TextBox_Nombre.Text = usuarioEntidad.Nombre;
                guna2TextBox_Email.Text = usuarioEntidad.Email;

                pictureBox_Foto.Image = CargarImgen(usuarioEntidad.FotoPerfil);
                if (usuarioEntidad.Tipo == "A")
                {
                    guna2ComboBox_Privi.Text = "Administrador";
                }
                else
                {
                    guna2ComboBox_Privi.Text = "Normal";
                }
            }
            else
            {
                MessageBox.Show("No se ha encontrado al usuario");

            }
        }

        private Image CargarImgen(byte[] fotoPerfil)
        {
            MemoryStream img = new MemoryStream(fotoPerfil);
            return Image.FromStream(img);
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            guna2TextBox_Cedula.Text = "";
            guna2TextBox_Nickname.Text = "";
            guna2TextBox_Nombre.Text = "";
            guna2TextBox_Email.Text = "";
            pictureBox_Foto.Image = Properties.Resources.avata;
            usuarioEntidad = new UsuarioEntidad();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (usuarioEntidad.IdUsuario != 0)
            {
                if (MessageBox.Show("Desea eliminar este usuario:\n" + usuarioEntidad.Cedula.ToString() + " - " +
                       usuarioEntidad.Nombre.ToString() + " - " + usuarioEntidad.Nickname.ToString(), "Eliminar bodega"
                       , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (UsuarioNegocio.EliminarUsuario(usuarioEntidad.IdUsuario))
                    {
                        MessageBox.Show("Se ha eliminado");
                        dataGridView_Contenido.DataSource = UsuarioNegocio.ListaUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido eliminar");
                    }
                }
            }
            else
            {
                MessageBox.Show("No se ha ingresado ningun usuario");
            }

        }
    }
}
