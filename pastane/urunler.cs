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
    public partial class urunler : Form
    {
        public urunler()
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
            MessageBox.Show("hoşçakalın");
            Environment.Exit(0);
        }
        public void listele(string a)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter(a, baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listele("select*from Urunler");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;


            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
           dateTimePicker2.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
            comboBox1.Text=dataGridView1.Rows[sectim].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {   baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into Urunler(UrunAdi,UrunFiyat,SKT,UretimTarihi,SaticiNo) values(@q1,@q2,@q3,@q4,@q5)", baglanti);
            cmd.Parameters.AddWithValue("@q1",textBox2.Text);
            cmd.Parameters.AddWithValue("@q2", textBox3.Text);
            cmd.Parameters.AddWithValue("@q3", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@q4", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@q5", comboBox1.SelectedItem);
            

            cmd.ExecuteNonQuery();
            listele("select*from Urunler");
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update Urunler set UrunAdi=@q1,UrunFiyat=@q2,SKT=@q3,UretimTarihi=@q4,SaticiNo=@q5 where UrunNo=@q6 ", baglanti);
            cmd.Parameters.AddWithValue("@q1", textBox2.Text);
            cmd.Parameters.AddWithValue("@q2", textBox3.Text);
            cmd.Parameters.AddWithValue("@q3", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@q4", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@q5", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@q6",textBox1.Text.ToString());

           
            cmd.ExecuteNonQuery();
            listele("select*from Urunler");
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from Urunler where UrunNo=@q1", baglanti);
            cmd.Parameters.AddWithValue("@q1", textBox1.Text);
            
            cmd.ExecuteNonQuery();
            listele("select*from Urunler");
            baglanti.Close();
        }

        private void urunler_Load(object sender, EventArgs e)
        {

            
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from Urunler", baglanti);// combo box içini otomatik doldurma
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["SaticiNo"]);
            }
            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Urunler where UrunAdi like '%" + textBox2.Text + "%'", baglanti);
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
    }
}
