using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy
{
    class ListaNast
    {
        public Lista[] tablica;

        public void WypiszListeNast()
        {
            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i].WypiszListe();
                Console.WriteLine();
            }
        }

        public void PrzepiszMacierz(int[,] macierz, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (macierz[i, j] == 1)
                    {
                        tablica[i].Wstaw2(j);
                    }
                }
            }
        }

        public int IloscPowrotnych(int[,] etykiety)
        {
            int ilosc = 0;

            for (int i = 0; i < tablica.Length; i++)
            {
                var tmp = tablica[i]._korzen;
                while (tmp.ZwrocNastepny() != null)
                {
                    tmp = tmp.ZwrocNastepny();
                    int n = tmp.ZwrocWartosc();
                    if (etykiety[0, n] < etykiety[0, i] && etykiety[0, i] < etykiety[1, i] &&
                        etykiety[1, i] < etykiety[1, n])
                        ilosc++;
                }
            }

            return ilosc;
        }

        public int[,] GenerowanieEtykiet()
        {
            bool[] odwiedzone = new bool[tablica.Length];
            for (int i = 0; i < odwiedzone.Length; i++)
            {
                odwiedzone[i] = false;
            }
            int[,] etykiety = new int[2,tablica.Length];
            Stack<Element> stack = new Stack<Element>();
            bool koniec = false;
            bool pop;
            int tempElem=0;
            int kolejnosc = 1;

            do
            {
                int i = 0;
                do
                {
                    if (odwiedzone[i] == false)
                    {
                        stack.Push(tablica[i]._korzen);
                        etykiety[0, i] = kolejnosc++;
                        odwiedzone[i] = true;
                        tempElem = i;
                        break;
                    }
                    i++;
                } while (i < odwiedzone.Length);
                while(stack.Any())
                {
                    pop = true;
                    var tmp = tablica[tempElem]._korzen;
                    while (tmp.ZwrocNastepny() != null)
                    {
                        tmp = tmp.ZwrocNastepny();
                        if (odwiedzone[tmp.ZwrocWartosc()] == false)
                        {
                            stack.Push(tablica[tmp.ZwrocWartosc()]._korzen);
                            etykiety[0, tmp.ZwrocWartosc()] = kolejnosc++;
                            odwiedzone[tmp.ZwrocWartosc()] = true;
                            tempElem = tmp.ZwrocWartosc();
                            pop = false;
                            break;
                        }
                        if (tmp.ZwrocNastepny() == null)
                        {
                            if (odwiedzone[tmp.ZwrocWartosc()] == false)
                            {
                                stack.Push(tablica[tmp.ZwrocWartosc()]._korzen);
                                etykiety[0, tmp.ZwrocWartosc()] = kolejnosc++;
                                odwiedzone[tmp.ZwrocWartosc()] = true;
                                tempElem = tmp.ZwrocWartosc();
                                pop = false;
                                break;
                            }
                        }
                    }
                    if (pop)
                    {
                        tmp = stack.Pop();
                        etykiety[1, tmp.ZwrocWartosc()] = kolejnosc++;
                        //Console.Write(tmp.ZwrocWartosc()+" ");
                        if (stack.Any())
                        {
                            tmp = stack.Peek();
                            tempElem = tmp.ZwrocWartosc();
                        }
                    }
                }
                i = 0;
                do
                {
                    if (odwiedzone[i] == false)
                    {
                        break;
                    }
                    i++;
                    if (i == odwiedzone.Length)
                        koniec = true;
                } while (i<odwiedzone.Length);
            } while (!koniec);
            

            return etykiety;
        }

        public ListaNast(int n)
        {
            tablica = new Lista[n];
            for (int i = 0; i < n; i++)
            {
                tablica[i]=new Lista(i);
            }
        }
        public class Element       // klasa opisująca poszczególny element listy - wartość danego elementu oraz zmienną przechowującą następny element listy
        {
            private int wierzcholek;
            private Element _nastepny;

            public Element(int i)
            {
                wierzcholek = i;
                _nastepny = null;
            }
            public int ZwrocWartosc() { return wierzcholek; }
            public Element ZwrocNastepny() { return _nastepny; }
            public void UstawNastepny(int i)
            {
                _nastepny = new Element(i);
            }
        }

        public class Lista     // klasa implementująca listę jednokierunkową
        {
            public Element _korzen;
            private Element _ostatni;

            public Lista(int i)
            {
                _korzen=new Element(i);
                _ostatni = _korzen;
            }

            public bool Wstaw(int i)
            {
                
                    Element tmp = _korzen;
                    if (tmp.ZwrocWartosc()==i)
                    {
                        return false;
                    }
                    while (tmp.ZwrocNastepny() != null)
                    {
                        tmp = tmp.ZwrocNastepny(); // przechodzimy na koniec listy, czyli do elementu, który nie posiada elementu następnego.
                        if (tmp.ZwrocWartosc() == i)
                        {
                            return false;
                        }
                    }
                    tmp.UstawNastepny(i);
                    return true;
            }

            public void Wstaw2(int i)
            {
                _ostatni.UstawNastepny(i);
                _ostatni = _ostatni.ZwrocNastepny();
            }

            public void WypiszListe()
            {
                Element tmp = _korzen;
                while (tmp.ZwrocNastepny() != null)
                {
                    Console.Write(tmp.ZwrocWartosc()+"->");
                    tmp = tmp.ZwrocNastepny(); // przechodzimy na koniec listy, czyli do elementu, który nie posiada elementu następnego.
                }
                Console.Write(tmp.ZwrocWartosc());
            }

            
        }
    }
}
