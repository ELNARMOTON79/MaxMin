namespace MaxMin
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();

            // Conectar el evento del botón Simplex
            this.btn_simplex.Click += new System.EventHandler(this.btn_simplex_Click);
        }

        private void btn_grafica_Click(object sender, EventArgs e)
        {
            //cambiar de frame al frame de grafica
            this.Hide();
            grafica grafica = new grafica();
            grafica.ShowDialog();
            this.Close();


        }

        // AGREGAR ESTE MÉTODO NUEVO:
        private void btn_simplex_Click(object sender, EventArgs e)
        {
            //cambiar de frame al frame de simplex
            this.Hide();
            SimplexForm simplexForm = new SimplexForm();
            simplexForm.ShowDialog();
            this.Close();
        }

        private void menu_Load(object sender, EventArgs e)
        {

        }

        private void btn_simplex_Click_1(object sender, EventArgs e)
        {

        }
    }
}
