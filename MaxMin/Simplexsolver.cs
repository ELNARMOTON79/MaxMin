using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMin
{
    public class Simplexsolver
    {
        private double[,] tabla;
        private int filas;
        private int columnas;
        private List<string> variablesBasicas;
        private List<string> todasLasVariables;
        private bool esMaximizacion;
        private int iteracion;

        public List<string> HistorialPasos { get; private set; }
        public List<double[,]> HistorialTablas { get; private set; }
        public bool TieneSolucion { get; private set; }
        public string MensajeError { get; private set; }
        public List<List<string>> HistorialVariablesBasicas { get; private set; }

        public Simplexsolver() { 
            iteracion = 0;
            HistorialPasos = new List<string>();
            HistorialTablas = new List<double[,]>();
            HistorialVariablesBasicas = new List<List<string>>();
            TieneSolucion = false;
            MensajeError = "";
        }

        public SimplexResultado ResolverSimplex(double[] funcionObjetivo, double[,] restricciones, double[] ladoDerecho, bool maximizar = true, string[] nombresVariables = null)

        {
            try
            { 
                esMaximizacion = maximizar;
                int numVariables = funcionObjetivo.Length;
                int numRestricciones = restricciones.GetLength(0);


                //configurar nombres de variables
                if (nombresVariables == null)
                {
                    nombresVariables = new string[numVariables];
                    for (int i = 0; i < numVariables; i++)
                        nombresVariables[i] = $"x{i + 1}";
                }

                //iniciar tabla
                IniciarTabla(funcionObjetivo, restricciones, ladoDerecho, nombresVariables);
                GuardarPaso("Tabla inicial creada", (double[,])tabla.Clone());

                //Resolver iterativamente
                while (!EsOptima() && iteracion < 50) //limite de seguridad
                {
                    iteracion++;

                    int columnaEntra = EncontrarColumnaEntra();
                    int filasale = EncontrarFilaSale(columnaEntra);

                    if (filasale == -1)
                    {
                        TieneSolucion = false;
                        MensajeError = "El problema no tiene solución acotada (ilimitado)";
                        return CrearResultado();
                    }

                    string mensajePaso = $"Iteracion {iteracion}: {todasLasVariables[columnaEntra]} entra, {variablesBasicas[filasale]} sale";

                    RealizarPivoteo(filasale, columnaEntra);
                    variablesBasicas[filasale] = todasLasVariables[columnaEntra];

                    GuardarPaso(mensajePaso, (double[,])tabla.Clone());
                }

                TieneSolucion = EsOptima();
                return CrearResultado();
            }
            catch (Exception ex)
            {
                TieneSolucion = false;
                MensajeError = $"Error al resolver: {ex.Message}";
                return CrearResultado();
            }
        }

        private void IniciarTabla(double[] funcionObjetivo, double[,] restricciones, double[] ladoDerecho, string[] nombrarVariables)
        {
            int numVariables = funcionObjetivo.Length;
            int numRestricciones = restricciones.GetLength(0);

            filas = numRestricciones + 1;
            columnas = numVariables + numRestricciones + 1;

            tabla = new double[filas, columnas];
            variablesBasicas = new List<string>();
            todasLasVariables = new List<string>();

            // Nombres de variables
            for (int i = 0; i < numVariables; i++)
                todasLasVariables.Add(nombrarVariables[i]);
            for (int i = 0; i < numRestricciones; i++)
                todasLasVariables.Add($"s{i + 1}");
            todasLasVariables.Add("RHS");

            // Llenar restricciones
            for (int i = 0; i < numRestricciones; i++)
            {
                for (int j = 0; j < numVariables; j++)
                    tabla[i, j] = restricciones[i, j];

                for (int j = 0; j < numRestricciones; j++)
                    tabla[i, numVariables + j] = (i == j) ? 1.0 : 0.0;

                tabla[i, columnas - 1] = ladoDerecho[i];
                variablesBasicas.Add($"s{i + 1}");
            }

            // Fila Z
            int filaZ = numRestricciones;
            for (int j = 0; j < numVariables; j++)
                tabla[filaZ, j] = esMaximizacion ? -funcionObjetivo[j] : funcionObjetivo[j];

            for (int j = numVariables; j < numVariables + numRestricciones; j++)
                tabla[filaZ, j] = 0.0;

            tabla[filaZ, columnas - 1] = 0.0;
        }

        private bool EsOptima() 
        {
            int filaZ = filas - 1;
            for (int j = 0; j < columnas - 1; j++)
            {
                if (esMaximizacion && tabla[filaZ, j] < -1e-10) return false;
                if (!esMaximizacion && tabla[filaZ, j] > 1e-10) return false;
            }
            return true;
        }

        private int EncontrarColumnaEntra()
        {
            int filaZ = filas - 1;
            int columnaEntra = -1;
            double mejorValor = 0;

            for (int j = 0; j < columnas - 1; j++)
            {
                if (esMaximizacion && tabla[filaZ, j] < mejorValor)
                {
                    mejorValor = tabla[filaZ, j];
                    columnaEntra = j;
                }
                else if (!esMaximizacion && tabla[filaZ, j] > mejorValor)
                {
                    mejorValor = tabla[filaZ, j];
                    columnaEntra = j;
                }
            }
            return columnaEntra;
        }

        private int EncontrarFilaSale(int columnaEntra) 
        {
            int filaSale = -1;
            double menorRazon = double.MaxValue;

            for (int i = 0; i < filas - 1; i++)
            {
                double coeficiente = tabla[i, columnaEntra];
                double rhs = tabla[i, columnas - 1];

                if (coeficiente > 1e-10)
                {
                    double razon = rhs / coeficiente;
                    if (razon < menorRazon)
                    {
                        menorRazon = razon;
                        filaSale = i;
                    }
                }
            }
            return filaSale;
        }

        private void RealizarPivoteo(int filaPivote, int columnaPivote)
        {
            double elementoPivote = tabla[filaPivote, columnaPivote];

            // Hacer pivote = 1
            for (int j = 0; j < columnas; j++)
                tabla[filaPivote, j] /= elementoPivote;

            // Hacer ceros en columna pivote
            for (int i = 0; i < filas; i++)
            {
                if (i != filaPivote)
                {
                    double factor = tabla[i, columnaPivote];
                    for (int j = 0; j < columnas; j++)
                        tabla[i, j] -= factor * tabla[filaPivote, j];
                }
            }
        }

        private void GuardarPaso(string descripcion, double[,] tablaActual)
        {
            HistorialPasos.Add(descripcion);
            HistorialTablas.Add((double[,])tablaActual.Clone());

            // 🔹 Guardar copia del estado de las variables básicas en este paso
            HistorialVariablesBasicas.Add(new List<string>(variablesBasicas));
        }

        private SimplexResultado CrearResultado()
        {

            var resultado = new SimplexResultado
            {
                TieneSolucion = this.TieneSolucion,
                MensajeError = this.MensajeError,
                NumeroIteraciones = this.iteracion,
                VariablesBasicas = new Dictionary<string, double>(),
                VariablesNoBasicas = new List<string>(),
                HistorialPasos = this.HistorialPasos,
                HistorialTablas = this.HistorialTablas,
                TodasLasVariables = new List<string>(this.todasLasVariables),
                HistorialVariablesBasicas = this.HistorialVariablesBasicas
            };

            if (TieneSolucion)
            {
                // Variables básicas
                for (int i = 0; i < variablesBasicas.Count; i++)
                {
                    resultado.VariablesBasicas[variablesBasicas[i]] = tabla[i, columnas - 1];
                }

                // Variables no básicas
                var todasVars = todasLasVariables.Take(todasLasVariables.Count - 1).ToList();
                resultado.VariablesNoBasicas = todasVars.Except(variablesBasicas).ToList();

                // Valor objetivo
                double valorZ = tabla[filas - 1, columnas - 1];
                resultado.ValorObjetivo = esMaximizacion ? valorZ : -valorZ;
                resultado.EsMaximizacion = esMaximizacion;
            }

            return resultado;
        }

        // Propiedades públicas para acceso desde la interfaz
        public double[,] TablaActual => tabla;
        public List<string> NombresVariables => todasLasVariables;
        public List<string> VariablesBasicasActuales => variablesBasicas;
        public int FilasTabla => filas;
        public int ColumnasTabla => columnas;

    }

    // Clase para encapsular el resultado
    public class SimplexResultado
    {
        public bool TieneSolucion { get; set; }
        public string MensajeError { get; set; }
        public int NumeroIteraciones { get; set; }
        public Dictionary<string, double> VariablesBasicas { get; set; }
        public List<string> VariablesNoBasicas { get; set; }
        public double ValorObjetivo { get; set; }
        public bool EsMaximizacion { get; set; }
        public List<string> HistorialPasos { get; set; }
        public List<double[,]> HistorialTablas { get; set; }
        public List<string> TodasLasVariables { get; set; }
        public List<List<string>> HistorialVariablesBasicas { get; set; }

    }
}
