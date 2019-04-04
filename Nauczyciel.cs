using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    public class Nauczyciel
    {
        public string Nazwa;
        public string Przedmiot;
        public int ID;
        public Nauczyciel(int nID, string nNazwa, string nPrzedmiot)
        {
            ID = nID;
            Nazwa = nNazwa;
            Przedmiot = nPrzedmiot;
        }
    }
}
