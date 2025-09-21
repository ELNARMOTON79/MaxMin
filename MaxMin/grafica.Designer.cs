namespace MaxMin
{
    partial class grafica
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
            lbl_grafica = new Label();
            btn_atras = new CustomButton();
            SuspendLayout();
            // 
            // lbl_grafica
            // 
            lbl_grafica.AutoSize = true;
            lbl_grafica.Font = new Font("Cooper Black", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_grafica.ForeColor = Color.White;
            lbl_grafica.Location = new Point(303, 27);
            lbl_grafica.Name = "lbl_grafica";
            lbl_grafica.Size = new Size(171, 46);
            lbl_grafica.TabIndex = 1;
            lbl_grafica.Text = "Grafica";
            // 
            // btn_atras
            // 
            btn_atras.BackColor = Color.FromArgb(50, 50, 100);
            btn_atras.FlatStyle = FlatStyle.Flat;
            btn_atras.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_atras.ForeColor = Color.White;
            btn_atras.Location = new Point(31, 22);
            btn_atras.Name = "btn_atras";
            btn_atras.Padding = new Padding(20, 0, 0, 0);
            btn_atras.Size = new Size(99, 69);
            btn_atras.TabIndex = 2;
            btn_atras.Text = "Atrás";
            btn_atras.TextAlign = ContentAlignment.MiddleLeft;
            btn_atras.UseVisualStyleBackColor = false;
            btn_atras.Click += btn_atras_Click;
            // 
            // grafica
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_grafica);
            Controls.Add(btn_atras);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "grafica";
            Text = "metodo_simplex";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomButton btn_atras;
        private Label lbl_grafica;
    }
}