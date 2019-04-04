using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace rapidset
{
    public partial class wczytaniePlanuForm : Form
    {
        public wczytaniePlanuForm()
        {
            InitializeComponent();
        }

        List<Klasa> KlasaList = new List<Klasa>();
        List<Przedmiot> PrzedmiotList = new List<Przedmiot>();
        List<Plan> PlanList = new List<Plan>();
        List<Nauczyciel> NauczycielList = new List<Nauczyciel>();
        List<Przedmiot> Buffor = new List<Przedmiot>();
        List<int> IndexNauczyciela = new List<int>();
        List<int> Zerowanie = new List<int>();
        List<Godziny> GodzinyList = new List<Godziny>();
        List<Sala> SalaList = new List<Sala>();
        List<string> CzasyLekcji = new List<string>();
        bool[, ,] Dostepnosc = new bool[14, 5, 50];
        List<DostepnoscSali> DostepnoscSalList = new List<DostepnoscSali>();
        string path;


        RichTextBox[] text = new RichTextBox[70];
        string nazwaKlasy;

        public void Generuj()
        {
            text[0] = richTextBox1;
            text[1] = richTextBox2;
            text[2] = richTextBox3;
            text[3] = richTextBox4;
            text[4] = richTextBox5;
            text[5] = richTextBox6;
            text[6] = richTextBox7;
            text[7] = richTextBox8;
            text[8] = richTextBox9;
            text[9] = richTextBox10;
            text[10] = richTextBox11;
            text[11] = richTextBox12;
            text[12] = richTextBox13;
            text[13] = richTextBox14;
            text[14] = richTextBox15;
            text[15] = richTextBox16;
            text[16] = richTextBox17;
            text[17] = richTextBox18;
            text[18] = richTextBox19;
            text[19] = richTextBox20;
            text[20] = richTextBox21;
            text[21] = richTextBox22;
            text[22] = richTextBox23;
            text[23] = richTextBox24;
            text[24] = richTextBox25;
            text[25] = richTextBox26;
            text[26] = richTextBox27;
            text[27] = richTextBox28;
            text[28] = richTextBox29;
            text[29] = richTextBox30;
            text[30] = richTextBox31;
            text[31] = richTextBox32;
            text[32] = richTextBox33;
            text[33] = richTextBox34;
            text[34] = richTextBox35;
            text[35] = richTextBox36;
            text[36] = richTextBox37;
            text[37] = richTextBox38;
            text[38] = richTextBox39;
            text[39] = richTextBox40;
            text[40] = richTextBox41;
            text[41] = richTextBox42;
            text[42] = richTextBox43;
            text[43] = richTextBox44;
            text[44] = richTextBox45;
            text[45] = richTextBox46;
            text[46] = richTextBox47;
            text[47] = richTextBox48;
            text[48] = richTextBox49;
            text[49] = richTextBox50;
            text[50] = richTextBox51;
            text[51] = richTextBox52;
            text[52] = richTextBox53;
            text[53] = richTextBox54;
            text[54] = richTextBox55;
            text[55] = richTextBox56;
            text[56] = richTextBox57;
            text[57] = richTextBox58;
            text[58] = richTextBox59;
            text[59] = richTextBox60;
            text[60] = richTextBox61;
            text[61] = richTextBox62;
            text[62] = richTextBox63;
            text[63] = richTextBox64;
            text[64] = richTextBox65;
            text[65] = richTextBox66;
            text[66] = richTextBox67;
            text[67] = richTextBox68;
            text[68] = richTextBox69;
            text[69] = richTextBox70; // przypisanie
            FileStream fs = new FileStream(Application.StartupPath + "\\data\\adres.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            path = sr.ReadToEnd();
            sr.Close();

            Form1 okno = new Form1();
            okno.ShowInTaskbar = false;
            poczatek:
            //czyszczenie list(żeby listy były czyste przy ponownym losowaniu)
            PlanList.Clear();
            KlasaList.Clear();
            PrzedmiotList.Clear();
            NauczycielList.Clear();
            SalaList.Clear();
            CzasyLekcji.Clear();
            DostepnoscSalList.Clear();
            XmlDocument XmlDoc = new XmlDocument();
            XmlNode Node;
            XmlDoc.Load(path);
            //wczytanie przedmiotów do listy

            int count = XmlDoc.GetElementsByTagName("przedmiot").Count;
            for (int i = 0; i < count; i++)
            {
                Node = XmlDoc.GetElementsByTagName("przedmiot").Item(i);
                PrzedmiotList.Add(new Przedmiot(Node.Attributes.GetNamedItem("nazwa_klasy").InnerText, Node.Attributes.GetNamedItem("nazwa_przedmiotu").InnerText, Node.Attributes.GetNamedItem("nazwa_nauczyciela").InnerText, Convert.ToInt32(Node.Attributes.GetNamedItem("id_nauczyciela").InnerText), Convert.ToInt32(Node.Attributes.GetNamedItem("ilosc_godzin").InnerText), Convert.ToInt32(Node.Attributes.GetNamedItem("max_dziennie").InnerText), Convert.ToBoolean(Node.Attributes.GetNamedItem("trudnosc").InnerText)));
            }

            //wczytanie klas do listy
            count = XmlDoc.GetElementsByTagName("klasa").Count;
            for (int i = 0; i < count; i++)
            {
                Node = XmlDoc.GetElementsByTagName("klasa").Item(i);
                KlasaList.Add(new Klasa(Node.Attributes.GetNamedItem("nazwa_klasy").InnerText, Convert.ToInt32(Node.Attributes.GetNamedItem("ilosc_godzin").InnerText), Convert.ToInt32(Node.Attributes.GetNamedItem("ilosc_przedmiotow").InnerText)));
            }

            //wczytanie nauczycieli do listy
            count = XmlDoc.GetElementsByTagName("nauczyciel").Count;
            for (int i = 0; i < count; i++)
            {
                Node = XmlDoc.GetElementsByTagName("nauczyciel").Item(i);
                NauczycielList.Add(new Nauczyciel(Convert.ToInt32(Node.Attributes.GetNamedItem("id_nauczyciela").InnerText), Node.Attributes.GetNamedItem("nazwa_nauczyciela").InnerText, Node.Attributes.GetNamedItem("nazwa_przedmiotu").InnerText));
            }

            //wczytanie sal do listy
            count = XmlDoc.GetElementsByTagName("sala").Count;
            for (int i = 0; i < count; i++)
            {
                Node = XmlDoc.GetElementsByTagName("sala").Item(i);
                SalaList.Add(new Sala(Node.Attributes.GetNamedItem("nr_sali").InnerText, Node.Attributes.GetNamedItem("nazwa_przedmiotu").InnerText));
            }
            //wczytanie czasu lekcji do listy
            count = XmlDoc.GetElementsByTagName("lekcja").Count;
            for (int i = 0; i < count; i++)
            {
                Node = XmlDoc.GetElementsByTagName("lekcja").Item(i);
                CzasyLekcji.Add(Node.Attributes.GetNamedItem("czas").InnerText);
            }
            for (int k = 0; k < 5; k++)
            {
                for (int j = 0; j < 14; j++)
                {
                    for (int i = 0; i < SalaList.Count; i++)
                    {
                        Sala SalaInfo = SalaList[i];
                        DostepnoscSalList.Add(new DostepnoscSali(k, j, SalaInfo.NrSali, SalaInfo.Przedmiot, true));
                        // MessageBox.Show(k.ToString() + " " + j.ToString() + " " + SalaInfo.Przedmiot + " " + SalaInfo.NrSali);
                    }
                }
            }

            Label[] labelBaza = new Label[50];
            int xL = 0, yL = 0;
            for (int i = 0; i < KlasaList.Count; i++)
            {
                Klasa KlasaInfo = KlasaList[i];
                labelBaza[i] = new Label();
                labelBaza[i].Location = new Point(xL, yL);
                labelBaza[i].Size = new Size(150, 20);
                labelBaza[i].Text = KlasaInfo.Nazwa;
                labelBaza[i].Font = new Font("Segoe UI", 9);
                labelBaza[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(78)))), ((int)(((byte)(171)))));
                labelBaza[i].ForeColor = Color.White;
                labelBaza[i].MouseHover += new EventHandler(this.labelBaza_MouseHover);
                labelBaza[i].MouseLeave += new EventHandler(this.labelBaza_MouseLeave);
                labelBaza[i].Click += new EventHandler(this.button1_Click);
                labelBaza[i].Parent = panel1;
                nazwaKlasy = KlasaInfo.Nazwa;
                yL = yL + 23;
            }





            Random RandomObj = new Random();

            int wynik = 0;
            int przedmiotRand;
            int licznik = 0;
            int sumaTrudnychPrzedmiotow = 0;
            int[] trudnosc = new int[KlasaList.Count];

            for (int i = 0; i < KlasaList.Count; i++)
                trudnosc[i] = 0;
            //sprawdzanie ile jest godzin "trudnych" w tygodniu
            for (int klasa = 0; klasa < KlasaList.Count; klasa++)
            {
                Klasa KlasaInfo = KlasaList[klasa];
                for (int i = 0; i < KlasaInfo.IloscPrzedmiotow; i++)
                {
                    Przedmiot PrzedmiotD = PrzedmiotList[i + PrzedmiotList.FindIndex(item => item.NazwaKlasy == KlasaInfo.Nazwa)];
                    if (PrzedmiotD.Trudnosc == true)
                    {
                        for (int j = 0; j < PrzedmiotD.IloscGodzin; j++)
                            trudnosc[klasa]++;
                    }
                }
                trudnosc[klasa] = trudnosc[klasa] / 5;
            }
            for (int i = 0; i < PrzedmiotList.Count; i++)
            {
                Przedmiot PrzedmiotInfo = PrzedmiotList[i];
                Zerowanie.Add(PrzedmiotInfo.IloscWDniu);
            }

            // sortowanie listy z przedmiotami wg ilości przedmiotu w tygodniu
            /*
            for (int klasa = 0; klasa < KlasaList.Count; klasa++)
            {
                Klasa KlasaDane = KlasaList[klasa];
                for (int i = 0; i < KlasaDane.IloscPrzedmiotow-1; i++)
                {
                    for (int j = 0; j < KlasaDane.IloscPrzedmiotow-1; j++)
                    {
                        Przedmiot PrzedmiotDaneJeden = PrzedmiotList[j + PrzedmiotList.FindIndex(item => item.NazwaKlasy == KlasaDane.Nazwa)];
                        Przedmiot PrzedmiotDaneDwa = PrzedmiotList[j + 1 + PrzedmiotList.FindIndex(item => item.NazwaKlasy == KlasaDane.Nazwa)];

                        if (PrzedmiotDaneJeden.IloscGodzin < PrzedmiotDaneDwa.IloscGodzin)
                        {
                            Buffor.Add(new Przedmiot(PrzedmiotDaneJeden.NazwaKlasy, PrzedmiotDaneJeden.NazwaPrzedmiotu, PrzedmiotDaneJeden.NazwaNauczyciela, PrzedmiotDaneJeden.ID, PrzedmiotDaneJeden.IloscGodzin, PrzedmiotDaneJeden.IloscWDniu, PrzedmiotDaneJeden.Trudnosc));
                            PrzedmiotDaneJeden.ID = PrzedmiotDaneDwa.ID;
                            PrzedmiotDaneJeden.IloscGodzin = PrzedmiotDaneDwa.IloscGodzin;
                            PrzedmiotDaneJeden.IloscWDniu = PrzedmiotDaneDwa.IloscWDniu;
                            PrzedmiotDaneJeden.NazwaKlasy = PrzedmiotDaneDwa.NazwaKlasy;
                            PrzedmiotDaneJeden.NazwaNauczyciela = PrzedmiotDaneDwa.NazwaNauczyciela;
                            PrzedmiotDaneJeden.NazwaPrzedmiotu = PrzedmiotDaneDwa.NazwaPrzedmiotu;
                            PrzedmiotDaneJeden.Trudnosc = PrzedmiotDaneDwa.Trudnosc;
                            Przedmiot temp = Buffor[0];
                            PrzedmiotDaneDwa.ID = temp.ID;
                            PrzedmiotDaneDwa.IloscGodzin = temp.IloscGodzin;
                            PrzedmiotDaneDwa.IloscWDniu = temp.IloscWDniu;
                            PrzedmiotDaneDwa.NazwaKlasy = temp.NazwaKlasy;
                            PrzedmiotDaneDwa.NazwaNauczyciela = temp.NazwaNauczyciela;
                            PrzedmiotDaneDwa.NazwaPrzedmiotu = temp.NazwaPrzedmiotu;
                            PrzedmiotDaneDwa.Trudnosc = temp.Trudnosc;
                            Buffor.Clear();
                        }
                    }
                }
            }
            */

            for (int i = 0; i < 14; i++)
                for (int j = 0; j < NauczycielList.Count; j++)
                    for (int k = 0; k < 5; k++)
                        Dostepnosc[i, k, j] = true;

            int y, x, z;
            for (int klasa = 0; klasa < KlasaList.Count; klasa++)
            {
                Klasa KlasaInfo = KlasaList[klasa];
                if (KlasaInfo.IloscGodzin >= 25)
                {
                    x = KlasaInfo.IloscGodzin;
                    for (int dzien = 5; dzien > 1; dzien--)
                    {
                        y = x / dzien;
                        z = (y - 2) + RandomObj.Next(0, 5);
                        GodzinyList.Add(new Godziny(KlasaInfo.Nazwa, z));
                        x = x - z;
                    }
                }
                else
                {
                    x = KlasaInfo.IloscGodzin;
                    for (int dzien = 5; dzien > 1; dzien--)
                    {
                        y = x / dzien;
                        z = (y - 1) + RandomObj.Next(0, 2);
                        GodzinyList.Add(new Godziny(KlasaInfo.Nazwa, z));
                        x = x - z;
                    }
                }
                GodzinyList.Add(new Godziny(KlasaInfo.Nazwa, x));
            }
            //pętla określająca dzień
            for (int day = 0; day < 5; day++)
            {
                //główna pętla określająca, która klasa jest obecnie rozpatrywana
                for (int klasa = 0; klasa < KlasaList.Count; klasa++)
                {
                    IndexNauczyciela.Clear();
                    Klasa KlasaInfo = KlasaList[klasa];
                    sumaTrudnychPrzedmiotow = 0;

                    //przypisywanie ile danego przedmiotu jest w danym dniu
                    for (int i = 0; i < PrzedmiotList.Count; i++)
                    {
                        Przedmiot PrzedmiotInfo = PrzedmiotList[i];
                        PrzedmiotInfo.IloscWDniu = Zerowanie[i];
                    }

                    //tworzenie listy z indexami nauczycieli uczących daną klase
                    for (int i = 0; i < KlasaInfo.IloscPrzedmiotow; i++)
                    {
                        Przedmiot PrzedmiotD = PrzedmiotList[i + PrzedmiotList.FindIndex(item => item.NazwaKlasy == KlasaInfo.Nazwa)];
                        IndexNauczyciela.Add(PrzedmiotD.ID);
                    }

                    //główna pętla dla jednego dnia dla jednej klasy
                    Godziny GodzinyInfo = GodzinyList[GodzinyList.FindIndex(item => item.Nazwa == KlasaInfo.Nazwa) + day];
                    for (int i = 0; i < GodzinyInfo.Ilosc; i++) //ilosc godzin jednego dnia
                    {
                        licznik = 0;
                        bool ustalenie = false;
                        do
                        {
                            wynik++;
                            if (wynik > 500)
                            {
                                goto poczatek;
                            }
                            if (licznik > IndexNauczyciela.Count - 1)
                                break;
                            przedmiotRand = IndexNauczyciela[RandomObj.Next(KlasaInfo.IloscPrzedmiotow)];
                            Przedmiot PrzedmiotInfo = PrzedmiotList[PrzedmiotList.FindIndex(item => item.NazwaKlasy == KlasaInfo.Nazwa && item.ID == przedmiotRand)];
                            DostepnoscSali DostSali = DostepnoscSalList[DostepnoscSalList.FindIndex(item => item.Przedmiot == PrzedmiotInfo.NazwaPrzedmiotu && item.Dzien == day && item.Godzina == i && item.Dostepnosc == true)];
                            //MessageBox.Show(DostSali.Dostepnosc.ToString() + DostSali.Przedmiot + " " + DostSali.NrSali + " " + DostSali.Godzina.ToString() + " " + DostSali.Dzien.ToString());
                            //"łatwy przedmiot"
                            //MessageBox.Show(Dostepnosc[i, day, przedmiotRand].ToString() + " " + PrzedmiotInfo.IloscGodzin.ToString() + " " + PrzedmiotInfo.IloscWDniu.ToString() + " " + PrzedmiotInfo.Trudnosc.ToString() + " " + DostSali.Dostepnosc.ToString());
                            if (Dostepnosc[i, day, przedmiotRand] == true && PrzedmiotInfo.IloscGodzin > 0 && PrzedmiotInfo.IloscWDniu > 0 && PrzedmiotInfo.Trudnosc == false && DostSali.Dostepnosc == true)
                            {
                                PlanList.Add(new Plan(KlasaInfo.Nazwa, PrzedmiotInfo.ID, PrzedmiotInfo.NazwaPrzedmiotu, PrzedmiotInfo.NazwaNauczyciela, Convert.ToInt32(DostSali.NrSali), i, day)); // wylosowanie przedmiotu
                                PrzedmiotInfo.IloscGodzin--;
                                PrzedmiotInfo.IloscWDniu--;
                                Dostepnosc[i, day, przedmiotRand] = false;
                                ustalenie = true;
                                DostSali.Dostepnosc = false;
                            }
                            //"trudny przedmiot"
                            if (Dostepnosc[i, day, przedmiotRand] == true && PrzedmiotInfo.IloscGodzin > 0 && PrzedmiotInfo.IloscWDniu > 0 && PrzedmiotInfo.Trudnosc == true && sumaTrudnychPrzedmiotow < trudnosc[klasa] + 5 && DostSali.Dostepnosc == true)
                            {
                                PlanList.Add(new Plan(KlasaInfo.Nazwa, PrzedmiotInfo.ID, PrzedmiotInfo.NazwaPrzedmiotu, PrzedmiotInfo.NazwaNauczyciela, Convert.ToInt32(DostSali.NrSali), i, day)); // wylosowanie przedmiotu
                                PrzedmiotInfo.IloscGodzin--;
                                PrzedmiotInfo.IloscWDniu--;
                                Dostepnosc[i, day, przedmiotRand] = false;
                                ustalenie = true;
                                sumaTrudnychPrzedmiotow++;
                                DostSali.Dostepnosc = false;
                            }
                        }
                        while (ustalenie == false);
                    }


                }
            }
            save_Click(null, null);
        }
        public void WczytajPlan()
        {
            text[0] = richTextBox1;
            text[1] = richTextBox2;
            text[2] = richTextBox3;
            text[3] = richTextBox4;
            text[4] = richTextBox5;
            text[5] = richTextBox6;
            text[6] = richTextBox7;
            text[7] = richTextBox8;
            text[8] = richTextBox9;
            text[9] = richTextBox10;
            text[10] = richTextBox11;
            text[11] = richTextBox12;
            text[12] = richTextBox13;
            text[13] = richTextBox14;
            text[14] = richTextBox15;
            text[15] = richTextBox16;
            text[16] = richTextBox17;
            text[17] = richTextBox18;
            text[18] = richTextBox19;
            text[19] = richTextBox20;
            text[20] = richTextBox21;
            text[21] = richTextBox22;
            text[22] = richTextBox23;
            text[23] = richTextBox24;
            text[24] = richTextBox25;
            text[25] = richTextBox26;
            text[26] = richTextBox27;
            text[27] = richTextBox28;
            text[28] = richTextBox29;
            text[29] = richTextBox30;
            text[30] = richTextBox31;
            text[31] = richTextBox32;
            text[32] = richTextBox33;
            text[33] = richTextBox34;
            text[34] = richTextBox35;
            text[35] = richTextBox36;
            text[36] = richTextBox37;
            text[37] = richTextBox38;
            text[38] = richTextBox39;
            text[39] = richTextBox40;
            text[40] = richTextBox41;
            text[41] = richTextBox42;
            text[42] = richTextBox43;
            text[43] = richTextBox44;
            text[44] = richTextBox45;
            text[45] = richTextBox46;
            text[46] = richTextBox47;
            text[47] = richTextBox48;
            text[48] = richTextBox49;
            text[49] = richTextBox50;
            text[50] = richTextBox51;
            text[51] = richTextBox52;
            text[52] = richTextBox53;
            text[53] = richTextBox54;
            text[54] = richTextBox55;
            text[55] = richTextBox56;
            text[56] = richTextBox57;
            text[57] = richTextBox58;
            text[58] = richTextBox59;
            text[59] = richTextBox60;
            text[60] = richTextBox61;
            text[61] = richTextBox62;
            text[62] = richTextBox63;
            text[63] = richTextBox64;
            text[64] = richTextBox65;
            text[65] = richTextBox66;
            text[66] = richTextBox67;
            text[67] = richTextBox68;
            text[68] = richTextBox69;
            text[69] = richTextBox70; //przypisanie
            path = Application.StartupPath + "/data/project/plan.xml";
            XmlDocument XmlDoc = new XmlDocument();
            XmlNode Node;
            XmlDoc.Load(path);
            //wczytanie przedmiotów do listy
            int count = XmlDoc.GetElementsByTagName("plan").Count;
            for (int i = 0; i < count; i++)
            {
                Node = XmlDoc.GetElementsByTagName("plan").Item(i);
                PlanList.Add(new Plan(Convert.ToString(Node.Attributes.GetNamedItem("klasa").InnerText), Convert.ToInt32(Node.Attributes.GetNamedItem("id").InnerText), Node.Attributes.GetNamedItem("przedmiot").InnerText, Node.Attributes.GetNamedItem("nauczyciel").InnerText, Convert.ToInt32(Node.Attributes.GetNamedItem("sala").InnerText), Convert.ToInt32(Node.Attributes.GetNamedItem("godzina").InnerText), Convert.ToInt32(Node.Attributes.GetNamedItem("dzien").InnerText)));
                Plan PlanInfo = PlanList[i];
                // MessageBox.Show(Convert.ToString(Node.Attributes.GetNamedItem("klasa").InnerText) + " " + Convert.ToString(Node.Attributes.GetNamedItem("id").InnerText) + " " + Node.Attributes.GetNamedItem("przedmiot").InnerText + " " + Node.Attributes.GetNamedItem("nauczyciel").InnerText + " " + Convert.ToString(Node.Attributes.GetNamedItem("sala").InnerText) + " " + Convert.ToString(Node.Attributes.GetNamedItem("godzina").InnerText) + " " + Convert.ToString(Node.Attributes.GetNamedItem("dzien").InnerText));
                if (i > 0)
                {
                    if (KlasaList.Exists(item => item.Nazwa == Node.Attributes.GetNamedItem("klasa").InnerText))
                    {
                    }
                    else
                    {
                        KlasaList.Add(new Klasa(Convert.ToString(Node.Attributes.GetNamedItem("klasa").InnerText), 0, 0));
                    }
                }
                else
                    KlasaList.Add(new Klasa(Convert.ToString(Node.Attributes.GetNamedItem("klasa").InnerText), 0, 0));
            }
            int x = 0;
            for(int i = 0; i < 14; i++)
            {
                for(int j=0;j<5;j++)
                {
                    for(int k=0;k<50;k++)
                    {
                        Node = XmlDoc.GetElementsByTagName("dostepnosc").Item(x);
                        Dostepnosc[i, j, k] = Convert.ToBoolean(Node.Attributes.GetNamedItem("dost").InnerText);
                        x++;
                    }
                }
            }
            for (int i = 0; i < 14; i++)
            {
                Node = XmlDoc.GetElementsByTagName("lekcja").Item(i);
                CzasyLekcji.Add(Node.Attributes.GetNamedItem("czas").InnerText);
            }

            Label[] labelBaza = new Label[50];
            int xL = 0, yL = 0;
            for (int i = 0; i < KlasaList.Count; i++)
            {
                Klasa KlasaInfo = KlasaList[i];
                labelBaza[i] = new Label();
                labelBaza[i].Location = new Point(xL, yL);
                labelBaza[i].Size = new Size(150, 20);
                labelBaza[i].Text = KlasaInfo.Nazwa;
                labelBaza[i].Font = new Font("Segoe UI", 9);
                labelBaza[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(78)))), ((int)(((byte)(171)))));
                labelBaza[i].ForeColor = Color.White;
                labelBaza[i].MouseHover += new EventHandler(this.labelBaza_MouseHover);
                labelBaza[i].MouseLeave += new EventHandler(this.labelBaza_MouseLeave);
                labelBaza[i].Click += new EventHandler(this.button1_Click);
                labelBaza[i].Parent = panel1;
                nazwaKlasy = KlasaInfo.Nazwa;
                yL = yL + 23;
            }
            for (int i = 0; i < CzasyLekcji.Count; i++)
            {
                godziny[i].Text = CzasyLekcji[i];
            }
            
        }
        string wybor;
        private void wczytaniePlanuForm_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\data\\lorg.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            wybor = sr.ReadToEnd();
            sr.Close();
            if (wybor == "1")
            {
                Generuj();
            }
            if (wybor == "2")
            {
                WczytajPlan();
            }
        }
        RichTextBox[] godziny = new RichTextBox[14];
        private void button1_Click(object sender, EventArgs e)
        {
            godziny[0] = godzina0;
            godziny[1] = godzina1;
            godziny[2] = godzina2;
            godziny[3] = godzina3;
            godziny[4] = godzina4;
            godziny[5] = godzina5;
            godziny[6] = godzina6;
            godziny[7] = godzina7;
            godziny[8] = godzina8;
            godziny[9] = godzina9;
            godziny[10] = godzina10;
            godziny[11] = godzina11;
            godziny[12] = godzina12;
            godziny[13] = godzina13;

            nazwaKlasy = (sender as Label).Text;
            label1.Text = "Klasa " + nazwaKlasy;
            for (int i = 0; i < 70; i++)
            {
                text[i].Text = "";
                text[i].BackColor = Color.White;
            }
            for (int i = 0; i < CzasyLekcji.Count; i++)
            {
                godziny[i].Text = CzasyLekcji[i];
            }
            for (int i = 0; i < PlanList.Count; i++)
            {
                Plan PlanInfo = PlanList[i];
                if (PlanInfo.Klasa == nazwaKlasy)
                {
                    text[PlanInfo.Godzina + (PlanInfo.Dzien * 14)].AppendText(PlanInfo.Przedmiot + " " + PlanInfo.Sala);
                    
                    int start =  text[PlanInfo.Godzina + (PlanInfo.Dzien * 14)].TextLength;
                    text[PlanInfo.Godzina + (PlanInfo.Dzien * 14)].AppendText("\n" + PlanInfo.Nauczyciel);
                    int stop = text[PlanInfo.Godzina + (PlanInfo.Dzien * 14)].TextLength - start;
                    text[PlanInfo.Godzina + (PlanInfo.Dzien * 14)].Select(start, stop);
                    text[PlanInfo.Godzina + (PlanInfo.Dzien * 14)].SelectionFont = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        int source;
        bool allow = false;
        int day = 0;
        int day2 = 0;
        private void komorkaHighlight(object sender, EventArgs e)
        {
         /*   
            (sender as RichTextBox).AllowDrop = true;
            (sender as RichTextBox).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
            for (int i = 0; i < 70; i++)
            {
                if ((sender as RichTextBox) != text[i])
                {
                    text[i].BackColor = Color.White;
                }
            }
            
            for (int i = 0; i < 70; i++)
            {

                if ((sender as RichTextBox).Name == text[i].Name)
                {
                    source = i;
                    break;
                }
            }
            if (text[source].Text != "" && text[source - 1].Text != "" && text[source + 1].Text == "")
            {
                if (source >= 13 && source < 28)
                    day = 1;
                if (source >= 28 && source < 42)
                    day = 2;
                if (source >= 42 && source < 56)
                    day = 3;
                if (source >= 56 && source < 70)
                    day = 4;
                Plan PlanInfo = PlanList[PlanList.FindIndex(item => item.Godzina == source % 14 && item.Dzien == day && item.Klasa == nazwaKlasy)];
                for (int j = 0; j < 14; j++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (text[j + (i * 14)].Text != "" && text[(j + 1) + (i * 14)].Text == "" && Dostepnosc[j + 1, i, PlanInfo.ID] == true && text[j + (i * 14)] != text[source])
                        {
                            // MessageBox.Show(Dostepnosc[j, i, PlanInfo.ID].ToString() + j.ToString() + i.ToString() + PlanInfo.ID.ToString());
                            text[(j + 1) + (i * 14)].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(156)))), ((int)(((byte)(11)))));
                            text[(j + 1) + (i * 14)].AllowDrop = true;
                        }
                    }
                }
            }
          * */
        }
        private void komorka_MouseDown(object sender, EventArgs e)
        {
            if (allow == true)
            {
                DragDropEffects effects = (sender as RichTextBox).DoDragDrop((sender as RichTextBox).Text, DragDropEffects.Move);
                if (effects != DragDropEffects.None)
                {
                    (sender as RichTextBox).Text = "";
                }
            }

            if (allow == false)
            {
                (sender as RichTextBox).AllowDrop = true;
                (sender as RichTextBox).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
                for (int i = 0; i < 70; i++)
                {
                    if ((sender as RichTextBox) != text[i])
                    {
                        text[i].BackColor = Color.White;
                    }
                }

                for (int i = 0; i < 70; i++)
                {

                    if ((sender as RichTextBox).Name == text[i].Name)
                    {
                        source = i;
                        break;
                    }
                }
                if (text[source].Text != "" && text[source + 1].Text != "")
                {
                        day = 0;
                    if (source >= 13 && source < 28)
                        day = 1;
                    if (source >= 28 && source < 42)
                        day = 2;
                    if (source >= 42 && source < 56)
                        day = 3;
                    if (source >= 56 && source < 70)
                        day = 4;
                    Plan PlanInfo = PlanList[PlanList.FindIndex(item => item.Godzina == source % 14 && item.Dzien == day && item.Klasa == nazwaKlasy)];
                    for (int j = 0; j < 14; j++)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (text[j + (i * 14)].Text != "" && text[(j + 1) + (i * 14)].Text == "" && Dostepnosc[j + 1, i, PlanInfo.ID] == true && text[j + (i * 14)] != text[source])
                            {
                                Dostepnosc[j + 1, i, PlanInfo.ID] = false;
                                Dostepnosc[source % 14, day, PlanInfo.ID] = true;

                                // MessageBox.Show(Dostepnosc[j, i, PlanInfo.ID].ToString() + j.ToString() + i.ToString() + PlanInfo.ID.ToString());
                                text[(j + 1) + (i * 14)].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(156)))), ((int)(((byte)(11)))));
                                text[(j + 1) + (i * 14)].AllowDrop = true;
                                allow = true;
                            }
                        }
                    }

                }
                if (allow == false)
                {
                    if (text[source].Text != "" && text[source - 1].Text != "" && text[source + 1].Text == "")
                    {
                        if (source >= 13 && source < 28)
                            day = 1;
                        if (source >= 28 && source < 42)
                            day = 2;
                        if (source >= 42 && source < 56)
                            day = 3;
                        if (source >= 56 && source < 70)
                            day = 4;
                        Plan PlanInfo = PlanList[PlanList.FindIndex(item => item.Godzina == source % 14 && item.Dzien == day && item.Klasa == nazwaKlasy)];
                        for (int j = 0; j < 14; j++)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (text[j + (i * 14)].Text != "" && text[(j + 1) + (i * 14)].Text == "" && Dostepnosc[j + 1, i, PlanInfo.ID] == true && text[j + (i * 14)] != text[source])
                                {
                                    Dostepnosc[j + 1, i, PlanInfo.ID] = false;
                                    Dostepnosc[source % 14, day, PlanInfo.ID] = true;

                                    // MessageBox.Show(Dostepnosc[j, i, PlanInfo.ID].ToString() + j.ToString() + i.ToString() + PlanInfo.ID.ToString());
                                    text[(j + 1) + (i * 14)].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(156)))), ((int)(((byte)(11)))));
                                    text[(j + 1) + (i * 14)].AllowDrop = true;
                                    allow = true;
                                }
                            }
                        }
                    }
                }
            }

           
        }

        private void komorka_DragEnter(object sender, DragEventArgs e)
        {
                if (e.Data.GetDataPresent(typeof(System.String)))
                {
                    e.Effect = DragDropEffects.Move;
                }
        }
        private void komorka_DragDrop(object sender, DragEventArgs e)
        {
                int i;
                if (e.Data.GetDataPresent(typeof(System.String)))
                {
                    for (i = 0; i < 70; i++)
                    {
                        if ((sender as RichTextBox).Name == text[i].Name)
                            break;
                    }
                    if (i >= 13 && i < 28)
                        day2 = 1;
                    if (i >= 28 && i < 42)
                        day2 = 2;
                    if (i >= 42 && i < 56)
                        day2 = 3;
                    if (i >= 56 && i < 70)
                        day2 = 4;

                    Object item = (object)e.Data.GetData(typeof(System.String));
                    (sender as RichTextBox).Text = item.ToString();
                    allow = false;
                    Plan PlanInfo = PlanList[PlanList.FindIndex(ite => ite.Godzina == source % 14 && ite.Dzien == day && ite.Klasa == nazwaKlasy)];
                    PlanInfo.Godzina = i % 14;
                    PlanInfo.Dzien = day2;
                    /*
                    (sender as RichTextBox).AllowDrop = false;
                    text[source].AllowDrop = false;
                     * */
                }
        }

        private void komorka_DoubleClick(object sender, EventArgs e)
        {
            allow = true;
        }


        private void komorka_MouseUp(object sender, MouseEventArgs e)
        {
        }
        public void labelBaza_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(117)))), ((int)(((byte)(218)))));
        }
        public void labelBaza_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(78)))), ((int)(((byte)(171)))));
        }

        private void htmlExport_Click(object sender, EventArgs e)
        {
            exportHTML okno = new exportHTML();
            okno.ShowDialog();
        }

        private void save_Click(object sender, EventArgs e)
        {
            XmlTextWriter Writer = new XmlTextWriter(Application.StartupPath + "/data/project/plan.xml", UTF8Encoding.UTF8);
            Writer.Formatting = Formatting.Indented;
            Writer.Indentation = 4;
            Writer.WriteStartDocument();
            //klasy
            Writer.WriteStartElement("root");
            for (int i = 0; i < PlanList.Count; i++)
            {
                Writer.WriteStartElement("plan");
                Plan PlanInfo = PlanList[i];
                Writer.WriteAttributeString("dzien", Convert.ToString(PlanInfo.Dzien));
                Writer.WriteAttributeString("godzina", Convert.ToString(PlanInfo.Godzina));
                Writer.WriteAttributeString("id", Convert.ToString(PlanInfo.ID));
                Writer.WriteAttributeString("klasa", Convert.ToString(PlanInfo.Klasa));
                Writer.WriteAttributeString("nauczyciel", Convert.ToString(PlanInfo.Nauczyciel));
                Writer.WriteAttributeString("przedmiot", Convert.ToString(PlanInfo.Przedmiot));
                Writer.WriteAttributeString("sala", Convert.ToString(PlanInfo.Sala));
                Writer.WriteEndElement();
            }
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 50; k++)
                    {
                        Writer.WriteStartElement("dostepnosc");
                        Plan PlanInfo = PlanList[i];
                        Writer.WriteAttributeString("godzina", i.ToString());
                        Writer.WriteAttributeString("dzien", j.ToString());
                        Writer.WriteAttributeString("id", k.ToString());
                        Writer.WriteAttributeString("dost", Dostepnosc[i, j, k].ToString());
                        Writer.WriteEndElement();
                    }
                }
            }
            for (int i = 0; i < 14; i++)
            {
                Writer.WriteStartElement("lekcja");
                Plan PlanInfo = PlanList[i];
                Writer.WriteAttributeString("czas", CzasyLekcji[i]);
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Close();

            /*
            XmlTextWriter Writer = new XmlTextWriter(Application.StartupPath + "/data/project/plan.xml", UTF8Encoding.UTF8);
            Writer.Formatting = Formatting.Indented;
            Writer.Indentation = 4;
            Writer.WriteStartDocument();
            //klasy
            Writer.WriteStartElement("root");
            Writer.WriteStartElement("klasy");
            for (int i = 0; i < KlasaList.Count; i++)
            {
                Writer.WriteStartElement("klasa");
                Klasa KlasaInfo = KlasaList[i];
                for (int day = 0; day < 5; day++)
                {
                    Godziny GodzinyInfo = GodzinyList[GodzinyList.FindIndex(item => item.Nazwa == KlasaInfo.Nazwa) + day];
                    for (int j = 0; j < GodzinyInfo.Ilosc; j++)
                    {
                        try
                        {
                            Plan PlanInfo = PlanList[PlanList.FindIndex(item => item.Godzina == j && item.Dzien == day && item.Klasa == KlasaInfo.Nazwa)];
                            Writer.WriteStartElement("przedmiot");
                            Writer.WriteAttributeString("klasa", KlasaInfo.Nazwa);
                            Writer.WriteAttributeString("nazwa_przedmiotu", PlanInfo.Przedmiot);
                            Writer.WriteAttributeString("nazwa_nauczyciela", PlanInfo.Nauczyciel);
                            Writer.WriteAttributeString("sala", Convert.ToString(PlanInfo.Sala));
                            Writer.WriteAttributeString("dzien", Convert.ToString(PlanInfo.Dzien));
                            Writer.WriteAttributeString("godzina", Convert.ToString(PlanInfo.Godzina));
                            Writer.WriteEndElement();
                        }
                        catch
                        {
                            GodzinyInfo.Ilosc++;
                        }

                    }
                }
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Close();
          * */
            /*
            //przedmioty
            Writer.WriteStartElement("przedmioty");
            for (int i = 0; i < PrzedmiotListGlowny.Count; i++)
            {
                Writer.WriteStartElement("przedmiot");
                Przedmiot PrzedmiotInfo = PrzedmiotListGlowny[i];
                Writer.WriteAttributeString("nazwa_klasy", PrzedmiotInfo.NazwaKlasy);
                Writer.WriteAttributeString("nazwa_przedmiotu", PrzedmiotInfo.NazwaPrzedmiotu);
                Writer.WriteAttributeString("nazwa_nauczyciela", PrzedmiotInfo.NazwaNauczyciela);
                Writer.WriteAttributeString("id_nauczyciela", Convert.ToString(PrzedmiotInfo.ID));
                Writer.WriteAttributeString("ilosc_godzin", Convert.ToString(PrzedmiotInfo.IloscGodzin));
                Writer.WriteAttributeString("max_dziennie", Convert.ToString(PrzedmiotInfo.IloscWDniu));
                Writer.WriteAttributeString("trudnosc", Convert.ToString(PrzedmiotInfo.Trudnosc));
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            //nauczyciele
            Writer.WriteStartElement("nauczyciele");
            for (int i = 0; i < NauczycielList.Count; i++)
            {
                Writer.WriteStartElement("nauczyciel");
                Nauczyciel NauczycielInfo = NauczycielList[i];
                Writer.WriteAttributeString("id_nauczyciela", Convert.ToString(NauczycielInfo.ID));
                Writer.WriteAttributeString("nazwa_nauczyciela", NauczycielInfo.Nazwa);
                Writer.WriteAttributeString("nazwa_przedmiotu", NauczycielInfo.Przedmiot);
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            //sale
            Writer.WriteStartElement("sale");
            for (int i = 0; i < SalaList.Count; i++)
            {
                Writer.WriteStartElement("sala");
                Sala SalaInfo = SalaList[i];
                Writer.WriteAttributeString("nr_sali", Convert.ToString(SalaInfo.NrSali));
                Writer.WriteAttributeString("nazwa_przedmiotu", SalaInfo.Przedmiot);
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            //czasy lekcji
            Writer.WriteStartElement("lekcje");
            for (int i = 0; i < CzasyLekcji.Count; i++)
            {
                Writer.WriteStartElement("lekcja");
                Sala SalaInfo = SalaList[i];
                Writer.WriteAttributeString("czas", CzasyLekcji[i]);
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Close();
             * */
        }

        private void save_MouseHover(object sender, EventArgs e)
        {
            save.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\save2.png");
            label3.Text = "Zapisz plan";
            label3.Location = new Point(772, 710);
        }

        private void save_MouseLeave(object sender, EventArgs e)
        {
            save.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\save.png");
            label3.Text = "";
        }

        private void htmlExport_MouseHover(object sender, EventArgs e)
        {
            htmlExport.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\export2.png");
            label3.Text = "Exportuj plan";
            label3.Location = new Point(858, 710);
        }

        private void htmlExport_MouseLeave(object sender, EventArgs e)
        {
            htmlExport.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\export.png");
            label3.Text = "";
        }


    }
}
