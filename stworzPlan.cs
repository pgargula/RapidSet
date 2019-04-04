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
    public partial class stworzPlan : Form
    {
        public stworzPlan()
        {
            InitializeComponent();
        }

        public string show = "0";
        public string sciezkaBaza = "0";



        Label[] labelBaza = new Label[200];
        public void stworzPlan_Load(object sender, EventArgs e)
        {
            int x = 0, y = 10, j=0;
	
            DirectoryInfo d = new DirectoryInfo(Application.StartupPath + "/data/project");
            FileInfo[] Files = d.GetFiles("*.xml");          
            foreach(FileInfo file in Files )
            {
                labelBaza[j] = new Label();
                labelBaza[j].Location = new Point(x, y);
                labelBaza[j].Size = new Size(150, 20);
                labelBaza[j].Text = file.Name;
                labelBaza[j].Font = new Font("Segoe UI", 9);
                labelBaza[j].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(78)))), ((int)(((byte)(171)))));
                labelBaza[j].ForeColor = Color.White;
                labelBaza[j].MouseHover += new EventHandler(this.labelBaza_MouseHover);
                labelBaza[j].MouseLeave += new EventHandler(this.labelBaza_MouseLeave);
                labelBaza[j].Click += new EventHandler(this.labelBaza_Click);
                labelBaza[j].Parent = panel2;
                y = y + 23;
                j++;
            }

            label1.Visible = true;
            label1.Text = "Edycja klas";
            wybrane = 1;
            dodajButton_Click(sender, e);
            panel1.Size = new Size(320, 579);
            //timer5.Start();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Application.StartupPath + "\\data\\settings.xml");
            XmlNodeList elemList = XmlDoc.GetElementsByTagName("tip");
            show = elemList[0].Attributes["show"].Value;
            if (show == "1")
            {
                transparentRichTextBox1.Text = "Podaj nazwy klas, które znajdują się w szkole, a następnie kliknij dalej.";
                transparentRichTextBox2.Text = "Uzupełnij listę przedmiotów szkolnych oraz podaj, które klasy uczą się danego przedmiotu.";
                transparentRichTextBox3.Text = "Uzupełnij listę nauczycieli, podaj jakiego uczą przedmiotu oraz jakie klasy.";
                transparentRichTextBox4.Text = "Podaj nazwy sal lekcyjnych oraz jakie przedmioty mogą się w nich odbywać.";
                transparentRichTextBox1.Visible = true;
                transparentRichTextBox2.Visible = false;
                transparentRichTextBox3.Visible = false;
                transparentRichTextBox4.Visible = false;
                imgTip.Visible = true;
                checkBox1.Visible = true;
            }
            else
            {
                transparentRichTextBox1.Visible = false;
                transparentRichTextBox2.Visible = false;
                transparentRichTextBox3.Visible = false;
                transparentRichTextBox4.Visible = false;
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
        int indexBaza;
        public void labelBaza_Click(object sender, EventArgs e)
        {
            for (indexBaza = 0; indexBaza < 50; indexBaza++)
            {
                if (sender == labelBaza[indexBaza])
                    break;
            }
            FileStream fs = new FileStream(Application.StartupPath + "\\data\\adres.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(Application.StartupPath + "\\data\\project\\" + labelBaza[indexBaza].Text);
            sw.Close();

            FileStream fs2 = new FileStream(Application.StartupPath + "\\data\\lorg.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw2 = new StreamWriter(fs2);
            sw2.Write("1");
            sw2.Close();

            wczytaniePlanuForm okienko = new wczytaniePlanuForm();
            okienko.ShowDialog();
            Close();
        }
        
        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        int index = 0;
        int wybrane=1;
        int iloscPrzedmiotow = 0;
        public string nazwaBazy = "default";
        int yGroupBx = 0;
        
        List<string> nazwaKlasy = new List<string>();
        public List<IloscGodzinPrzedmiotu> dodaniePrzedmiotuList = new List<IloscGodzinPrzedmiotu>();
        List<Nauczyciel> NauczycielList = new List<Nauczyciel>(); //pomocnicza

        List<Sala> SalaList = new List<Sala>();
        List<Nauczyciel> NauczycielListGlowny = new List<Nauczyciel>(); //głowna lista nauczycieli
        List<Klasa> KlasaListGlowny = new List<Klasa>(); //główna lista klas
        List<Przedmiot> PrzedmiotListGlowny = new List<Przedmiot>(); //główna lista przedmiotów
        List<bool> Trudnosc = new List<bool>();
        List<string> PrzedmiotWszystkie = new List<string>();
        List<string> CzasyLekcji = new List<string>();

        PictureBox[] picBx = new PictureBox[50];
        Panel[] groupBx = new Panel[200];
        TextBox[] textBx = new TextBox[200];
        Label[] label = new Label[200];
        Label[] labelNauczyciel = new Label[200];
        ComboBox[] comboBx = new ComboBox[200];
        public void picBx_MouseHover(object sender, EventArgs e)
        {
            (sender as PictureBox).BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\wlasciwosci2.png");
        }
        public void picBx_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\wlasciwosci.png");
        }


        public void dodajButton_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                panel1.Controls.Clear();
            }
            if (wybrane == 1) //dodanie klasy
            {
                panel1.Size = new Size(320, 579);
                groupBx[index] = new Panel();
                textBx[index] = new TextBox();
                label[index] = new Label();


                textBx[index].Location = new Point(85, 17);
                textBx[index].Size = new Size(180, 20);
                textBx[index].Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                textBx[index].Focus();

                label[index].Location = new Point(10, 20);
                label[index].Size = new Size(80, 20);
                label[index].Text = "Nazwa klasy: ";
                label[index].Font = new Font("Segoe UI", 8.25F);
                label[index].ForeColor = Color.White;

                groupBx[index].Visible = true;
                groupBx[index].Controls.Add(textBx[index]);
                groupBx[index].Controls.Add(label[index]);
                groupBx[index].Location = new Point(0, yGroupBx);
                groupBx[index].Size = new Size(300, 50);
                groupBx[index].TabIndex = 6;
                groupBx[index].TabStop = false;
                groupBx[index].Parent = panel1;
                groupBx[index].BackColor = Color.Transparent;
                groupBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_tworzenie.png");

                yGroupBx = yGroupBx + 53;

                
                index++;
            }
            if (wybrane == 2) //dodanie przedmiotu
            {
                panel1.Size = new Size(480, 579);
             

                label1.Text = "Edycja przedmiotów";
                label1.Location = new Point(150, 36);

                groupBx[index] = new Panel();
                textBx[index] = new TextBox();
                label[index] = new Label();
                picBx[index] = new PictureBox();

                textBx[index].Location = new Point(125, 17);
                textBx[index].Size = new Size(140, 20);
                textBx[index].Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                textBx[index].Focus();

                label[index].Location = new Point(10, 20);
                label[index].Size = new Size(120, 20);
                label[index].Text = "Nazwa przedmiotu: ";
                label[index].Font = new Font("Segoe UI", 8.25F);
                label[index].ForeColor = Color.White;

                picBx[index].Location = new Point(270, 17);
                picBx[index].BackColor = Color.Transparent;
                picBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\wlasciwosci.png");
                picBx[index].Size = new Size(20, 20);
                picBx[index].Click += new System.EventHandler(this.picBx_Click);
                picBx[index].MouseHover += new System.EventHandler(this.picBx_MouseHover);
                picBx[index].MouseLeave += new System.EventHandler(this.picBx_MouseLeave);
                picBx[index].Name = "picBx" + index;

                groupBx[index].Controls.Add(picBx[index]);
                groupBx[index].Controls.Add(textBx[index]);
                groupBx[index].Controls.Add(label[index]);
                groupBx[index].Location = new Point(0, yGroupBx);
                groupBx[index].Size = new Size(300, 50);
                groupBx[index].TabIndex = 6;
                groupBx[index].TabStop = false;
                groupBx[index].Parent = panel1;
                groupBx[index].BackColor = Color.Transparent;
                groupBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_tworzenie.png");

                yGroupBx = yGroupBx + 53;
                index++;
                iloscPrzedmiotow++;
            }

            if (wybrane == 3) //dodanie nauczyciela
            {
                panel1.Size = new Size(480, 579);
                label1.Text = "Edycja nauczycieli";
                label1.Location = new Point(150, 36);
        

                groupBx[index] = new Panel();
                textBx[index] = new TextBox();
                label[index] = new Label();
                labelNauczyciel[index] = new Label();
                comboBx[index] = new ComboBox();
                picBx[index] = new PictureBox();

                textBx[index].Location = new Point(120, 17);
                textBx[index].Size = new Size(140, 20);
                textBx[index].Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                textBx[index].Focus();

                label[index].Location = new Point(20, 20);
                label[index].Size = new Size(120, 20);
                label[index].Text = "Imię i nazwisko: ";
                label[index].Font = new Font("Segoe UI", 8.25F);
                label[index].ForeColor = Color.White;

                labelNauczyciel[index].Location = new Point(20, 50);
                labelNauczyciel[index].Size = new Size(100, 20);
                labelNauczyciel[index].Text = "Przedmiot: ";
                labelNauczyciel[index].Font = new Font("Segoe UI", 8.25F);
                labelNauczyciel[index].ForeColor = Color.White;

                comboBx[index].Location = new Point(120, 47);
                comboBx[index].Size = new Size(140, 20);
                comboBx[index].Name = "comboBx" + index;
                comboBx[index].Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                comboBx[index].Text = "Wybierz...";
                comboBx[index].SelectedIndexChanged += new System.EventHandler(this.comboBx_SelectedIndexChanged);
                for (int i = 0; i < nazwyKlasPrzedmiot.Count; i++)
                {
                    IloscGodzinPrzedmiotu dodaniePrzedmiotuInfo = dodaniePrzedmiotuList[i];
                    if (i > 0 && comboBx[index].Items.Contains(dodaniePrzedmiotuInfo.NazwaPrzedmiotu))
                    {
                    }
                    else
                    {
                        comboBx[index].Items.Add(dodaniePrzedmiotuInfo.NazwaPrzedmiotu);
                    }
                }

                picBx[index].Location = new Point(270, 33);
                picBx[index].BackColor = Color.Transparent;
                picBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\wlasciwosci.png");
                picBx[index].Size = new Size(20, 20);
                picBx[index].Click += new System.EventHandler(this.picBx_Click);
                picBx[index].MouseHover += new System.EventHandler(this.picBx_MouseHover);
                picBx[index].MouseLeave += new System.EventHandler(this.picBx_MouseLeave);
                groupBx[index].Controls.Add(picBx[index]);
                groupBx[index].Controls.Add(labelNauczyciel[index]);
                groupBx[index].Controls.Add(comboBx[index]);
                groupBx[index].Controls.Add(textBx[index]);
                groupBx[index].Controls.Add(label[index]);
                groupBx[index].Location = new Point(0, yGroupBx);
                groupBx[index].Size = new Size(300, 80);
                groupBx[index].TabIndex = 6;
                groupBx[index].TabStop = false;
                groupBx[index].Parent = panel1;
                groupBx[index].BackColor = Color.Transparent;
                groupBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_tworzenie.png");

                yGroupBx = yGroupBx + 83;


                index++;
            }
            if (wybrane == 4)
            {
                panel1.Size = new Size(480, 579);


                label1.Text = "Edycja sal lekcyjnych";
                label1.Location = new Point(150, 36);

                groupBx[index] = new Panel();
                textBx[index] = new TextBox();
                label[index] = new Label();
                picBx[index] = new PictureBox();

                textBx[index].Location = new Point(125, 17);
                textBx[index].Size = new Size(140, 20);
                textBx[index].Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                textBx[index].Focus();

                label[index].Location = new Point(10, 20);
                label[index].Size = new Size(120, 20);
                label[index].Text = "Nazwa sali: ";
                label[index].Font = new Font("Segoe UI", 8.25F);
                label[index].ForeColor = Color.White;

                picBx[index].Location = new Point(270, 17);
                picBx[index].BackColor = Color.Transparent;
                picBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\wlasciwosci.png");
                picBx[index].Size = new Size(20, 20);
                picBx[index].Click += new System.EventHandler(this.picBx_Click);
                picBx[index].MouseHover += new System.EventHandler(this.picBx_MouseHover);
                picBx[index].MouseLeave += new System.EventHandler(this.picBx_MouseLeave);
                picBx[index].Name = "picBx" + index;

                groupBx[index].Controls.Add(picBx[index]);
                groupBx[index].Controls.Add(textBx[index]);
                groupBx[index].Controls.Add(label[index]);
                groupBx[index].Location = new Point(0, yGroupBx);
                groupBx[index].Size = new Size(300, 50);
                groupBx[index].TabIndex = 6;
                groupBx[index].TabStop = false;
                groupBx[index].Parent = panel1;
                groupBx[index].BackColor = Color.Transparent;
                groupBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_tworzenie.png");

                yGroupBx = yGroupBx + 53;
                index++;
            }
            if (wybrane == 5)
            {
                panel1.Size = new Size(480, 579);


                label1.Text = "Edycja godzin lekcyjnych";
                label1.Location = new Point(150, 36);

                groupBx[index] = new Panel();
                textBx[index] = new TextBox();
                label[index] = new Label();

                textBx[index].Location = new Point(125, 17);
                textBx[index].Size = new Size(140, 20);
                textBx[index].Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                textBx[index].Focus();

                label[index].Location = new Point(10, 20);
                label[index].Size = new Size(120, 20);
                label[index].Text = "Czas trwania lekcji: ";
                label[index].Font = new Font("Segoe UI", 8.25F);
                label[index].ForeColor = Color.White;

              

                groupBx[index].Controls.Add(picBx[index]);
                groupBx[index].Controls.Add(textBx[index]);
                groupBx[index].Controls.Add(label[index]);
                groupBx[index].Location = new Point(0, yGroupBx);
                groupBx[index].Size = new Size(300, 50);
                groupBx[index].TabIndex = 6;
                groupBx[index].TabStop = false;
                groupBx[index].Parent = panel1;
                groupBx[index].BackColor = Color.Transparent;
                groupBx[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_tworzenie.png");

                yGroupBx = yGroupBx + 53;
                index++;
            }


        }

        Panel[] groupBxEdit = new Panel[50];
        Label[] labelEdit = new Label[50];
        Label[] labelTrudnosc = new Label[50];
        CheckBox[] checkBxEdit = new CheckBox[200];
        CheckBox[] checkBxEditTrudny = new CheckBox[200];
        CheckBox[] checkBxEditLatwy = new CheckBox[200];
        List<PrzechowaneKlasy> nazwyKlasPrzedmiot = new List<PrzechowaneKlasy>();
        List<KlasaNauczycielPrzedmiot> KlasaNauczycielPrzedmiotList = new List<KlasaNauczycielPrzedmiot>();

        public int iloscTygodniowo = 0;
        public int maxDziennie = 0;
        int iloscCheckBoxClick = 0;
        int iloscCheckBoxNauczycieleClick = 0;
        public void checkBxEdit_Click(object sender, EventArgs e)
        {
            iloscCheckBoxClick++;
            int i;
            for (i = 0; i < 50;i++ )
            {
                if (sender == checkBxEdit[i])
                    break;
            }
            if (checkBxEdit[i].Checked == true)
            {
                dodaniePrzedmiotuKlasie okno = new dodaniePrzedmiotuKlasie();
                okno.Wyswietl(nazwaKlasy[i], textBx[wybrany].Text);
                okno.ShowDialog();
                dodaniePrzedmiotuList.Add(new IloscGodzinPrzedmiotu(nazwaKlasy[i], textBx[wybrany].Text, okno.UstawienieGodzinTygodniowo(iloscTygodniowo), okno.UstawienieGodzinDziennie(maxDziennie)));
                nazwyKlasPrzedmiot.Add(new PrzechowaneKlasy(nazwaKlasy[i], textBx[wybrany].Text));
            }
        }
        int wybranyIndexPrzedmiotu = 0;
        public void comboBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < 50; i++)
            {
                if (sender == comboBx[i])
                    break;
            }
            wybranyIndexPrzedmiotu = i;
        }
        int wybrany = 0, yGroupBxEdit = 0;
        public void picBx_Click(object sender, EventArgs e)
        {
            if (wybrane == 2)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (sender == picBx[i])
                    {
                        wybrany = i;
                        break;
                    }
                }
                if (textBx[wybrany].Text != "")
                {
                    string nazwa;
                    //sprawdzenie czy istnieją jakieś groupbxedit, jeśli tak to je ukryje
                    for (int i = 0; i < 50; i++)
                    {
                        nazwa = "groupBxEdit" + i;
                        Control[] controls = Controls.Find(nazwa, true);
                        if (controls.Length > 0)
                        {
                            groupBxEdit[i].Visible = false;
                        }
                    }

                    nazwa = "groupBxEdit" + wybrany;
                    Control[] control = Controls.Find(nazwa, true);
                    if (control.Length > 0)
                    {
                        groupBxEdit[wybrany].Visible = true;
                    }
                    else
                    {
                        yGroupBxEdit = 53 * wybrany;
                        groupBxEdit[wybrany] = new Panel();
                        labelEdit[wybrany] = new Label();
                        labelTrudnosc[wybrany] = new Label();

                        labelEdit[wybrany].Text = "Klasy uczące się tego przedmiotu";
                        labelEdit[wybrany].Size = new Size(130, 50);
                        labelEdit[wybrany].Location = new Point(5, 10);
                        labelEdit[wybrany].Font = new Font("Segoe UI", 8.25F);
                        labelEdit[wybrany].ForeColor = Color.White;
                        int x = 5, y = 50;
                        for (int i = 0; i < nazwaKlasy.Count; i++)
                        {
                            checkBxEdit[i] = new CheckBox();
                            checkBxEdit[i].Size = new Size(50, 20);
                            checkBxEdit[i].Location = new Point(x, y);
                            checkBxEdit[i].Text = nazwaKlasy[i];
                            checkBxEdit[i].Name = "checkBxEdit" + i;
                            checkBxEdit[i].Font = new Font("Segoe UI", 8.25F);
                            checkBxEdit[i].ForeColor = Color.White;
                            checkBxEdit[i].Click += new System.EventHandler(this.checkBxEdit_Click);
                            x = x + 50;
                            if (i % 2 == 0 && i != 0)
                            {
                                y = y + 20;
                                x = 5;
                            }
                            groupBxEdit[wybrany].Controls.Add(checkBxEdit[i]);
                        }
                        labelTrudnosc[wybrany].Text = "Trudność przedmiotu";
                        labelTrudnosc[wybrany].Size = new Size(130, 20);
                        labelTrudnosc[wybrany].Location = new Point(5, 100 + ((nazwaKlasy.Count / 3) * 20));
                        labelTrudnosc[wybrany].Font = new Font("Segoe UI", 8.25F);
                        labelTrudnosc[wybrany].ForeColor = Color.White;

                        checkBxEditTrudny[wybrany] = new CheckBox();
                        checkBxEditLatwy[wybrany] = new CheckBox();

                        checkBxEditTrudny[wybrany].Size = new Size(65, 20);
                        checkBxEditTrudny[wybrany].Location = new Point(5, 120 + ((nazwaKlasy.Count / 3) * 20));
                        checkBxEditTrudny[wybrany].Text = "Trudny";
                        checkBxEditTrudny[wybrany].Name = "checkBxEditTrudny" + wybrany;
                        checkBxEditTrudny[wybrany].Font = new Font("Segoe UI", 8.25F);
                        checkBxEditTrudny[wybrany].ForeColor = Color.White;
                        checkBxEditTrudny[wybrany].Click += new System.EventHandler(this.checkBxTrudnosc_Click);

                        checkBxEditLatwy[wybrany].Size = new Size(60, 20);
                        checkBxEditLatwy[wybrany].Location = new Point(80, 120 + ((nazwaKlasy.Count / 3) * 20));
                        checkBxEditLatwy[wybrany].Text = "Łatwy";
                        checkBxEditLatwy[wybrany].Name = "checkBxEditLatwy" + wybrany;
                        checkBxEditLatwy[wybrany].Font = new Font("Segoe UI", 8.25F);
                        checkBxEditLatwy[wybrany].ForeColor = Color.White;
                        checkBxEditLatwy[wybrany].Checked = true;
                        checkBxEditLatwy[wybrany].Click += new System.EventHandler(this.checkBxTrudnosc_Click);

                        groupBxEdit[wybrany].Controls.Add(checkBxEditTrudny[wybrany]);
                        groupBxEdit[wybrany].Controls.Add(checkBxEditLatwy[wybrany]);
                        groupBxEdit[wybrany].Controls.Add(labelTrudnosc[wybrany]);
                        groupBxEdit[wybrany].Controls.Add(labelEdit[wybrany]);
                        groupBxEdit[wybrany].BackColor = Color.Transparent;
                        groupBxEdit[wybrany].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_edycja.png");
                        groupBxEdit[wybrany].Location = new Point(300, yGroupBxEdit);
                        groupBxEdit[wybrany].Size = new Size(160, 170 + ((nazwaKlasy.Count / 3) * 20));
                        groupBxEdit[wybrany].TabIndex = 6;
                        groupBxEdit[wybrany].TabStop = false;
                        groupBxEdit[wybrany].Parent = panel1;
                        groupBxEdit[wybrany].Name = "groupBxEdit" + wybrany;
                    }
                }
                else
                {
                    MessageBox.Show("Uzupełnij nazwę przedmiotu");
                }
            }
            if (wybrane == 3)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (sender == picBx[i])
                    {
                        wybrany = i;
                        break;
                    }
                }
                if (textBx[wybrany].Text != "")
                {
                    string nazwa;
                    //sprawdzenie czy istnieją jakieś groupbxedit, jeśli tak to je ukryje
                    for (int i = 0; i < 50; i++)
                    {
                        nazwa = "groupBxEdit" + i;
                        Control[] controls = Controls.Find(nazwa, true);
                        if (controls.Length > 0)
                        {
                            groupBxEdit[i].Visible = false;
                        }
                    }

                    nazwa = "groupBxEdit" + wybrany;
                    Control[] control = Controls.Find(nazwa, true);
                    if (control.Length > 0)
                    {
                        groupBxEdit[wybrany].Visible = true;
                    }
                    else
                    {
                        groupBxEdit[wybrany] = new Panel();
                        labelEdit[wybrany] = new Label();
                        labelTrudnosc[wybrany] = new Label();

                        labelEdit[wybrany].Text = "Klasy uczone przez nauczyciela:";
                        labelEdit[wybrany].Size = new Size(130, 50);
                        labelEdit[wybrany].Location = new Point(5, 10);
                        labelEdit[wybrany].Font = new Font("Segoe UI", 8.25F);
                        labelEdit[wybrany].ForeColor = Color.White;
                        int x = 5, y = 50;
                        for (int i = 0; i < nazwyKlasPrzedmiot.Count; i++)
                        {
                            PrzechowaneKlasy nazwyKlasPrzedmiotInfo = nazwyKlasPrzedmiot[i];
                            if (nazwyKlasPrzedmiotInfo.NazwaPrzedmiotu == comboBx[wybranyIndexPrzedmiotu].Text)
                            {
                                checkBxEdit[i] = new CheckBox();
                                checkBxEdit[i].Size = new Size(50, 20);
                                checkBxEdit[i].Location = new Point(x, y);
                                checkBxEdit[i].Text = nazwyKlasPrzedmiotInfo.NazwaKlasy;
                                checkBxEdit[i].Name = "checkBxEdit" + i;
                                checkBxEdit[i].Font = new Font("Segoe UI", 8.25F);
                                checkBxEdit[i].ForeColor = Color.White;
                                checkBxEdit[i].Click += new System.EventHandler(this.checkBxEditNauczyciele_Click);
                                x = x + 50;
                                if (i % 2 == 0 && i != 0)
                                {
                                    y = y + 20;
                                    x = 5;
                                }
                                groupBxEdit[wybrany].Controls.Add(checkBxEdit[i]);
                            }
                           
                        }
                        
                        groupBxEdit[wybrany].Controls.Add(labelEdit[wybrany]);
                        groupBxEdit[wybrany].BackColor = Color.Transparent;
                        groupBxEdit[wybrany].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_edycja.png");
                        groupBxEdit[wybrany].Location = new Point(300, yGroupBxEdit);
                        groupBxEdit[wybrany].Size = new Size(160, 100 + ((nazwaKlasy.Count / 3) * 20));
                        groupBxEdit[wybrany].TabIndex = 6;
                        groupBxEdit[wybrany].TabStop = false;
                        groupBxEdit[wybrany].Parent = panel1;
                        groupBxEdit[wybrany].Name = "groupBxEdit" + wybrany;
                        yGroupBxEdit = yGroupBxEdit + 83;
                    }
                }
                else
                {
                    MessageBox.Show("Uzupełnij dane nauczyciela");
                }
            }
            if (wybrane == 4)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (sender == picBx[i])
                    {
                        wybrany = i;
                        break;
                    }
                }
                if (textBx[wybrany].Text != "")
                {
                    string nazwa;
                    //sprawdzenie czy istnieją jakieś groupbxedit, jeśli tak to je ukryje
                    for (int i = 0; i < 50; i++)
                    {
                        nazwa = "groupBxEdit" + i;
                        Control[] controls = Controls.Find(nazwa, true);
                        if (controls.Length > 0)
                        {
                            groupBxEdit[i].Visible = false;
                        }
                    }

                    nazwa = "groupBxEdit" + wybrany;
                    Control[] control = Controls.Find(nazwa, true);
                    if (control.Length > 0)
                    {
                        groupBxEdit[wybrany].Visible = true;
                    }
                    else
                    {
                        groupBxEdit[wybrany] = new Panel();
                        labelEdit[wybrany] = new Label();
                        labelTrudnosc[wybrany] = new Label();

                        labelEdit[wybrany].Text = "Przedmioty odbywające się w tej sali:";
                        labelEdit[wybrany].Size = new Size(130, 50);
                        labelEdit[wybrany].Location = new Point(5, 10);
                        labelEdit[wybrany].Font = new Font("Segoe UI", 8.25F);
                        labelEdit[wybrany].ForeColor = Color.White;
                        int x = 5, y = 50;
                        for (int i = 0; i < PrzedmiotWszystkie.Count; i++)
                        {
                            checkBxEdit[i] = new CheckBox();
                            checkBxEdit[i].Size = new Size(100, 20);
                            checkBxEdit[i].Location = new Point(x, y);
                            checkBxEdit[i].Text = PrzedmiotWszystkie[i];
                            checkBxEdit[i].Name = "checkBxEdit" + i;
                            checkBxEdit[i].Font = new Font("Segoe UI", 8.25F);
                            checkBxEdit[i].ForeColor = Color.White;
                            checkBxEdit[i].Click += new System.EventHandler(this.checkBxEditSale_Click);
                            y = y + 20;
                            groupBxEdit[wybrany].Controls.Add(checkBxEdit[i]);
                        }
                        groupBxEdit[wybrany].Controls.Add(labelEdit[wybrany]);
                        groupBxEdit[wybrany].BackColor = Color.Transparent;
                        groupBxEdit[wybrany].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tlo_edycja.png");
                        groupBxEdit[wybrany].Location = new Point(300, yGroupBxEdit);
                        groupBxEdit[wybrany].Size = new Size(160, 100 + (PrzedmiotWszystkie.Count * 20));
                        groupBxEdit[wybrany].TabIndex = 6;
                        groupBxEdit[wybrany].TabStop = false;
                        groupBxEdit[wybrany].Parent = panel1;
                        groupBxEdit[wybrany].Name = "groupBxEdit" + wybrany;
                        yGroupBxEdit = yGroupBxEdit + 83;
                    }
                }
                else
                {
                    MessageBox.Show("Uzupełnij nazwę sali");
                }
            }
        }
        int iloscPrzedmiotList = 0;
        public void checkBxEditSale_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < 50; i++)
            {
                if (sender == checkBxEdit[i])
                    break;
            }
            SalaList.Add(new Sala(textBx[wybrany].Text, checkBxEdit[i].Text));  // !!!!!!!!!! główna lista sal
        }

        public void checkBxEditNauczyciele_Click(object sender, EventArgs e)
        {
            iloscCheckBoxNauczycieleClick++;
            int i;
            for (i = 0; i < 50; i++)
            {
                if (sender == checkBxEdit[i])
                    break;
            }
            KlasaNauczycielPrzedmiotList.Add(new KlasaNauczycielPrzedmiot(checkBxEdit[i].Text, textBx[wybrany].Text, comboBx[wybrany].Text,wybrany));
            iloscPrzedmiotList++;
          //Nauczyciel NauczycielListInfo = NauczycielList[0];
          //NauczycielListGlowny.Add(new Nauczyciel(NauczycielListInfo.ID,NauczycielListInfo.Nazwa,


        }
        public void checkBxTrudnosc_Click(object sender, EventArgs e)
        {
            CheckBox btn = (sender as CheckBox);
            if (btn.Text == "Łatwy")
            {
                btn.Checked = true;
                checkBxEditTrudny[wybrany].Checked = false;
            }
            if (btn.Text == "Trudny")
            {
                btn.Checked = true;
                checkBxEditLatwy[wybrany].Checked = false;
            }
           

        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            bool sprawdzenie = true;
            if (wybrane == 1)
            {
                for(int i=0;i<index;i++)
                {
                    if (textBx[i].Text == "")
                    {
                        sprawdzenie = false;
                        MessageBox.Show("Uzupełnij wszystkie dane");
                        break;
                    }
                }

                if (sprawdzenie == true)
                {
                    for (int j = 0; j < index; j++)
                    {
                        nazwaKlasy.Add(textBx[j].Text); // dodanie klasy do listy
                    }
                    if (show == "1")
                    {
                        transparentRichTextBox1.Visible = false;
                        transparentRichTextBox2.Visible = true;
                        transparentRichTextBox3.Visible = false;
                        transparentRichTextBox4.Visible = false;
                    }
                }
            }
            if (wybrane == 2)
            {

                for (int i = 0; i < index; i++)
                {
                    if (textBx[i].Text == "" || iloscCheckBoxClick < index)
                    {
                        sprawdzenie = false;
                        MessageBox.Show("Uzupełnij wszystkie dane");
                        break;
                    }
                }
                if (sprawdzenie == true)
                {
                    for (int i = 0; i < index; i++)
                    {
                        if (checkBxEditLatwy[i].Checked == true)
                        {
                            Trudnosc.Add(false);
                        }
                        if (checkBxEditTrudny[i].Checked == true)
                        {
                            Trudnosc.Add(true);
                        }
                    }
                    for (int i = 0; i < index; i++)
                    {
                        PrzedmiotWszystkie.Add(textBx[i].Text);
                    }
                    if (show == "1")
                    {
                        transparentRichTextBox1.Visible = false;
                        transparentRichTextBox2.Visible = false;
                        transparentRichTextBox3.Visible = true;
                        transparentRichTextBox4.Visible = false;
                    }
                }
            }


            if (wybrane == 3)
            {
                
                for (int i = 0; i < index; i++)
                {
                    if (textBx[i].Text == "" || iloscCheckBoxNauczycieleClick < index || iloscPrzedmiotList < index)
                    {
                        sprawdzenie = false;
                        MessageBox.Show("Uzupełnij wszystkie dane");
                        break;
                    }
                }
                if (sprawdzenie == true)
                {
                    int sumaGodzin = 0;
                    int sumaPrzedmiotow = 0;
                    for (int i = 0; i < index; i++)
                    {
                        NauczycielList.Add(new Nauczyciel(i,textBx[i].Text, comboBx[i].Text)); //!!!!!!!!!!! główna lista nauczycieli
                    }
                    int k = 0;
                    for (int i = 0; i < nazwaKlasy.Count; i++)
                    {
                        for (int j = 0; j < dodaniePrzedmiotuList.Count; j++)
                        {
                            IloscGodzinPrzedmiotu dodaniePrzedmiotuInfo = dodaniePrzedmiotuList[j];
                            if (dodaniePrzedmiotuInfo.NazwaKlasy == nazwaKlasy[i])
                            {
                                sumaGodzin = sumaGodzin + dodaniePrzedmiotuInfo.IloscWTygodniu;
                            }
                        }
                        for (int j = 0; j < nazwyKlasPrzedmiot.Count; j++)
                        {
                            PrzechowaneKlasy nazwyKlasPrzedmiotInfo = nazwyKlasPrzedmiot[j];
                            if (nazwyKlasPrzedmiotInfo.NazwaKlasy == nazwaKlasy[k])
                            {
                                sumaPrzedmiotow++;
                            }
                        }

                        KlasaListGlowny.Add(new Klasa(nazwaKlasy[i], sumaGodzin, sumaPrzedmiotow)); // !!!!!!!!!!!! główna lista klas




                        k++;
                        sumaGodzin = 0;
                        sumaPrzedmiotow = 0;
                    }

                    for (int j = 0; j < iloscPrzedmiotList; j++)
                    {
                        IloscGodzinPrzedmiotu dodaniePrzedmiotuInfo = dodaniePrzedmiotuList[j];

                        KlasaNauczycielPrzedmiot KlasaNauczycielPrzedmiotInfo = KlasaNauczycielPrzedmiotList[j];

                        //!!!!!!!!!!!!! główna lista przedmiotów
                        PrzedmiotListGlowny.Add(new Przedmiot(dodaniePrzedmiotuInfo.NazwaKlasy, dodaniePrzedmiotuInfo.NazwaPrzedmiotu, KlasaNauczycielPrzedmiotInfo.NazwaNauczyciela, KlasaNauczycielPrzedmiotInfo.ID, dodaniePrzedmiotuInfo.IloscWTygodniu, dodaniePrzedmiotuInfo.MaxIlosc, Trudnosc[KlasaNauczycielPrzedmiotInfo.ID]));

                        //MessageBox.Show(Convert.ToString(dodaniePrzedmiotuInfo.NazwaKlasy + ", " + dodaniePrzedmiotuInfo.NazwaPrzedmiotu + ", " + KlasaNauczycielPrzedmiotInfo.NazwaNauczyciela + ", " + KlasaNauczycielPrzedmiotInfo.ID + ", " + dodaniePrzedmiotuInfo.IloscWTygodniu + ", " + dodaniePrzedmiotuInfo.MaxIlosc + ", " + Trudnosc[KlasaNauczycielPrzedmiotInfo.ID].ToString()));


                    }
                    if (show == "1")
                    {
                        transparentRichTextBox1.Visible = false;
                        transparentRichTextBox2.Visible = false;
                        transparentRichTextBox3.Visible = false;
                        transparentRichTextBox4.Visible = true;
                    }

                }
            }
            if (wybrane == 4)
            {
                for (int i = 0; i < index; i++)
                {
                    if (textBx[i].Text == "")
                    {
                        sprawdzenie = false;
                        MessageBox.Show("Uzupełnij wszystkie dane");
                        break;

                    }
                    
                }
                
            }
            if (wybrane == 5)
            {
                for (int j = 0; j < index; j++)
                {
                    if (textBx[j].Text == "")
                    {
                        sprawdzenie = false;
                        MessageBox.Show("Uzupełnij wszystkie dane");
                        break;

                    }
                    else
                    {
                        CzasyLekcji.Add(textBx[j].Text);
                        utworzenieBazy okno = new utworzenieBazy();
                        okno.ShowDialog();

                        XmlTextWriter Writer = new XmlTextWriter(Application.StartupPath + "/data/project/" + okno.Adres(nazwaBazy) + ".xml", UTF8Encoding.UTF8);
                        Writer.Formatting = Formatting.Indented;
                        Writer.Indentation = 4;
                        Writer.WriteStartDocument();
                        //klasy
                        Writer.WriteStartElement("root");
                        Writer.WriteStartElement("klasy");
                        for (int i = 0; i < KlasaListGlowny.Count; i++)
                        {
                            Writer.WriteStartElement("klasa");
                            Klasa KlasaInfo = KlasaListGlowny[i];
                            Writer.WriteAttributeString("nazwa_klasy", KlasaInfo.Nazwa);
                            Writer.WriteAttributeString("ilosc_godzin", Convert.ToString(KlasaInfo.IloscGodzin));
                            Writer.WriteAttributeString("ilosc_przedmiotow", Convert.ToString(KlasaInfo.IloscPrzedmiotow));
                            Writer.WriteEndElement();
                        }
                        Writer.WriteEndElement();
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


                        wczytaniePlanuForm okienko = new wczytaniePlanuForm();
                        okienko.Generuj();
                        okienko.ShowDialog();
                        Close();
                    }
                }

                

            }
            if (sprawdzenie == true)
            {
                index = 0;
                wybrane++;
                yGroupBx = 0;
                yGroupBxEdit = 0;
                dodajButton_Click(sender, e);
            }


        }

        private void stworzPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                dodajButton_Click(sender, e);
        }

        private void usunButton_Click(object sender, EventArgs e)
        {
            if (wybrane == 1)
            {
                index--;
                yGroupBx = yGroupBx - 53;
                groupBx[index].Visible = false;
                groupBx[index].Parent = panel1;
            }
            if (wybrane == 2)
            {
                yGroupBx = yGroupBx - 53;
                index--;
                iloscPrzedmiotow--;
                groupBx[index].Visible = false;
                groupBx[index].Parent = panel1;
            }
            if (wybrane == 3)
            {
                yGroupBx = yGroupBx - 83;
                yGroupBxEdit = yGroupBxEdit - 83;
                index--;
                iloscPrzedmiotList--;
                groupBx[index].Visible = false;
                groupBx[index].Parent = panel1;
                comboBx[index].Items.Remove(index);
            }
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
            transparentRichTextBox2.Visible = false;
            transparentRichTextBox3.Visible = false;
            transparentRichTextBox4.Visible = false;
            show = "0";
        }

        private void dodajButton_MouseHover(object sender, EventArgs e)
        {
            label3.Location = new Point(778, 710);
            label3.Text = "Dodaj";
            dodajButton.BackgroundImage = Image.FromFile(Application.StartupPath + "//images//button-plus-podswietlony.png");
        }

        private void usunButton_MouseHover(object sender, EventArgs e)
        {
            label3.Location = new Point(855, 710);
            label3.Text = "Usuń";
            usunButton.BackgroundImage = Image.FromFile(Application.StartupPath + "//images//button-minus-podswietlony.png");
        }

        private void nextButton_MouseHover(object sender, EventArgs e)
        {
            label3.Location = new Point(850, 710);
            label3.Text = "Następny krok";
            nextButton.BackgroundImage = Image.FromFile(Application.StartupPath + "//images//strzalka-prawo2.png");
        }

        private void dodajButton_MouseLeave(object sender, EventArgs e)
        {
            label3.Text = "";
            dodajButton.BackgroundImage = Image.FromFile(Application.StartupPath + "//images//button-plus2.png");
        }

        private void usunButton_MouseLeave(object sender, EventArgs e)
        {
            label3.Text = "";
            usunButton.BackgroundImage = Image.FromFile(Application.StartupPath + "//images//button-minus.png");
        }

        private void nextButton_MouseLeave(object sender, EventArgs e)
        {
            label3.Text = "";
            nextButton.BackgroundImage = Image.FromFile(Application.StartupPath + "//images//strzalka-prawo.png");
        }
    }
}
