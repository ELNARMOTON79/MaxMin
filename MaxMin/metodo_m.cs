using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MaxMin
{
    // Form principal para el método de la M grande (Big M)
    public partial class metodo_m : Form
    {
        // Constructor del formulario
        public metodo_m()
        {
            InitializeComponent();
        }

        // Clase interna para representar una restricción
        class Restriccion
        {
            public double[] Coeficientes { get; set; } // Coeficientes de las variables
            public double RHS { get; set; }           // Término independiente (lado derecho)
            public string Tipo { get; set; }          // Tipo de restricción: <=, >=, =
        }

        // --- MÉTODOS DE CONFIGURACIÓN DE LA INTERFAZ ---

        // Evento de carga del formulario. Configura controles y conecta eventos.
        private void metodo_m_Load(object sender, EventArgs e)
        {
            // Conecta el evento para el selector de número de variables
            this.nudNumeroVariables.ValueChanged += new System.EventHandler(this.nudNumeroVariables_ValueChanged);
            ConfigurarTablasDeEntrada(); // Inicializa las tablas de entrada
            cmbTipo.SelectedIndex = 0;  // Selecciona "Maximizar" por defecto
        }

        // Configura las tablas de entrada para función objetivo y restricciones
        private void ConfigurarTablasDeEntrada()
        {
            // Guarda los valores actuales para no perderlos al cambiar el número de variables
            var funcionObjetivoValores = new List<string>();
            if (dgvFuncionObjetivo.Rows.Count > 0 && dgvFuncionObjetivo.Columns.Count > 0)
            {
                foreach (DataGridViewCell cell in dgvFuncionObjetivo.Rows[0].Cells)
                {
                    funcionObjetivoValores.Add(cell.Value?.ToString() ?? "");
                }
            }

            // Limpia completamente las columnas antes de volver a crearlas
            dgvFuncionObjetivo.Columns.Clear();
            dgvRestricciones.Columns.Clear();

            int numVars = (int)nudNumeroVariables.Value;

            // Configura columnas para la función objetivo
            for (int i = 1; i <= numVars; i++)
            {
                dgvFuncionObjetivo.Columns.Add($"x{i}", $"x{i}");
            }
            if (dgvFuncionObjetivo.RowCount == 0) dgvFuncionObjetivo.Rows.Add();

            // Restaura los valores que quepan en las nuevas columnas
            for (int i = 0; i < funcionObjetivoValores.Count && i < dgvFuncionObjetivo.ColumnCount; i++)
            {
                dgvFuncionObjetivo.Rows[0].Cells[i].Value = funcionObjetivoValores[i];
            }

            dgvFuncionObjetivo.RowHeadersVisible = false;
            dgvFuncionObjetivo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFuncionObjetivo.AllowUserToAddRows = false;

            // Configura columnas para las restricciones
            for (int i = 1; i <= numVars; i++)
            {
                dgvRestricciones.Columns.Add($"x{i}", $"x{i}");
            }

            // Columna para el tipo de desigualdad
            var desigualdadCol = new DataGridViewComboBoxColumn { HeaderText = "Signo", Name = "desigualdad", Items = { "<=", ">=", "=" }, Width = 60 };
            dgvRestricciones.Columns.Add(desigualdadCol);
            // Columna para el RHS
            var rhsCol = new DataGridViewTextBoxColumn { HeaderText = "RHS", Name = "rhs", Width = 70 };
            dgvRestricciones.Columns.Add(rhsCol);

            dgvRestricciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRestricciones.RowHeadersVisible = true;
        }

        // Evento que se ejecuta al cambiar el número de variables
        private void nudNumeroVariables_ValueChanged(object sender, EventArgs e)
        {
            ConfigurarTablasDeEntrada();
        }

        // Establece el valor por defecto de la columna "desigualdad" en las restricciones
        private void dgvRestricciones_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["desigualdad"].Value = "<=";
        }

        // --- EVENTOS DE BOTONES ---

        // Evento del botón para regresar (sin implementación aquí)
        private void btnRegresar_Click(object sender, EventArgs e)
        {
        }

        // Evento del botón para resolver el problema
        private void btnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                tabIteraciones.TabPages.Clear(); // Limpia las iteraciones previas
                txtResultadoFinal.Text = "Resolviendo...";

                bool maximize = (cmbTipo.SelectedItem?.ToString() ?? "Maximizar") == "Maximizar";

                int nVars = (int)nudNumeroVariables.Value;

                // Lee los coeficientes de la función objetivo
                double[] cOrig = new double[nVars];
                for (int i = 0; i < nVars; i++)
                {
                    if (dgvFuncionObjetivo.Rows[0].Cells[i].Value != null && double.TryParse(dgvFuncionObjetivo.Rows[0].Cells[i].Value.ToString(), out double val))
                        cOrig[i] = val;
                    else
                        cOrig[i] = 0;
                }

                // Lee las restricciones ingresadas
                List<Restriccion> restrs = new List<Restriccion>();
                foreach (DataGridViewRow row in dgvRestricciones.Rows)
                {
                    if (row.IsNewRow) continue;
                    var r = new Restriccion { Coeficientes = new double[nVars], Tipo = row.Cells["desigualdad"].Value?.ToString() ?? "<=", RHS = 0 };
                    for (int i = 0; i < nVars; i++)
                    {
                        if (row.Cells[i].Value != null && double.TryParse(row.Cells[i].Value.ToString(), out double val))
                            r.Coeficientes[i] = val;
                    }
                    if (row.Cells["rhs"].Value != null && double.TryParse(row.Cells["rhs"].Value.ToString(), out double rhsVal))
                        r.RHS = rhsVal;
                    restrs.Add(r);
                }

                if (restrs.Count == 0)
                {
                    MessageBox.Show("Introduce al menos una restricción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtResultadoFinal.Text = "Error: Faltan restricciones.";
                    return;
                }

                // Llama al método principal de resolución
                SolveBigM(cOrig, restrs, maximize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al resolver: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtResultadoFinal.Text = "Ocurrió un error durante la resolución.";
            }
        }

        // --- LÓGICA DEL ALGORITMO SIMPLEX CON MÉTODO DE LA M GRANDE ---

        // Método principal que implementa el algoritmo Big M
        // Recibe los coeficientes de la función objetivo, las restricciones y si es maximización
        private void SolveBigM(double[] cOrig, List<Restriccion> restrs, bool maximize)
        {
            double M = 1e6; // Valor grande para penalizar variables artificiales
            double tol = 1e-9; // Tolerancia numérica
            int nVars = cOrig.Length;

            double sign = maximize ? 1.0 : -1.0;
            for (int i = 0; i < nVars; i++) cOrig[i] *= sign;

            // Normaliza restricciones con RHS negativo
            foreach (var r in restrs)
            {
                if (r.RHS < 0)
                {
                    for (int k = 0; k < r.Coeficientes.Length; k++) r.Coeficientes[k] *= -1;
                    r.RHS *= -1;
                    if (r.Tipo == "<=") r.Tipo = ">=";
                    else if (r.Tipo == ">=") r.Tipo = "<=";
                }
            }

            int mRows = restrs.Count;
            List<string> varNames = new List<string>();
            for (int i = 0; i < nVars; i++) varNames.Add($"x{i + 1}");

            int slackCount = 0;
            int artificialCount = 0;
            var Atemp = new List<List<double>>();
            var basis = new List<int>();

            // Construye la matriz de restricciones y variables adicionales
            for (int i = 0; i < mRows; i++)
            {
                Atemp.Add(new List<double>(restrs[i].Coeficientes));
            }

            for (int i = 0; i < mRows; i++)
            {
                var r = restrs[i];
                if (r.Tipo == "<=")
                {
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = 1;
                    slackCount++;
                    varNames.Add($"s{slackCount}");
                    basis.Add(varNames.Count - 1);
                }
                else if (r.Tipo == ">=")
                {
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = -1;
                    slackCount++;
                    varNames.Add($"s{slackCount}");
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = 1;
                    artificialCount++;
                    varNames.Add($"a{artificialCount}");
                    basis.Add(varNames.Count - 1);
                }
                else // =
                {
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = 1;
                    artificialCount++;
                    varNames.Add($"a{artificialCount}");
                    basis.Add(varNames.Count - 1);
                }
            }

            int nTot = varNames.Count;
            foreach (var row in Atemp)
            {
                while (row.Count < nTot) row.Add(0);
            }

            double[,] A = new double[mRows, nTot];
            double[] b = new double[mRows];
            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < nTot; j++) A[i, j] = Atemp[i][j];
                b[i] = restrs[i].RHS;
            }

            double[] c = new double[nTot];
            Array.Copy(cOrig, c, nVars);
            for (int j = 0; j < nTot; j++)
            {
                if (varNames[j].StartsWith("a")) c[j] = -M;
            }

            int iter = 0;
            bool unbounded = false;

            // Bucle principal del método simplex
            while (iter < 500)
            {
                // Calcula los valores de CB, Zj y RC
                double[] CB = new double[mRows];
                for (int i = 0; i < mRows; i++) CB[i] = c[basis[i]];

                double[] Zj = new double[nTot];
                for (int j = 0; j < nTot; j++)
                {
                    double s = 0;
                    for (int r = 0; r < mRows; r++) s += CB[r] * A[r, j];
                    Zj[j] = s;
                }

                double[] RC = new double[nTot];
                for (int j = 0; j < nTot; j++) RC[j] = c[j] - Zj[j];

                // Crea una tabla para mostrar la iteración actual
                DataTable dt = new DataTable();
                dt.Columns.Add("Base");
                dt.Columns.Add("CB");
                foreach (var name in varNames) dt.Columns.Add(name);
                dt.Columns.Add("RHS");
                for (int r = 0; r < mRows; r++)
                {
                    var dataRow = dt.NewRow();
                    dataRow["Base"] = varNames[basis[r]];
                    dataRow["CB"] = CB[r].ToString("G6", CultureInfo.InvariantCulture);
                    for (int j = 0; j < nTot; j++) dataRow[varNames[j]] = Math.Round(A[r, j], 6).ToString(CultureInfo.InvariantCulture);
                    dataRow["RHS"] = Math.Round(b[r], 6).ToString(CultureInfo.InvariantCulture);
                    dt.Rows.Add(dataRow);
                }
                var zRow = dt.NewRow();
                zRow["Base"] = "Z";
                for (int j = 0; j < nTot; j++) zRow[varNames[j]] = Math.Round(Zj[j], 6).ToString(CultureInfo.InvariantCulture);
                zRow["RHS"] = Math.Round(CB.Zip(b, (cb_val, b_val) => cb_val * b_val).Sum(), 6).ToString(CultureInfo.InvariantCulture);
                dt.Rows.Add(zRow);

                // Muestra la tabla en una nueva pestaña
                var tp = new TabPage($"Iteración {iter}");
                var dgv = new DataGridView() { Dock = DockStyle.Fill, ReadOnly = true, DataSource = dt, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells };
                tp.Controls.Add(dgv);
                tabIteraciones.TabPages.Add(tp);
                tabIteraciones.SelectedTab = tp;

                // Verifica condición de optimalidad
                if (RC.All(rc => rc <= tol))
                {
                    break;
                }

                // Determina variable entrante
                int entering = Array.IndexOf(RC, RC.Max());

                // Determina variable saliente
                double minRatio = double.PositiveInfinity;
                int leavingRow = -1;
                for (int r = 0; r < mRows; r++)
                {
                    if (A[r, entering] > tol)
                    {
                        double ratio = b[r] / A[r, entering];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            leavingRow = r;
                        }
                    }
                }

                // Si no hay variable saliente, el problema es no acotado
                if (leavingRow == -1)
                {
                    unbounded = true;
                    break;
                }

                // Realiza el pivoteo
                double pivot = A[leavingRow, entering];
                for (int j = 0; j < nTot; j++) A[leavingRow, j] /= pivot;
                b[leavingRow] /= pivot;

                for (int r = 0; r < mRows; r++)
                {
                    if (r == leavingRow) continue;
                    double factor = A[r, entering];
                    for (int j = 0; j < nTot; j++) A[r, j] -= factor * A[leavingRow, j];
                    b[r] -= factor * b[leavingRow];
                }

                basis[leavingRow] = entering;
                iter++;
            }

            // --- RESULTADOS FINALES ---
            if (unbounded)
            {
                txtResultadoFinal.Text = "PROBLEMA NO ACOTADO (UNBOUNDED)." + Environment.NewLine + "La solución tiende a infinito.";
                return;
            }

            // Calcula la solución final
            double[] xsol = new double[nTot];
            for (int r = 0; r < mRows; r++) xsol[basis[r]] = b[r];

            // Verifica si hay variables artificiales mayores a cero
            bool artificialNonZero = false;
            for (int j = 0; j < nTot; j++)
            {
                if (varNames[j].StartsWith("a") && Math.Abs(xsol[j]) > tol)
                {
                    artificialNonZero = true;
                    break;
                }
            }

            if (artificialNonZero)
            {
                txtResultadoFinal.Text = "NO EXISTE SOLUCIÓN BÁSICA FACTIBLE." + Environment.NewLine + "Una o más variables artificiales son mayores a cero en la solución óptima.";
                return;
            }

            // Calcula el valor de la función objetivo
            double Zval = 0;
            for (int i = 0; i < nVars; i++) Zval += cOrig[i] * xsol[i];

            double Zreport = maximize ? Zval : -Zval;

            // Muestra el resultado final
            string resultado = $"SOLUCIÓN ÓPTIMA ENCONTRADA:" + Environment.NewLine;
            resultado += $"Valor de Z = {Zreport:G6}" + Environment.NewLine + Environment.NewLine;
            resultado += "Variables de Decisión:" + Environment.NewLine;
            for (int j = 0; j < nVars; j++)
            {
                resultado += $"{varNames[j]} = {xsol[j]:G6}" + Environment.NewLine;
            }

            txtResultadoFinal.Text = resultado;
            MessageBox.Show("Proceso finalizado.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- EVENTOS DE INTERFAZ ADICIONALES ---
        private void tabIteraciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evento al cambiar de pestaña de iteraciones
        }

        private void gbIteraciones_Enter(object sender, EventArgs e)
        {
            // Evento al entrar al groupbox de iteraciones
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            // Evento al hacer click en el título
        }

        private void gbConfiguracion_Enter(object sender, EventArgs e)
        {
            // Evento al entrar al groupbox de configuración
        }

        private void gbEntrada_Enter(object sender, EventArgs e)
        {
            // Evento al entrar al groupbox de entrada
        }

        // Evento del botón regresar (implementación que oculta este formulario y muestra el menú principal)
        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            menu menu = new menu();
            menu.ShowDialog();
            this.Close();
        }
    }
}