using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy
{
    class ListaLukow
    {
        public class Element       // klasa opisująca poszczególny element listy - wartość danego elementu oraz zmienną przechowującą następny element listy
        {
            private int[] luk;
            private Element _nastepny;

            public Element(int i,int j)
            {
                luk = new int[2];
                luk[0] = i;
                luk[1] = j;
                _nastepny = null;
            }
            public int[] ZwrocWartosc() { return luk; }
            public Element ZwrocNastepny() { return _nastepny; }
            public void UstawNastepny(int i,int j)
            {
                _nastepny = new Element(i,j);
            }
        }

        public class Lista // klasa implementująca listę jednokierunkową
        {
            private Element _korzen;
            private Element _ostatni;

            public Lista()
            {
                _korzen = null;
                _ostatni = null;
            }

            public bool Wstaw(int i, int j)
            {
                int[] tab = new int[2];
                tab[0] = i;
                tab[1] = j;
                if (_korzen == null)
                {
                    _korzen = new Element(i, j);
                    return true;
                }
                else
                {
                    Element tmp = _korzen;
                    if (tmp.ZwrocWartosc() == tab)
                    {
                        return false;
                    }
                    while (tmp.ZwrocNastepny() != null)
                    {
                        tmp = tmp.ZwrocNastepny();      // przechodzimy na koniec listy, czyli do elementu, który nie posiada elementu następnego.
                        if (tmp.ZwrocWartosc() == tab)
                        {
                            return false;
                        }
                    }
                    tmp.UstawNastepny(i, j);
                    return true;
                }
            }

            public void Wstaw2(int i, int j)
            {
                if (_korzen == null)
                {
                    _korzen = new Element(i, j);
                    _ostatni = _korzen;
                }
                else
                {
                    _ostatni.UstawNastepny(i,j);
                    _ostatni = _ostatni.ZwrocNastepny();
                }
            }

            public void WypiszListe()
            {
                Element tmp = _korzen;
                while (tmp.ZwrocNastepny() != null)
                {
                    Console.Write(tmp.ZwrocWartosc()[0] + "->" + tmp.ZwrocWartosc()[1] + "\n");
                    tmp = tmp.ZwrocNastepny();      // przechodzimy na koniec listy, czyli do elementu, który nie posiada elementu następnego.
                }
                Console.Write(tmp.ZwrocWartosc()[0] + "->" + tmp.ZwrocWartosc()[1]);
            }

            public void PrzepiszMacierz(byte[,] macierz, int n)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (macierz[i, j] == 1)
                        {
                            this.Wstaw2(i, j);
                        }
                    }
                }
            }

            public int IloscPowrotnych(int[,] etykiety)
            {
                int ilosc = 0,u,v;

                var tmp = this._korzen;
                while (tmp.ZwrocNastepny() != null)
                {
                    u = tmp.ZwrocWartosc()[0];
                    v = tmp.ZwrocWartosc()[1];
                    if (etykiety[0, v] < etykiety[0, u] && etykiety[0, u] < etykiety[1, u] &&
                        etykiety[1, u] < etykiety[1, v])
                        ilosc++;
                    tmp=tmp.ZwrocNastepny();
                }
                u = tmp.ZwrocWartosc()[0];
                v = tmp.ZwrocWartosc()[1];
                if (etykiety[0, v] < etykiety[0, u] && etykiety[0, u] < etykiety[1, u] &&
                    etykiety[1, u] < etykiety[1, v])
                    ilosc++;

                return ilosc;
            }
        }
    }
}
