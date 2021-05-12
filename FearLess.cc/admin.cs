using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FearLess.cc
{
    public partial class admin : Form
    {
        public int r = 255, g = 0, b = 0;

        public static void UpdateChangelog(string filename, string path)
        {

            string input = Interaction.InputBox("What is new update?", "New Update");
            //string lines = "Tesy";

            //"changelogs.txt"
            File.WriteAllText(filename, input);

            //// Get the object used to communicate with the server.
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.sidesense.eu/public_html/");
            //request.Method = WebRequestMethods.Ftp.UploadFile;

            //// This example assumes the FTP site uses anonymous logon.
            //request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");

            //// Copy the contents of the file to the request stream.
            //byte[] fileContents;
            //using (StreamReader sourceStream = new StreamReader("changelogs.txt"))
            //{
            //    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            //}

            //request.ContentLength = fileContents.Length;

            //using (Stream requestStream = request.GetRequestStream())
            //{
            //    requestStream.Write(fileContents, 0, fileContents.Length);
            //}

            //using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            //{
            //    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            //}


            string PureFileName = new FileInfo(filename).Name;
            String uploadUrl = path + PureFileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");
            request.Proxy = null;
            request.KeepAlive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // Copy the contents of the file to the request stream.  
            StreamReader sourceStream = new StreamReader(filename);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);



            File.Delete("changelogs.txt");
        }
        public static void UpdateDll(string filename, string path)
        {

            //string lines = "Tesy";

            //"changelogs.txt"


            //// Get the object used to communicate with the server.
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.sidesense.eu/public_html/");
            //request.Method = WebRequestMethods.Ftp.UploadFile;

            //// This example assumes the FTP site uses anonymous logon.
            //request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");

            //// Copy the contents of the file to the request stream.
            //byte[] fileContents;
            //using (StreamReader sourceStream = new StreamReader("changelogs.txt"))
            //{
            //    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            //}

            //request.ContentLength = fileContents.Length;

            //using (Stream requestStream = request.GetRequestStream())
            //{
            //    requestStream.Write(fileContents, 0, fileContents.Length);
            //}

            //using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            //{
            //    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            //}


            string PureFileName = new FileInfo(filename).Name;
            String uploadUrl = path + PureFileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");
            request.Proxy = null;
            request.KeepAlive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // Copy the contents of the file to the request stream.  
            StreamReader sourceStream = new StreamReader(filename);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);



            File.Delete("changelogs.txt");
        }
        static string DeleteFile(string fileName,string path)
        {
            //ftp://sidesense.eu/public_html/
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path + fileName);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }


        private static void AppendString()
        {
            if (!string.IsNullOrEmpty(DeleteFile("changelogs.txt", "ftp://www.sidesense.eu/public_html/main/")))
            {
                UpdateChangelog("changelogs.txt", "ftp://www.sidesense.eu/public_html/");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppendString();
        }

        public void UploadFtpFile(string fileName)
        {

            //FtpWebRequest request;


            //string absoluteFileName = Path.GetFileName(fileName);

            //request = WebRequest.Create(new Uri("ftp://sidesense.eu/public_html/main/")) as FtpWebRequest;
            //request.Method = WebRequestMethods.Ftp.UploadFile;
            //request.UseBinary = true;
            //request.UsePassive = true;
            //request.KeepAlive = true;
            //request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");
            //request.ConnectionGroupName = "group";

            //using (FileStream fs = File.OpenRead(fileName))
            //{
            //    byte[] buffer = new byte[fs.Length];
            //    fs.Read(buffer, 0, buffer.Length);
            //    fs.Close();
            //    Stream requestStream = request.GetRequestStream();
            //    requestStream.Write(buffer, 0, buffer.Length);
            //    requestStream.Flush();
            //    requestStream.Close();
            //}
            FtpWebRequest request =
    (FtpWebRequest)WebRequest.Create("ftp://sidesense.eu/public_html/main/imaratforurpcccccc1234io1uJKFDHOIAFANL.dll");
            request.Credentials = new NetworkCredential("admin@sidesense.eu", "zo6YFHdW");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream fileStream = File.OpenRead(fileName))
            using (Stream ftpStream = request.GetRequestStream())
            {
                fileStream.CopyTo(ftpStream);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Dll files (*.dll)|*.dll|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            File.Copy(filePath, "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll",true);
            string hello = "d" + "f";
            if (!string.IsNullOrEmpty(DeleteFile("imaratforurpcccccc1234io1uJKFDHOIAFANL.dll", "ftp://www.sidesense.eu/public_html/main/")))
            {
                UploadFtpFile("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll");
                File.Delete("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll");
            }
        }

        private void admin_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want a custom invite key?", "Key", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //custom key ya
                string input = Interaction.InputBox("Enter invite custom key:", "Custom invite key");
                string input2 = Interaction.InputBox("Enter expiry (in days):", "Expiry");
                //string input3 = Interaction.InputBox("Enter type of key (standard or admin):", "Type");



                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("Server=bf7ea4rwjqiikn2hyr51-mysql.services.clever-cloud.com;Port=20707;Database=bf7ea4rwjqiikn2hyr51;Uid=ubssgp2yjqh5w77z;Pwd=NlfDfQHkmxxMC1q7YHY;");
                conn.Open();
                MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand("INSERT INTO invtable (inv_code,expiry,used) VALUES ('" + input + "','" + input2 + "','" + "no" + "')", conn);
                com.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Done");
            }
            else if (dialogResult == DialogResult.No)
            {
                //generated key bruh

                string[] idk = { "a","b","c","d","1","2","3","4" };
                string invitekey = string.Empty;
                Random test = new Random();

                for (int i = 0;i < 10;i++)
                {


                    invitekey = invitekey + idk[test.Next(0, idk.Count())];
                }

                string input2 = Interaction.InputBox("Enter expiry (in days):", "Expiry");

                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("Server=bf7ea4rwjqiikn2hyr51-mysql.services.clever-cloud.com;Port=20707;Database=bf7ea4rwjqiikn2hyr51;Uid=ubssgp2yjqh5w77z;Pwd=NlfDfQHkmxxMC1q7YHY;");
                conn.Open();
                MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand("INSERT INTO invtable (inv_code,who,expiry,used) VALUES ('" + invitekey + "','admin','" + input2 + "','" + "no" + "')", conn);
                com.ExecuteNonQuery();
                conn.Close();

                Clipboard.SetText(invitekey);

                MessageBox.Show("Done | Key copied!");
                

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public admin()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(r, g, b);
            panel1.BackColor = Color.FromArgb(r, g, b);
            foreach (Control x in this.Controls)
            {
                if (x is Button)
                {
                    ((Button)x).BackColor = Color.FromArgb(r, g, b);
                }
            }

            //button1.BackColor = Color.FromArgb(r, g, b);


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
    }
}
