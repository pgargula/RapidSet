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
    public partial class utworzenieBazy : Form
    {
        public utworzenieBazy()
        {
            InitializeComponent();
        }
        public string Adres(string sciezka)
        {
            sciezka = textBox1.Text;
            return sciezka;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                stworzPlan okno = new stworzPlan();
                okno.nazwaBazy = textBox1.Text;
                Close();
            }
            else
                MessageBox.Show("Podaj nazwę pliku");
        }
    }
}
