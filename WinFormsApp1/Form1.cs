using MySqlConnector;
using MySql.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Org.BouncyCastle.Asn1.X500;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string connection = File.ReadAllText("\\codes\\project\\Language\\WinFormsApp1\\connectinString.txt");
        int nums = 0;
        int num = 0;
        int times = 0;
        int points = 0;
        int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (times == 0)
            {
                Random rnd = new Random();
                num = rnd.Next(count);
                string word1 = Word(num);
                while(Getnums(word1) == 10)
                {
                    num = rnd.Next(count);
                    word1 = Word(num);
                }
                textBox1.Text = word1;
                times++;
                label1.Text = points.ToString();
                button1.Text = "Провери";
            }
            else
            {
                string word = textBox1.Text;
                string word1 = textBox2.Text;
                if (Getprevod(word) == word1)
                {
                    Random rnd = new Random();
                    num = rnd.Next(count);
                    string word2 = Word(num);
                    while (Getnums(word2) == 10)
                    {
                        num = rnd.Next(count);
                        word1 = Word(num);
                    }
                    textBox1.Text = word2;
                    nums = Getnums(word) + 1;
                    Setnums(word, nums);
                    points++;
                    times++;
                    label1.Text = points.ToString();
                    textBox2.Text = null;
                }
                else
                {
                    Random rnd = new Random();
                    num = rnd.Next(count);
                    string word2 = Word(num);
                    while (Getnums(word2) == 10)
                    {
                        num = rnd.Next(count);
                        word1 = Word(num);
                    }
                    textBox1.Text = word2;
                    nums = Getnums(word) - 1;
                    Setnums(word, nums);
                    times++;
                    textBox2.Text = null;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {    
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string query = "Select Count(id) from word";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }
        public string Word(int n)
        {  
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string query = "select (word) from word where id = " + n;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            string getword = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
            return getword;
        }
        public string Getprevod(string f)
        {
            string otg;           
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string query = "select (prevod) from word where word = '" + f + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            otg = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();
            return otg;
        }
        public int Getnums(string f)
        {
            int otg;            
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string query = "select (num) from word where word = '" + f + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            otg = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
            conn.Close();
            return otg;
        }
        public void Setnums(string f,int j)
        {
            int otg;
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string query = "update word set nums="+j+" where word = '" + f + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            otg = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
            conn.Close();
        }
    }
}