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
    public partial class exportHTML : Form
    {
        public exportHTML()
        {
            InitializeComponent();
        }
        List<Klasa> KlasaList = new List<Klasa>();
        List<Plan> PlanList = new List<Plan>();
        List<string> CzasyLekcji = new List<string>();
        bool[, ,] Dostepnosc = new bool[14, 5, 50];
        string path;


        public void WczytajPlan()
        {

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
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 50; k++)
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

        }
        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nextButton_MouseHover(object sender, EventArgs e)
        {
            nextButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\strzalka-prawo2.png");
            label3.Text = "Exportuj";
            label3.Location = new Point(782, 710);
        }

        private void nextButton_MouseLeave(object sender, EventArgs e)
        {
            nextButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\strzalka-prawo.png");
            label3.Text = "";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string newValue = "0";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + "/data/settings.xml");
            XmlNode node = xmlDoc.SelectSingleNode("tip");
            node.Attributes[0].Value = newValue;
            xmlDoc.Save(Application.StartupPath + "/data/settings.xml");
            imgTip.Visible = false;
            checkBox1.Visible = false;
            transparentRichTextBox1.Visible = false;
            show = "0";
        }
        string show;
        private void exportHTML_Load(object sender, EventArgs e)
        {
            WczytajPlan();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Application.StartupPath + "\\data\\settings.xml");
            XmlNodeList elemList = XmlDoc.GetElementsByTagName("tip");
            show = elemList[0].Attributes["show"].Value;
            if (show == "1")
            {
                transparentRichTextBox1.Text = "Wybierz szablon, który Ci najbardziej odpowiada, a następnie kliknij dalej.";
                transparentRichTextBox1.Visible = true;
                imgTip.Visible = true;
                checkBox1.Visible = true;
            }
            else
            {
                transparentRichTextBox1.Visible = false;
                checkBox1.Visible = false;
                imgTip.Visible = false;
            }
            Label[] labelBaza = new Label[50];
            string[] nazwy = new string[5];
            nazwy[0] = "Niebieski";
            nazwy[1] = "Czerwony";
            nazwy[2] = "Zielony";
            nazwy[3] = "Czarny";
            nazwy[4] = "Pomarańczowy";

            int xL = 0, yL = 0;
            for (int i = 0; i < 5; i++)
            {
                labelBaza[i] = new Label();
                labelBaza[i].Location = new Point(xL, yL);
                labelBaza[i].Size = new Size(150, 20);
                labelBaza[i].Text = nazwy[i];
                labelBaza[i].Font = new Font("Segoe UI", 9);
                labelBaza[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(78)))), ((int)(((byte)(171)))));
                labelBaza[i].ForeColor = Color.White;
                labelBaza[i].MouseHover += new EventHandler(this.labelBaza_MouseHover);
                labelBaza[i].MouseLeave += new EventHandler(this.labelBaza_MouseLeave);
                labelBaza[i].Click += new EventHandler(this.labelBaza_Click);
                labelBaza[i].Parent = panel1;
                yL = yL + 23;
            }
        }
        public void labelBaza_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(117)))), ((int)(((byte)(218)))));
        }
        public void labelBaza_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(78)))), ((int)(((byte)(171)))));
        }
        int wybrany;
        string[] html = new string[20];
        string[] htmlPlan = new string[70];
        
        public void labelBaza_Click(object sender, EventArgs e)
        {
            if ((sender as Label).Text == "Niebieski")
            {

                wybrany = 1;
            }
            if ((sender as Label).Text == "Czerwony")
            {
                wybrany = 2;
            }
            if ((sender as Label).Text == "Zielony")
            {
                wybrany = 3;
            }
            if ((sender as Label).Text == "Czarny")
            {
                wybrany = 4;
            }
            if ((sender as Label).Text == "Pomarańczowy")
            {
                wybrany = 5;
            }
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            string style = "";
            if (wybrany == 1)
            {
                style = "style1.css";
            }

            for (int klasa = 0; klasa < KlasaList.Count; klasa++) //każda klasa - nowy dokument
            {
                Klasa NazwaKlasy = KlasaList[klasa];
                File.Copy(Application.StartupPath + "\\data\\html\\index.html", Application.StartupPath + "\\data\\html\\klasa" + klasa + ".html",true);

                FileStream fs = new FileStream(Application.StartupPath + "\\data\\html\\klasa" + klasa + ".html", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                html[0] = sr.ReadToEnd();
                sr.Close();
                
                StringBuilder Bar = new StringBuilder(html[0]);
                for (int i = KlasaList.Count-1; i >= 0; i--)
                {
                    Klasa KlasaInfo = KlasaList[i];
                    Bar.Insert(140, KlasaInfo.Nazwa + "</br>\n");
                }
                Bar.Insert(292, NazwaKlasy.Nazwa);
                for (int i = 2; i >=0; i--)
                {
                    if (i % 2 == 0)
                    {
                        try
                        {
                            Plan Piatek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 4 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Piatek.Przedmiot + "</div></br>\n");
                            Bar.Insert(554, "<div class=" + "tresc" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc" + "></div></br>\n");
                        }
                        try
                        {
                            Plan Czwartek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 3 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Czwartek.Przedmiot + "</div>");
                            Bar.Insert(554, "<div class=" + "tresc" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc" + "></div>");
                        }
                        try
                        {
                            Plan Sroda = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 2 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Sroda.Przedmiot + "</div>");
                            Bar.Insert(554, "<div class=" + "tresc" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc" + "></div>");
                        }
                        try
                        {
                            Plan Wtorek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 1 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Wtorek.Przedmiot + "</div>");
                            Bar.Insert(554, "<div class=" + "tresc" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc" + "></div>");
                        }
                        try
                        {
                            Plan Poniedzialek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 0 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Poniedzialek.Przedmiot + "</div>");
                            Bar.Insert(554, "<div class=" + "tresc" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc" + "></div>");
                        }

                        try
                        {
                            Bar.Insert(554, CzasyLekcji[i] + "</div>");
                            Bar.Insert(554, "<div class=" + "tytul" + ">");
                        }
                        catch
                        {
                            Bar.Insert(544, "<div class=" + "tytul" + "></div");
                        }
                    }
                    else
                    {
                        try
                        {
                            Plan Piatek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 4 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Piatek.Przedmiot + "</div></br>");
                            Bar.Insert(554, "<div class=" + "tresc2" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc2" + "></div></br>");
                        }
                        try
                        {
                            Plan Czwartek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 3 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Czwartek.Przedmiot + "</div>");
                            Bar.Insert(554, "</div><div class=" + "tresc2" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc2" + "></div>");
                        }
                        try
                        {
                            Plan Sroda = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 2 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Sroda.Przedmiot + "</div>");
                            Bar.Insert(554, "</div><div class=" + "tresc2" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc2" + "></div>");
                        }
                        try
                        {
                            Plan Wtorek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 1 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Wtorek.Przedmiot + "</div>");
                            Bar.Insert(554, "</div><div class=" + "tresc2" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc2" + "></div>");
                        }
                        try
                        {
                            Plan Poniedzialek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 0 && item.Klasa == NazwaKlasy.Nazwa)];
                            Bar.Insert(554, Poniedzialek.Przedmiot + "</div>");
                            Bar.Insert(554, "<div class=" + "tresc2" + ">");
                        }
                        catch
                        {
                            Bar.Insert(554, "<div class=" + "tresc2" + "></div>");
                        }

                        try
                        {
                            Bar.Insert(554, CzasyLekcji[i] + "</div>");
                            Bar.Insert(554, "<div class=" + "tytul" + ">");
                        }
                        catch
                        {
                            Bar.Insert(544, "<div class=" + "tytul" + "></div");
                        }
                    }
                }

                Bar.ToString().Replace("</div></div>", "</div>");

                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\data\\html\\klasa" + klasa + ".html");
                sw.Write(Bar.ToString());
                sw.Close();



                //przed style
                /*
                FileStream fs = new FileStream(Application.StartupPath + "\\data\\html\\1.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                html[0] = sr.ReadToEnd();
                sr.Close();
                html[1] = style;
                 * */
                //po style
                FileStream fs2 = new FileStream(Application.StartupPath + "\\data\\html\\2.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr2 = new StreamReader(fs2);
                html[2] = sr2.ReadToEnd();
                sr2.Close();
                //klasy
                for (int i = 0; i < KlasaList.Count; i++)
                {
                    Klasa KlasaInfo = KlasaList[i];
                    html[3] = html[3] + KlasaInfo.Nazwa + "</br>";
                }
                //po klasach
                FileStream fs3 = new FileStream(Application.StartupPath + "\\data\\html\\3.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr3 = new StreamReader(fs3);
                html[4] = sr3.ReadToEnd();
                sr3.Close();
                //nazwa rozpatrywanej klasy
                html[5] = NazwaKlasy.Nazwa;
                //dni tygodnia
                FileStream fs4 = new FileStream(Application.StartupPath + "\\data\\html\\4.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr4 = new StreamReader(fs4);
                html[6] = sr4.ReadToEnd();
                sr4.Close();
                //PLAN
                for (int i = 0; i < 14; i++)
                {
                    if (i % 2 == 0)
                    {
                        /*
                        Plan Poniedzialek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 0 && item.Klasa == NazwaKlasy.Nazwa)];
                        Plan Wtorek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 1 && item.Klasa == NazwaKlasy.Nazwa)];
                        Plan Sroda = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 2 && item.Klasa == NazwaKlasy.Nazwa)];
                        Plan Czwartek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 3 && item.Klasa == NazwaKlasy.Nazwa)];
                        Plan Piatek = PlanList[PlanList.FindIndex(item => item.Godzina == i && item.Dzien == 4 && item.Klasa == NazwaKlasy.Nazwa)];

                        htmlPlan[i] = '<div class="tytul">' + CzasyLekcji[i];
                        htmlPlan[i+1] = '</div><div class="tresc">' +  Poniedzialek.Przedmiot;
                        htmlPlan[i+2] = '</div><div class="tresc">' + Wtorek.Przedmiot;
                        htmlPlan[i+3] = '</div><div class="tresc">' + Sroda.Przedmiot;
                        htmlPlan[i+4] = '</div><div class="tresc">' + Czwartek.Przedmiot + '</div><div class="tresc">' + Piatek.Przedmiot + '</div> <br />';
                    */
                    }
                    else
                    {
                    }
                }

            }

        }

    }
}
