namespace MaxMin
{
    partial class metodo_simplex
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
            // metodo_simplex
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_grafica);
            Name = "metodo_simplex";
            Text = "metodo_simplex";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_grafica;
    }
}