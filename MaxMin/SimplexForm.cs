using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace MaxMin
{
    public partial class SimplexForm : Form
    {
        private Simplexsolver solver;
        private SimplexResultado resultado;

        // Controles principales
        private Panel panelEntrada;
        private Panel panelResultado;
        private DataGridView gridTabla;
        private TextBox txtIteraciones;
        private TextBox txtSolucion;
        private Button btnResolver;
        private Button btnAtras;
        private Button btnEjemplo;

        // Controles de configuración
        private NumericUpDown numVariables;
        private NumericUpDown numRestricciones;
        private RadioButton rbMaximizar;
        private RadioButton rbMinimizar;
        private DataGridView gridFuncionObjetivo;
        private DataGridView gridRestricciones;
        private Button btnGenerarCampos;
        private ScrollableControl panelFormularios;

        public SimplexForm()
        {
            solver = new Simplexsolver();
            InicializarComponentes();
        }

        private void BtnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu menuForm = new menu();
            menuForm.ShowDialog();
            this.Close();
        }
    

        private void InicializarComponentes()
        {
            // Configuración del formulario - MÁS GRANDE
            this.Size = new Size(1400, 900);
            this.Text = "Método Simplex - MaxMin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.FormBorderStyle = FormBorderStyle.Sizable; // Permitir redimensionar
            this.MinimumSize = new Size(1400, 900);

            CrearControlesEntrada();
            CrearControlesResultado();
            CrearBotones();
        }

        private void CrearControlesEntrada()
        {
            // Panel de entrada más organizado
            panelEntrada = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(450, 750),
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            int yPos = 15;

            // Título principal
            Label lblTitulo = new Label
            {
                Text = " Configuración del Problema",
                Location = new Point(20, yPos),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.LightBlue,
                TextAlign = ContentAlignment.MiddleCenter
            };
            yPos += 40;


            // SECCIÓN 1: Variables
            Label lblSeccion1 = new Label
            {
                Text = " VARIABLES DE DECISIÓN",
                Location = new Point(20, yPos),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.LightGreen
            };
            yPos += 30;

            Label lblVarDesc = new Label
            {
                Text = "¿Cuántas variables tiene su problema?\n(x₁, x₂, x₃.)",
                Location = new Point(20, yPos),
                AutoSize = true,   // 🔹 Esto evita que se corte el texto
                MaximumSize = new Size(320, 0), // 🔹 Ajusta el ancho, y el alto se adapta solo
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White
            };


            numVariables = new NumericUpDown
            {
                Location = new Point(350, yPos + 5),
                Size = new Size(70, 25),
                Minimum = 2,
                Maximum = 6,
                Value = 2,
                Font = new Font("Segoe UI", 10)
            };
            yPos += 50;

            // SECCIÓN 2: Restricciones
            Label lblSeccion2 = new Label
            {
                Text = " RESTRICCIONES DEL PROBLEMA",
                Location = new Point(20, yPos),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.LightCoral
            };
            yPos += 30;

            Label lblRestDesc = new Label
            {
                Text = "¿Cuántas limitaciones tiene su problema?",
                Location = new Point(20, yPos),
                AutoSize = true,   // 🔹 Esto evita que se corte el texto
                MaximumSize = new Size(320, 0), // 🔹 Ajusta el ancho, y el alto se adapta solo
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White
            };

            numRestricciones = new NumericUpDown
            {
                Location = new Point(350, yPos + 5),
                Size = new Size(70, 25),
                Minimum = 1,
                Maximum = 8,
                Value = 4,
                Font = new Font("Segoe UI", 10)
            };
            yPos += 60;

            // SECCIÓN 3: Tipo de optimización
            Label lblSeccion3 = new Label
            {
                Text = " TIPO DE OPTIMIZACIÓN",
                Location = new Point(20, yPos),
                AutoSize = true,   // 🔹 Esto evita que se corte el texto
                MaximumSize = new Size(320, 0), // 🔹 Ajusta el ancho, y el alto se adapta solo
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.LightBlue
            };
            yPos += 35;

            rbMaximizar = new RadioButton
            {
                Text = "🔺 MAXIMIZAR - Obtener el mayor valor posible",
                Location = new Point(30, yPos),
                Size = new Size(380, 25),
                Checked = true,
                ForeColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10)
            };
            yPos += 30;

            rbMinimizar = new RadioButton
            {
                Text = "🔻 MINIMIZAR - Obtener el menor valor posible",
                Location = new Point(30, yPos),
                Size = new Size(380, 25),
                ForeColor = Color.LightCoral,
                Font = new Font("Segoe UI", 10)
            };
            yPos += 30;

            // BOTÓN GENERAR
            btnGenerarCampos = new Button
            {
                Text = "GENERAR FORMULARIO",
                Location = new Point(80, yPos),
                Size = new Size(300, 45),
                BackColor = Color.FromArgb(60, 120, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnGenerarCampos.Click += BtnGenerarCampos_Click;

            // Agregar todos los controles de configuración
            panelEntrada.Controls.AddRange(new Control[] {
                lblTitulo,
                lblSeccion1, lblVarDesc, numVariables,
                lblSeccion2, lblRestDesc, numRestricciones,
                lblSeccion3, rbMaximizar, rbMinimizar,
                btnGenerarCampos
            });

            this.Controls.Add(panelEntrada);
        }

        private void CrearControlesResultado()
        {
            // Panel de resultados más grande
            panelResultado = new Panel
            {
                Location = new Point(490, 20),
                Size = new Size(880, 750),
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Título del panel de resultados
            Label lblResultados = new Label
            {
                Text = " Resultados del Método Simplex",
                Location = new Point(20, 15),
                AutoSize = true,   // 🔹 Esto evita que se corte el texto
                MaximumSize = new Size(320, 0), // 🔹 Ajusta el ancho, y el alto se adapta solo
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            // Área principal para la tabla
            gridTabla = new DataGridView
            {
                Location = new Point(20, 55),
                Size = new Size(840, 280),
                BackgroundColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(70, 70, 70),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            };

            // Área de iteraciones (izquierda)
            Label lblIteraciones = new Label
            {
                Text = "📋 Historial de Iteraciones:",
                Location = new Point(20, 345),
                Size = new Size(200, 25),
                ForeColor = Color.LightBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            txtIteraciones = new TextBox
            {
                Location = new Point(20, 375),
                Size = new Size(420, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                ReadOnly = true,
                Font = new Font("Consolas", 8),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Área de solución (derecha)
            Label lblSolucion = new Label
            {
                Text = "🎯 Solución Óptima:",
                Location = new Point(460, 345),
                Size = new Size(200, 25),
                ForeColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            txtSolucion = new TextBox
            {
                Location = new Point(460, 375),
                Size = new Size(400, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                ReadOnly = true,
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };

            panelResultado.Controls.AddRange(new Control[] {
                lblResultados, gridTabla, lblIteraciones, txtIteraciones, lblSolucion, txtSolucion
            });

            this.Controls.Add(panelResultado);
        }

        private void CrearBotones()
        {
            // Botones en la parte inferior
            btnResolver = new Button
            {
                Text = "🚀 RESOLVER\nPROBLEMA",
                Location = new Point(50, 790),
                Size = new Size(140, 60),
                BackColor = Color.FromArgb(40, 150, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Enabled = false
            };
            btnResolver.Click += BtnResolver_Click;

   

            btnAtras = new Button
            {
                Text = "← REGRESAR\nAL MENÚ",
                Location = new Point(1200, 790),
                Size = new Size(140, 60),
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

            // Limpiar controles anteriores si existen
            LimpiarFormularios();

            // 🔹 Calcular posición justo debajo del último control existente
            int yPos = 0;
            foreach (Control ctrl in panelEntrada.Controls)
            {
                if (ctrl.Bottom > yPos)
                    yPos = ctrl.Bottom + 20; // +20 píxeles de margen
            }

            // FUNCIÓN OBJETIVO
            Label lblTituloFO = new Label
            {
                Text = "📈 FUNCIÓN OBJETIVO:",
                Location = new Point(20, yPos),
                Size = new Size(200, 25),
                ForeColor = Color.LightBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            yPos += 30;

            string tipoObj = rbMaximizar.Checked ? "MAXIMIZAR" : "MINIMIZAR";
            Label lblDescFO = new Label
            {
                Text = $"Coeficientes para {tipoObj} z = c₁x₁ + c₂x₂ + ...",
                Location = new Point(20, yPos),
                AutoSize = true,
                MaximumSize = new Size(400, 0),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8)
            };
            yPos += 25;

            gridFuncionObjetivo = new DataGridView
            {
                Location = new Point(20, yPos),
                Size = new Size(400, 60),
                BackgroundColor = Color.FromArgb(90, 90, 90),
                ForeColor = Color.White,
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersVisible = true,
                RowHeadersVisible = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            for (int i = 0; i < numVars; i++)
                gridFuncionObjetivo.Columns.Add($"x{i + 1}", $"x{i + 1}");

            gridFuncionObjetivo.Rows.Add();
            gridFuncionObjetivo.Rows[0].HeaderCell.Value = "z =";
            yPos += gridFuncionObjetivo.Height + 20;

            // RESTRICCIONES
            Label lblTituloRest = new Label
            {
                Text = "📋 RESTRICCIONES:",
                Location = new Point(20, yPos),
                Size = new Size(200, 25),
                ForeColor = Color.LightCoral,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            yPos += 30;

            Label lblDescRest = new Label
            {
                Text = "Coeficientes y límites: a₁x₁ + a₂x₂ + ... ≤ b",
                Location = new Point(20, yPos),
                AutoSize = true,
                MaximumSize = new Size(400, 0),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8)
            };
            yPos += 25;

            int alturaGrid = Math.Min(200, 40 + numRest * 25);
            gridRestricciones = new DataGridView
            {
                Location = new Point(20, yPos),
                Size = new Size(400, alturaGrid),
                BackgroundColor = Color.FromArgb(90, 90, 90),
                ForeColor = Color.White,
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersVisible = true,
                RowHeadersVisible = true,
                ScrollBars = ScrollBars.Vertical,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            for (int i = 0; i < numVars; i++)
                gridRestricciones.Columns.Add($"x{i + 1}", $"x{i + 1}");

            gridRestricciones.Columns.Add("rhs", "≤");

            for (int i = 0; i < numRest; i++)
            {
                gridRestricciones.Rows.Add();
                gridRestricciones.Rows[i].HeaderCell.Value = $"R{i + 1}";
            }

            // Agregar controles
            panelEntrada.Controls.AddRange(new Control[] {
            lblTituloFO, lblDescFO, gridFuncionObjetivo,
            lblTituloRest, lblDescRest, gridRestricciones
            });

            // Expandir panel si no cabe
            panelEntrada.AutoScrollMinSize = new Size(0, yPos + alturaGrid + 50);

            btnResolver.Enabled = true;
        }


        private void LimpiarFormularios()
        {
            // Remover controles de formularios anteriores
            var controlesARemover = panelEntrada.Controls.OfType<Control>()
                .Where(c => c is DataGridView ||
                           (c is Label && (c.Text.Contains("FUNCIÓN") || c.Text.Contains("RESTRICCIONES") || c.Text.Contains("Coeficientes"))))
                .ToList();

            foreach (var control in controlesARemover)
            {
                panelEntrada.Controls.Remove(control);
                control.Dispose();
            }

            gridFuncionObjetivo = null;
            gridRestricciones = null;
        }

        private void BtnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                // Leer datos y resolver
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

                string[] nombresVariables = new string[numVars];
                for (int i = 0; i < numVars; i++)
                    nombresVariables[i] = $"x{i + 1}";

                // Resolver
                resultado = solver.ResolverSimplex(funcionObjetivo, restricciones, ladoDerecho, maximizar, nombresVariables);
                MostrarResultados();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al resolver: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarResultados()
        {
            if (resultado.TieneSolucion)
            {
                MostrarTablasIteraciones();
                MostrarHistorialCompleto();
                MostrarSolucionOptima();
                MessageBox.Show("✅ ¡Problema resuelto exitosamente!\n\nRevise los resultados en las tres secciones.",
                               "Solución Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtSolucion.Text = $"❌ PROBLEMA SIN SOLUCIÓN:\n{resultado.MensajeError}";
                txtIteraciones.Text = "No se pudo resolver el problema.";
                MessageBox.Show($"❌ El problema no tiene solución:\n{resultado.MensajeError}",
                               "Sin Solución", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarTablasIteraciones()
        {
            // Crear un TabControl para mostrar cada iteración
            TabControl tabControl = new TabControl
            {
                Location = new Point(20, 80), // bajamos un poco para no tapar el label
                Size = new Size(840, 280)
            };

            var variables = resultado.TodasLasVariables;

            for (int iter = 0; iter < resultado.HistorialTablas.Count; iter++)
            {
                var tabla = resultado.HistorialTablas[iter];
                string titulo = iter == 0 ? "Tabla Inicial" : $"Iteración {iter}";

                // Crear una pestaña para esta iteración
                TabPage tabPage = new TabPage(titulo);

                // Crear un DataGridView para mostrar la tabla
                DataGridView grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    BackgroundColor = Color.FromArgb(80, 80, 80),
                    ForeColor = Color.White,
                    GridColor = Color.Gray,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.FromArgb(70, 70, 70),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    }
                };

                // Columna de variables básicas
                grid.Columns.Add("VB", "Variable Básica");
                for (int j = 0; j < variables.Count; j++)
                    grid.Columns.Add(variables[j], variables[j]);

                // 🔹 Variables básicas específicas de esta iteración
                List<string> varsBasicasIter = null;
                if (resultado.HistorialVariablesBasicas != null && resultado.HistorialVariablesBasicas.Count > iter)
                    varsBasicasIter = resultado.HistorialVariablesBasicas[iter];

                // Llenar filas
                for (int i = 0; i < solver.FilasTabla; i++)
                {
                    object[] fila = new object[variables.Count + 1];

                    if (i < solver.FilasTabla - 1)
                        fila[0] = (varsBasicasIter != null && i < varsBasicasIter.Count)
                                    ? varsBasicasIter[i]
                                    : $"s{i + 1}";
                    else
                        fila[0] = "z (Objetivo)";

                    for (int j = 0; j < variables.Count; j++)
                        fila[j + 1] = Math.Round(tabla[i, j], 4).ToString("F4");

                    grid.Rows.Add(fila);
                }

                tabPage.Controls.Add(grid);
                tabControl.TabPages.Add(tabPage);
            }

            // Limpiar y agregar al panel de resultados
            panelResultado.Controls.Remove(gridTabla); // quitamos el grid único
            panelResultado.Controls.Add(tabControl);   // agregamos el TabControl
        }



        private void MostrarHistorialCompleto()
        {
            txtIteraciones.Clear();
            txtIteraciones.AppendText("📋 HISTORIAL COMPLETO DEL MÉTODO SIMPLEX\n");
            txtIteraciones.AppendText("".PadRight(50, '=') + "\n\n");

            for (int iter = 0; iter < resultado.HistorialPasos.Count; iter++)
            {
                string titulo = iter == 0 ? "📊 TABLA INICIAL" : $"🔄 ITERACIÓN {iter}";
                txtIteraciones.AppendText($"{titulo}:\n");
                txtIteraciones.AppendText($"{resultado.HistorialPasos[iter]}\n");
                txtIteraciones.AppendText("".PadRight(40, '-') + "\n");
            }

            txtIteraciones.AppendText($"\n🎯 TOTAL DE ITERACIONES: {resultado.NumeroIteraciones}\n");
            txtIteraciones.AppendText("✅ SOLUCIÓN ÓPTIMA ENCONTRADA");
        }

        private void MostrarSolucionOptima()
        {
            txtSolucion.Clear();
            txtSolucion.AppendText(" SOLUCIÓN ÓPTIMA FINAL\n");
            txtSolucion.AppendText("".PadRight(30, '=') + "\n\n");

            txtSolucion.AppendText(" VARIABLES BÁSICAS:\n");
            foreach (var variable in resultado.VariablesBasicas)
            {
                txtSolucion.AppendText($"   {variable.Key} = {Math.Round(variable.Value, 2):F4}\n,\n");
            }

            txtSolucion.AppendText("\n VARIABLES NO BÁSICAS:\n");
            foreach (var variable in resultado.VariablesNoBasicas)
            {
                txtSolucion.AppendText($"   {variable} = 0.0000\n,\n");
            }

            string tipoOptimo = resultado.EsMaximizacion ? "MÁXIMO" : "MÍNIMO";
            txtSolucion.AppendText($"\n VALOR ÓPTIMO:\n");
            txtSolucion.AppendText($"   z = {Math.Round(resultado.ValorObjetivo, 2):F4} ({tipoOptimo})\n");

        }
    }
}