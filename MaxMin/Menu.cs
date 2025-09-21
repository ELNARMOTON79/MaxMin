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
    }
}
