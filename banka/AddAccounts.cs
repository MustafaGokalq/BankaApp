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

namespace banka
{
    public partial class AddAccounts : Form
    {
        public AddAccounts()
        {
            InitializeComponent();
            DisplayAccounts();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gokal\OneDrive\Belgeler\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAccounts()
        {
            Con.Open();
            string Query = "select * from AccountTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Buidler = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AccountDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void AddAccounts_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            AcNameTb.Text = "";
            AcPhoneTb.Text = "";
            AcAddressTb.Text = "";
            GenCb.SelectedIndex = -1;
            OccupationTb.Text = "";
            IncomeTb.Text = "";
            EdicationCb.SelectedIndex = -1;
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (AcNameTb.Text == "" || AcPhoneTb.Text == "" || AcAddressTb.Text == "" || GenCb.SelectedIndex == -1 || EdicationCb.SelectedIndex == -1 || IncomeTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AccountTb1(ACName,Acphone,AcAddress,AcGen,AcOccup,AcEduc,AcInc,AcBal)values(@AN,@AP,@AA,@AG,@AO,@AE,@AI,@AB)", Con);
                    cmd.Parameters.AddWithValue("@AN", AcNameTb.Text);
                    cmd.Parameters.AddWithValue("@AP", AcPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                    cmd.Parameters.AddWithValue("@AE", EdicationCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", IncomeTb.Text);
                    cmd.Parameters.AddWithValue("@AB", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hesap Oluşturuldu");
                    Con.Close();
                    Reset();
                    DisplayAccounts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void AcAddressTb_TextChanged(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("delete from AccountTb1 where ACNUM=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AcKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hesap Silindi!!");
                    Con.Close();
                    Reset();
                    DisplayAccounts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void AccountDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AcNameTb.Text = AccountDGV.SelectedRows[0].Cells[1].Value.ToString();
            AcPhoneTb.Text = AccountDGV.SelectedRows[0].Cells[2].Value.ToString();
            AcAddressTb.Text = AccountDGV.SelectedRows[0].Cells[3].Value.ToString();
            GenCb.Text = AccountDGV.SelectedRows[0].Cells[4].Value.ToString();
            OccupationTb.Text = AccountDGV.SelectedRows[0].Cells[5].Value.ToString();
            EdicationCb.SelectedItem = AccountDGV.SelectedRows[0].Cells[6].Value.ToString();
            IncomeTb.Text = AccountDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (AcNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AccountDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (AcNameTb.Text == "" || AcPhoneTb.Text == "" || AcAddressTb.Text == "" || GenCb.SelectedIndex == -1 || EdicationCb.SelectedIndex == -1 || IncomeTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AccountTb1 set ACName=@AN,ACphone=@AP,AcAddress=@AA,AcGen=@AG,AcOccup=@AO,AcEduc=@AE,AcInc=@AI where ACNUm=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AN", AcNameTb.Text);
                    cmd.Parameters.AddWithValue("@AP", AcPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                    cmd.Parameters.AddWithValue("@AE", EdicationCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", IncomeTb.Text);
                    cmd.Parameters.AddWithValue("@AcKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hesap Güncellendi");
                    Con.Close();
                    Reset();
                    DisplayAccounts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close(); // veya this.Hide(); kullanabilirsiniz.
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}
