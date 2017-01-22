using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    class GraWideo: Gra
    {
        private string platforma;
        
        public GraWideo() { }

        public GraWideo(string nazwa, double cena, string gatunek, int ograniczenieWiekowe, string platforma) 
            :base(nazwa,cena,gatunek, ograniczenieWiekowe)
        {
            this.platforma = platforma;
        }

        //przesłoniona metoda ToString do otrzymania dokladnych informacji o danym obiekcie w stringu
        public override string ToString()
        {
            return "Nazwa: " + nazwa + ", cena: " + cena + ", gatunek: " + gatunek + ", ograniczenie wiekowe: " + ograniczenieWiekowe
                + "+, platforma: " + platforma;
        }
    }
}
