using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Login : Form
    {

        #region Diseño
        private int bordesRadio = 30;
        private int bordeTamano = 2;
        private Color coloreBorede = Color.RoyalBlue;
        #endregion
        public Login()
        {
            InitializeComponent();
            this.Padding = new Padding(bordeTamano);
        }
        #region Diseño
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCaprure();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsq, int wParam, int lParam);
        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCaprure();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = 0x20000;
                return cp;
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curceSive = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curceSive, curceSive, 180, 90);
            path.AddArc(rect.Right - curceSive, rect.Y, curceSive, curceSive, 270, 90);
            path.AddArc(rect.Right - curceSive, rect.Bottom - curceSive, curceSive, curceSive, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curceSive, curceSive, curceSive, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void FormRegionAndBorder(Form form, float radius, Graphics graphics, Color bodeColor, float bordeSize)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                using (GraphicsPath roundPath = GetRoundedPath(form.ClientRectangle, radius))
                using (Pen penBoder = new Pen(bodeColor, bordeSize))
                using (Matrix transform = new Matrix())
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    form.Region = new Region(roundPath);
                    if (bordeSize >= 1)
                    {
                        Rectangle rect = form.ClientRectangle;
                        float scaleX = 1.0F - ((bordeSize + 1) / rect.Width);
                        float scaleY = 1.0F - ((bordeSize + 1) / rect.Height);

                        transform.Scale(scaleX, scaleY);
                        transform.Translate(bordeSize / 1.6F, bordeSize / 1.6F);

                        graphics.Transform = transform;
                        graphics.DrawPath(penBoder, roundPath);
                    }
                }
            }
        }
        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            FormRegionAndBorder(this, bordesRadio, e.Graphics, coloreBorede, bordeTamano);

        }


        #endregion

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //if (guna2TextBox_Usuario.Text != "" && guna2TextBox_Contrasenia.Text != "")
            //{
                Inicio inicio = new Inicio();
                inicio.Show();
                inicio.FormClosed += CerrarSesion;
                this.Hide();
            //}
        }
        private void CerrarSesion(object sender, FormClosedEventArgs e)
        {
            guna2TextBox_Usuario.Clear();
            guna2TextBox_Contrasenia.Clear();
            this.Show();
            guna2TextBox_Usuario.Focus();
        }
    }
}
