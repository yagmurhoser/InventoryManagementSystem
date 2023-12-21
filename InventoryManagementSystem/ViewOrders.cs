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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=InventoryDB;Integrated Security=True");

        private void gridAyarOrders()
        {
            try
            {
                baglanti.Open();
                string MyQuery = "select * from OrderTb";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(da); // insert update delete komutları otomatikmen oluştu.
                DataSet ds = new DataSet();
                da.Fill(ds);
                OrderGv.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            gridAyarOrders();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

      

        private void OrderGv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Order Summary",new Font("Century",25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Order Id : " + OrderGv.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 20, FontStyle.Regular), Brushes.Black, new PointF(80, 100));
            e.Graphics.DrawString("Customer Id : " + OrderGv.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 20, FontStyle.Regular), Brushes.Black, new PointF(80, 150));
            e.Graphics.DrawString("Customer Name : " + OrderGv.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 20, FontStyle.Regular), Brushes.Black, new PointF(80, 200));
            e.Graphics.DrawString("Order Date : " + OrderGv.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 20, FontStyle.Regular), Brushes.Black, new PointF(80, 250));
            e.Graphics.DrawString("Total Amount : " + OrderGv.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 20, FontStyle.Regular), Brushes.Black, new PointF(80, 300));
            e.Graphics.DrawString("PoweredByYağmurHoşer", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new PointF(350,450));


        }

       
    }
}
