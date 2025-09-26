namespace MaxMin
{
    partial class metodo_m
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // --- DECLARACIONES DE CONTROLES ACTUALIZADAS ---
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbEntrada;
        private System.Windows.Forms.GroupBox gbConfiguracion;
        private System.Windows.Forms.GroupBox gbIteraciones;
        private System.Windows.Forms.GroupBox gbResultado;
        private System.Windows.Forms.DataGridView dgvFuncionObjetivo;
        private System.Windows.Forms.DataGridView dgvRestricciones;
        private System.Windows.Forms.TextBox txtResultadoFinal;
        private System.Windows.Forms.Label lblFuncionObjetivo;
        private System.Windows.Forms.Label lblRestricciones;
        private System.Windows.Forms.Button btnResolver;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.TabControl tabIteraciones;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Panel panelTop;
        // NUEVOS CONTROLES PARA LA VERSIÓN DINÁMICA
        private System.Windows.Forms.Label lblNumeroVariables;
        private System.Windows.Forms.NumericUpDown nudNumeroVariables;


        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblTitulo = new Label();
            gbEntrada = new GroupBox();
            lblRestricciones = new Label();
            lblFuncionObjetivo = new Label();
            dgvRestricciones = new DataGridView();
            dgvFuncionObjetivo = new DataGridView();
            gbConfiguracion = new GroupBox();
            nudNumeroVariables = new NumericUpDown();
            lblNumeroVariables = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            btnRegresar = new Button();
            btnResolver = new Button();
            gbIteraciones = new GroupBox();
            tabIteraciones = new TabControl();
            gbResultado = new GroupBox();
            txtResultadoFinal = new TextBox();
            panelTop.SuspendLayout();
            gbEntrada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRestricciones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFuncionObjetivo).BeginInit();
            gbConfiguracion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumeroVariables).BeginInit();
            gbIteraciones.SuspendLayout();
            gbResultado.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(29, 185, 84);
            panelTop.Controls.Add(lblTitulo);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1353, 80);
            panelTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 0, 0);
            lblTitulo.Location = new Point(514, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(351, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Resolución por Método M";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // gbEntrada
            // 
            gbEntrada.BackColor = Color.FromArgb(60, 63, 65);
            gbEntrada.Controls.Add(lblRestricciones);
            gbEntrada.Controls.Add(lblFuncionObjetivo);
            gbEntrada.Controls.Add(dgvRestricciones);
            gbEntrada.Controls.Add(dgvFuncionObjetivo);
            gbEntrada.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            gbEntrada.ForeColor = Color.Teal;
            gbEntrada.Location = new Point(27, 246);
            gbEntrada.Margin = new Padding(3, 4, 3, 4);
            gbEntrada.Name = "gbEntrada";
            gbEntrada.Padding = new Padding(3, 4, 3, 4);
            gbEntrada.Size = new Size(606, 480);
            gbEntrada.TabIndex = 2;
            gbEntrada.TabStop = false;
            gbEntrada.Text = "Entrada del Problema";
            gbEntrada.Enter += gbEntrada_Enter;
            // 
            // lblRestricciones
            // 
            lblRestricciones.AutoSize = true;
            lblRestricciones.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblRestricciones.Location = new Point(17, 163);
            lblRestricciones.Name = "lblRestricciones";
            lblRestricciones.Size = new Size(113, 23);
            lblRestricciones.TabIndex = 0;
            lblRestricciones.Text = "Restricciones:";
            // 
            // lblFuncionObjetivo
            // 
            lblFuncionObjetivo.AutoSize = true;
            lblFuncionObjetivo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblFuncionObjetivo.Location = new Point(17, 34);
            lblFuncionObjetivo.Name = "lblFuncionObjetivo";
            lblFuncionObjetivo.Size = new Size(255, 23);
            lblFuncionObjetivo.TabIndex = 1;
            lblFuncionObjetivo.Text = "Función Objetivo (Coeficientes):";
            // 
            // dgvRestricciones
            // 
            dgvRestricciones.BackgroundColor = Color.FromArgb(45, 45, 48);
            dgvRestricciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRestricciones.Location = new Point(21, 197);
            dgvRestricciones.Margin = new Padding(3, 4, 3, 4);
            dgvRestricciones.Name = "dgvRestricciones";
            dgvRestricciones.RowHeadersWidth = 62;
            dgvRestricciones.Size = new Size(560, 263);
            dgvRestricciones.TabIndex = 1;
            dgvRestricciones.DefaultValuesNeeded += dgvRestricciones_DefaultValuesNeeded;
            // 
            // dgvFuncionObjetivo
            // 
            dgvFuncionObjetivo.AllowUserToAddRows = false;
            dgvFuncionObjetivo.BackgroundColor = Color.FromArgb(45, 45, 48);
            dgvFuncionObjetivo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFuncionObjetivo.Location = new Point(21, 66);
            dgvFuncionObjetivo.Margin = new Padding(3, 4, 3, 4);
            dgvFuncionObjetivo.Name = "dgvFuncionObjetivo";
            dgvFuncionObjetivo.RowHeadersWidth = 62;
            dgvFuncionObjetivo.Size = new Size(560, 58);
            dgvFuncionObjetivo.TabIndex = 0;
            // 
            // gbConfiguracion
            // 
            gbConfiguracion.BackColor = Color.FromArgb(60, 63, 65);
            gbConfiguracion.Controls.Add(nudNumeroVariables);
            gbConfiguracion.Controls.Add(lblNumeroVariables);
            gbConfiguracion.Controls.Add(cmbTipo);
            gbConfiguracion.Controls.Add(lblTipo);
            gbConfiguracion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            gbConfiguracion.ForeColor = Color.Teal;
            gbConfiguracion.Location = new Point(27, 106);
            gbConfiguracion.Margin = new Padding(3, 4, 3, 4);
            gbConfiguracion.Name = "gbConfiguracion";
            gbConfiguracion.Padding = new Padding(3, 4, 3, 4);
            gbConfiguracion.Size = new Size(606, 114);
            gbConfiguracion.TabIndex = 1;
            gbConfiguracion.TabStop = false;
            gbConfiguracion.Text = "Configuración";
            gbConfiguracion.Enter += gbConfiguracion_Enter;
            // 
            // nudNumeroVariables
            // 
            nudNumeroVariables.Font = new Font("Segoe UI", 9F);
            nudNumeroVariables.Location = new Point(480, 56);
            nudNumeroVariables.Margin = new Padding(3, 4, 3, 4);
            nudNumeroVariables.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNumeroVariables.Name = "nudNumeroVariables";
            nudNumeroVariables.Size = new Size(98, 27);
            nudNumeroVariables.TabIndex = 3;
            nudNumeroVariables.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblNumeroVariables
            // 
            lblNumeroVariables.AutoSize = true;
            lblNumeroVariables.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblNumeroVariables.Location = new Point(320, 58);
            lblNumeroVariables.Name = "lblNumeroVariables";
            lblNumeroVariables.Size = new Size(174, 23);
            lblNumeroVariables.TabIndex = 4;
            lblNumeroVariables.Text = "Número de Variables:";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Font = new Font("Segoe UI", 9F);
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Items.AddRange(new object[] { "Maximizar", "Minimizar" });
            cmbTipo.Location = new Point(137, 56);
            cmbTipo.Margin = new Padding(3, 4, 3, 4);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(161, 28);
            cmbTipo.TabIndex = 2;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblTipo.Location = new Point(17, 58);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(125, 23);
            lblTipo.TabIndex = 5;
            lblTipo.Text = "Tipo Problema:";
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.FromArgb(220, 53, 69);
            btnRegresar.FlatAppearance.BorderSize = 0;
            btnRegresar.FlatStyle = FlatStyle.Flat;
            btnRegresar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegresar.ForeColor = Color.White;
            btnRegresar.Location = new Point(27, 746);
            btnRegresar.Margin = new Padding(3, 4, 3, 4);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(137, 54);
            btnRegresar.TabIndex = 4;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = false;
            btnRegresar.Click += btnRegresar_Click_1;
            // 
            // btnResolver
            // 
            btnResolver.BackColor = Color.FromArgb(29, 185, 84);
            btnResolver.FlatAppearance.BorderSize = 0;
            btnResolver.FlatStyle = FlatStyle.Flat;
            btnResolver.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnResolver.ForeColor = Color.White;
            btnResolver.Location = new Point(496, 746);
            btnResolver.Margin = new Padding(3, 4, 3, 4);
            btnResolver.Name = "btnResolver";
            btnResolver.Size = new Size(137, 54);
            btnResolver.TabIndex = 3;
            btnResolver.Text = "Resolver";
            btnResolver.UseVisualStyleBackColor = false;
            btnResolver.Click += btnResolver_Click;
            // 
            // gbIteraciones
            // 
            gbIteraciones.BackColor = Color.FromArgb(60, 63, 65);
            gbIteraciones.Controls.Add(tabIteraciones);
            gbIteraciones.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            gbIteraciones.ForeColor = Color.Teal;
            gbIteraciones.Location = new Point(657, 106);
            gbIteraciones.Margin = new Padding(3, 4, 3, 4);
            gbIteraciones.Name = "gbIteraciones";
            gbIteraciones.Padding = new Padding(3, 4, 3, 4);
            gbIteraciones.Size = new Size(669, 389);
            gbIteraciones.TabIndex = 5;
            gbIteraciones.TabStop = false;
            gbIteraciones.Text = "Tablas de Iteraciones";
            gbIteraciones.Enter += gbIteraciones_Enter;
            // 
            // tabIteraciones
            // 
            tabIteraciones.Dock = DockStyle.Fill;
            tabIteraciones.Font = new Font("Segoe UI", 9F);
            tabIteraciones.Location = new Point(3, 29);
            tabIteraciones.Margin = new Padding(3, 4, 3, 4);
            tabIteraciones.Name = "tabIteraciones";
            tabIteraciones.SelectedIndex = 0;
            tabIteraciones.Size = new Size(663, 356);
            tabIteraciones.TabIndex = 0;
            tabIteraciones.SelectedIndexChanged += tabIteraciones_SelectedIndexChanged;
            // 
            // gbResultado
            // 
            gbResultado.BackColor = Color.FromArgb(60, 63, 65);
            gbResultado.Controls.Add(txtResultadoFinal);
            gbResultado.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            gbResultado.ForeColor = Color.Teal;
            gbResultado.Location = new Point(657, 515);
            gbResultado.Margin = new Padding(3, 4, 3, 4);
            gbResultado.Name = "gbResultado";
            gbResultado.Padding = new Padding(3, 4, 3, 4);
            gbResultado.Size = new Size(669, 365);
            gbResultado.TabIndex = 6;
            gbResultado.TabStop = false;
            gbResultado.Text = "Resultado Final";
            // 
            // txtResultadoFinal
            // 
            txtResultadoFinal.BackColor = Color.FromArgb(45, 45, 48);
            txtResultadoFinal.BorderStyle = BorderStyle.None;
            txtResultadoFinal.Dock = DockStyle.Fill;
            txtResultadoFinal.Font = new Font("Consolas", 11.25F);
            txtResultadoFinal.ForeColor = Color.FromArgb(128, 255, 255);
            txtResultadoFinal.Location = new Point(3, 29);
            txtResultadoFinal.Margin = new Padding(3, 4, 3, 4);
            txtResultadoFinal.Multiline = true;
            txtResultadoFinal.Name = "txtResultadoFinal";
            txtResultadoFinal.ReadOnly = true;
            txtResultadoFinal.Size = new Size(663, 332);
            txtResultadoFinal.TabIndex = 0;
            txtResultadoFinal.Text = "Presiona \"Resolver\" para ver los resultados.";
            // 
            // metodo_m
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1353, 844);
            Controls.Add(gbResultado);
            Controls.Add(gbIteraciones);
            Controls.Add(btnResolver);
            Controls.Add(btnRegresar);
            Controls.Add(gbConfiguracion);
            Controls.Add(gbEntrada);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "metodo_m";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Método M - Big M (Simplex)";
            Load += metodo_m_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            gbEntrada.ResumeLayout(false);
            gbEntrada.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRestricciones).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFuncionObjetivo).EndInit();
            gbConfiguracion.ResumeLayout(false);
            gbConfiguracion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumeroVariables).EndInit();
            gbIteraciones.ResumeLayout(false);
            gbResultado.ResumeLayout(false);
            gbResultado.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}