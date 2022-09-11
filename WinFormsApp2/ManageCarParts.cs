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
    public partial class ManageCarParts : Form
    {
        public ManageCarParts()
        {
            InitializeComponent();
            displayData();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IU69SHJ;Initial Catalog=abcCarCompany;Integrated Security=True");
        SqlCommand cmd;

        private void label10_Click(object sender, EventArgs e)
        {
            Dashboard b = new Dashboard();
            b.Show();
            this.Hide();
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
          
        }


        // Save Button/Insert data to the database
        private void btnSave_Click(object sender, EventArgs e)
        {




        }


        public void displayData()
        {
            cmd = new SqlCommand("select * from CarParts ", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.DataSource = dt;
            DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
            pic1 = (DataGridViewImageColumn)dataGridView1.Columns[7];
            pic1.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void ManageCarParts_Load(object sender, EventArgs e)
        {
            displayData();
            
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            cmd = new SqlCommand("insert into CarParts (PartName,Status,Condition,Price,Category,Warrenty,Photo) VALUES (@PartName,@Status,@Condition,@Price,@Category,@Warrenty,@Photo)", conn);
            cmd.Parameters.AddWithValue("PartName", txtName.Text);
            cmd.Parameters.AddWithValue("Status", cmbStatus.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Condition", cmbCondition.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("Category", cmbCategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Warrenty", dateTimePicker1.Text);


            MemoryStream mster = new MemoryStream();
            pictureBox1.Image.Save(mster, pictureBox1.Image.RawFormat);
            cmd.Parameters.AddWithValue("Photo", mster.ToArray());
            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("produt Saved");
            conn.Close();
            displayData();
        }

        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Select image (*.jPG; *.png; *.Gif) | *.jPG; *.png; *.Gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

            }
        }






        // Update Data....................
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update CarParts set PartName=@PartName,Status=@Status,Condition=@Condition,Price=@Price,Category=@Category,Warrenty=@Warrenty,Photo=@Photo Where Part_ID=@Part_ID", conn);

            cmd.Parameters.AddWithValue("Part_ID", Part_ID.Text);
            cmd.Parameters.AddWithValue("PartName", txtName.Text);
            cmd.Parameters.AddWithValue("Status", cmbStatus.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Condition", cmbCondition.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("Category", cmbCategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("Warrenty", dateTimePicker1.Text);
            

            MemoryStream mster = new MemoryStream();
            pictureBox1.Image.Save(mster, pictureBox1.Image.RawFormat);
            cmd.Parameters.AddWithValue("Photo", mster.ToArray());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            displayData();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Part_ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbStatus.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbCondition.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbCategory.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            
            MemoryStream ms = new MemoryStream((byte[])dataGridView1.CurrentRow.Cells[7].Value);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Delete from CarParts Where Part_ID=@Part_ID ", conn);
            cmd.Parameters.AddWithValue("Part_ID", Part_ID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            displayData();
            pictureBox1.Image = null;
            txtName.Text = "";
            txtPrice.Text = "";
            dateTimePicker1.Text = "";
            cmbCategory.Text = "";
            cmbCondition.Text = "";
            cmbStatus.Text = "";
            
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
