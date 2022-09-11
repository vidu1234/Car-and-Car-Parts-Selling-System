using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WinFormsApp2
{
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
        SqlCommand cmd;
       
        public Form3()
        {
            InitializeComponent();
            displayData();
        }






        private void Form3_Load(object sender, EventArgs e)
        {
            label5.Text = Class1.uname;
            combo1();
            display_datagrid();
            
            //form border radius set............................................................................
            IntPtr ptr = NativeMethods.CreateRoundRectRgn(10, 10, this.Width, this.Height, 20, 20); // 
            this.Region = System.Drawing.Region.FromHrgn(ptr);
            NativeMethods.DeleteObject(ptr);


            style();
        }

        //form border radius set.................................................................................
        

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

        private void label1_Click(object sender, EventArgs e)
        {

        }




        public void displayData()
        {
            cmd = new SqlCommand("select * from CarDetails ", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.DataSource = dt;
            DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
            pic1 = (DataGridViewImageColumn)dataGridView1.Columns[11];
            pic1.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }


        //Add Styles for DataGrid view
        public void style()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(72, 61, 139);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }


        // add filter 1 to search prrducts....................................
        private void combo1()
        {
            string query1 = "select distinct Model from CarDetails";
            SqlDataAdapter da = new SqlDataAdapter(query1, con);
            con.Open();
            DataSet dt = new DataSet();
            da.Fill(dt, "Model");
            comboBox1.DisplayMember = "Model";
            comboBox1.ValueMember = "Model";
            comboBox1.DataSource = dt.Tables["Model"];
            con.Close();
            if (comboBox1.Items.Count>1)
            {
                comboBox1.SelectedIndex = -1;
            }


            
        }

        private void display_datagrid()
        {
            SqlCommand quer2 = new SqlCommand("select CarName,Status,Condition,Price,Category,TotalMiles,EngineCapacity,Year,Color,Model,Photo from CarDetails where Model like '%" + comboBox1.Text + "'", con);
            SqlDataAdapter da2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            da2.SelectCommand = quer2;
            dt2.Clear();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_datagrid();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Hide();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Orders myForm = new Orders();

            myForm.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            myForm.textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            myForm.textBox3.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            myForm.textBox4.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();


            myForm.ShowDialog();
        }


    }
}
