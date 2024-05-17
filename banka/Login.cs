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

namespace banka
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gokal\OneDrive\Belgeler\BankDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UsernameTb.Text = "";
            PasswordTb.Text = "";
            RoleCb.SelectedIndex = -1;
            RoleCb.Text = "Lütfen Rol Seçiniz";
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Rol Seçiniz");
            }
            else if (RoleCb.SelectedIndex == 0)
            {
                if (UsernameTb.Text == "" || PasswordTb.Text == "")
                {
                    MessageBox.Show("Rolü, İsmi ve Parolayı giriniz");
                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AdminTb1 where AdName='" + UsernameTb.Text + "' and Adpass='" + PasswordTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Agents Obj = new Agents();
                        Obj.Show();
                        this.Hide();
                        Con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Yanlış Admin bilgisi girdiniz");
                        UsernameTb.Text = "";
                        PasswordTb.Text = "";
                    }
                    Con.Close();

                }
            }
            else
            {
                if (UsernameTb.Text == "" || PasswordTb.Text == "")
                {
                    MessageBox.Show("Rolü, İsmi ve Parolayı giriniz");
                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AgentTb1 where AName='" + UsernameTb.Text + "' and APass='" + PasswordTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MainMenu Obj = new MainMenu();
                        Obj.Show();
                        this.Hide();
                        Con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Yanlış Temsilci bilgisi girdiniz");
                        UsernameTb.Text = "";
                        PasswordTb.Text = "";
                    }
                    Con.Close();

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RoleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
