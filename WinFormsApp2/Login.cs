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

namespace WinFormsApp2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from Customer where username='" + txtUser.Text + "' and password='" + txtPass.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string cmbItemValue = comboBox1.SelectedItem.ToString();
            if(dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["usertype"].ToString()==cmbItemValue)
                    {
                   
                        if(comboBox1.SelectedIndex == 0)
                        {
                            
                            Dashboard f = new Dashboard();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            Class1.uname = txtUser.Text;

                            Form3 ff = new Form3();
                            Orders myForm = new Orders();
                            
                            ff.Show();
                            this.Hide();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("error");
            }
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Register c = new Register();
            c.Show();
            this.Hide();
        }
    }
}
