using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace Login_and_Register
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        string connectionString =ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" ||
                txtPassword.Text == "" ||
                txtConPassword.Text == "")
            {
                MessageBox.Show(
                    "Username and Password fields are empty",
                    "Register Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (txtPassword.Text == txtConPassword.Text)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string registerQuery = @"INSERT INTO tbl_users
                                             (username, password)
                                             VALUES
                                             (@username, @password)";

                    using (SqlCommand cmd = new SqlCommand(registerQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                txtUsername.Clear();
                txtPassword.Clear();
                txtConPassword.Clear();
                txtUsername.Focus();

                MessageBox.Show(
                    "Your Account has been Successfully Created",
                    "Registration Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Password does not match, Please Re-enter",
                    "Register Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtConPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUsername.Focus();
        }

        private void clickLogin_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
