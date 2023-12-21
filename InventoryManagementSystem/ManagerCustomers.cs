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
    public partial class ManagerCustomers : Form
    {
        public ManagerCustomers()
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
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into CustomerTb(Custid, CustName, CustPhone)values('" + txtCustomerId.Text + "', '" + txtCustomerName.Text + "', '" + txtCustomerPhone.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("müşteri başarılı şekilde eklendi");
                baglanti.Close();
                gridAyar();
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        private void ManagerCustomers_Load(object sender, EventArgs e)
        {
            gridAyar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtCustomerId.Text == "")
            {
                MessageBox.Show("Enter the Customer Id.");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from CustomerTb where Custid = '" + txtCustomerId.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted");
                baglanti.Close();
                gridAyar();
            }

        }

        private void CustomersGV_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomersGV.SelectedRows.Count > 0)
            {
                txtCustomerId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
                txtCustomerName.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
                txtCustomerPhone.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();
                baglanti.Open();

                string Mquery = "select Count(*) from OrderTb where CustId = '"+txtCustomerId.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(Mquery,baglanti);
                DataTable table = new DataTable();
                sda.Fill(table);
                orderLabel.Text = table.Rows[0][0].ToString();

                string mquery = "select Sum(TotalAmt) from OrderTb where CustId = '" + txtCustomerId.Text + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter(mquery, baglanti);
                DataTable table2 = new DataTable();
                sda2.Fill(table2);
                AmountLabel.Text = table2.Rows[0][0].ToString();

                string mquery2 = "select Max(OrderDate) from OrderTb where CustId = '" + txtCustomerId.Text + "'";
                SqlDataAdapter sda3 = new SqlDataAdapter(mquery2, baglanti);
                DataTable table3 = new DataTable();
                sda3.Fill(table3);
                DateLabel.Text = table3.Rows[0][0].ToString();


                baglanti.Close();
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
                SqlCommand komut = new SqlCommand("Update CustomerTb set Custid = '" + txtCustomerId.Text + "',CustName ='" + txtCustomerName.Text + "',CustPhone ='" + txtCustomerPhone.Text + "' where Custid ='" + txtCustomerId.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("müşteri başarılı şekilde güncellendi");
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
