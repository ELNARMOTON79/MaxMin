namespace MaxMin
{
    partial class menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_max = new Label();
            btn_simplex = new Button();
            btn_m = new Button();
            btn_grafica = new Button();
            SuspendLayout();
            // 
            // menu (form)
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ClientSize = new Size(1600, 1000);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "menu";
            this.Text = "MaxMin";
            this.Load += menu_Load;
            // 
            // lbl_max
            // 
            lbl_max.AutoSize = true;
            lbl_max.BackColor = Color.Transparent;
            lbl_max.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lbl_max.ForeColor = Color.White;
            lbl_max.Location = new Point(650, 80);
            lbl_max.Name = "lbl_max";
            lbl_max.Size = new Size(350, 70);
            lbl_max.TabIndex = 0;
            lbl_max.Text = "Max Min";
            // 
            // btn_simplex
            // 
            btn_simplex.BackColor = Color.FromArgb(60, 80, 150);
            btn_simplex.FlatStyle = FlatStyle.Flat;
            btn_simplex.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btn_simplex.ForeColor = Color.White;
            btn_simplex.Location = new Point(600, 200);
            btn_simplex.Name = "btn_simplex";
            btn_simplex.Size = new Size(400, 80);
            btn_simplex.TabIndex = 1;
            btn_simplex.Text = "Método Simplex";
            btn_simplex.UseVisualStyleBackColor = false;
            // 
            // btn_m
            // 
            btn_m.BackColor = Color.FromArgb(60, 80, 150);
            btn_m.FlatStyle = FlatStyle.Flat;
            btn_m.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btn_m.ForeColor = Color.White;
            btn_m.Location = new Point(600, 320);
            btn_m.Name = "btn_m";
            btn_m.Size = new Size(400, 80);
            btn_m.TabIndex = 2;
            btn_m.Text = "Método de la M";
            btn_m.UseVisualStyleBackColor = false;
            // 
            // btn_grafica
            // 
            btn_grafica.BackColor = Color.FromArgb(60, 80, 150);
            btn_grafica.FlatStyle = FlatStyle.Flat;
            btn_grafica.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btn_grafica.ForeColor = Color.White;
            btn_grafica.Location = new Point(600, 440);
            btn_grafica.Name = "btn_grafica";
            btn_grafica.Size = new Size(400, 80);
            btn_grafica.TabIndex = 3;
            btn_grafica.Text = "Gráfica de Programación Lineal";
            btn_grafica.UseVisualStyleBackColor = false;
            btn_grafica.Click += btn_grafica_Click;
            // 
            // menu (add controls)
            // 
            this.Controls.Add(lbl_max);
            this.Controls.Add(btn_simplex);
            this.Controls.Add(btn_m);
            this.Controls.Add(btn_grafica);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lbl_max;
        private Button btn_simplex;
        private Button btn_grafica;
        private Button btn_m;
    }
}
