namespace MaxMin
{
    partial class metodo_m
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles
        private System.Windows.Forms.Label lblFuncionObjetivo;
        private System.Windows.Forms.TextBox txtFuncionObjetivo;
        private System.Windows.Forms.Label lblRestricciones;
        private System.Windows.Forms.TextBox txtRestricciones;
        private System.Windows.Forms.Button btnResolver;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.TabControl tabIteraciones;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblMaxIter;
        private System.Windows.Forms.NumericUpDown nudMaxIter;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.Panel panelTop;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblFuncionObjetivo = new System.Windows.Forms.Label();
            this.txtFuncionObjetivo = new System.Windows.Forms.TextBox();
            this.lblRestricciones = new System.Windows.Forms.Label();
            this.txtRestricciones = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblMaxIter = new System.Windows.Forms.Label();
            this.nudMaxIter = new System.Windows.Forms.NumericUpDown();
            this.btnResolver = new System.Windows.Forms.Button();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.tabIteraciones = new System.Windows.Forms.TabControl();
            this.lblResultado = new System.Windows.Forms.Label();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIter)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop (estético)
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(34, 45, 65);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 60;
            // 
            // lblFuncionObjetivo
            // 
            this.lblFuncionObjetivo.AutoSize = true;
            this.lblFuncionObjetivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFuncionObjetivo.ForeColor = System.Drawing.Color.White;
            this.lblFuncionObjetivo.Location = new System.Drawing.Point(20, 75);
            this.lblFuncionObjetivo.Name = "lblFuncionObjetivo";
            this.lblFuncionObjetivo.Size = new System.Drawing.Size(150, 19);
            this.lblFuncionObjetivo.Text = "Función Objetivo (Z):";
            // 
            // txtFuncionObjetivo
            // 
            this.txtFuncionObjetivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFuncionObjetivo.Location = new System.Drawing.Point(180, 72);
            this.txtFuncionObjetivo.Size = new System.Drawing.Size(620, 25);
            this.txtFuncionObjetivo.Name = "txtFuncionObjetivo";
            this.txtFuncionObjetivo.PlaceholderText = "Ej: 3x1 + 5x2  (selecciona Maximizar o Minimizar)";
            // 
            // lblRestricciones
            // 
            this.lblRestricciones.AutoSize = true;
            this.lblRestricciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRestricciones.ForeColor = System.Drawing.Color.Black;
            this.lblRestricciones.Location = new System.Drawing.Point(20, 110);
            this.lblRestricciones.Name = "lblRestricciones";
            this.lblRestricciones.Size = new System.Drawing.Size(105, 19);
            this.lblRestricciones.Text = "Restricciones:";
            // 
            // txtRestricciones
            // 
            this.txtRestricciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRestricciones.Location = new System.Drawing.Point(180, 110);
            this.txtRestricciones.Multiline = true;
            this.txtRestricciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRestricciones.Size = new System.Drawing.Size(620, 120);
            this.txtRestricciones.Name = "txtRestricciones";
            this.txtRestricciones.PlaceholderText = "Una restricción por línea. Ej: 2x1 + x2 <= 10";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipo.Location = new System.Drawing.Point(20, 245);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(95, 15);
            this.lblTipo.Text = "Tipo problema:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTipo.Location = new System.Drawing.Point(120, 240);
            this.cmbTipo.Size = new System.Drawing.Size(130, 23);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Items.AddRange(new object[] { "Maximizar", "Minimizar" });
            this.cmbTipo.SelectedIndex = 0;
            // 
            // lblMaxIter
            // 
            this.lblMaxIter.AutoSize = true;
            this.lblMaxIter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaxIter.Location = new System.Drawing.Point(270, 245);
            this.lblMaxIter.Name = "lblMaxIter";
            this.lblMaxIter.Size = new System.Drawing.Size(120, 15);
            this.lblMaxIter.Text = "Máx. iteraciones:";
            // 
            // nudMaxIter
            // 
            this.nudMaxIter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudMaxIter.Location = new System.Drawing.Point(390, 240);
            this.nudMaxIter.Minimum = 1;
            this.nudMaxIter.Maximum = 500;
            this.nudMaxIter.Value = 20;
            this.nudMaxIter.Name = "nudMaxIter";
            this.nudMaxIter.Size = new System.Drawing.Size(80, 23);
            // 
            // btnResolver
            // 
            this.btnResolver.BackColor = System.Drawing.Color.FromArgb(17, 90, 149);
            this.btnResolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResolver.ForeColor = System.Drawing.Color.White;
            this.btnResolver.Location = new System.Drawing.Point(500, 235);
            this.btnResolver.Name = "btnResolver";
            this.btnResolver.Size = new System.Drawing.Size(140, 30);
            this.btnResolver.Text = "Resolver";
            this.btnResolver.UseVisualStyleBackColor = false;
            this.btnResolver.Click += new System.EventHandler(this.btnResolver_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.Gray;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRegresar.ForeColor = System.Drawing.Color.White;
            this.btnRegresar.Location = new System.Drawing.Point(660, 235);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(140, 30);
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // tabIteraciones
            // 
            this.tabIteraciones.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabIteraciones.Location = new System.Drawing.Point(20, 280);
            this.tabIteraciones.Name = "tabIteraciones";
            this.tabIteraciones.SelectedIndex = 0;
            this.tabIteraciones.Size = new System.Drawing.Size(640, 300);
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxLogs.Location = new System.Drawing.Point(670, 280);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(260, 300);
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResultado.Location = new System.Drawing.Point(20, 590);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(300, 19);
            this.lblResultado.Text = "Resultado de Z y variables: (presiona Resolver)";
            // 
            // metodo_m
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(960, 630);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblFuncionObjetivo);
            this.Controls.Add(this.txtFuncionObjetivo);
            this.Controls.Add(this.lblRestricciones);
            this.Controls.Add(this.txtRestricciones);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.lblMaxIter);
            this.Controls.Add(this.nudMaxIter);
            this.Controls.Add(this.btnResolver);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.tabIteraciones);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.lblResultado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "metodo_m";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Método M - Big M (Simplex)";
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIter)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
