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
    public partial class Agents : Form
    {
        public Agents()
        {
            InitializeComponent();
            DisplayAgents();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gokal\OneDrive\Belgeler\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAgents()
        {
            Con.Open();
            string Query = "select * from AgentTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Buidler = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AgentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            ANameTb.Text = "";
            APhoneTb.Text = "";
            AAddressTb.Text = "";
            APasswordTb.Text = "";

        }
        int Key = 0;
        private void AccountDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ANameTb.Text = AgentDGV.SelectedRows[0].Cells[1].Value.ToString();
            APasswordTb.Text = AgentDGV.SelectedRows[0].Cells[2].Value.ToString();
            AAddressTb.Text = AgentDGV.SelectedRows[0].Cells[3].Value.ToString();
            APhoneTb.Text = AgentDGV.SelectedRows[0].Cells[4].Value.ToString();


            if (ANameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AgentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || APhoneTb.Text == "" || AAddressTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AgentTb1(AName,APass,APhone,Aaddress)values(@AN,@APA,@APh,@AA)", Con);
                    cmd.Parameters.AddWithValue("@AN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@APA", APasswordTb.Text);
                    cmd.Parameters.AddWithValue("@APh", APhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AAddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Temsilci Oluşturuldu");
                    Con.Close();
                    Reset();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Hesap Seçin");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from AgentTb1 where AId=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Temsilci Silindi!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || APhoneTb.Text == "" || AAddressTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AgentTb1 set AName=@AN,APass=@AP, APhone=@APh, Aaddress=@AA where AId=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@AP", APasswordTb.Text);
                    cmd.Parameters.AddWithValue("@APh", APhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AAddressTb.Text);

                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Temsilci Güncellendi");
                    Con.Close();
                    Reset();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Agents_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Settings Obj = new Settings();
            Obj.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close(); // veya this.Hide(); kullanabilirsiniz.
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}
