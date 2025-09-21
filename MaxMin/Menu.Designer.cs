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
            btn_simplex = new CustomButton();
            button1 = new CustomButton();
            btn_grafica = new CustomButton();
            SuspendLayout();
            // 
            // lbl_max
            // 
            lbl_max.AutoSize = true;
            lbl_max.Font = new Font("Cooper Black", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_max.ForeColor = Color.White;
            lbl_max.Location = new Point(507, 177);
            lbl_max.Name = "lbl_max";
            lbl_max.Size = new Size(188, 46);
            lbl_max.TabIndex = 0;
            lbl_max.Text = "Max Min";
            // 
            // btn_simplex
            // 
            btn_simplex.BackColor = Color.Transparent;
            btn_simplex.FlatStyle = FlatStyle.Flat;
            btn_simplex.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_simplex.ForeColor = Color.White;
            btn_simplex.Location = new Point(20, 30);
            btn_simplex.Name = "btn_simplex";
            btn_simplex.Padding = new Padding(20, 0, 0, 0);
            btn_simplex.Size = new Size(381, 102);
            btn_simplex.TabIndex = 0;
            btn_simplex.Tag = "Resuelve problemas de programación lineal de cualquier tamaño.";
            btn_simplex.Text = "Método Simplex";
            btn_simplex.TextAlign = ContentAlignment.MiddleLeft;
            btn_simplex.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(20, 155);
            button1.Name = "button1";
            button1.Padding = new Padding(20, 0, 0, 0);
            button1.Size = new Size(381, 116);
            button1.TabIndex = 1;
            button1.Tag = "Resuelve problemas con restricciones de igualdad y mayor o igual.";
            button1.Text = "Método de la M";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = false;
            // 
            // btn_grafica
            // 
            btn_grafica.BackColor = Color.Transparent;
            btn_grafica.FlatStyle = FlatStyle.Flat;
            btn_grafica.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_grafica.ForeColor = Color.White;
            btn_grafica.Location = new Point(20, 295);
            btn_grafica.Name = "btn_grafica";
            btn_grafica.Padding = new Padding(20, 0, 0, 0);
            btn_grafica.Size = new Size(381, 100);
            btn_grafica.TabIndex = 2;
            btn_grafica.Tag = "Visualiza región factible y la solución óptima.";
            btn_grafica.Text = "Gráfica de Programación Lineal";
            btn_grafica.TextAlign = ContentAlignment.MiddleLeft;
            btn_grafica.UseVisualStyleBackColor = false;
            btn_grafica.Click += btn_grafica_Click;
            // 
            // menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_max);
            Controls.Add(btn_simplex);
            Controls.Add(button1);
            Controls.Add(btn_grafica);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "menu";
            Text = "MaxMin";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label lbl_max;
        private CustomButton btn_simplex;
        private CustomButton btn_grafica;
        private CustomButton button1;
    }
}
