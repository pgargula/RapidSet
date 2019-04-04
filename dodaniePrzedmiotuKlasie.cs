using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rapidset
{
    public partial class dodaniePrzedmiotuKlasie : Form
    {
        public dodaniePrzedmiotuKlasie()
        {
            InitializeComponent();
        }
        public void Wyswietl(string nklasa, string nprzedmiot)
        {
            label1.Text = "Przedmiot: " + nprzedmiot + " Klasa: " + nklasa;
            klasa = nklasa;
            przedmiot = nprzedmiot;
        }
        string klasa, przedmiot;

        public int UstawienieGodzinTygodniowo(int tygodniowo)
        {
            tygodniowo = Convert.ToInt32(iloscTygodniowo.Text);
            return tygodniowo;
        }
        public int UstawienieGodzinDziennie(int maxDzien)
        {
            maxDzien = Convert.ToInt32(maxDziennie.Text);
            return maxDzien;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (iloscTygodniowo.Text != "" && maxDziennie.Text != "")
            {
                stworzPlan okno = new stworzPlan();
                okno.iloscTygodniowo = Convert.ToInt32(iloscTygodniowo.Text);
                okno.maxDziennie = Convert.ToInt32(maxDziennie.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola");
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tickh.png");
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\crossh.png");
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\przycisk-exit.png");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tick.png");
        }
    }
}
