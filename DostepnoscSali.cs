using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    class DostepnoscSali
    {
        public int Dzien;
        public int Godzina;
        public string NrSali;
        public string Przedmiot;
        public bool Dostepnosc;
        public DostepnoscSali(int nDzien, int nGodzina, string nNrSali, string nPrzedmiot, bool nDostepnosc)
        {
            Dzien = nDzien;
            Godzina = nGodzina;
            NrSali = nNrSali;
            Przedmiot = nPrzedmiot;
            Dostepnosc = nDostepnosc;
        }
    }
}
