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
    public partial class musteriler : Form
    {
        public musteriler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu git=new menu();
            git.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hoşçakalın");
            Environment.Exit(0);
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=pastane;Integrated Security=True");

        public void listele(string a)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter(a, baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
        }
        private void musteriler_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from Siparis",baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["SiparisNo"]);
            }
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listele("select*from Musteriler");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into Musteriler (MusteriAdSoyad,MusteriTelefon,SiparisNo) values(@m1,@m2,@m3)", baglanti);
            cmd.Parameters.AddWithValue("@m1",textBox2.Text);
            cmd.Parameters.AddWithValue("@m2", maskedTextBox1.Text);
            cmd.Parameters.AddWithValue("@m3",comboBox1.SelectedItem);
            cmd.ExecuteNonQuery();
            listele("select*from Musteriler");
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update  Musteriler set MusteriAdSoyad='" + textBox2.Text + "',MusteriTelefon='" + maskedTextBox1.Text + "',SiparisNo='" + comboBox1.SelectedItem +
                "' where MusteriNo='" + textBox1.Text.ToString() + "'", baglanti);
            cmd.ExecuteNonQuery();
            listele("select*from Musteriler");
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;

            
            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from Musteriler where MusteriNo=@m1", baglanti);
            cmd.Parameters.AddWithValue("@m1", textBox1.Text);
            listele("select*from Musteriler");
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Musteriler where MusteriAdSoyad like '%" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            raporlar git=new raporlar();
            git.Show();
            this.Hide();
        }
    }
}
