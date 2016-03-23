using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ColorearPalabras
{
    class Program
    {
        /// <summary>
        /// Recorre linea a linea el texto del fichero e imprime por consola su contenido.
        /// </summary>
        /// <param name="ruta">Ruta del fichero.</param>
        /// <param name="palabrasAPintar">Lista de palabras que se pintarán de otros colores</param>
        static void RecorrerTexto(string ruta, List<string> palabrasAPintar)
        {
            FileStream fichero = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            StreamReader texto = new StreamReader(fichero, Encoding.Default);
            string linea = string.Empty;

            do
            {
                linea = texto.ReadLine();
                Imprimir(linea, palabrasAPintar);
            } while (!texto.EndOfStream);

            texto.Close();
        }


        /// <summary>
        /// Imprime por consola las palabras contenidas en la linea.
        /// Las palabras buscadas se pintarán de otro color.
        /// </summary>
        /// <param name="linea">Linea que se imprimirá por consola.</param>
        /// <param name="palabrasAPintar">Lista de palabras que se pintarán de distintos colores.</param>
        static void Imprimir(string linea, List<string> palabrasAPintar)
        {
            List<string> palabras;
            ConsoleColor backUpColor = Console.ForegroundColor;
            bool encontrado = false;

            palabras = GetPalabras(linea);

            for (int i = 0; i < palabras.Count; i++)
            {
                for (int j = 0; j < palabrasAPintar.Count; j++)
                {
                    if (palabras[i] == palabrasAPintar[j])
                    {
                        Console.ForegroundColor = (ConsoleColor)(j + 1);
                        Console.Write(palabrasAPintar[j] + " ");
                        Console.ForegroundColor = backUpColor;
                        encontrado = true;
                    }
                }

                if (!encontrado)
                    Console.Write(palabras[i] + " ");
                else
                    encontrado = false;
            }

            Console.WriteLine();
        }



        /// <summary>
        /// Obtiene las palabras de un string.
        /// </summary>
        /// <param name="texto">String del cual se obtendrá las palabras que contiene.</param>
        /// <returns>Devuelve una lista con las palabras contenidas en el string.</returns>
        static List<string> GetPalabras(string texto)
        {
            List<string> palabras = new List<string>();
            string palabra = string.Empty;

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == ' ' || texto[i] == ',' || texto[i] == '.')
                {
                    palabras.Add(palabra);
                    palabra = string.Empty;
                }
                else if (i == texto.Length - 1)
                    palabras.Add(palabra);
                else
                    palabra += texto[i];
            }

            return palabras;
        }



        //Método Principal.
        static void Main(string[] args)
        {
            List<string> palabrasAPintar = new List<string>();
            string ruta = args[0];

            if (!File.Exists(ruta))
                Console.WriteLine("La ruta no existe en el sistema ...");
            else
            {
                for (int i = 1; i < args.Length; i++)
                    palabrasAPintar.Add(args[i]);

                RecorrerTexto(ruta, palabrasAPintar);
            }

            Console.ReadLine();
        }
    }
}
