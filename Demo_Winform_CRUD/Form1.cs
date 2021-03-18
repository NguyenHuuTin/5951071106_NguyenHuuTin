using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Winform_CRUD
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-F5QFG7D\SQLEXPRESS;Initial Catalog=DemoCRUD;User ID=sa; password=0369");
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void GetStudentRecord()
        {
            
            SqlCommand cmd = new SqlCommand("select * from StudentTB", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvSV.DataSource = dt;
        }
        private bool IsvalidData()
        {
            if(txthosv.Text == String.Empty|| txtTensv.Text == String.Empty|| txtDiachi.Text == String.Empty|| String.IsNullOrEmpty(txtDienthoai.Text) || String.IsNullOrEmpty(txtSBD.Text))
            {
                MessageBox.Show("Có chỗ chưa nhaappj dữ liệu!!!", "Lỗi dữ liệu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsvalidData())
            {
                SqlCommand cmd = new SqlCommand("insert into StudentTB values (@Name, @FatherName, @RollNumber,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txthosv.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTensv.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSBD.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiachi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtDienthoai.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }
        public int StudentID;
        private void dgvSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idex = e.RowIndex;
            StudentID = Convert.ToInt32(dgvSV.Rows[idex].Cells[0].Value);
            txthosv.Text = dgvSV.Rows[idex].Cells[1].Value.ToString();
            txtTensv.Text = dgvSV.Rows[idex].Cells[2].Value.ToString();
            txtSBD.Text = dgvSV.Rows[idex].Cells[3].Value.ToString();
            txtDienthoai.Text = dgvSV.Rows[idex].Cells[4].Value.ToString();
            txtDiachi.Text = dgvSV.Rows[idex].Cells[5].Value.ToString();
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("update StudentTB set Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber, Address = @Address, Mobile = @Mobile where StudentID = @StudentID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txthosv.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTensv.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSBD.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiachi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtDienthoai.Text);
                cmd.Parameters.AddWithValue("@StudentID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
            else
            {
                MessageBox.Show("Cập nhật lỗi!!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
