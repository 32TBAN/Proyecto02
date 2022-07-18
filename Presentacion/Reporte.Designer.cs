namespace Presentacion
{
    partial class Reporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2DateTimePicker_Fecha1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2DateTimePicker_Fecha2 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.button_Personalizado2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button_30Dias = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button_Personalisar = new System.Windows.Forms.Button();
            this.consultasEntidadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.consultasEntidadBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button_30Dias);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button_Personalizado2);
            this.panel1.Controls.Add(this.button_Personalisar);
            this.panel1.Controls.Add(this.guna2DateTimePicker_Fecha2);
            this.panel1.Controls.Add(this.guna2DateTimePicker_Fecha1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 39);
            this.panel1.TabIndex = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 26;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.consultasEntidadBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion.Reporte.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 40);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(981, 439);
            this.reportViewer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opciones de reporte";
            // 
            // guna2DateTimePicker_Fecha1
            // 
            this.guna2DateTimePicker_Fecha1.Checked = true;
            this.guna2DateTimePicker_Fecha1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(69)))), ((int)(((byte)(242)))));
            this.guna2DateTimePicker_Fecha1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2DateTimePicker_Fecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.guna2DateTimePicker_Fecha1.Location = new System.Drawing.Point(154, 9);
            this.guna2DateTimePicker_Fecha1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker_Fecha1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker_Fecha1.Name = "guna2DateTimePicker_Fecha1";
            this.guna2DateTimePicker_Fecha1.Size = new System.Drawing.Size(121, 21);
            this.guna2DateTimePicker_Fecha1.TabIndex = 1;
            this.guna2DateTimePicker_Fecha1.Value = new System.DateTime(2022, 7, 17, 20, 10, 38, 132);
            // 
            // guna2DateTimePicker_Fecha2
            // 
            this.guna2DateTimePicker_Fecha2.Checked = true;
            this.guna2DateTimePicker_Fecha2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(69)))), ((int)(((byte)(242)))));
            this.guna2DateTimePicker_Fecha2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2DateTimePicker_Fecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.guna2DateTimePicker_Fecha2.Location = new System.Drawing.Point(281, 9);
            this.guna2DateTimePicker_Fecha2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker_Fecha2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker_Fecha2.Name = "guna2DateTimePicker_Fecha2";
            this.guna2DateTimePicker_Fecha2.Size = new System.Drawing.Size(121, 21);
            this.guna2DateTimePicker_Fecha2.TabIndex = 2;
            this.guna2DateTimePicker_Fecha2.Value = new System.DateTime(2022, 7, 17, 20, 10, 38, 132);
            // 
            // button_Personalizado2
            // 
            this.button_Personalizado2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.button_Personalizado2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Personalizado2.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Personalizado2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.button_Personalizado2.Location = new System.Drawing.Point(455, 7);
            this.button_Personalizado2.Name = "button_Personalizado2";
            this.button_Personalizado2.Size = new System.Drawing.Size(114, 27);
            this.button_Personalizado2.TabIndex = 4;
            this.button_Personalizado2.Text = "Personalizado";
            this.button_Personalizado2.UseVisualStyleBackColor = true;
            this.button_Personalizado2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.button3.Location = new System.Drawing.Point(568, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 27);
            this.button3.TabIndex = 5;
            this.button3.Text = "Hoy";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.button4.Location = new System.Drawing.Point(630, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 27);
            this.button4.TabIndex = 6;
            this.button4.Text = "Ultimos 7 dias";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button_30Dias
            // 
            this.button_30Dias.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.button_30Dias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_30Dias.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_30Dias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.button_30Dias.Location = new System.Drawing.Point(746, 7);
            this.button_30Dias.Name = "button_30Dias";
            this.button_30Dias.Size = new System.Drawing.Size(141, 27);
            this.button_30Dias.TabIndex = 7;
            this.button_30Dias.Text = "Ultimos 30 dias";
            this.button_30Dias.UseVisualStyleBackColor = true;
            this.button_30Dias.Click += new System.EventHandler(this.button_30Dias_Click);
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(188)))), ((int)(((byte)(15)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.button6.Location = new System.Drawing.Point(883, 7);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(98, 27);
            this.button6.TabIndex = 8;
            this.button6.Text = "Este mes";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button_Personalisar
            // 
            this.button_Personalisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(242)))), ((int)(((byte)(73)))));
            this.button_Personalisar.FlatAppearance.BorderSize = 0;
            this.button_Personalisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Personalisar.Image = global::Presentacion.Properties.Resources.checkMin;
            this.button_Personalisar.Location = new System.Drawing.Point(424, 7);
            this.button_Personalisar.Name = "button_Personalisar";
            this.button_Personalisar.Size = new System.Drawing.Size(30, 27);
            this.button_Personalisar.TabIndex = 3;
            this.button_Personalisar.UseVisualStyleBackColor = false;
            // 
            // consultasEntidadBindingSource
            // 
            this.consultasEntidadBindingSource.DataSource = typeof(Entidades.ConsultasEntidad);
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(69)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(984, 480);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.Reporte_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.consultasEntidadBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker_Fecha2;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker_Fecha1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button_30Dias;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_Personalizado2;
        private System.Windows.Forms.Button button_Personalisar;
        private System.Windows.Forms.BindingSource consultasEntidadBindingSource;
    }
}