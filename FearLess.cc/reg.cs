using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FearLess.cc
{
    public partial class reg : Form
    {
        public int r = 255, g = 0, b = 0;

        public reg()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

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
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM invtable WHERE inv_code='" + textBox3.Text + "'", conn);
            MySql.Data.MySqlClient.MySqlDataReader rd = com.ExecuteReader();

            if (rd.HasRows)
            {
                while(rd.Read())
                {
                    if(rd[3].ToString() == "yes")
                    {
                        System.Windows.Forms.MessageBox.Show("Invite key used", "Error",
        System.Windows.Forms.MessageBoxButtons.OK,
        System.Windows.Forms.MessageBoxIcon.Error);
                        this.Close();

                    }
                    else
                    {
                        string json = get_web_content("http://windowsrilsite.site/date.php");

                        dynamic array = JsonConvert.DeserializeObject(json);
                        //Console.WriteLine(array.date);

                        DateTime dt = Convert.ToDateTime(array.date);
                        DateTime expiry = dt.AddDays(Convert.ToDouble(rd[2].ToString()));
                        //MessageBox.Show(curElement.GetAttribute("InnerText")); // Do something you want
                        //DateTime inputDate = DateTime.ParseExact(array.date, "yyyy/MMMM/d", CultureInfo.InvariantCulture);

                        //DateTime endDate = DateTime.ParseExact(dt.AddDays(Convert.ToDouble(rd.GetString(3))).ToString(), "yyyy/MMMM/d", CultureInfo.InvariantCulture);

                        

                        MySql.Data.MySqlClient.MySqlConnection conn1 = new MySql.Data.MySqlClient.MySqlConnection("Server=bf7ea4rwjqiikn2hyr51-mysql.services.clever-cloud.com;Port=20707;Database=bf7ea4rwjqiikn2hyr51;Uid=ubssgp2yjqh5w77z;Pwd=NlfDfQHkmxxMC1q7YHY;");
                        conn1.Open();
                        MySql.Data.MySqlClient.MySqlCommand com1 = new MySql.Data.MySqlClient.MySqlCommand("INSERT INTO users (name,pass,hwid,expiry,type) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + id.ToString() + "','" + rd[2].ToString() + "','standard');", conn1);
                        com1.ExecuteNonQuery();
                        conn1.Close();

                        MySql.Data.MySqlClient.MySqlConnection conn11 = new MySql.Data.MySqlClient.MySqlConnection("Server=bf7ea4rwjqiikn2hyr51-mysql.services.clever-cloud.com;Port=20707;Database=bf7ea4rwjqiikn2hyr51;Uid=ubssgp2yjqh5w77z;Pwd=NlfDfQHkmxxMC1q7YHY;");
                        conn11.Open();
                        MySql.Data.MySqlClient.MySqlCommand com11 = new MySql.Data.MySqlClient.MySqlCommand("UPDATE invtable SET used='yes' WHERE inv_code='" + textBox3.Text + "'", conn11);
                        com11.ExecuteNonQuery();
                        conn11.Close();


                        System.Windows.Forms.MessageBox.Show("Done, now you should login, if you have a problem dm on discord NothingM#0093 | INFO: You got invited by: " + rd[1].ToString() + "!", "Error",
System.Windows.Forms.MessageBoxButtons.OK,
System.Windows.Forms.MessageBoxIcon.Information);
                        key ky = new key();
                        ky.Show();
                        this.Dispose();
                    }

                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Invite key not found", "Error",
        System.Windows.Forms.MessageBoxButtons.OK,
        System.Windows.Forms.MessageBoxIcon.Error);
                this.Close();

            }

            rd.Close();
            conn.Close();
        }

        private void reg_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         /*   panel3.BackColor = Color.FromArgb(r, g, b);
            panel1.BackColor = Color.FromArgb(r, g, b);
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
            }*/
        }
    }
}
