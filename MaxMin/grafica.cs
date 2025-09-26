using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaxMin
{
    public partial class grafica : Form
    {
        // Controles de la interfaz gráfica
        private Panel panelEntrada;
        private Panel panelGrafica;
        private DataGridView gridFuncionObjetivo;
        private DataGridView gridRestricciones;
        private Button btnGenerarCampos;
        private Button btnResolver;
        private Button btnAtras;
        private RadioButton rbMaximizar;
        private RadioButton rbMinimizar;
        private PictureBox pictureGrafica;
        private NumericUpDown numVariablesControl;
        private NumericUpDown numRestriccionesControl;
        private TextBox txtPasos;

        public grafica()
        {
            InitializeComponent();
            InicializarComponentesGrafico();
        }

        private void InicializarComponentesGrafico()
        {
            this.Size = new Size(1600, 1000); // Ventana más grande
            this.Text = "Método Gráfico - MaxMin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CrearPanelEntrada();
            CrearPanelGrafica();
            CrearBotonesGrafico();
        }

        private void CrearPanelEntrada()
        {
            panelEntrada = new Panel
            {
                Location = new Point(30, 30),
                Size = new Size(400, 600), // Altura reducida
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            Label lblTitulo = new Label
            {
                Text = "Configuración del Problema",
                Location = new Point(20, 20),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            Label lblVariables = new Label
            {
                Text = "Número de variables:",
                Location = new Point(20, 70),
                Size = new Size(170, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            numVariablesControl = new NumericUpDown
            {
                Location = new Point(220, 70),
                Size = new Size(80, 25),
                Minimum = 2,
                Maximum = 10,
                Value = 2,
                ForeColor = Color.Black // Color negro para los números
            };

            Label lblRestricciones = new Label
            {
                Text = "Número de restricciones:",
                Location = new Point(20, 110),
                Size = new Size(170, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            numRestriccionesControl = new NumericUpDown
            {
                Location = new Point(220, 110),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 10,
                Value = 4,
                ForeColor = Color.Black // Color negro para los números
            };

            GroupBox gbTipo = new GroupBox
            {
                Text = "Tipo de optimización",
                Location = new Point(20, 150),
                Size = new Size(350, 60),
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
                Location = new Point(140, 25),
                Size = new Size(100, 25),
                ForeColor = Color.White
            };
            gbTipo.Controls.AddRange(new Control[] { rbMaximizar, rbMinimizar });

            btnGenerarCampos = new Button
            {
                Text = "Generar Campos",
                Location = new Point(20, 230),
                Size = new Size(350, 40),
                BackColor = Color.FromArgb(60, 80, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnGenerarCampos.Click += BtnGenerarCampos_Click;

            panelEntrada.Controls.AddRange(new Control[] {
                lblTitulo, lblVariables, numVariablesControl, lblRestricciones, numRestriccionesControl, gbTipo, btnGenerarCampos
            });

            this.Controls.Add(panelEntrada);
        }

        private void CrearPanelGrafica()
        {
            panelGrafica = new Panel
            {
                Location = new Point(450, 30),
                Size = new Size(1100, 900), // Panel más grande
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label lblGrafica = new Label
            {
                Text = "Gráfica del Problema",
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            pictureGrafica = new PictureBox
            {
                Location = new Point(20, 60),
                Size = new Size(1050, 600), // PictureBox más grande
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            txtPasos = new TextBox
            {
                Location = new Point(20, 680),
                Size = new Size(1050, 200), // Más espacio para los pasos
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };

            panelGrafica.Controls.AddRange(new Control[] { lblGrafica, pictureGrafica, txtPasos });
            this.Controls.Add(panelGrafica);
        }

        private void CrearBotonesGrafico()
        {
            int btnHeight = 45;
            int btnWidth = 220;
            int margin = 40;
            int bottomY = this.ClientSize.Height - btnHeight - margin;

            btnResolver = new Button
            {
                Text = "Resolver Gráficamente",
                Location = new Point(margin, bottomY),
                Size = new Size(btnWidth, btnHeight),
                BackColor = Color.FromArgb(60, 150, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Enabled = false,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnResolver.Click += BtnResolver_Click;

            btnAtras = new Button
            {
                Text = "← Atrás",
                Location = new Point(margin + btnWidth + 20, bottomY), // Justo a la derecha de btnResolver
                Size = new Size(btnWidth, btnHeight),
                BackColor = Color.FromArgb(150, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnAtras.Click += btn_atras_Click;

            this.Controls.AddRange(new Control[] { btnResolver, btnAtras });
        }

        private void BtnGenerarCampos_Click(object sender, EventArgs e)
        {
            int numVars = (int)numVariablesControl.Value;
            int numRest = (int)numRestriccionesControl.Value;

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
                Location = new Point(20, 290),
                Size = new Size(350, 50), // Más ancho
                BackgroundColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.Black, // Color negro para los números
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersVisible = true,
                RowHeadersVisible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ScrollBars = ScrollBars.Horizontal,
                DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.White }
            };
            gridFuncionObjetivo.Columns.Clear();
            for (int i = 0; i < numVars; i++)
            {
                gridFuncionObjetivo.Columns.Add($"x{i + 1}", $"x{i + 1}");
                gridFuncionObjetivo.Columns[i].Width = 60;
            }
            gridFuncionObjetivo.Rows.Add();

            // Grid para restricciones
            gridRestricciones = new DataGridView
            {
                Location = new Point(20, 350),
                Size = new Size(350, 40 * numRest + 40), // Más alto y ancho
                BackgroundColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.Black, // Color negro para los números
                GridColor = Color.Gray,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersVisible = true,
                RowHeadersVisible = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ScrollBars = ScrollBars.Both,
                DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.White }
            };
            gridRestricciones.Columns.Clear();
            for (int i = 0; i < numVars; i++)
            {
                gridRestricciones.Columns.Add($"x{i + 1}", $"x{i + 1}");
                gridRestricciones.Columns[i].Width = 50;
            }
            // Columna para operador <= o >=
            var operadorCol = new DataGridViewComboBoxColumn
            {
                Name = "operador",
                HeaderText = "Operador",
                Width = 60,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.LightGray,
                    SelectionForeColor = Color.Black
                }
            };
            operadorCol.Items.AddRange(new string[] { "≤", "≥" });
            gridRestricciones.Columns.Add(operadorCol);
            // Columna para el lado derecho
            var rhsCol = new DataGridViewTextBoxColumn
            {
                Name = "rhs",
                HeaderText = "Valor",
                Width = 60
            };
            gridRestricciones.Columns.Add(rhsCol);
            for (int i = 0; i < numRest; i++)
            {
                gridRestricciones.Rows.Add();
                gridRestricciones.Rows[i].HeaderCell.Value = $"R{i + 1}";
                gridRestricciones.Rows[i].Cells[gridRestricciones.Columns["operador"].Index].Value = "≤"; // Valor por defecto
            }

            panelEntrada.Controls.AddRange(new Control[] { gridFuncionObjetivo, gridRestricciones });
            btnResolver.Enabled = true;
        }

        private void BtnResolver_Click(object sender, EventArgs e)
        {
            int numVars = gridFuncionObjetivo.ColumnCount;
            int numRest = gridRestricciones.RowCount;
            if (numVars != 2)
            {
                MessageBox.Show("La gráfica solo está disponible para 2 variables.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Leer función objetivo
            double[] funcionObjetivo = new double[2];
            for (int i = 0; i < 2; i++)
            {
                double val = 0;
                double.TryParse(gridFuncionObjetivo.Rows[0].Cells[i].Value?.ToString(), out val);
                funcionObjetivo[i] = val;
            }

            // Leer restricciones
            double[,] restricciones = new double[numRest, 2];
            string[] operadores = new string[numRest];
            double[] rhs = new double[numRest];
            for (int i = 0; i < numRest; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    double val = 0;
                    double.TryParse(gridRestricciones.Rows[i].Cells[j].Value?.ToString(), out val);
                    restricciones[i, j] = val;
                }
                operadores[i] = gridRestricciones.Rows[i].Cells[2].Value?.ToString() ?? "≤";
                double valRhs = 0;
                double.TryParse(gridRestricciones.Rows[i].Cells[3].Value?.ToString(), out valRhs);
                rhs[i] = valRhs;
            }

            // Graficar restricciones
            Bitmap bmp = new Bitmap(pictureGrafica.Width, pictureGrafica.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Pen penRestriccion = new Pen(Color.Blue, 2);
            Pen penFactible = new Pen(Color.Green, 2);
            Pen penEjes = new Pen(Color.Black, 2);
            Pen penGrid = new Pen(Color.LightGray, 1);
            Font fontEjes = new Font("Segoe UI", 9);
            Brush brushEjes = Brushes.Black;
            Brush brushPunto = Brushes.Red;
            Font fontPunto = new Font("Segoe UI", 8, FontStyle.Bold);
            // Parámetros del plano cartesiano
            int margen = 40;
            int escala = 30;
            int xMax = (bmp.Width - 2 * margen) / escala;
            int yMax = (bmp.Height - 2 * margen) / escala;
            // Dibujar cuadrícula
            for (int i = 0; i <= xMax; i++)
            {
                int x = margen + i * escala;
                g.DrawLine(penGrid, x, margen, x, bmp.Height - margen);
            }
            for (int i = 0; i <= yMax; i++)
            {
                int y = bmp.Height - margen - i * escala;
                g.DrawLine(penGrid, margen, y, bmp.Width - margen, y);
            }
            // Dibujar ejes
            g.DrawLine(penEjes, margen, bmp.Height - margen, bmp.Width - margen, bmp.Height - margen); // eje X
            g.DrawLine(penEjes, margen, bmp.Height - margen, margen, margen); // eje Y
            // Etiquetas de los ejes
            g.DrawString("X", fontEjes, brushEjes, bmp.Width - margen - 15, bmp.Height - margen + 5);
            g.DrawString("Y", fontEjes, brushEjes, margen - 20, margen - 10);
            // Números en los ejes
            for (int i = 0; i <= xMax; i++)
            {
                int x = margen + i * escala;
                g.DrawString(i.ToString(), fontEjes, brushEjes, x - 8, bmp.Height - margen + 5);
            }
            for (int i = 0; i <= yMax; i++)
            {
                int y = bmp.Height - margen - i * escala;
                g.DrawString(i.ToString(), fontEjes, brushEjes, margen - 30, y - 8);
            }
            // Graficar restricciones y puntos de inicio
            var puntosRestricciones = new System.Collections.Generic.List<(float x, float y, string etiqueta)>();
            for (int i = 0; i < numRest; i++)
            {
                double a = restricciones[i, 0];
                double b = restricciones[i, 1];
                double c = rhs[i];
                var puntos = new System.Collections.Generic.List<PointF>();
                // Intersección con x=0 (eje Y)
                if (b != 0)
                {
                    double y = c / b;
                    if (y >= 0 && y <= yMax)
                    {
                        float px = margen;
                        float py = bmp.Height - margen - (float)(y * escala);
                        puntos.Add(new PointF(px, py));
                        puntosRestricciones.Add((px, py, $"(0, {y:0.##})"));
                    }
                }
                // Intersección con y=0 (eje X)
                if (a != 0)
                {
                    double x = c / a;
                    if (x >= 0 && x <= xMax)
                    {
                        float px = margen + (float)(x * escala);
                        float py = bmp.Height - margen;
                        puntos.Add(new PointF(px, py));
                        puntosRestricciones.Add((px, py, $"({x:0.##}, 0)"));
                    }
                }
                // Intersección con x=xMax
                if (b != 0)
                {
                    double y = (c - a * xMax) / b;
                    if (y >= 0 && y <= yMax)
                    {
                        float px = margen + xMax * escala;
                        float py = bmp.Height - margen - (float)(y * escala);
                        puntos.Add(new PointF(px, py));
                        puntosRestricciones.Add((px, py, $"({xMax}, {y:0.##})"));
                    }
                }
                // Intersección con y=yMax
                if (a != 0)
                {
                    double x = (c - b * yMax) / a;
                    if (x >= 0 && x <= xMax)
                    {
                        float px = margen + (float)(x * escala);
                        float py = bmp.Height - margen - yMax * escala;
                        puntos.Add(new PointF(px, py));
                        puntosRestricciones.Add((px, py, $"({x:0.##}, {yMax})"));
                    }
                }
                if (puntos.Count >= 2)
                {
                    puntos.Sort((p1, p2) => p1.X.CompareTo(p2.X));
                    g.DrawLine(penRestriccion, puntos[0], puntos[1]);
                }
            }
            // Dibujar puntos de inicio
            foreach (var punto in puntosRestricciones)
            {
                g.FillEllipse(brushPunto, punto.x - 4, punto.y - 4, 8, 8);
                g.DrawString(punto.etiqueta, fontPunto, brushPunto, punto.x + 5, punto.y - 18);
            }
            // Calcular y dibujar intersecciones entre restricciones
            for (int i = 0; i < numRest; i++)
            {
                double a1 = restricciones[i, 0];
                double b1 = restricciones[i, 1];
                double c1 = rhs[i];
                for (int j = i + 1; j < numRest; j++)
                {
                    double a2 = restricciones[j, 0];
                    double b2 = restricciones[j, 1];
                    double c2 = rhs[j];
                    double det = a1 * b2 - a2 * b1;
                    if (Math.Abs(det) > 1e-8)
                    {
                        double x = (c1 * b2 - c2 * b1) / det;
                        double y = (a1 * c2 - a2 * c1) / det;
                        if (x >= 0 && x <= xMax && y >= 0 && y <= yMax)
                        {
                            float px = margen + (float)(x * escala);
                            float py = bmp.Height - margen - (float)(y * escala);
                            g.FillEllipse(Brushes.Orange, px - 5, py - 5, 10, 10);
                            g.DrawString($"({x:0.##}, {y:0.##})", fontPunto, Brushes.Orange, px + 5, py - 18);
                        }
                    }
                }
            }
            pictureGrafica.Image = bmp;
            // Mostrar pasos
            string pasos = "Pasos realizados:\r\n";
            pasos += "1. Se grafican las restricciones como rectas en el plano cartesiano.\r\n";
            pasos += "2. Se dibujan los puntos de inicio (intersección con los ejes) usando los datos ingresados:\r\n";
            for (int i = 0; i < numRest; i++)
            {
                double a = restricciones[i, 0];
                double b = restricciones[i, 1];
                double c = rhs[i];
                pasos += $"   Restricción R{i + 1}: {a}*x + {b}*y = {c}\r\n";
                if (b != 0)
                    pasos += $"     Para x=0: y = {c}/{b} = {(c / b):0.##}\r\n";
                if (a != 0)
                    pasos += $"     Para y=0: x = {c}/{a} = {(c / a):0.##}\r\n";
            }
            pasos += "3. Se dibujan los puntos de intersección entre restricciones usando los datos ingresados:\r\n";
            for (int i = 0; i < numRest; i++)
            {
                double a1 = restricciones[i, 0];
                double b1 = restricciones[i, 1];
                double c1 = rhs[i];
                for (int j = i + 1; j < numRest; j++)
                {
                    double a2 = restricciones[j, 0];
                    double b2 = restricciones[j, 1];
                    double c2 = rhs[j];
                    double det = a1 * b2 - a2 * b1;
                    if (Math.Abs(det) > 1e-8)
                    {
                        double x = (c1 * b2 - c2 * b1) / det;
                        double y = (a1 * c2 - a2 * c1) / det;
                        pasos += $"   Entre R{i + 1} y R{j + 1}:\r\n";
                        pasos += $"     Sistema: {a1}*x + {b1}*y = {c1}, {a2}*x + {b2}*y = {c2}\r\n";
                        pasos += $"     x = ({c1}*{b2} - {c2}*{b1}) / ({a1}*{b2} - {a2}*{b1}) = {x:0.##}\r\n";
                        pasos += $"     y = ({a1}*{c2} - {a2}*{c1}) / ({a1}*{b2} - {a2}*{b1}) = {y:0.##}\r\n";
                    }
                }
            }
            pasos += "4. Se determina la región factible (no implementado en esta versión).\r\n";
            pasos += "5. Se evalúa la función objetivo en los vértices de la región factible (no implementado en esta versión).\r\n";
            pasos += "6. Se selecciona el óptimo según el tipo de optimización.\r\n";
            txtPasos.Text = pasos;
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu menu = new menu();
            menu.ShowDialog();
            this.Close();
        }
    }
}
