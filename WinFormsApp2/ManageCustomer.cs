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
    public partial class ManageCustomer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        int user_id;

        public ManageCustomer()
        {
            InitializeComponent();
            displayData();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Dashboard a = new Dashboard();
            a.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        //form border radius set....................................................................................
            IntPtr ptr = NativeMethods.CreateRoundRectRgn(10, 10, this.Width, this.Height, 20, 20); 
            this.Region = System.Drawing.Region.FromHrgn(ptr);
            NativeMethods.DeleteObject(ptr);
            style();

        }



        //form border radius set....................................................................................

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //form border radius set....................................................................................


        public class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern System.IntPtr CreateRoundRectRgn
             (
              int nLeftRect, // x-coordinate of upper-left corner
              int nTopRect, // y-coordinate of upper-left corner
              int nRightRect, // x-coordinate of lower-right corner
              int nBottomRect, // y-coordinate of lower-right corner
              int nWidthEllipse, // height of ellipse
              int nHeightEllipse // width of ellipse
             );

            [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
            public static extern bool DeleteObject(System.IntPtr hObject);
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
            public static extern bool ReleaseCapture();

            [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("update Customer set username='" + txtUsername.Text + "',password= '" + txtPassword.Text + "',usertype= '" + cmbUserType.SelectedItem.ToString() + "',firstname= '" + txtFname.Text + "',lastname= '" + txtLname.Text + "',address= '" + txtAddress.Text + "',gender= '" + cmbGender.SelectedItem.ToString() + "',email= '" + txtEmail.Text + "',phone= '" + txtPhone.Text + "'where User_id='"+user_id+"'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            displayData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("insert into Customer values('" + txtUsername.Text + "', '" + txtPassword.Text + "', '" + cmbUserType.SelectedItem.ToString() + "', '" + txtFname.Text + "', '" + txtLname.Text + "', '" + txtAddress.Text + "', '" + cmbGender.SelectedItem.ToString() + "', '" + txtEmail.Text + "', '" + txtPhone.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            displayData();
            Clear();
        }

        public void displayData()
        {
            con.Open();
            adpt = new SqlDataAdapter("select * from Customer", con);
            dt = new DataTable();
            adpt.Fill(dt);
            CustomerGrid.DataSource = dt;
            con.Close();
        }

        public void Clear()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbUserType.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";

        }

        private void CustomerGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            user_id = Convert.ToInt32(CustomerGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtUsername.Text = CustomerGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPassword.Text = CustomerGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbUserType.Text = CustomerGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFname.Text = CustomerGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtLname.Text = CustomerGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = CustomerGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
            cmbGender.Text = CustomerGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtEmail.Text = CustomerGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtPhone.Text = CustomerGrid.Rows[e.RowIndex].Cells[9].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("delete from Customer where User_id='" + user_id + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            displayData();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd = new SqlCommand ( "select * from Customer where firstname='" + txtSearch.Text + "'", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            CustomerGrid.DataSource = dt;
            con.Close();
            
        }

        //Add Styles for DataGrid view
        public void style()
        {
            CustomerGrid.BorderStyle = BorderStyle.None;
            CustomerGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            CustomerGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CustomerGrid.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            CustomerGrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            CustomerGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            CustomerGrid.EnableHeadersVisualStyles = false;
            CustomerGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;          
            CustomerGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(72, 61, 139);
            CustomerGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
