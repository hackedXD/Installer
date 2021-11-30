using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace LCPInstaller
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimer.Start();
        }

        void noJava()
        {
            DialogResult result = MessageBox.Show("You are missing JDK 1.8, would you like to install it?\nThis may take up to 5 minutes.", "Missing JDK", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (var client = new WebClient())
                {
                    label2.Invoke((MethodInvoker)delegate {
                        label2.Text = "Downloading JDK. Please wait.";
                    });
                    client.DownloadFile("https://assets.lunarproxy.me/jdk.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCP_JDK.exe"));
                    Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCP_JDK.exe")).WaitForExit();
                    button1.Invoke((MethodInvoker)delegate {
                        label2.Text = "";
                        button1.Text = "Install";
                        button1.Enabled = true;
                    });
                    client.Dispose();
                }
            } else
            {
                MessageBox.Show("The installation cannot continue.", "Missing JDK", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        void installLCP()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey key = hklm.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment\\1.8");
            if (key != null)
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://assets.lunarproxy.me/server.cer", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCPcert.cer"));
                    String javaHome = (string)key.GetValue("JavaHome");
                    Process.Start(javaHome + "\\bin\\keytool.exe", "-keystore \"" + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.lunarclient\\jre\\zulu17.30.15-ca-fx-jre17.0.1-win_x64\\lib\\security\\cacerts\" -trustcacerts -importcert -alias lcproxy -storepass changeit -file \"" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCPcert.cer") + "\" -noprompt");
                    client.Dispose();
                }

                if (!File.Exists("C:\\Windows\\System32\\drivers\\etc\\hosts"))
                {
                    DirectorySecurity dSecurity = Directory.GetAccessControl("C:\\Windows\\System32\\drivers\\etc");
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
                    Directory.SetAccessControl("C:\\Windows\\System32\\drivers\\etc", dSecurity);

                    File.Create("C:\\Windows\\System32\\drivers\\etc\\hosts");

                    dSecurity.RemoveAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
                    Directory.SetAccessControl("C:\\Windows\\System32\\drivers\\etc", dSecurity);
                }

                FileSecurity fSecurity = File.GetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts");

                fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

                File.SetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts", fSecurity);

                String hostsFile = File.ReadAllText("C:\\Windows\\System32\\drivers\\etc\\hosts");
                if (hostsFile.Contains("51.195.220.243 assetserver.lunarclientprod.com")) hostsFile = hostsFile.Replace("51.195.220.243 assetserver.lunarclientprod.com", "");
                File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", hostsFile);
                if (!hostsFile.Contains("194.163.177.249 assetserver.lunarclientprod.com"))
                {
                    hostsFile += "\n194.163.177.249 assetserver.lunarclientprod.com";
                }
                File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", hostsFile);

                fSecurity.RemoveAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts", fSecurity);

                button1.Text = "Complete";
            }
            else
            {
                new Thread(noJava).Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Installing";
            button1.Enabled = false;
            installLCP();
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString(); 
        }

        private void dragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void joinDiscordLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("discord:///invite-proxy/914512197781176392");
        }

        private void closeApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
