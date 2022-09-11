using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsApp2
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Customer]
           ([username]
           ,[password]
           ,[usertype]
           ,[firstname]
           ,[lastname]
           ,[address]
           ,[gender]
           ,[email]
           ,[phone])
     VALUES
           ('" + txtUsername.Text + "', '" + txtPassword.Text + "', '" + cmbUserType.SelectedItem.ToString() + "', '" + txtFname.Text + "', '" + txtLname.Text + "', '" + txtAddress.Text + "', '" + cmbGender.SelectedItem.ToString() + "', '" + txtEmail.Text + "', '" + txtPhone.Text + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Register Successfully");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Login d = new Login();
            d.Show();
            this.Hide();
        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}
