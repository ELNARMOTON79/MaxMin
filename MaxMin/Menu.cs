namespace MaxMin
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();

            // Conectar el evento del bot�n Simplex
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

        // AGREGAR ESTE M�TODO NUEVO:
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

        private void btn_m_Click(object sender, EventArgs e)
        {
            this.Hide();
            metodo_m metodo_m = new metodo_m();
            metodo_m.ShowDialog();
            this.Close();
        }
    }
}
