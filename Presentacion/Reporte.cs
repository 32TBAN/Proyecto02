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
using Microsoft.Reporting.WinForms;
using Negocio;

namespace Presentacion
{
    public partial class Reporte : Form
    {
        private Button botonSeleccionado;
        ConsultasEntidad consultasEntidad = new ConsultasEntidad();
        List<ListaConsulta1> listaConsulta1s = new List<ListaConsulta1>(); 
        public Reporte()
        {
            InitializeComponent();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            CargarComponnetes();
        }

        private void CargarComponnetes()
        {
            AparienciaBoton(button4);
            ConsultaUltimos7dias();
        }

        private void ConsultaUltimos7dias()
        {
            this.reportViewer1.RefreshReport();
            //reportViewer1.LocalReport.DataSources.Clear();
            consultasEntidad.FechaInicio = DateTime.UtcNow.AddDays(-7);
            consultasEntidad.FechaFin = DateTime.UtcNow;
            consultasEntidad.NumDias = 7;
            consultasEntidad = ConsultasNegocio.DevolverConsulta(consultasEntidad);
            consultasEntidadBindingSource.DataSource = consultasEntidad;
            consultasEntidad.FechaInicio = DateTime.UtcNow.AddDays(-7);
            consultasEntidad.FechaFin = DateTime.UtcNow;
            consultasEntidad.NumDias = 7;
            listaConsulta1s = ConsultasNegocio.ConsultaMontoFecha(consultasEntidad.FechaInicio, consultasEntidad.FechaFin, consultasEntidad.NumDias);
            listaConsulta1BindingSource.DataSource = listaConsulta1s;

            consultaStockProductosBindingSource.DataSource = ConsultasNegocio.ConsultaStockFecha(consultasEntidad.FechaInicio, consultasEntidad.FechaFin);
            consultaTopProductosBindingSource.DataSource = ConsultasNegocio.ConsultaTopProductos(consultasEntidad.FechaInicio, consultasEntidad.FechaFin);
            //ReportDataSource rd2 = new ReportDataSource("DataSet2", listaConsulta1s);
            //reportViewer1.LocalReport.DataSources.Add(rd2);
            //reportViewer1.LocalReport.ReportPath = @"D:\Programacion en C#\Proyecto\Presentacion\Reporte\Report1.rdlc";
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
            ConsultaUltimos7dias();
        }

        private void button_30Dias_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
        }

        private void button_Personalisar_Click(object sender, EventArgs e)
        {
            ConsultaPersonalizada(guna2DateTimePicker_Fecha1.Value,guna2DateTimePicker_Fecha2.Value);
        }

        private void ConsultaPersonalizada(DateTime inicio, DateTime fin)
        {
            this.reportViewer1.RefreshReport();
            fin = new DateTime(fin.Year,fin.Month,fin.Day,fin.Hour,fin.Minute,59);
            consultasEntidad.FechaInicio = inicio;
            consultasEntidad.FechaFin = fin;
            consultasEntidad.NumDias = (inicio-fin).Days;

            consultasEntidad = ConsultasNegocio.DevolverConsulta(consultasEntidad);
            consultasEntidadBindingSource.DataSource = consultasEntidad;

            listaConsulta1s = ConsultasNegocio.ConsultaMontoFecha(consultasEntidad.FechaInicio, consultasEntidad.FechaFin, consultasEntidad.NumDias);
            listaConsulta1BindingSource.DataSource = listaConsulta1s;

            consultaStockProductosBindingSource.DataSource = ConsultasNegocio.ConsultaStockFecha(consultasEntidad.FechaInicio, consultasEntidad.FechaFin);
            consultaTopProductosBindingSource.DataSource = ConsultasNegocio.ConsultaTopProductos(consultasEntidad.FechaInicio, consultasEntidad.FechaFin);
        }
    }
}
