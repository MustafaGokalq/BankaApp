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
    public partial class Transferler : Form
    {
        public Transferler()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gokal\OneDrive\Belgeler\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        int Balance;
        private void CheckBalance()
        {
            Con.Open();
            string Query = "select * from AccountTb1 where ACNum=" + CheckBaıTb.Text + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                BalanceLbl.Text = dr["AcBal"].ToString() + " TL";
                Balance = Convert.ToInt32(dr["AcBal"].ToString());
            }
            Con.Close();
        }

        private void GetNewBalance(string account)
        {
            Con.Open();
            string Query = "select * from AccountTb1 where ACNum=@acNum";
            SqlCommand cmd = new SqlCommand(Query, Con);
            cmd.Parameters.AddWithValue("@acNum", CheckBaıTb.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                //BalanceLbl.Text = dr["AcBal"].ToString() + " TL";
                Balance = Convert.ToInt32(dr["AcBal"].ToString());
            }
            Con.Close();
        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CheckBaıTb.Text == "")
            {
                MessageBox.Show("Enter");
            }
            else
            {
                CheckBalance();
                if (BalanceLbl.Text == "Bakiye")
                {
                    MessageBox.Show("Hesap numarası bulunamadı.");
                    CheckBaıTb.Text = "";
                }
            }
        }

        private void Deposit()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into TransactionTb1(TName, TDate, TAmt, TACNum)values(@TN, @TD, @TA, @TAC)", Con);
                cmd.Parameters.AddWithValue("@TN", "Deposit");
                cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TA", DepAmtTb.Text);
                cmd.Parameters.AddWithValue("@TAC", DepAmtTb.Text);

                cmd.ExecuteNonQuery();

                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void WithDraw()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into TransactionTb1(TName, TDate, TAmt, TACNum)values(@TN, @TD, @TA, @TAC)", Con);
                cmd.Parameters.AddWithValue("@TN", "Withdrawn");
                cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TA", DepAmtTb.Text);
                cmd.Parameters.AddWithValue("@TAC", DepAmtTb.Text);

                cmd.ExecuteNonQuery();

                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void AddBal()
        {
            GetNewBalance(ToTb.Text);
            int newBal = Balance + Convert.ToInt32(TransAmtTb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update AccountTb1 set AcBal=@AB where ACNUm=@AcKey", Con);
                cmd.Parameters.AddWithValue("@AB", newBal);

                cmd.Parameters.AddWithValue("@AcKey", ToTb.Text);
                cmd.ExecuteNonQuery();
                Con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void SubstractBal()
        {
            GetNewBalance(FromTb.Text);
            int newBal = Balance - Convert.ToInt32(TransAmtTb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update AccountTb1 set AcBal=@AB where ACNUm=@AcKey", Con);
                cmd.Parameters.AddWithValue("@AB", newBal);

                cmd.Parameters.AddWithValue("@AcKey", FromTb.Text);
                cmd.ExecuteNonQuery();
                Con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void DepositBtn_Click(object sender, EventArgs e)
        {
            if (DepAccountTb.Text == "" || DepAmtTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                Deposit();
                GetNewBalance(DepAccountTb.Text);
                int newBal = Balance + Convert.ToInt32(DepAmtTb.Text);
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AccountTb1 set AcBal=@AB where ACNUm=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AB", newBal);

                    cmd.Parameters.AddWithValue("@AcKey", DepAccountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Para Yatırma İşlemi Başarılı");
                    Con.Close();
                    DepAmtTb.Text = "";
                    DepAccountTb.Text = "";

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void WdBtn_Click(object sender, EventArgs e)
        {
            if (WdAccountTb.Text == "" || WdAmtTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {

                GetNewBalance(WdAccountTb.Text);
                WithDraw();
                if (Balance < Convert.ToInt32(WdAmtTb.Text))
                {
                    MessageBox.Show("Yetersiz Bakiye");
                }
                else
                {
                    int newBal = Balance - Convert.ToInt32(WdAmtTb.Text);

                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Update AccountTb1 set AcBal=@AB where ACNUm=@AcKey", Con);
                        cmd.Parameters.AddWithValue("@AB", newBal);
                        cmd.Parameters.AddWithValue("@AcKey", WdAccountTb.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Para Çekme İşlemi Başarılı");
                        Con.Close();
                        WdAmtTb.Text = "";
                        WdAccountTb.Text = "";

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }


            }
        }

        private void Transfer()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into TransferTb1(TrSrc, TrDest, TrAmt, TrDate)values(@TS, @TD, @TA, @TDa)", Con);
                cmd.Parameters.AddWithValue("@TS", FromTb.Text);
                cmd.Parameters.AddWithValue("@TD", ToTb.Text);
                cmd.Parameters.AddWithValue("@TA", TransAmtTb.Text);
                cmd.Parameters.AddWithValue("@TDa", DateTime.Now.Date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transfer Gerçekleşti");
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void TransBtn_Click(object sender, EventArgs e)
        {
            if (ToTb.Text == "" || FromTb.Text == "" || TransAmtTb.Text == "")
            {
                MessageBox.Show("Bilgileri Doldurunuz.");
            }
            else if (Convert.ToInt16(TransAmtTb.Text) > Balance)
            {
                MessageBox.Show("Yetersiz Bakiye");
            }
            else
            {
                Transfer();
                SubstractBal();
                AddBal();
                FromTb.Text = "";
                ToTb.Text = "";
                TransAmtTb.Text = "";

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close(); // veya this.Hide(); kullanabilirsiniz.
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BalanceLbl.Text = "";
            CheckBaıTb.Text = "";
            WdAccountTb.Text = "";
            WdAmtTb.Text = "";
            DepAccountTb.Text = "";
            DepAmtTb.Text = "";
            FromTb.Text = "";
            ToTb.Text = "";
            TransAmtTb.Text = "";
        }
    }
}
