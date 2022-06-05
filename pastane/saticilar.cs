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
    public partial class saticilar : Form
    {
        public saticilar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=pastane;Integrated Security=True");
        public void listele(string a)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter(a, baglanti);
            DataTable doldur = new DataTable();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            menu git = new menu();
            git.Show();
            this.Hide();
        }

        private void saticilar_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listele("select*from Satici");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            raporlar git = new raporlar();
            git.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Satici where SiparisAdi like '%" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from Satici where SaticiNo=@m1", baglanti);
            cmd.Parameters.AddWithValue("@m1", textBox1.Text);
      
            cmd.ExecuteNonQuery();
            listele("select*from Satici");
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update Satici set SaticiAdSoyad=@1,SaticiAdres=@2,Saticiİl=@3,Saticiİlce=@4 where SaticiNo=@5", baglanti);
            cmd.Parameters.AddWithValue("@1", textBox2.Text);
            cmd.Parameters.AddWithValue("@2", textBox3.Text);
            cmd.Parameters.AddWithValue("@3", textBox4.Text);
            cmd.Parameters.AddWithValue("@4", textBox5.Text);
            cmd.Parameters.AddWithValue("@5", textBox1.Text);
         
            cmd.ExecuteNonQuery();
            listele("select*from Satici");
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into Satici (SaticiAdSoyad,SaticiAdres,Saticiİl,Saticiİlce) values(@1,@2,@3,@4)", baglanti);
            cmd.Parameters.AddWithValue("@1", textBox2.Text);
            cmd.Parameters.AddWithValue("@2", textBox3.Text);
            cmd.Parameters.AddWithValue("@3", textBox4.Text);
            cmd.Parameters.AddWithValue("@4", textBox5.Text);
           
            cmd.ExecuteNonQuery();
            listele("select*from Satici");
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;


            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
        }
    }
}
