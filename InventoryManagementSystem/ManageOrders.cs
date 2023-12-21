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
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=InventoryDB;Integrated Security=True");

        private void gridAyar()
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from CustomerTb";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(da); // insert update delete komutları otomatikmen oluştu.
                DataSet ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }




        private void gridAyar2()
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





        void fillCategory()
        {
            SqlCommand komut = new SqlCommand("select * from CategoryTb", baglanti);
            SqlDataReader rd;

            try
            {
                baglanti.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rd = komut.ExecuteReader();
                dt.Load(rd);
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                baglanti.Close();
                //catcombo.Text = "Product Category";


            }
            catch (Exception)
            {

                throw;
            }
        }

        void updateproduct()
        {
            int id = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[0].Value.ToString());
            int newQty = stock - Convert.ToInt32(txtQty.Text);
            if(newQty < 0)
            {
                MessageBox.Show("Oparation Failed");
            }
            else
            {
                baglanti.Open();
                string query = "Update ProductTb set ProdQty = " + newQty + " where ProdId = " + id + ";";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                gridAyar2();
            }

        }


        private void ManageOrders_Load(object sender, EventArgs e)
        {
            gridAyar();
            gridAyar2();
            fillCategory();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    product = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            //    //qty = Convert.ToInt32(txtQty.Text);
            //    uprice = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
            //    //totprice = qty * uprice;
            //    flag = 1;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }


        int flag = 0;
        int stock;

        int num = 0;
        int uprice, totprice, qty;
        string product;
        int sum = 0;

        private void ProductsGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                product = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
                //qty = Convert.ToInt32(txtQty.Text);
                stock = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[2].Value.ToString());
                uprice = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
                //totprice = qty * uprice;
                flag = 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Tabloyu oluşturma
            DataTable table = new DataTable();

            // Sütunları tanımlama
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UnitPrice", typeof(decimal));
            table.Columns.Add("TotalPrice", typeof(decimal));


            if (txtQty.Text == "")
            {
                MessageBox.Show("Enter the Quantity of Products");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the Product");
            }
            else if (Convert.ToInt32(txtQty.Text) > stock)
            {
                MessageBox.Show("No Enough Stock Available");
            }
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(txtQty.Text);
                totprice = qty * uprice;
                table.Rows.Add(num, product, qty, uprice, totprice);
                OrderGv.DataSource = table;
                flag = 0;

            }
            sum = sum + totprice;
            TotAmount.Text = sum.ToString();
            updateproduct();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtOrderid.Text == "" || txtCustomerId.Text == "" || txtCustomerName.Text == "" || TotAmount.Text == "")
            {
                MessageBox.Show("Fill the Data Correctly");
            }
            else
            {

                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into OrderTb(OrderId, CustId, CustName, OrderDate, TotalAmt)values('" + txtOrderid.Text + "', '" + txtCustomerId.Text + "', '" + txtCustomerName.Text + "', '" + OrderDate.Value.ToString("yyyy-MM-dd")+ "','" + TotAmount.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully ");
                    baglanti.Close();
                    //gridAyar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);

                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void OrderGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OrderGv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOrderid.Text = OrderGv.SelectedRows[0].Cells[0].Value.ToString();

        }

        private void CustomersGV_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomersGV.SelectedRows.Count > 0)
            {
                txtCustomerId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
                txtCustomerName.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();

            }
            else
            {
                Console.WriteLine("Hata: Satır seçilmedi!");
            }

        }



        private void SearchCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from ProductTb where ProdCat = '" + SearchCombo.SelectedValue.ToString() + "'";
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
    }
}
