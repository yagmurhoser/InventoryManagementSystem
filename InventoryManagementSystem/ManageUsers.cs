using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=InventoryDB;Integrated Security=True");


        private void gridAyar()
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from UserTb1";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(da); // insert update delete komutları otomatikmen oluştu.
                DataSet ds = new DataSet();
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            gridAyar();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into UserTb1(Uname, Ufullname, Upassword, Uphone)values('" + txtName.Text + "', '" + txtFullName.Text + "', '" + txtSifre.Text + "', '" + txtTelephone.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("User Successfully Added.");
                baglanti.Close();
                gridAyar();
            }
            catch (Exception)
            {

                throw;
            }



        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (txtTelephone.Text == "")
            {
                MessageBox.Show("Enter the Users Phone Number.");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from UserTb1 where Uphone = '" + txtTelephone.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                baglanti.Close();
                gridAyar();
            }



        }



        private void UsersGV_SelectionChanged(object sender, EventArgs e)
        {
            if (UsersGV.SelectedRows.Count > 0)
            {
                txtName.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
                txtFullName.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
                txtSifre.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
                txtTelephone.Text = UsersGV.SelectedRows[0].Cells[3].Value.ToString();
            }
            else
            {
                Console.WriteLine("Hata: Satır seçilmedi!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update UserTb1 set Uname = '" + txtName.Text + "',Ufullname ='" + txtFullName.Text + "',Upassword ='" + txtSifre.Text + "' where Uphone ='" + txtTelephone.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("kullanıcı başarılı şekilde güncellendi");
                baglanti.Close();
                gridAyar();

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
