using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    interface IPobierzNazwy
    {
        /// <summary>
        /// Zwraca kolekcje nazw
        /// </summary>
       
        IEnumerable<string> PobierzNazwy();
    }
}
