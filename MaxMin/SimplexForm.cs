using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace MaxMin
{
    public partial class SimplexForm : Form
    {
        private Simplexsolver solver;
        private SimplexResultado resultado;

        // Controles de la interfaz
        private Panel panelEntrada;
        private Panel panelResultado;
        private DataGridView gridTabla;
        private TextBox txtIteraciones;
        private TextBox txtSolucion;
        private Button btnResolver;
        private Button btnAtras;
        private Button btnEjemplo;

        // Controles para entrada de datos
        private NumericUpDown numVariables;
        private NumericUpDown numRestricciones;
        private RadioButton rbMaximizar;
        private RadioButton rbMinimizar;
        private DataGridView gridFuncionObjetivo;
        private DataGridView gridRestricciones;
        private Button btnGenerarCampos;

        public SimplexForm()
        {
            solver = new Simplexsolver();
            InicializarComponentes(); // Cambiamos el nombre para evitar conflictos
        }

        private void InicializarComponentes() // Nombre cambiado
        {
            // Configuración básica del formulario
            this.Size = new Size(1200, 800);
            this.Text = "Método Simplex - MaxMin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CrearControlesEntrada();
            CrearControlesResultado();
            CrearBotones();
        }

        private void CrearControlesEntrada()
        {
            panelEntrada = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(400, 600),
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Título
            Label lblTitulo = new Label
            {
                Text = "Configuración del Problema",
                Location = new Point(20, 20),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            // Número de variables
            Label lblVariables = new Label
            {
                Text = "Número de variables:",
                Location = new Point(20, 70),
                Size = new Size(150, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };

            numVariables = new NumericUpDown
            {
                Location = new Point(180, 70),
                Size = new Size(80, 25),
                Minimum = 2,
                Maximum = 10,
                Value = 2
            };

            // Número de restricciones
            Label lblRestricciones = new Label
            {
                Text = "Número de restricciones:",
                Location = new Point(20, 110),
                Size = new Size(150, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };

            numRestricciones = new NumericUpDown
            {
                Location = new Point(180, 110),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 10,
                Value = 4
            };

            // Tipo de optimización
            GroupBox gbTipo = new GroupBox
            {
                Text = "Tipo de optimización",
                Location = new Point(20, 150),
                Size = new Size(200, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };

            rbMaximizar = new RadioButton
            {
                Text = "Maximizar",
                Location = new Point(20, 25),
                Size = new Size(100, 25),
                Checked = true,
                ForeColor = Color.White
            };

            rbMinimizar = new RadioButton
            {
                Text = "Minimizar",
                Location = new Point(20, 50),
                Size = new Size(100, 25),
                ForeColor = Color.White
            };

            gbTipo.Controls.AddRange(new Control[] { rbMaximizar, rbMinimizar });

            // Usar Button estándar en lugar de CustomButton para evitar problemas
            btnGenerarCampos = new Button
            {
                Text = "Generar Campos",
                Location = new Point(20, 250),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(60, 80, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnGenerarCampos.Click += BtnGenerarCampos_Click;

            panelEntrada.Controls.AddRange(new Control[] {
                lblTitulo, lblVariables, numVariables, lblRestricciones,
                numRestricciones, gbTipo, btnGenerarCampos
            });

            this.Controls.Add(panelEntrada);
        }

        private void CrearControlesResultado()
        {
            panelResultado = new Panel
            {
                Location = new Point(440, 20),
                Size = new Size(740, 600),
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblResultados = new Label
            {
                Text = "Resultados del Método Simplex",
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            gridTabla = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(700, 300),
                BackgroundColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            txtIteraciones = new TextBox
            {
                Location = new Point(20, 380),
                Size = new Size(340, 200),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                ReadOnly = true,
                Font = new Font("Consolas", 9)
            };

            txtSolucion = new TextBox
            {
                Location = new Point(380, 380),
                Size = new Size(340, 200),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                ReadOnly = true,
                Font = new Font("Segoe UI", 10)
            };

            panelResultado.Controls.AddRange(new Control[] {
                lblResultados, gridTabla, txtIteraciones, txtSolucion
            });

            this.Controls.Add(panelResultado);
        }

        private void CrearBotones()
        {
            btnResolver = new Button
            {
                Text = "Resolver Simplex",
                Location = new Point(20, 640),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(60, 150, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Enabled = false
            };
            btnResolver.Click += BtnResolver_Click;

            btnEjemplo = new Button
            {
                Text = "Cargar Ejemplo",
                Location = new Point(190, 640),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(150, 100, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnEjemplo.Click += BtnEjemplo_Click;

            btnAtras = new Button
            {
                Text = "← Regresar",
                Location = new Point(1000, 640),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(150, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnAtras.Click += BtnAtras_Click;

            this.Controls.AddRange(new Control[] { btnResolver, btnEjemplo, btnAtras });
        }

        private void BtnGenerarCampos_Click(object sender, EventArgs e)
        {
            int numVars = (int)numVariables.Value;
            int numRest = (int)numRestricciones.Value;

            // Limpiar controles anteriores
            if (gridFuncionObjetivo != null)
            {
                panelEntrada.Controls.Remove(gridFuncionObjetivo);
                gridFuncionObjetivo.Dispose();
            }
            if (gridRestricciones != null)
            {
                panelEntrada.Controls.Remove(gridRestricciones);
                gridRestricciones.Dispose();
            }

            // Grid para función objetivo
            gridFuncionObjetivo = new DataGridView
            {
                Location = new Point(20, 320),
                Size = new Size(350, 80),
                BackgroundColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersVisible = true,
                RowHeadersVisible = false
            };

            // Configurar columnas función objetivo
            for (int i = 0; i < numVars; i++)
            {
                gridFuncionObjetivo.Columns.Add($"x{i + 1}", $"x{i + 1}");
                gridFuncionObjetivo.Columns[i].Width = 60;
            }

            gridFuncionObjetivo.Rows.Add();

            // Grid para restricciones
            gridRestricciones = new DataGridView
            {
                Location = new Point(20, 420),
                Size = new Size(350, 150),
                BackgroundColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersVisible = true,
                RowHeadersVisible = true
            };

            // Configurar columnas restricciones
            for (int i = 0; i < numVars; i++)
            {
                gridRestricciones.Columns.Add($"x{i + 1}", $"x{i + 1}");
                gridRestricciones.Columns[i].Width = 50;
            }
            gridRestricciones.Columns.Add("rhs", "≤");
            gridRestricciones.Columns[gridRestricciones.Columns.Count - 1].Width = 60;

            // Agregar filas
            for (int i = 0; i < numRest; i++)
            {
                gridRestricciones.Rows.Add();
                gridRestricciones.Rows[i].HeaderCell.Value = $"R{i + 1}";
            }

            panelEntrada.Controls.AddRange(new Control[] { gridFuncionObjetivo, gridRestricciones });
            btnResolver.Enabled = true;
        }

        private void BtnEjemplo_Click(object sender, EventArgs e)
        {
            // Cargar ejemplo "La Mancha"
            numVariables.Value = 2;
            numRestricciones.Value = 4;
            rbMaximizar.Checked = true;

            BtnGenerarCampos_Click(sender, e);

            try
            {
                // Función objetivo: 5x1 + 4x2
                gridFuncionObjetivo.Rows[0].Cells[0].Value = 5;
                gridFuncionObjetivo.Rows[0].Cells[1].Value = 4;

                // Restricciones
                // 6x1 + 4x2 <= 24
                gridRestricciones.Rows[0].Cells[0].Value = 6;
                gridRestricciones.Rows[0].Cells[1].Value = 4;
                gridRestricciones.Rows[0].Cells[2].Value = 24;

                // x1 + 2x2 <= 6
                gridRestricciones.Rows[1].Cells[0].Value = 1;
                gridRestricciones.Rows[1].Cells[1].Value = 2;
                gridRestricciones.Rows[1].Cells[2].Value = 6;

                // -x1 + x2 <= 1
                gridRestricciones.Rows[2].Cells[0].Value = -1;
                gridRestricciones.Rows[2].Cells[1].Value = 1;
                gridRestricciones.Rows[2].Cells[2].Value = 1;

                // x2 <= 2
                gridRestricciones.Rows[3].Cells[0].Value = 0;
                gridRestricciones.Rows[3].Cells[1].Value = 1;
                gridRestricciones.Rows[3].Cells[2].Value = 2;

                MessageBox.Show("Ejemplo 'La Mancha' cargado exitosamente!", "Ejemplo Cargado",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ejemplo: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                // Leer datos de la interfaz
                int numVars = (int)numVariables.Value;
                int numRest = (int)numRestricciones.Value;
                bool maximizar = rbMaximizar.Checked;

                // Función objetivo
                double[] funcionObjetivo = new double[numVars];
                for (int i = 0; i < numVars; i++)
                {
                    var valor = gridFuncionObjetivo.Rows[0].Cells[i].Value;
                    funcionObjetivo[i] = valor != null ? Convert.ToDouble(valor) : 0;
                }

                // Restricciones
                double[,] restricciones = new double[numRest, numVars];
                double[] ladoDerecho = new double[numRest];

                for (int i = 0; i < numRest; i++)
                {
                    for (int j = 0; j < numVars; j++)
                    {
                        var valor = gridRestricciones.Rows[i].Cells[j].Value;
                        restricciones[i, j] = valor != null ? Convert.ToDouble(valor) : 0;
                    }
                    var valorRHS = gridRestricciones.Rows[i].Cells[numVars].Value;
                    ladoDerecho[i] = valorRHS != null ? Convert.ToDouble(valorRHS) : 0;
                }

                // Nombres de variables
                string[] nombresVariables = new string[numVars];
                for (int i = 0; i < numVars; i++)
                    nombresVariables[i] = $"x{i + 1}";

                // Resolver
                resultado = solver.ResolverSimplex(funcionObjetivo, restricciones, ladoDerecho, maximizar, nombresVariables);

                MostrarResultados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al resolver: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarResultados()
        {
            if (resultado.TieneSolucion)
            {
                MostrarTablaFinal();
                MostrarHistorialIteraciones();
                MostrarSolucionOptima();
            }
            else
            {
                txtSolucion.Text = $"El problema no tiene solución:\r\n{resultado.MensajeError}";
                txtIteraciones.Text = "No se pudo resolver el problema.";
            }
        }

        private void MostrarTablaFinal()
        {
            gridTabla.Columns.Clear();
            gridTabla.Rows.Clear();

            if (resultado.HistorialTablas.Count == 0) return;

            var tablaFinal = resultado.HistorialTablas[resultado.HistorialTablas.Count - 1];
            var variables = resultado.TodasLasVariables;

            // Agregar columna para variables básicas
            gridTabla.Columns.Add("VB", "VB");

            // Agregar columnas para variables
            for (int j = 0; j < variables.Count; j++)
            {
                gridTabla.Columns.Add(variables[j], variables[j]);
            }

            // Agregar filas
            for (int i = 0; i < solver.FilasTabla; i++)
            {
                object[] fila = new object[variables.Count + 1];

                // Variable básica
                if (i < solver.FilasTabla - 1)
                    fila[0] = solver.VariablesBasicasActuales[i];
                else
                    fila[0] = "z";

                // Valores de la tabla
                for (int j = 0; j < variables.Count; j++)
                {
                    fila[j + 1] = Math.Round(tablaFinal[i, j], 4).ToString();
                }

                gridTabla.Rows.Add(fila);
            }

            // Resaltar fila Z
            if (gridTabla.Rows.Count > 0)
            {
                var filaZ = gridTabla.Rows[gridTabla.Rows.Count - 1];
                filaZ.DefaultCellStyle.BackColor = Color.FromArgb(100, 50, 200);
                filaZ.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void MostrarHistorialIteraciones()
        {
            txtIteraciones.Clear();
            txtIteraciones.AppendText("HISTORIAL DE ITERACIONES:\r\n");
            txtIteraciones.AppendText("========================\r\n\r\n");

            for (int i = 0; i < resultado.HistorialPasos.Count; i++)
            {
                txtIteraciones.AppendText($"{i + 1}. {resultado.HistorialPasos[i]}\r\n");
            }

            txtIteraciones.AppendText($"\r\nTotal de iteraciones: {resultado.NumeroIteraciones}");
        }

        private void MostrarSolucionOptima()
        {
            txtSolucion.Clear();
            txtSolucion.AppendText("SOLUCIÓN ÓPTIMA:\r\n");
            txtSolucion.AppendText("================\r\n\r\n");

            txtSolucion.AppendText("Variables básicas:\r\n");
            foreach (var variable in resultado.VariablesBasicas)
            {
                txtSolucion.AppendText($"  {variable.Key} = {Math.Round(variable.Value, 4)}\r\n");
            }

            txtSolucion.AppendText("\r\nVariables no básicas:\r\n");
            foreach (var variable in resultado.VariablesNoBasicas)
            {
                txtSolucion.AppendText($"  {variable} = 0.0000\r\n");
            }

            string tipoOptimo = resultado.EsMaximizacion ? "máximo" : "mínimo";
            txtSolucion.AppendText($"\r\nValor óptimo de la función objetivo:\r\n");
            txtSolucion.AppendText($"  z = {Math.Round(resultado.ValorObjetivo, 4)} ({tipoOptimo})\r\n");

            // Interpretación para problema La Mancha
            if (Math.Abs(resultado.ValorObjetivo - 21) < 0.01 && resultado.VariablesBasicas.Count >= 2)
            {
                txtSolucion.AppendText($"\r\nINTERPRETACIÓN:\r\n");
                txtSolucion.AppendText($"La empresa debe producir:\r\n");

                if (resultado.VariablesBasicas.ContainsKey("x1"))
                    txtSolucion.AppendText($"• {Math.Round(resultado.VariablesBasicas["x1"], 1)} toneladas de pintura exterior\r\n");
                if (resultado.VariablesBasicas.ContainsKey("x2"))
                    txtSolucion.AppendText($"• {Math.Round(resultado.VariablesBasicas["x2"], 1)} toneladas de pintura interior\r\n");

                txtSolucion.AppendText($"Para obtener una utilidad máxima de ${Math.Round(resultado.ValorObjetivo, 0)},000 pesos diarios.");
            }
        }

        private void BtnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu menuForm = new menu();
            menuForm.ShowDialog();
            this.Close();
        }
    }
}
