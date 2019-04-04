using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    class KlasaNauczycielPrzedmiot
    {
        public string NazwaKlasy;
        public string NazwaNauczyciela;
        public string NazwaPrzedmiotu;
        public int ID;
        public KlasaNauczycielPrzedmiot(string nNazwaKlasy, string nNazwaNauczyciela, string nNazwaPrzedmiotu, int nID)
        {
            NazwaKlasy = nNazwaKlasy;
            NazwaNauczyciela = nNazwaNauczyciela;
            NazwaPrzedmiotu = nNazwaPrzedmiotu;
            ID = nID;
        }
    }
}
