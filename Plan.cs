using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapidset
{
    public class Plan
    {
        public string Klasa;
        public int ID;
        public string Przedmiot;
        public string Nauczyciel;
        public int Sala;
        public int Godzina;
        public int Dzien;


        public Plan(string nKlasa, int nID, string nPrzedmiot, string nNauczyciel, int nSala, int nGodzina, int nDzien)
        {
            Klasa = nKlasa;
            ID = nID;
            Przedmiot = nPrzedmiot;
            Nauczyciel = nNauczyciel;
            Sala = nSala;
            Godzina = nGodzina;
            Dzien = nDzien;
        }
    }
}
