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
using System.IO;


namespace WinFormsApp2
{
    public partial class ManageOrder : Form
    {
        public ManageOrder()
        {
            InitializeComponent();
            displayData();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
        SqlCommand cmd;

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into OrderDetails (CarName,Price,Year,Color,Customer_ID,CustomerName,Phone,Address) VALUES (@CarName,@Price,@Year,@Color,@Customer_ID,@CustomerName,@Phone,@Address)", conn);
            cmd.Parameters.AddWithValue("CarName", txtCarName.Text);
            cmd.Parameters.AddWithValue("Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("Year", cmbYear.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Color", cmbColor.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Customer_ID",txtCustomerID.Text );
            cmd.Parameters.AddWithValue("CustomerName", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("Phone", TxtPhone.Text);
            cmd.Parameters.AddWithValue("Address", txtAddress.Text);

            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("produt Saved");
            conn.Close();
            displayData();
        }



        public void displayData()
        {
            cmd = new SqlCommand("select * from OrderDetails ", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.DataSource = dt;
            

        }





        private void ManageOrder_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update OrderDetails set CarName =@CarName,Price=@Price,Color=@Color,Year=@Year,Customer_ID=@Customer_ID,CustomerName=@CustomerName,Phone=@Phone,Address=@Address Where Order_ID=@Order_ID", conn);
           
            cmd.Parameters.AddWithValue("Order_ID", Order_ID.Text);
            cmd.Parameters.AddWithValue("CarName", txtCarName.Text);
            cmd.Parameters.AddWithValue("Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("Color", cmbColor.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Year", cmbYear.SelectedItem.ToString());      
            cmd.Parameters.AddWithValue("Customer_ID", txtCustomerID.Text);
            cmd.Parameters.AddWithValue("CustomerName", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("Phone", TxtPhone.Text);
            cmd.Parameters.AddWithValue("Address", txtAddress.Text);


         
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            displayData();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Order_ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtCarName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbColor.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cmbYear.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtCustomerID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtCustomerName.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            TxtPhone.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Delete from OrderDetails Where Order_ID=@Order_ID ", conn);
            cmd.Parameters.AddWithValue("Order_ID", Order_ID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            displayData();
            txtCarName.Text = "";
            txtPrice.Text = "";
            txtPrice.Text = "";
            cmbYear.Text = "";
            cmbColor.Text = "";
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            TxtPhone.Text = "";
            txtAddress.Text = "";

        }
    }
}
