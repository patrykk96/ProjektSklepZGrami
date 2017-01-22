using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    interface IInformacje
    {
        /// <summary>
        /// Metoda zwracajace informacje na temat danego obiektu, wybranego z listy za pomoca danego
        /// indeksu z listboxa
        /// </summary>
       
        string Informacje(int indeks);
    }
}
