using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    class Gra
    {
        //domyslne pola obiektow Gra
        public string nazwa;
        public double cena;
        protected string gatunek;
        protected int ograniczenieWiekowe;

        public Gra() { }

        public Gra(string nazwa, double cena, string gatunek, int ograniczenieWiekowe)
        {
            this.nazwa = nazwa;
            this.cena = cena;
            this.gatunek = gatunek;
            this.ograniczenieWiekowe = ograniczenieWiekowe;
        }

        public string ZwrocNazwe()
        {
            return this.nazwa;
        }

    }
}
