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
    public partial class ManageCar : Form
    {
        

        public ManageCar()
        {
            InitializeComponent();
            displayData();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
        SqlCommand cmd;

        private void label12_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        private void label10_Click_1(object sender, EventArgs e)
        {
            Dashboard b = new Dashboard();
            b.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            ManageCustomer c = new ManageCustomer();
            c.Show();
            this.Hide();
        }


        // Upload Picture Button......................................................
        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Select image (*.jPG; *.png; *.Gif) | *.jPG; *.png; *.Gif";
            if(openFileDialog1.ShowDialog() ==DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

            }
        }


        // Save Button/Insert data to the database
        private void btnSave_Click(object sender, EventArgs e)
        {
           
            cmd = new SqlCommand("insert into CarDetails (CarName,Status,Condition,Price,Category,TotalMiles,EngineCapacity,Year,Color,Model,Photo) VALUES (@CarName,@Status,@Condition,@Price,@Category,@TotalMiles,@EngineCapacity,@Year,@Color,@Model,@Photo)", conn);
            cmd.Parameters.AddWithValue("CarName", txtName.Text);
            cmd.Parameters.AddWithValue("Status", cmbStatus.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Condition", cmbCondition.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("Category", cmbCategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("TotalMiles", txtMiles.Text);
            cmd.Parameters.AddWithValue("EngineCapacity", cmbCapacity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Year", cmbYear.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Color", cmbColor.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Model", cmbModel.SelectedItem.ToString());
            MemoryStream mster = new MemoryStream();
            pictureBox1.Image.Save(mster, pictureBox1.Image.RawFormat);
            cmd.Parameters.AddWithValue("Photo", mster.ToArray());
            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("produt Saved");
            conn.Close();
            displayData();







        }


        public void displayData()
        {
            cmd = new SqlCommand("select * from CarDetails ", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.DataSource = dt;
            DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
            pic1 = (DataGridViewImageColumn)dataGridView1.Columns[11];
            pic1.ImageLayout = DataGridViewImageCellLayout.Zoom; 
        }



        // Update Data....................
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update CarDetails set CarName =@CarName,Status=@Status,Condition=@Condition,Price=@Price,Category=@Category,TotalMiles=@TotalMiles,EngineCapacity=@EngineCapacity,Year=@Year,Color=@Color,Model=@Model,Photo=@Photo Where Car_ID=@Car_ID", conn);

            cmd.Parameters.AddWithValue("Car_ID", carID.Text);
            cmd.Parameters.AddWithValue("CarName", txtName.Text);
            cmd.Parameters.AddWithValue("Status", cmbStatus.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Condition", cmbCondition.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("Category", cmbCategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("TotalMiles", txtMiles.Text);
            cmd.Parameters.AddWithValue("EngineCapacity", cmbCapacity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Year", cmbYear.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Color", cmbColor.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Model", cmbModel.SelectedItem.ToString());
           

            MemoryStream mster = new MemoryStream();
            pictureBox1.Image.Save(mster, pictureBox1.Image.RawFormat);
            cmd.Parameters.AddWithValue("Photo", mster.ToArray());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            displayData();

        }
            


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void ManageCar_Load(object sender, EventArgs e)
        {
        //form border radius set....................................................................................
            IntPtr ptr = NativeMethods.CreateRoundRectRgn(10, 10, this.Width, this.Height, 20, 20); 
            this.Region = System.Drawing.Region.FromHrgn(ptr);
            NativeMethods.DeleteObject(ptr);
            displayData();
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            carID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbStatus.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbCondition.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbCategory.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtMiles.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cmbCapacity.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            cmbYear.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            cmbColor.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            cmbModel.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            MemoryStream ms = new MemoryStream((byte[])dataGridView1.CurrentRow.Cells[11].Value);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Delete from CarDetails Where Car_ID=@Car_ID ", conn);
            cmd.Parameters.AddWithValue("Car_ID", carID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            displayData();
            pictureBox1.Image = null;
            txtName.Text = "";
            txtPrice.Text = "";
            cmbCapacity.Text = "";
            txtMiles.Text = "";
            cmbCategory.Text = "";
            cmbColor.Text = "";
            cmbCondition.Text = "";
            cmbModel.Text = "";
            cmbStatus.Text = "";
            cmbYear.Text = "";

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

    }




}
