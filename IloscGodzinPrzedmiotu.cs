using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    public class IloscGodzinPrzedmiotu
    {
        public string NazwaKlasy;
        public string NazwaPrzedmiotu;
        public int IloscWTygodniu;
        public int MaxIlosc;
        public IloscGodzinPrzedmiotu(string nNazwaKlasy, string nNazwaPrzedmiotu, int nIloscWTygodniu, int nMaxIlosc)
        {
            NazwaKlasy = nNazwaKlasy;
            NazwaPrzedmiotu = nNazwaPrzedmiotu;
            IloscWTygodniu = nIloscWTygodniu;
            MaxIlosc = nMaxIlosc;
        }
    }
}
