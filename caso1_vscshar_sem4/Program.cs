using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caso1_vscshar_sem4
{
    internal class Program
    {
    
        // --- 1. ALCANCE DE VARIABLES (Variable Global estática) ---
        static string NOMBRE_INSTITUCION = "---------------****UPN****----------";

        static void MostrarEncabezado()
        {
            /* 
               Función SIN retorno (Procedimiento -> void).
               Imprime el título institucional en pantalla.
            */
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"   {NOMBRE_INSTITUCION} - CÁLCULO DE PROMEDIO PONDERADO");
            Console.WriteLine(new string('=', 50));
        }

        static double ValidarNota(string mensaje)
        {
            /* 
               Función CON retorno (devuelve un double).
               Valida que la entrada sea numérica y esté entre 0.0 y 20.0,
               previniendo errores de conversión en C#.
            */
            double nota;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                // double.TryParse evita que el programa falle si ingresan letras
                if (double.TryParse(entrada, out nota) && nota >= 0.0 && nota <= 20.0)
                {
                    return nota;
                }
                Console.WriteLine("❌ Error: Ingresa un número válido entre 0.0 y 20.0.");
            }
        }

        static double CalcularExamenFinalBase(double proyectoFinal, double laboratorio)
        {
            /* 
               Función CON retorno.
               Calcula la nota base del examen final (Proyecto 40% + Laboratorio 60%).
            */
            double notaEfBase = (proyectoFinal * 0.40) + (laboratorio * 0.60);
            return notaEfBase;
        }

        static double AplicarBonoCisco(double notaEf, string tieneCisco)
        {
            /* 
               Función CON retorno.
               Aplica +1.0 punto extra si completó Cisco (máximo 20.0).
            */
            if (tieneCisco == "s")
            {
                notaEf += 1.0;
                if (notaEf > 20.0)
                {
                    notaEf = 20.0;
                }
            }
            return notaEf;
        }

        static double CalcularPromedioTotal(double t1, double t2, double t3, double parcial, double notaExamenFinal)
        {
            /* 
               Función CON retorno.
               Calcula el promedio ponderado final con todos los pesos.
            */
            double promedio = (t1 * 0.10) +
                              (t2 * 0.10) +
                              (t3 * 0.10) +
                              (parcial * 0.20) +
                              (notaExamenFinal * 0.50);
            return promedio;
        }

        static string ObtenerEstadoAcademico(double promedio)
        {
            /* 
               Función CON retorno (devuelve un string).
               Evalúa si el estudiante aprobó (>= 12).
            */
            string estado;
            if (promedio >= 12.0)
            {
                estado = "APROBADO";
            }
            else
            {
                estado = "DESAPROBADO";
            }
            return estado;
        }

        // ==========================================
        // MÉTODO PRINCIPAL (PROGRAMA PRINCIPAL)
        // ==========================================
        static void Main(string[] args)
        {
            // 1. Llamada a función sin retorno (void)
            MostrarEncabezado();

            // 2. Captura de datos interactiva del usuario
            Console.Write("Ingresa el nombre del estudiante: ");
            string nombreAlumno = Console.ReadLine();

            Console.WriteLine("\n--- INGRESO DE NOTAS CONTINUAS Y PARCIAL ---");
            double notaT1 = ValidarNota("Ingresa la nota de la T1 (10%): ");
            double notaT2 = ValidarNota("Ingresa la nota de la T2 (10%): ");
            double notaT3 = ValidarNota("Ingresa la nota de la T3 (10%): ");
            double notaParcial = ValidarNota("Ingresa la nota del Examen Parcial (20%): ");

            Console.WriteLine("\n--- INGRESO DE COMPONENTES DEL EXAMEN FINAL (50%) ---");
            double notaProyecto = ValidarNota("Ingresa la nota del Proyecto Final [40% del EF]: ");
            double notaLab = ValidarNota("Ingresa la nota del Laboratorio [60% del EF]:    ");

            string ciscoInput;
            while (true)
            {
                Console.Write("¿Completó el curso de Cisco? (s/n): ");
                ciscoInput = Console.ReadLine()?.Trim().ToLower();
                if (ciscoInput == "s" || ciscoInput == "n")
                {  
                    break;
                }
                Console.WriteLine("❌ Error: Ingresa únicamente 's' (sí) o 'n' (no).");
            }

            // 3. Procesamiento modular usando las funciones con retorno
            double notaEfCalculada = CalcularExamenFinalBase(notaProyecto, notaLab);
            double notaEfFinal = AplicarBonoCisco(notaEfCalculada, ciscoInput);
            double promedioFinal = CalcularPromedioTotal(notaT1, notaT2, notaT3, notaParcial, notaEfFinal);
            string estadoAlumno = ObtenerEstadoAcademico(promedioFinal);

            // 4. Reporte final en pantalla
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"          REPORTE FINAL: {nombreAlumno.ToUpper()}");
            Console.WriteLine(new string('=', 50));
            if (ciscoInput == "s")
            {
                Console.WriteLine("¡Bono de Cisco aplicado (+1.0 en Examen Final)!");
            }
            Console.WriteLine($"Nota Examen Final (con/sin bono): {notaEfFinal:F2}");
            Console.WriteLine($"Promedio Ponderado Final        : {promedioFinal:F2}");
            Console.WriteLine($"Condición Académica             : {estadoAlumno}");
            Console.WriteLine(new string('=', 50));
        }

    }
        
    }

