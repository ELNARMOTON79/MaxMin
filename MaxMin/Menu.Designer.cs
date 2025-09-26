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
            // lbl_max
            // 
            lbl_max.AutoSize = true;
            lbl_max.BackColor = Color.Transparent;
            lbl_max.Font = new Font("Cooper Black", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_max.ForeColor = Color.Black;
            lbl_max.Location = new Point(507, 177);
            lbl_max.Name = "lbl_max";
            lbl_max.Size = new Size(188, 46);
            lbl_max.TabIndex = 0;
            lbl_max.Text = "Max Min";
            // 
            // btn_simplex
            // 
            btn_simplex.BackColor = SystemColors.Control;
            btn_simplex.FlatStyle = FlatStyle.Flat;
            btn_simplex.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_simplex.ForeColor = Color.Black;
            btn_simplex.Location = new Point(20, 30);
            btn_simplex.Name = "btn_simplex";
            btn_simplex.Padding = new Padding(20, 0, 0, 0);
            btn_simplex.Size = new Size(381, 102);
            btn_simplex.TabIndex = 0;
            btn_simplex.Text = "Método Simplex";
            btn_simplex.TextAlign = ContentAlignment.MiddleLeft;
            btn_simplex.UseVisualStyleBackColor = false;
            // 
            // btn_m
            // 
            btn_m.BackColor = SystemColors.Control;
            btn_m.FlatStyle = FlatStyle.Flat;
            btn_m.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_m.ForeColor = Color.Black;
            btn_m.Location = new Point(20, 155);
            btn_m.Name = "btn_m";
            btn_m.Padding = new Padding(20, 0, 0, 0);
            btn_m.Size = new Size(381, 116);
            btn_m.TabIndex = 1;
            btn_m.Text = "Método de la M";
            btn_m.TextAlign = ContentAlignment.MiddleLeft;
            btn_m.UseVisualStyleBackColor = false;
            // 
            // btn_grafica
            // 
            btn_grafica.BackColor = SystemColors.Control;
            btn_grafica.FlatStyle = FlatStyle.Flat;
            btn_grafica.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_grafica.ForeColor = Color.Black;
            btn_grafica.Location = new Point(20, 295);
            btn_grafica.Name = "btn_grafica";
            btn_grafica.Padding = new Padding(20, 0, 0, 0);
            btn_grafica.Size = new Size(381, 100);
            btn_grafica.TabIndex = 2;
            btn_grafica.Text = "Gráfica de Programación Lineal";
            btn_grafica.TextAlign = ContentAlignment.MiddleLeft;
            btn_grafica.UseVisualStyleBackColor = false;
            btn_grafica.Click += btn_grafica_Click;
            // 
            // menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_max);
            Controls.Add(btn_simplex);
            Controls.Add(btn_m);
            Controls.Add(btn_grafica);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "menu";
            Text = "MaxMin";
            Load += menu_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label lbl_max;
        private Button btn_simplex;
        private Button btn_grafica;
        private Button btn_m;
    }
}
