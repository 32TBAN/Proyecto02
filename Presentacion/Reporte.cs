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
    public partial class Reporte : Form
    {
        private Button botonSeleccionado;
        ConsultasEntidad consultasEntidad = new ConsultasEntidad();
        public Reporte()
        {
            InitializeComponent();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            CargarComponnetes();
        }

        private void CargarComponnetes()
        {
            AparienciaBoton(button4);
            ConsultaUltimos7dias();
        }

        private void ConsultaUltimos7dias()
        {
            consultasEntidad.FechaInicio = DateTime.UtcNow.AddDays(-7);
            consultasEntidad.FechaFin = DateTime.UtcNow;
            consultasEntidad.NumDias = 7;
            consultasEntidad = ConsultasNegocio.DevolverConsulta(consultasEntidad.FechaInicio, consultasEntidad.FechaFin);
            consultasEntidadBindingSource.DataSource = consultasEntidad;
        }

        private void AparienciaBoton(object boton)
        {
            var btn = (Button)boton;
            btn.BackColor = button_30Dias.FlatAppearance.BorderColor;
            btn.ForeColor = Color.White;
            if (botonSeleccionado != null && botonSeleccionado!= btn)
            {
                botonSeleccionado.BackColor = this.BackColor;
                botonSeleccionado.ForeColor = Color.FromArgb(242, 242, 242);
            }
            botonSeleccionado = btn;
            if (btn==button_Personalizado2)
            {
                guna2DateTimePicker_Fecha1.Enabled = true;
                guna2DateTimePicker_Fecha2.Enabled = true;
                button_Personalisar.Visible = true;
                guna2DateTimePicker_Fecha1.Cursor = Cursors.Hand;
                guna2DateTimePicker_Fecha2.Cursor = Cursors.Hand;
            }
            else
            {
                guna2DateTimePicker_Fecha1.Enabled = false;
                guna2DateTimePicker_Fecha2.Enabled = false;
                button_Personalisar.Visible = false;
                guna2DateTimePicker_Fecha1.Cursor = Cursors.Default;
                guna2DateTimePicker_Fecha2.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }

        private void button_30Dias_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }
    }
}
