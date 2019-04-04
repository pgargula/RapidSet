using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    public class Przedmiot
    {
        public string NazwaKlasy;
        public string NazwaPrzedmiotu;
        public string NazwaNauczyciela;
        public int ID;
        public int IloscGodzin;
        public int IloscWDniu;
        public bool Trudnosc;

        public Przedmiot(string nNazwaKlasy, string nNazwaPrzedmiotu, string nNazwaNauczyciela, int nID, int nIloscGodzin, int nIloscWDniu, bool nTrudnosc)
        {
            NazwaKlasy = nNazwaKlasy;
            NazwaPrzedmiotu = nNazwaPrzedmiotu;
            NazwaNauczyciela = nNazwaNauczyciela;
            ID = nID;
            IloscGodzin = nIloscGodzin;
            IloscWDniu = nIloscWDniu;
            Trudnosc = nTrudnosc;
        }
    }
}
