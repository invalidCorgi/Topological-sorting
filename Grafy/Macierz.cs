using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy
{
    class Macierz
    {
        public int[,] macierz;
        private int _rozmiar;

        public Macierz(int n)
        {
            macierz = new int[n,n];
            _rozmiar = n;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    macierz[i, j] = 0;
                }
            }
        }

        public bool DodajLuk(int i,  int j)
        {
            if (macierz[i, j] == 1 || i==j)
            {
                return false;
            }
            macierz[i, j] = 1;
            return true;
        }

        public void WypiszMacierz()
        {
            for (int i = 0; i < _rozmiar; i++)
            {
                for (int j = 0; j < _rozmiar; j++)
                {
                    Console.Write(macierz[i,j]);
                }
                Console.WriteLine();
            }
        }

        public int IloscPowrotnych(int[,] etykiety)
        {
            int ilosc = 0;

            for (int u = 0; u < _rozmiar; u++)
            {
                for (int v = 0; v < _rozmiar; v++)
                {
                    if(macierz[u,v]==1)
                        if (etykiety[0, v] < etykiety[0, u] && etykiety[0, u] < etykiety[1, u] &&
                            etykiety[1, u] < etykiety[1, v])
                            ilosc++;
                }
            }
            
            return ilosc;
        }
    }
}
