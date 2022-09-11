using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace WinFormsApp2
{
    public partial class Orders : Form
    {
        public static Orders instance;
        public TextBox tb1;




        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
        SqlCommand cmd;
        Class1 sdm = new Class1();
        public Orders()
        {
            InitializeComponent();
            instance = this;
            tb1 = textBox1;
            
        }

        private void Order_Load(object sender, EventArgs e)
        {
            con.Open();
            string user = Class1.uname;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Customer where username='" + user + "'";
            cmd.Parameters.AddWithValue("username", txt_UserID.Text);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            
           txt_UserID.Text = dt.Rows[0][0].ToString();
           txt_Username.Text = dt.Rows[0][4].ToString();
           txtPhone.Text = dt.Rows[0][9].ToString();
           txtAddress.Text = dt.Rows[0][6].ToString();

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[OrderDetails]
           ([CarName]
           ,[Price]
           ,[Color]
           ,[Year]
           ,[Customer_ID]
           ,[CustomerName]
           ,[Phone]
           ,[Address])
     VALUES
           ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + txt_UserID.Text + "', '" + txt_Username.Text + "', '" + txtPhone.Text + "', '" + txtAddress.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
            OrderStatus os = new OrderStatus();
            os.Show();
            OrderStatus.instance.labl.Text = textBox1.Text;
            OrderStatus.instance.labe2.Text = textBox2.Text;
            OrderStatus.instance.labe3.Text = textBox3.Text;
            OrderStatus.instance.labe4.Text = textBox4.Text;
            OrderStatus.instance.labe5.Text = txt_UserID.Text;
            OrderStatus.instance.labe6.Text = txt_Username.Text;
            OrderStatus.instance.labe7.Text = txtPhone.Text;
            OrderStatus.instance.labe8.Text = txtAddress.Text;



        }

      
    }



    

}
