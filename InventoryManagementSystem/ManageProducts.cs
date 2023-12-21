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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=InventoryDB;Integrated Security=True");




        private void gridAyar()
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from ProductTb";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(da); // insert update delete komutları otomatikmen oluştu.
                DataSet ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }


        private void filterbyCategory()
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from ProductTb where ProdCat = '"+SearchCombo.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(da); // insert update delete komutları otomatikmen oluştu.
                DataSet ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }








        void fillCategory()
        {
            SqlCommand komut = new SqlCommand("select * from CategoryTb",baglanti);
            SqlDataReader rd;
            
            try
            {
                baglanti.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rd = komut.ExecuteReader();
                dt.Load(rd);
                catcombo.ValueMember = "CatName";
                catcombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                baglanti.Close();
                catcombo.Text = "Product Category";
                SearchCombo.Text = "Select Category";


            }
            catch (Exception)
            {

                throw;
            }
        }




       


        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillCategory();
            gridAyar();

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
                SqlCommand komut = new SqlCommand("insert into ProductTb(ProdId, ProdName, ProdQty, ProdPrice, ProdDesc, ProdCat)values('" + txtProductid.Text + "', '" + txtProductName.Text + "', '" + txtProductQty.Text + "','"+TxtProductPrice.Text+"', '"+txtDesc.Text+"','"+catcombo.SelectedValue.ToString()+"')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Product successfully Added");
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
                SqlCommand komut = new SqlCommand("Update ProductTb set ProdId = '" + txtProductid.Text + "',ProdName ='" + txtProductName.Text + "',ProdQty ='" + txtProductQty.Text + "',ProdPrice ='"+TxtProductPrice.Text+"', ProdDesc = '"+txtDesc.Text+"',ProdCat = '"+catcombo.SelectedValue.ToString()+"' where ProdId ='" + txtProductid.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Product successfully Updated");
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
            if (txtProductid.Text == "")
            {
                MessageBox.Show("Enter the Product Id.");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from ProductTb where ProdId = '" + txtProductid.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
                baglanti.Close();
                gridAyar();
            }

        }

        private void ProductsGV_SelectionChanged(object sender, EventArgs e)
        {

            if (ProductsGV.SelectedRows.Count > 0)
            {
                txtProductid.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
                txtProductName.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
                txtProductQty.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
                TxtProductPrice.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
                txtDesc.Text = ProductsGV.SelectedRows[0].Cells[4].Value.ToString();
                catcombo.Text = ProductsGV.SelectedRows[0].Cells[5].Value.ToString();
            }
            else
            {
                Console.WriteLine("Hata: Satır seçilmedi!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterbyCategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gridAyar();
            SearchCombo.Text = "Select Category";
            catcombo.Text = "Product Category";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
