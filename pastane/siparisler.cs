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
    public partial class siparisler : Form
    {
        public siparisler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu git = new menu();
            git.Show();
            this.Hide();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=pastane;Integrated Security=True");
        private void siparisler_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from Urunler", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["UrunNo"]);
            }
            baglanti.Close();
        }
        public void listele(string a)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter(a, baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hoşçakalın");
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listele("select*from Siparis");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            double a, b,c;
            a= Convert.ToDouble(textBox4.Text);
            b= Convert.ToDouble(textBox5.Text);
            c = a * b;
            textBox6.Text = c.ToString();
            
            SqlCommand cmd = new SqlCommand("insert into Siparis (SiparisAdi,SiparisAdres,SiparisAdet,SiparisFiyat,UrunNo,Tutar) values(@1,@2,@3,@4,@5,@6)", baglanti);
            cmd.Parameters.AddWithValue("@1", textBox2.Text);
            cmd.Parameters.AddWithValue("@2", textBox3.Text);
            cmd.Parameters.AddWithValue("@3", a);
            cmd.Parameters.AddWithValue("@4", b);
            cmd.Parameters.AddWithValue("@5", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@6", c);
            
            cmd.ExecuteNonQuery();
            listele("select*from Siparis");

            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;


            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();

            comboBox1.Text = dataGridView1.Rows[sectim].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[sectim].Cells[6].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            double a, b, c;
            a = Convert.ToDouble(textBox4.Text);
            b = Convert.ToDouble(textBox5.Text);
            c = a * b;

            textBox6.Text= c.ToString();
            SqlCommand cmd = new SqlCommand("Update Siparis set SiparisAdi=@1,SiparisAdres=@2,SiparisAdet=@3,SiparisFiyat=@4,UrunNo=@5,Tutar=@6 where SiparisNo=@7", baglanti);
            cmd.Parameters.AddWithValue("@1", textBox2.Text);
            cmd.Parameters.AddWithValue("@2", textBox3.Text);
            cmd.Parameters.AddWithValue("@3", textBox4.Text);
            cmd.Parameters.AddWithValue("@4", textBox5.Text);
            cmd.Parameters.AddWithValue("@5", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@6", c);
            cmd.Parameters.AddWithValue("@7", textBox1.Text);

            
            cmd.ExecuteNonQuery();
            listele("select*from Siparis");
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from Musteriler where SiparisNo=@m1", baglanti);
            cmd.Parameters.AddWithValue("@m1", textBox1.Text);
            listele("select*from Siparis");
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Siparis where SiparisAdi like '%" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            raporlar git = new raporlar();
            git.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
