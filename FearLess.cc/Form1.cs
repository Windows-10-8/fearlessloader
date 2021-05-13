using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManualMapInjection.Injection;




namespace FearLess.cc
{
    
    public partial class Form1 : Form
    {
        public int r = 255, g = 0,b = 0;

        string pat;
        // winapi imports
        private static readonly IntPtr INTPTR_ZERO = (IntPtr)0;
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        const int PROCESS_CREATE_THREAD = 0x2;
        const int PROCESS_QUERY_INFORMATION = 0x400;
        const int PROCESS_VM_OPERATION = 0x8;
        const int PROCESS_VM_WRITE = 0x20;
        const int PROCESS_VM_READ = 0x10;

        const int MEM_COMMIT = 0x1000;
        const int MEM_RESERVE = 0x2000;
        const int MEM_RELEASE = 0x8000;

        const int PAGE_READWRITE = 0x4;
        const int PAGE_EXECUTE_READWRITE = 0x40;
        const uint INFINITE = 0xFFFFFFFF;




        public Form1()
        {
            InitializeComponent();

        }

        bool injectOld()
        {
            return false;
            //DllInjector dj = new DllInjector();

            //if (dj.Inject("csgo", "dll.idkid"))
            //{
                //return true;
            //}
            //else
            //{
                //return false;
            //}
        }
        void inject()
        {

            //Console.WriteLine("Waiting csgo...");
            //button1.Text = "Waiting csgo...";

            //Process[] pname = Process.GetProcessesByName("csgo");
            //if (pname.Length == 0)
            //{
            //    MessageBox.Show("Csgo not open!");
            //    return;

            //}
            //else
            //{

            //}

            bool found = false;
            while (!found)
            {
                foreach (Process clsProcess in Process.GetProcesses())
                    if (clsProcess.ProcessName == "csgo")
                        found = true;

                Thread.Sleep(1000);
            }


            //Process[] pname = Process.GetProcessesByName("csgo");
            //while (pname.Length == 0)
            //{
            //    System.Threading.Thread.Sleep(100);
            //   //pname.Refresh();
            //}


            Thread.Sleep(28000);

            //while (process.MainWindowTitle != "Title")
            //{
            //    Thread.Sleep(10);
            //}


            //Process[] pname1 = Process.GetProcessesByName("steam");
            //if (pname1.Length == 0)
            //{
            //}
            //else
            //{
            //    System.Diagnostics.Process.Start("cmd.exe", "/c taskkill /IM steam.exe /T");
            //}

            Random test = new Random();

            string[] pathname = { "a", "b", "c", "1", "2", "5", "11111", "iftr" };


            string realpathanme = string.Empty;

            for (int i = 0; i < 25; i++)
            {
                realpathanme += pathname[test.Next(0, pathname.Count())];
            }

            pat = @"C:\users\" + Environment.UserName.ToString() + @"\AppData\Roaming\" + realpathanme.ToString();
            if (Directory.Exists(pat))
            {
                Directory.Delete(pat);
            }
            else
            {
                Directory.CreateDirectory(pat);
            }
            //checkonline();
            string testforbreak = "hello";
            if (ByteArrayToFile() == true)
            {
                var name = "csgo"; //Replace "csgo" with any exe you want [Example: For Team Fortress 2 you would replace it with "hl2"]
                var target = Process.GetProcessesByName(name).FirstOrDefault();

                //Checking if the DLL isn't found
                //if (!File.Exists(pat + "\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll"))
                //{
                    //MessageBox.Show("Unknown Error!");
                    //return;
                //}
                
				var file = getDllsByte();
                //Injection, just leave this alone if you are a beginner
                var injector = new ManualMapInjector(target) { AsyncInjection = true };
                Console.WriteLine("Infos: " + $"hmodule = 0x{injector.Inject(file).ToInt64():x8}");

                if (System.IO.File.Exists(pat + "\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll")) //Checking if the DLL exists
                {
                    System.IO.File.Delete(pat + "\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll"); //Deleting the DLL
                }
            }

            Directory.Delete(pat);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.AppendText("\nWaiting Csgo to open...");
            inject();
            richTextBox2.AppendText("\nInjected!");

        }

        public string xor(string text, string key)
        {
            var result = new StringBuilder();
            for (int c = 0; c < text.Length; c++)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));
            return result.ToString();
        }

        public byte[] getDllsByte()
        {

            WebClient cli = new WebClient();
            cli.Credentials = new NetworkCredential(xor("!$$%", "immaraturpclmaothatsdakey")+"o", xor("#\n", "immaraturpclmaothatsdakey"));
            byte[] bytes = cli.DownloadData(@xor("[[Z\t\rOK\nF\x00\x00\x00\x00\x00BVR_X'*4%<:;1%-#-A\r", "immaraturpclmaothatsdakey"));

                //File.WriteAllBytes(pat + "\\imaratforurpcccccc1234io1uJKFDHOIAFANL.dll", bytes);
            return bytes;
     
        }
        public bool ByteArrayToFile()
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!injectOld())
            {
                MessageBox.Show("Injection Error", "Injection Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Injection: Success", "Injection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox2.AppendText("Hello!");
            //Task.Run(() => RaimbowColor());
            string webRequest = @"http://sidesense.eu/changelogs.txt";

            var textFromFile = (new WebClient()).DownloadString(webRequest);

            richTextBox1.Text = textFromFile;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(r, g, b);
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
            }
        }
    }


}
