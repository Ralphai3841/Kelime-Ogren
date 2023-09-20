using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kelime_Ogren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source="C:\Users\ralpd\OneDrive\Masaüstü\c# - html - css - javascript - .NET\C# ile 25 Proje\Kelime Ogren\dbSozluk.accdb"

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C: \Users\ralpd\OneDrive\Masaüstü\c# - html - css - javascript - .NET\C# ile 25 Proje\Kelime Ogren\dbSozluk.accdb'");
        Random rast = new Random();
        int sure = 90;
        int kelime = 0;

        void getir()
        {
            int sayi;
            sayi = rast.Next(1, 2490);
            LblCevap.Text = sayi.ToString();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select ingilizce from sozluk where id=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Txtİngilizce.Text = dr[1].ToString();
                LblCevap.Text = dr[2].ToString();
                LblCevap.Text = LblCevap.Text.ToLower();
            }
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
            TxtTurkce.Focus();
            timer1.Start();
        }

        private void TxtTurkce_TextChanged(object sender, EventArgs e)
        {
            if (TxtTurkce.Text == LblCevap.Text)
            {
                kelime++;
                LblKelime.Text = kelime.ToString();
                getir();
                TxtTurkce.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            LblSure.Text = sure.ToString();
            if (sure == 0)
            {
                TxtTurkce.Enabled = false;
                Txtİngilizce.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
