﻿using System;
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
        List<ConsultaStockProductos> consultaStocks = new List<ConsultaStockProductos>();
        List<ConsultaTopProductos> consultaTopProductos = new List<ConsultaTopProductos>();

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
            ConsultaPersonalizada(DateTime.Today.AddDays(-7), DateTime.Now);
        }
        private void ConsultaPersonalizada(DateTime inicio, DateTime fin)
        {
            if (inicio <= fin)
            {
                LimpiarListas();
                consultasEntidad.FechaInicio = inicio;
                consultasEntidad.FechaFin = fin;
                consultasEntidad.NumDias = (fin - inicio).Days;

                consultasEntidad = ConsultasNegocio.DevolverConsulta(consultasEntidad);
                consultasEntidadBindingSource.DataSource = consultasEntidad;

                listaConsulta1s = ConsultasNegocio.ConsultaMontoFecha(consultasEntidad);
                listaConsulta1BindingSource.DataSource = listaConsulta1s;

                consultaStocks = ConsultasNegocio.ConsultaStockFecha(consultasEntidad);
                consultaStockProductosBindingSource.DataSource = consultaStocks;

                consultaTopProductos = ConsultasNegocio.ConsultaTopProductos(consultasEntidad);
                consultaTopProductosBindingSource.DataSource = consultaTopProductos;
            }
            else
            {
                MessageBox.Show("Fecha incorrecta primeo 'Fecha menor' luego 'Fecha mayor'");
            }
          
        }

        private void LimpiarListas()
        {
            listaConsulta1s = new List<ListaConsulta1>();
            consultaStocks = new List<ConsultaStockProductos>();
            consultaTopProductos = new List<ConsultaTopProductos>();
            consultasEntidad = new ConsultasEntidad();
            this.reportViewer1.RefreshReport();

        }

        //private void ConsultaUltimos7dias()
        //{
        //    this.reportViewer1.RefreshReport();
        //    //reportViewer1.LocalReport.DataSources.Clear();
        //    consultasEntidad.FechaInicio = ;
        //    consultasEntidad.FechaFin = ;
        //    consultasEntidad.NumDias = 7;
        //    consultasEntidad = ConsultasNegocio.DevolverConsulta(consultasEntidad);
        //    consultasEntidadBindingSource.DataSource = consultasEntidad;
        //    consultasEntidad.FechaInicio = DateTime.UtcNow.AddDays(-7);
        //    consultasEntidad.FechaFin = DateTime.UtcNow;
        //    consultasEntidad.NumDias = 7;
        //    listaConsulta1s = ConsultasNegocio.ConsultaMontoFecha(consultasEntidad);
        //    listaConsulta1BindingSource.DataSource = listaConsulta1s;

        //    consultaStockProductosBindingSource.DataSource = ConsultasNegocio.ConsultaStockFecha(consultasEntidad);
        //    consultaTopProductosBindingSource.DataSource = ConsultasNegocio.ConsultaTopProductos(consultasEntidad);
        //    //ReportDataSource rd2 = new ReportDataSource("DataSet2", listaConsulta1s);
        //    //reportViewer1.LocalReport.DataSources.Add(rd2);
        //    //reportViewer1.LocalReport.ReportPath = @"D:\Programacion en C#\Proyecto\Presentacion\Reporte\Report1.rdlc";
        //}

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
            ConsultaPersonalizada(DateTime.Today,DateTime.Now);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
            ConsultaPersonalizada(DateTime.Today.AddDays(-7), DateTime.Now);
        }

        private void button_30Dias_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
            ConsultaPersonalizada(DateTime.Today.AddDays(-30), DateTime.Now);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AparienciaBoton(sender);
            ConsultaPersonalizada(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1), DateTime.Now);

        }

        private void button_Personalisar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = new DateTime(guna2DateTimePicker_Fecha1.Value.Year,
                guna2DateTimePicker_Fecha1.Value.Month, guna2DateTimePicker_Fecha1.Value.Day,0,0,0);

            DateTime fechaFin = new DateTime(guna2DateTimePicker_Fecha2.Value.Year,
            guna2DateTimePicker_Fecha2.Value.Month, guna2DateTimePicker_Fecha2.Value.Day, 
            DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            ConsultaPersonalizada(fechaInicio,fechaFin);
        }


    }
}
