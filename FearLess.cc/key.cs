using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FearLess.cc
{
    public partial class key : Form
    {
        public int r = 255, g = 0, b = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(r, g, b);
            panel3.BackColor = Color.FromArgb(r, g, b);
            button1.BackColor = Color.FromArgb(r, g, b);


            if (r > 0 && b == 0)
            {
                r--;
                g++;
            }
            if (g > 0 && r == 0)
            {
                g--;
                b++;
            }
            if (b > 0 && g == 0)
            {
                b--;
                r++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("Server=bf7ea4rwjqiikn2hyr51-mysql.services.clever-cloud.com;Port=20707;Database=bf7ea4rwjqiikn2hyr51;Uid=ubssgp2yjqh5w77z;Pwd=NlfDfQHkmxxMC1q7YHY;");
            conn.Open();
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM users WHERE name='" + textBox1.Text + "' AND pass='" + textBox2.Text + "'", conn);
            MySql.Data.MySqlClient.MySqlDataReader rd = com.ExecuteReader();

            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (rd[3].ToString() == null || rd[3].ToString() == "")
                    {
                        Console.WriteLine("No hwid detected, inserting one");
                        MySql.Data.MySqlClient.MySqlConnection conn1 = new MySql.Data.MySqlClient.MySqlConnection("Server=bf7ea4rwjqiikn2hyr51-mysql.services.clever-cloud.com;Port=20707;Database=bf7ea4rwjqiikn2hyr51;Uid=ubssgp2yjqh5w77z;Pwd=NlfDfQHkmxxMC1q7YHY;");
                        conn1.Open();
                        MySql.Data.MySqlClient.MySqlCommand com1 = new MySql.Data.MySqlClient.MySqlCommand("UPDATE users SET hwid='" + id.ToString() + "' WHERE name='" + textBox1.Text + "'", conn1);
                        com1.ExecuteNonQuery();
                        conn1.Close();


                        if (rd[5].ToString() == "standard")
                        {
                            Form1 frm = new Form1();
                            frm.Show();
                            Hide();
                        }
                        else
                        {
                            admin frm = new admin();
                            frm.Show();
                            Hide();
                        }
                    }
                    else
                    {
                        if (rd[3].ToString() != id.ToString())
                        {

                            System.Windows.Forms.MessageBox.Show("HWID don't match", "Error",
        System.Windows.Forms.MessageBoxButtons.OK,
        System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else if (rd[5].ToString() == "standard")
                        {
                            Properties.Settings.Default.Username = textBox1.Text;
                            Properties.Settings.Default.Password = textBox2.Text;
                            Properties.Settings.Default.Save();
                            Console.WriteLine("normal key, proceed");
                            //WebBrowser wb = new WebBrowser();
                            //wb.Navigate("http://windowsrilsite.site/date.php");
                            //System.Threading.Thread.Sleep(5000);
                            //HtmlElementCollection theElementCollection = default(HtmlElementCollection);
                            //theElementCollection = wb.Document.GetElementsByTagName("p");
                            //foreach (HtmlElement he in webBrowser1.Document.All.GetElementsByName("date"))
                            //{
                            //Console.WriteLine("ff");

                            string json = get_web_content("http://windowsrilsite.site/date.php");

                            dynamic array = JsonConvert.DeserializeObject(json);
                            //Console.WriteLine(array.date);

                            DateTime dt = Convert.ToDateTime(array.date);
                            DateTime expiry = dt.AddDays(Convert.ToDouble(rd[4].ToString()));
                            //MessageBox.Show(curElement.GetAttribute("InnerText")); // Do something you want
                            //DateTime inputDate = DateTime.ParseExact(array.date, "yyyy/MMMM/d", CultureInfo.InvariantCulture);

                            //DateTime endDate = DateTime.ParseExact(dt.AddDays(Convert.ToDouble(rd.GetString(3))).ToString(), "yyyy/MMMM/d", CultureInfo.InvariantCulture);

                            if (DateTime.Compare(dt, expiry) < 0)
                            {
                                Console.WriteLine(dt);
                                Console.WriteLine(expiry);

                                


                                Console.WriteLine("yes");

                                Form1 frm = new Form1();
                                frm.Show();
                                Hide();

                            }
                            else
                            {
                                Console.WriteLine(dt);
                                Console.WriteLine(expiry);
                                Console.WriteLine("no");

                                System.Windows.Forms.MessageBox.Show("Time Expired!", "Error",
System.Windows.Forms.MessageBoxButtons.OK,
System.Windows.Forms.MessageBoxIcon.Error);
                                Form1 frm = new Form1();
                                frm.Show();
                                Hide();

                            }
                            //}


                        }
                        else if (rd[5].ToString() == "admin")
                        {
                            Console.WriteLine("Admin detected! Proceed!");
                            admin ad = new admin();
                            ad.Show();
                            Hide();
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Username or password not found!", "Error",
        System.Windows.Forms.MessageBoxButtons.OK,
        System.Windows.Forms.MessageBoxIcon.Error);
            }

            rd.Close();
            conn.Close();
        }
        public string get_web_content(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string output = reader.ReadToEnd();
            response.Close();

            return output;
        }

        public key()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reg re = new reg();
            re.ShowDialog();
        }

        public static async Task<bool> IsDown(string url)
        {
            HttpClient client = new HttpClient();
            var checkingResponse = await client.GetAsync(url);
            if (!checkingResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void key_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.Username != string.Empty)
            {
                textBox1.Text = Properties.Settings.Default.Username;
                textBox2.Text = Properties.Settings.Default.Password;
            }

            try
            {
                if (await IsDown("https://sidesense.eu/") == false)
                {
                    MessageBox.Show("Website is Down, the loader will be usable until website is fixed!");
                    Close();
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Website is Down, the loader will not be usable until website is fixed!");
                Close();
            }
        }
    }
}
