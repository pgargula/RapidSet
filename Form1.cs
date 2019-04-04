using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace rapidset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int source = 0;
        private void stworzPlanButton_MouseHover(object sender, EventArgs e)
        {
            stworzPlanButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\przycisk-stworz-podswietlony.png");
            tekstHover.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\stworz-nowy-plan.png");
            tekstHover.Location = new Point(364, 461);
            tekstHover.Size = new Size(284, 36);
        }

        private void stworzPlanButton_MouseLeave(object sender, EventArgs e)
        {
            stworzPlanButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\przycisk-stworz.png");
            tekstHover.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tekst.png");
        }

        private void wczytajPlanButton_MouseHover(object sender, EventArgs e)
        {
            wczytajPlanButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\lupa-podswietlony.png");
            tekstHover.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\wczytaj-plan.png");
            tekstHover.Location = new Point(378, 461);
            tekstHover.Size = new Size(275, 36);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Autorzy: Krzysztof Rybicki, Kacper Zaręba, Piotr Gargula");
        }

        public void wczytajPlanButton_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\data\\lorg.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write("2");
            sw.Close();

            wczytaniePlanuForm okno = new wczytaniePlanuForm();
            okno.ShowDialog();
        }

        private void wczytajPlanButton_MouseLeave(object sender, EventArgs e)
        {
            wczytajPlanButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\lupa.png");
            tekstHover.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tekst.png");
        }

        private void edycjaBazButton_MouseHover(object sender, EventArgs e)
        {
            edycjaBazButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\przycisk-edytuj-podswietlony.png");
            tekstHover.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\edytuj-baze-danych.png");
            tekstHover.Location = new Point(370, 461);
            tekstHover.Size = new Size(299, 36);
        }

        private void edycjaBazButton_MouseLeave(object sender, EventArgs e)
        {
            edycjaBazButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\przycisk-edytuj.png");
            tekstHover.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\tekst.png");
        }

        private void stworzPlanButton_Click(object sender, EventArgs e)
        {
            stworzPlan okno = new stworzPlan();
            okno.ShowDialog();
        }

    }
}
