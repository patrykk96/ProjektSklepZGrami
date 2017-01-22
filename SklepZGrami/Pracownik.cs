using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    class Pracownik: IPobierzNazwy, ISortowania, IInformacje
    {
        private List<Gra> listaGier;    //lista z asortymentem sklepu

        public Pracownik()
        {
            listaGier = new List<Gra>();
        }

        //metoda dodaje gre wideo do asortymentu(listy obiektow typu Gra)
        public void DodajGreWideo(string nazwa, double cena, string gatunek, int ograniczenieWiekowe, string platforma)
        {
            listaGier.Add(new GraWideo(nazwa, cena, gatunek, ograniczenieWiekowe, platforma));
        }

        //metoda dodaje gre planszową do asortymentu(listy obiektow typu Gra)
        public void DodajGrePlanszowa(string nazwa, double cena, string gatunek, int ograniczenieWiekowe, int minimumOsob, int maximumOsob)
        {
            listaGier.Add(new GraPlanszowa(nazwa, cena, gatunek, ograniczenieWiekowe, minimumOsob, maximumOsob));
        }

        //metoda pozwala na usunięcie wybranego obiektu z listy gier
        public void UsunDodany(int indeks)
        {
            listaGier.Remove(listaGier[indeks]);
        }

        //metoda tworzy tablice o wielkosci ilosci elementow na liscie, i umieszcza w niej pola nazwa
        //danych obiektow
        public IEnumerable<string> PobierzNazwy()
        {
            string[] nazwaGry = new string[listaGier.Count];
            for (int i = 0; i < listaGier.Count; i++)
                nazwaGry[i] = listaGier[i].nazwa;
            return nazwaGry;
        }

        public List<Gra> ZwrocListe()
        {
            return listaGier;
        }

        //Metoda dodająca domyślny asortyment sklepu
        public void DomyslneProdukty()
        {
            listaGier.Add(new GraWideo("Wiedźmin 3: Dziki Gon", 99.99, "RPG", 18, "PC"));
            listaGier.Add(new GraWideo("Battlefield 1", 229.99, "FPS", 18, "PS4"));
            listaGier.Add(new GraWideo("Batman: Arkham Knight", 59.99, "Akcja", 18, "PC"));
            listaGier.Add(new GraPlanszowa("Wiedźmin: Gra Przygodowa", 199.99, "RPG", 16, 2, 4));
            listaGier.Add(new GraPlanszowa("Władca Pierścieni", 149.99, "RPG", 12, 2, 8));
            listaGier.Add(new GraPlanszowa("Reksio i przyjaciele", 49.99, "Edukacyjna", 3, 2, 6));
            listaGier.Add(new GraWideo("Rocket League", 79.99, "Sportowa", 3, "PC"));
            listaGier.Add(new GraWideo("Wolfenstein: The New Order", 129.99, "FPS", 18, "XBOXONE"));
            listaGier.Add(new GraWideo("7 Days To Die", 219.99, "Survival", 18, "PS4"));
        }

        //metoda sortuje liste alfabetycznie
        public void SortowanieAlfabetycznie()
        {
            listaGier.Sort((x, y) => x.nazwa.CompareTo(y.nazwa));
        }
        
        //metoda sortuje liste rosnaco wg cen
        public void SortujCenyRosnaco()
        {
            listaGier.Sort((x, y) => x.cena.CompareTo(y.cena));
        }
        
        //metoda sortuje liste malejaco wg cen
        public void SortujCenyMalejaco()
        {
            listaGier.Sort((x, y) => y.cena.CompareTo(x.cena));
        }

        //metoda zwraca informacje na temat wybranego obiektu wskazanego za pomoca indeksu
        public string Informacje(int indeks)
        {
            return listaGier[indeks].ToString();
        }
    }
}
