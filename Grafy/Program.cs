using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Grafy
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamWriter writer = new StreamWriter("p1.txt");
            writer.AutoFlush=true;
            var rand = new Random();
            const int cN = 1000;
            const double cGestosc = 0.2;
            for (int y = 1; y <= 15; y++)
            {
                int n = cN * y;
                for (int r = 1; r <= 2; r++)
                {
                    double gestosc = cGestosc * r;
                    writer.WriteLine("\n\nn: {0} gestosc: {1}",n,gestosc);
                    writer.WriteLine();
                    writer.WriteLine("etykiety           nastepniki         luki               macierz            liczba lukow");

                    for (int u = 0; u < 10; u++)
                    {
                        //var watch = System.Diagnostics.Stopwatch.StartNew();
                        Macierz macierz = new Macierz(n);

                        for (int m = 0; m < (n * n - n) * gestosc; m++)
                        {
                            int i = rand.Next(n);
                            int j = rand.Next(n);
                            if (!macierz.DodajLuk(i, j))
                                m--;
                        }
                        //Console.WriteLine(watch.Elapsed);

                        //Console.WriteLine("graf jako macierz:\n");
                        //macierz.WypiszMacierz();

                        ListaNast lista = new ListaNast(n);
                        //watch = System.Diagnostics.Stopwatch.StartNew();
                        lista.PrzepiszMacierz(macierz.macierz, n);
                        //Console.WriteLine(watch.Elapsed);
                        /*for (int m = 0; m < (n * n - n) * gestosc; m++)
                        {
                            int i = rand.Next(n);
                            int j = rand.Next(n);
                            if (!lista.tablica[i].Wstaw(j))
                                m--;
                        }*/

                        //Console.WriteLine("\ngraf jako lista nastepnikow:\n");
                        //lista.WypiszListeNast();

                        ListaLukow.Lista listaLukow = new ListaLukow.Lista();
                        //watch = System.Diagnostics.Stopwatch.StartNew();
                        listaLukow.PrzepiszMacierz(macierz.macierz, n);
                        //Console.WriteLine(watch.Elapsed);

                        //Console.WriteLine("\ngraf jako lista lukow:\n");
                        //listaLukow.WypiszListe();

                        //Console.WriteLine("\n");

                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        var etykiety = lista.GenerowanieEtykiet();
                        writer.Write(+watch.Elapsed + "   ");

                        watch = System.Diagnostics.Stopwatch.StartNew();
                        lista.IloscPowrotnych(etykiety);
                        writer.Write(watch.Elapsed + "   ");

                        watch = System.Diagnostics.Stopwatch.StartNew();
                        listaLukow.IloscPowrotnych(etykiety);
                        writer.Write(watch.Elapsed + "   ");

                        watch = System.Diagnostics.Stopwatch.StartNew();
                        macierz.IloscPowrotnych(etykiety);
                        writer.Write(watch.Elapsed + "   ");

                        writer.WriteLine(lista.IloscPowrotnych(etykiety));

                        //Console.WriteLine("\nIlosc lukow powrotnych (lista nastepnikow): " + lista.IloscPowrotnych(etykiety));
                        //Console.WriteLine("Ilosc lukow powrotnych (lista lukow): " + listaLukow.IloscPowrotnych(etykiety));
                        //Console.WriteLine("Ilosc lukow powrotnych (macierz sasiedztwa): " + macierz.IloscPowrotnych(etykiety));
                    }
                    writer.WriteLine();
                }
            }
            //Console.Read();
        }
    }
}