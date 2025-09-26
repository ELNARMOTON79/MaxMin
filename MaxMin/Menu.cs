namespace MaxMin
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void btn_grafica_Click(object sender, EventArgs e)
        {
            //cambiar de frame al frame de grafica
            this.Hide();
            grafica grafica = new grafica();
            grafica.ShowDialog();
            this.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            metodo_m metodoM = new metodo_m();
            metodoM.ShowDialog();
            this.Show();
        }

        private void btn_simplex_Click(object sender, EventArgs e)
        {
            this.Hide();
            metodo_simplex metodoSimplex = new metodo_simplex();
            metodoSimplex.ShowDialog();
            this.Close();
        }
    }
}
