using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MaxMin
{
    public partial class metodo_m : Form
    {
        public metodo_m()
        {
            InitializeComponent();
        }

        class Restriccion
        {
            public double[] Coeficientes { get; set; }
            public double RHS { get; set; }
            public string Tipo { get; set; } // <=, >=, =
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                // limpiar UI previa
                tabIteraciones.TabPages.Clear();
                listBoxLogs.Items.Clear();
                lblResultado.Text = "Resultado de Z y variables: (resolviendo...)";

                // leer inputs
                string funStr = txtFuncionObjetivo.Text.Trim();
                string restrStr = txtRestricciones.Text.Trim();

                if (string.IsNullOrWhiteSpace(funStr) || string.IsNullOrWhiteSpace(restrStr))
                {
                    MessageBox.Show("Introduce la función objetivo y al menos una restricción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool maximize = (cmbTipo.SelectedItem?.ToString() ?? "Maximizar") == "Maximizar";
                int maxIter = (int)nudMaxIter.Value;

                // parsear y resolver
                SolveBigM(funStr, restrStr, maximize, maxIter);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al resolver: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Parsing helpers ----------
        private int GetMaxVarIndex(string objective, string restrictions)
        {
            int maxIdx = 0;
            string both = objective + "\n" + restrictions;
            var matches = Regex.Matches(both, @"x\s*(\d+)", RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                if (int.TryParse(m.Groups[1].Value, out int idx))
                    if (idx > maxIdx) maxIdx = idx;
            }
            return Math.Max(1, maxIdx); // al menos x1
        }

        private double[] ParseCoeffsForLine(string line, int nVars)
        {
            double[] arr = new double[nVars];
            var matches = Regex.Matches(line, @"([+\-]?\s*\d*\.?\d*)\s*x\s*(\d+)", RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                string coefStr = m.Groups[1].Value.Replace(" ", "");
                if (coefStr == "" || coefStr == "+") coefStr = "1";
                if (coefStr == "-") coefStr = "-1";
                if (!double.TryParse(coefStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double coef))
                    coef = 0;
                int idx = int.Parse(m.Groups[2].Value);
                if (idx >= 1 && idx <= nVars) arr[idx - 1] = coef;
            }
            return arr;
        }

        private double ParseRHS(string line)
        {
            // obtiene lo que está a la derecha del operador (<=, >=, =)
            string rhs = line;
            if (line.Contains("<=")) rhs = line.Split(new[] { "<=" }, StringSplitOptions.None)[1];
            else if (line.Contains(">=")) rhs = line.Split(new[] { ">=" }, StringSplitOptions.None)[1];
            else if (line.Contains("=")) rhs = line.Split(new[] { "=" }, StringSplitOptions.None)[1];

            rhs = rhs.Trim();
            if (double.TryParse(rhs, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                return val;
            // si no parsea, lanzar excepción controlada
            throw new Exception($"No se pudo leer RHS en: {line}");
        }

        private string ParseTipo(string line)
        {
            if (line.Contains("<=")) return "<=";
            if (line.Contains(">=")) return ">=";
            if (line.Contains("=")) return "=";
            throw new Exception($"Restricción sin operador válido (<=, >=, =) en: {line}");
        }

        // ---------- Método M (Big M) ----------
        private void SolveBigM(string objective, string restrictions, bool maximize, int maxIter)
        {
            // constantes y tolerancia
            double M = 1e6;
            double tol = 1e-9;

            // determinar número máximo de variables x
            int nVars = GetMaxVarIndex(objective, restrictions);

            // parsear coef. objetivo
            double[] cOrig = new double[nVars];
            var objMatches = Regex.Matches(objective, @"([+\-]?\s*\d*\.?\d*)\s*x\s*(\d+)", RegexOptions.IgnoreCase);
            foreach (Match m in objMatches)
            {
                string coefStr = m.Groups[1].Value.Replace(" ", "");
                if (coefStr == "" || coefStr == "+") coefStr = "1";
                if (coefStr == "-") coefStr = "-1";
                if (!double.TryParse(coefStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double coef))
                    coef = 0;
                int idx = int.Parse(m.Groups[2].Value);
                if (idx >= 1 && idx <= nVars) cOrig[idx - 1] = coef;
            }

            // si el usuario quiere minimizar, convertimos a maximizar multiplicando por -1
            double sign = maximize ? 1.0 : -1.0;
            for (int i = 0; i < nVars; i++) cOrig[i] *= sign;

            // parsear restricciones
            string[] lines = restrictions.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<Restriccion> restrs = new List<Restriccion>();
            foreach (var l in lines)
            {
                var r = new Restriccion();
                r.Tipo = ParseTipo(l);
                r.Coeficientes = ParseCoeffsForLine(l, nVars);
                r.RHS = ParseRHS(l);
                // si RHS negativa, multiplicar por -1 y cambiar sentido (normalización)
                if (r.RHS < 0)
                {
                    for (int k = 0; k < r.Coeficientes.Length; k++) r.Coeficientes[k] *= -1;
                    r.RHS *= -1;
                    if (r.Tipo == "<=") r.Tipo = ">=";
                    else if (r.Tipo == ">=") r.Tipo = "<=";
                }
                restrs.Add(r);
            }

            int mRows = restrs.Count;

            // construir variables: x1..xn ya existentes
            List<string> varNames = new List<string>();
            for (int i = 0; i < nVars; i++) varNames.Add($"x{i + 1}");

            // contadores para slack/artif.
            int slackCount = 0;
            int artificialCount = 0;

            // matrices A (m x nTot), b (m), c (nTot), basis (m)
            List<double[]> Arows = new List<double[]>();
            double[] b = new double[mRows];

            // inicialmente colocar coeficientes de decisión
            // pero no sabemos nTot aún, así que construimos filas dinámicamente: inicialmente tienen nVars, iremos agregando columnas
            // Vamos a usar listas de listas y luego convertir a double[,] con tamaño total.

            List<List<double>> Atemp = new List<List<double>>();
            List<int> basis = new List<int>(); // índice de variable básica por fila

            // Le añadiremos columnas conforme aparezcan slack/artif
            for (int i = 0; i < mRows; i++)
            {
                List<double> row = new List<double>();
                // agregar coef decision
                for (int j = 0; j < nVars; j++) row.Add(restrs[i].Coeficientes[j]);
                Atemp.Add(row);
            }

            // construir variables adicionales por cada restricción
            for (int i = 0; i < mRows; i++)
            {
                var r = restrs[i];
                if (r.Tipo == "<=")
                {
                    // agregar slack +1 (una columna nueva)
                    foreach (var row in Atemp) row.Add(0); // ampliar todas filas con 0
                    Atemp[i][Atemp[i].Count - 1] = 1;
                    slackCount++;
                    varNames.Add($"s{slackCount}");
                    basis.Add(nVars + slackCount - 1); // la variable slack es básica en esta fila
                }
                else if (r.Tipo == ">=")
                {
                    // agregar columna surplus (-1) y artificial (+1)
                    // surplus
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = -1;
                    slackCount++;
                    varNames.Add($"s{slackCount}"); // surplus nombrado como s...
                    // artificial
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = 1;
                    artificialCount++;
                    varNames.Add($"a{artificialCount}");
                    // la artificial será básica
                    basis.Add(nVars + slackCount + artificialCount - 1);
                }
                else // =
                {
                    // agregar artificial +1
                    foreach (var row in Atemp) row.Add(0);
                    Atemp[i][Atemp[i].Count - 1] = 1;
                    artificialCount++;
                    varNames.Add($"a{artificialCount}");
                    basis.Add(nVars + slackCount + artificialCount - 1);
                }
            }

            // preparar Arows (double[][]) y b
            int nTot = varNames.Count;
            double[,] A = new double[mRows, nTot];
            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < nTot; j++)
                {
                    A[i, j] = Atemp[i][j];
                }
                b[i] = restrs[i].RHS;
            }

            // construir vector c (costos) para todas variables
            double[] c = new double[nTot];
            for (int j = 0; j < nVars; j++) c[j] = cOrig[j];
            // los slacks tienen costo 0 por defecto (ya agregado en varNames)
            // las artificiales deben tener costo -M (penalización) para maximización
            for (int j = nVars + slackCount; j < nTot; j++)
            {
                // suponer que las últimas artificialCount columnas son artificiales por construcción
                c[j] = -M;
            }

            // basis: ya contiene índices de variables básicas por fila
            if (basis.Count != mRows)
            {
                // caso en que algunas filas no añadieron slack (raro), forzar una base con columnas unitarias si existe
                basis.Clear();
                for (int i = 0; i < mRows; i++) basis.Add(nVars + i); // tentativa
            }

            // helper para mostrar iteraciones
            int iter = 0;
            bool optimal = false;
            bool unbounded = false;

            // Iteraciones
            while (iter <= maxIter)
            {
                // calcular CB (costos de variables básicas)
                double[] CB = new double[mRows];
                for (int i = 0; i < mRows; i++) CB[i] = c[basis[i]];

                // calcular Zj = sum_r CB[r] * A[r,j]
                double[] Zj = new double[nTot];
                for (int j = 0; j < nTot; j++)
                {
                    double s = 0;
                    for (int r = 0; r < mRows; r++) s += CB[r] * A[r, j];
                    Zj[j] = s;
                }

                // calcular Cj - Zj (reduced costs)
                double[] RC = new double[nTot];
                for (int j = 0; j < nTot; j++) RC[j] = c[j] - Zj[j];

                // crear tabla DataTable y añadir al TabControl (iteración actual)
                DataTable dt = new DataTable();
                dt.Columns.Add("Base");
                dt.Columns.Add("CB");
                foreach (var name in varNames) dt.Columns.Add(name);
                dt.Columns.Add("RHS");

                for (int r = 0; r < mRows; r++)
                {
                    var row = dt.NewRow();
                    row["Base"] = varNames[basis[r]];
                    row["CB"] = CB[r].ToString("G6", CultureInfo.InvariantCulture);
                    for (int j = 0; j < nTot; j++)
                        row[varNames[j]] = Math.Round(A[r, j], 8).ToString(CultureInfo.InvariantCulture);
                    row["RHS"] = Math.Round(b[r], 8).ToString(CultureInfo.InvariantCulture);
                    dt.Rows.Add(row);
                }

                // fila Zj
                var zRow = dt.NewRow();
                zRow["Base"] = "Zj";
                zRow["CB"] = "";
                for (int j = 0; j < nTot; j++) zRow[varNames[j]] = Math.Round(Zj[j], 8).ToString(CultureInfo.InvariantCulture);
                zRow["RHS"] = Math.Round(CB.Zip(b, (cb, bv) => cb * bv).Sum(), 8).ToString(CultureInfo.InvariantCulture);
                dt.Rows.Add(zRow);

                // fila Cj - Zj
                var rcRow = dt.NewRow();
                rcRow["Base"] = "Cj - Zj";
                rcRow["CB"] = "";
                for (int j = 0; j < nTot; j++) rcRow[varNames[j]] = Math.Round(RC[j], 8).ToString(CultureInfo.InvariantCulture);
                rcRow["RHS"] = "";
                dt.Rows.Add(rcRow);

                // añadir DataGridView dentro de una pestaña
                var tp = new TabPage($"Iteración {iter}");
                var dgv = new DataGridView() { Dock = DockStyle.Fill, ReadOnly = true, DataSource = dt, AllowUserToAddRows = false };
                tp.Controls.Add(dgv);
                tabIteraciones.TabPages.Add(tp);
                tabIteraciones.SelectedTab = tp;

                // comprobar condición óptima (para maximizar, si todos RC <= tol => óptimo)
                double maxRC = RC.Max();
                if (maxRC <= tol)
                {
                    optimal = true;
                    break;
                }

                // Choosing entering variable: index with maximum RC
                int entering = -1;
                double bestRC = double.MinValue;
                for (int j = 0; j < nTot; j++)
                {
                    if (RC[j] > bestRC + tol)
                    {
                        bestRC = RC[j];
                        entering = j;
                    }
                }

                if (entering == -1 || bestRC <= tol)
                {
                    optimal = true;
                    break;
                }

                // check ratios for leaving variable (only rows with A[r,entering] > tol)
                double minRatio = double.PositiveInfinity;
                int leavingRow = -1;
                for (int r = 0; r < mRows; r++)
                {
                    double aij = A[r, entering];
                    if (aij > tol)
                    {
                        double ratio = b[r] / aij;
                        if (ratio < minRatio - tol)
                        {
                            minRatio = ratio;
                            leavingRow = r;
                        }
                    }
                }

                if (leavingRow == -1)
                {
                    // no positive entries -> ilimitado
                    unbounded = true;
                    break;
                }

                // Log variable entering/leaving
                string log = $"Iter {iter}: Entra {varNames[entering]}, Sale {varNames[basis[leavingRow]]} (fila {leavingRow})";
                listBoxLogs.Items.Add(log);

                // Pivot operation
                double pivot = A[leavingRow, entering];
                // dividir pivot row por pivot
                for (int j = 0; j < nTot; j++) A[leavingRow, j] /= pivot;
                b[leavingRow] /= pivot;

                // eliminar en otras filas
                for (int r = 0; r < mRows; r++)
                {
                    if (r == leavingRow) continue;
                    double factor = A[r, entering];
                    if (Math.Abs(factor) < tol) continue;
                    for (int j = 0; j < nTot; j++)
                        A[r, j] -= factor * A[leavingRow, j];
                    b[r] -= factor * b[leavingRow];
                }

                // actualizar base
                basis[leavingRow] = entering;

                iter++;
            } // end while

            // al finalizar: construir solución y comprobar artificiales
            // x decision variables:
            double[] xsol = new double[nTot];
            for (int j = 0; j < nTot; j++) xsol[j] = 0;
            for (int r = 0; r < mRows; r++)
            {
                int varIdx = basis[r];
                if (varIdx >= 0 && varIdx < nTot)
                    xsol[varIdx] = b[r];
            }

            // comprobar artificiales (si alguna artificial > tol => no BF)
            bool artificialNonZero = false;
            List<string> artificialPresent = new List<string>();
            for (int j = 0; j < nTot; j++)
            {
                if (varNames[j].StartsWith("a"))
                {
                    if (Math.Abs(xsol[j]) > 1e-6)
                    {
                        artificialNonZero = true;
                        artificialPresent.Add($"{varNames[j]}={xsol[j]:G6}");
                    }
                }
            }

            if (unbounded)
            {
                MessageBox.Show("El problema es no acotado (unbounded).", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblResultado.Text = "No tiene solución (no acotado).";
                return;
            }

            if (artificialNonZero)
            {
                // no existe solución básica factible
                string arti = string.Join(", ", artificialPresent);
                MessageBox.Show("No existe solución básica factible. Variables artificiales no nulas: " + arti, "Sin solución", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblResultado.Text = "No existe solución básica factible.";
                return;
            }

            // calcular Z final: Z = sum(cb_i * b_i) pero recordar que si el usuario pidió minimizar, multiplicamos por -1 la solución de Z
            // obtener CB final
            double[] CBfinal = new double[mRows];
            for (int i = 0; i < mRows; i++) CBfinal[i] = c[basis[i]];
            double Zval = 0;
            for (int i = 0; i < mRows; i++) Zval += CBfinal[i] * b[i];

            // para revertir si el usuario pidió minimizar:
            double Zreport = maximize ? Zval : -Zval;

            // construir texto resultado para variables x1..xn
            string resultado = $"Z = {Zreport:G6}; ";
            for (int j = 0; j < nVars; j++)
            {
                resultado += $"{varNames[j]} = {xsol[j]:G6}; ";
            }

            lblResultado.Text = "Resultado de Z y variables: " + resultado;
            MessageBox.Show("Proceso finalizado. Revisa pestañas (Iteraciones) y el log.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
