using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    class Klient: IPobierzNazwy, ISortowania, IInformacje
    {
        private List<Gra> koszyk; //lista produktow w koszyku
        private List<Gra> listaProduktów;
        

        public Klient()
        {
            koszyk = new List<Gra>();
            listaProduktów = new List<Gra>();
        }

        //metoda tworzy tablice o wielkosci ilosci elementow na liscie, i umieszcza w niej pola nazwa
        //danych obiektow, potrzebna do dzialania wyswietlonej listy produktow
        public IEnumerable<string> PobierzNazwy()
        {
            string[] nazwaGry = new string[listaProduktów.Count];
            for (int i = 0; i < listaProduktów.Count; i++)
                nazwaGry[i] = listaProduktów[i].nazwa;
            return nazwaGry;
        }

        //metoda tworzy tablice o wielkosci ilosci elementow na liscie, i umieszcza w niej pola nazwa
        //danych obiektow, potrzebna do dzialania koszyka
        public IEnumerable<string> PobierzNazwyDoKoszyka()
        {
            string[] nazwaGry = new string[koszyk.Count];
            for (int i = 0; i < koszyk.Count; i++)
                nazwaGry[i] = koszyk[i].nazwa;
            return nazwaGry;
        }

        //metoda tworzaca tablice o wielkosci podanej ilosci elementow na danej liscie, 
        //umieszcza w niej zawartosci pol cena danych obiektow
        public IEnumerable<double> PobierzCenyDoKoszyka()
        {
            double[] cenaGry = new double[koszyk.Count];
            for (int i = 0; i < koszyk.Count; i++)
                cenaGry[i] = koszyk[i].cena;
            return cenaGry;
        }

        //Metoda kopiująca wybrany obiekt aby mozliwe bylo przeniesienie go do koszyka
        public Gra GraDoPrzeniesienia(int indeks)
        {
            Gra GraDoZamiany = listaProduktów[indeks];
            return GraDoZamiany;
        }

        public Gra GraDoUsuniecia(int indeks)
        {
            Gra GraDoUsuniecia = koszyk[indeks];
            return GraDoUsuniecia;
        }

        //metoda kopiująca liste produktów
        public void KopiowanieListyGier(List<Gra> lista)
        {
            listaProduktów = lista;
        }

        //liczy ilosc obiektow na liscie
        public int PoliczProdukty()
        {
            return listaProduktów.Count;
        }

        //liczy ilosc obiektow w koszyku
        public int PoliczIloscWKoszyku()
        {
            return koszyk.Count();
        }

        //Metoda dodajaca do listy koszyk wybrany obiekt
        public void DodajDoKoszyka(Gra GraDoKoszyka)
        {
            koszyk.Add(GraDoKoszyka);
        }

        //Metoda usuwa wybrany obiekt z listy koszyk
        public void UsunZKoszyka(Gra GraZKoszyka)
        {
            koszyk.Remove(GraZKoszyka);
        }

        //Metoda zwracajaca string z informacjami na temat wybranego obiektu
        public string Informacje(int indeks)
        {
            return listaProduktów[indeks].ToString();
        }

         public void WyczyscListeKoszyk()
        {
            koszyk.Clear();
        }


        //Tworzy string z Informacjami na temat wszystkich obiektow na danej liscie
        public string PodajInformacje()
        {
            string zawartosc = "Dziękujemy za dokonanie zakupu w naszym sklepie!";
            zawartosc += Environment.NewLine + "Oto dokonane przez Ciebie zamówienie:";

            foreach(var element in koszyk)
            {
                zawartosc += Environment.NewLine + element.ToString();
            }

            return zawartosc;
        }

        public void SortowanieAlfabetycznie()
        {
            listaProduktów.Sort((x, y) => x.nazwa.CompareTo(y.nazwa));
        }
       
        public void SortujCenyRosnaco()
        {
            listaProduktów.Sort((x, y) => x.cena.CompareTo(y.cena));
        }

        public void SortujCenyMalejaco()
        {
            listaProduktów.Sort((x, y) => y.cena.CompareTo(x.cena));
        }
    }
}
