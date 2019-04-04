using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    public class Klasa
    {
        public int IloscGodzin;
        public int IloscPrzedmiotow;
        public string Nazwa;

        public Klasa(string nNazwa, int nIloscGodzin, int nIloscPrzedmiotow)
        {
            IloscGodzin = nIloscGodzin;
            IloscPrzedmiotow = nIloscPrzedmiotow;
            Nazwa = nNazwa;
        }
    }
}
