using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepZGrami
{
    interface ISortowania
    {
        /// <summary>
        /// Metody pozwalające na sortowanie lsity produktów wg danego kryterium
        /// </summary>
        void SortowanieAlfabetycznie();
        void SortujCenyRosnaco();
        void SortujCenyMalejaco();
    }
}
