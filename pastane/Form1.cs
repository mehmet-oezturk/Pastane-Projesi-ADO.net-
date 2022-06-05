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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=pastane;Integrated Security=True");
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "select*from KullaniciGiris where KullaniciAd='" + textBox1.Text + "' and KullaniciSifre='" + textBox2.Text + "'";
            baglanti.Open();
            SqlDataReader dr;
            dr=cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("tebrikler başarılı giriş yaptınız","başarılı",MessageBoxButtons.OK);
                menu git = new menu();
                git.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("hatalı kullanıcı adı veya şifre girdiniz\n tekrar deneyini yada kayıt ol butonuna basınız","hata",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                textBox1.Clear();
                textBox2.Clear();
            }
            baglanti.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into KullaniciGiris(KullaniciAd,KullaniciSifre,Email,Telefon)values(@k1,@k2,@k3,@k4)",baglanti);

            cmd.Parameters.AddWithValue("@k1",textBox4.Text);
            cmd.Parameters.AddWithValue("@k2", textBox3.Text);
            cmd.Parameters.AddWithValue("@k3", textBox5.Text);
            cmd.Parameters.AddWithValue("@k4", maskedTextBox1.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("kayıt işleminiz yapıldı");
            textBox4.Clear();
            textBox3.Clear();
            textBox5.Clear();
            maskedTextBox1.Clear();

            baglanti.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
