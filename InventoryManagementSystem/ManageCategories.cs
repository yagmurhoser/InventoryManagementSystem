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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=InventoryDB;Integrated Security=True");


        private void gridAyar()
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from CategoryTb";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(da); // insert update delete komutları otomatikmen oluştu.
                DataSet ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }




        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into CategoryTb(Catid, CatName)values('" + txtCategoriesId.Text + "', '" + txtCategoriesName.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Added");
                baglanti.Close();
                gridAyar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update CategoryTb set Catid = '" + txtCategoriesId.Text + "',CatName ='" + txtCategoriesName.Text + "' where Catid = '"+txtCategoriesId.Text+"'" , baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Updated");
                baglanti.Close();
                gridAyar();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            gridAyar();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtCategoriesId.Text == "")
            {
                MessageBox.Show("Enter the Category Id.");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from CategoryTb where Catid = '" + txtCategoriesId.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Deleted");
                baglanti.Close();
                gridAyar();
            }
        }

        private void CategoriesGV_SelectionChanged(object sender, EventArgs e)
        {
            if (CategoriesGV.SelectedRows.Count > 0)
            {
                txtCategoriesId.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
                txtCategoriesName.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
            }
            else
            {
                Console.WriteLine("Hata: Satır seçilmedi!");
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
