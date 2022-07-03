using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Opciones : Form
    {
        public Opciones()
        {
            InitializeComponent();
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
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            CambioarImagen(sender);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
