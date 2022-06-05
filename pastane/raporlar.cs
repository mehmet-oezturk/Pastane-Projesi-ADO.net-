using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace pastane
{
    public partial class raporlar : Form
    {
        public raporlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=pastane;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            menu git = new menu();
            git.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter("select SiparisNo,SiparisAdi,SiparisFiyat,UrunAdi from Siparis s inner join Urunler u on s.UrunNo=u.UrunNo where SiparisFiyat>50  ", baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
        SqlDataAdapter goruntule = new SqlDataAdapter(" select [SaticiAdSoyad],[Saticiİl],[Saticiİlce] from Satici  where [Saticiİl]='" + comboBox1.SelectedItem+ "'  order by [Saticiİlce]   ", baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
            }
            else
            {
                MessageBox.Show("Lütfen İl Seçiniz");
            }
    
        }

        private void raporlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select Saticiİl from Satici group by Saticiİl order by Saticiİl", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                

                comboBox1.Items.Add(dr["Saticiİl"]); 


            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand cm = new SqlCommand("select UrunAdi from Urunler group by UrunAdi order by UrunAdi", baglanti);
            SqlDataReader de = cm.ExecuteReader();
            while (de.Read())
            {


                comboBox2.Items.Add(de["UrunAdi"]);


            }
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            SqlDataAdapter goruntule = new SqlDataAdapter("select urunadi,skt,Datediff(day,getdate(),skt) as 'skt bitmesine kalan gün sayısı' from urunler where Datediff(day,getdate(),skt)<='" + textBox1.Text + "'", baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                SqlDataAdapter goruntule = new SqlDataAdapter("select urunadi,MusteriNo,MusteriAdSoyad,MusteriTelefon from " +
                "Siparis s inner join Musteriler m on s.SiparisNo=m.SiparisNo join Urunler u on s.UrunNo=u.UrunNo where UrunAdi='" + comboBox2.SelectedItem + "'", baglanti);
                DataTable doldur = new DataTable();
                goruntule.Fill(doldur);
                dataGridView1.DataSource = doldur;
            }
            else { MessageBox.Show("Lütfen ürün Seçiniz"); }
        }
    }
}
