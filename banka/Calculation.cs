using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace banka
{
    public partial class Calculation : Form
    {
        public Calculation()
        {
            InitializeComponent();
        }

        private void Calculation_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal miktar;
            decimal faiz;
            decimal zaman;
            decimal hesap = 0;

            miktar = decimal.Parse(textBox1.Text);
            faiz = decimal.Parse(textBox2.Text);
            zaman = decimal.Parse(textBox3.Text);

            hesap = (miktar * faiz * zaman) / 100;
            label6.Text = hesap.ToString();
            label7.Text = (miktar + hesap).ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label6.Text = "";
            label7.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close(); // veya this.Hide(); kullanabilirsiniz.
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
