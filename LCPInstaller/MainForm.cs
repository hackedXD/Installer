using DiscordRPC;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace LCPInstaller
{

    public partial class MainForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public DiscordRpcClient client;

        public MainForm()
        {
               InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimer.Start(); // Start the timer for the live clock display on the GUI


            client = new DiscordRpcClient("915657807079030814"); // Init the Discord RPC
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "Unlocking Lunar cosmetics with LCProxy",
                State = ".gg/LCProxy",
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = "LCProxy",
                    SmallImageKey = "image_small"
                }
            });
        }

        void removeLCP()
        {
            FileSecurity fSecurity = File.GetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts");

            fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

            File.SetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts", fSecurity);

            String replace = File.ReadAllText("C:\\Windows\\System32\\drivers\\etc\\hosts");
            if (replace.Contains("51.195.220.243 assetserver.lunarclientprod.com"))
            {
                replace = replace.Replace("51.195.220.243 assetserver.lunarclientprod.com", "");
            }
            File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", replace);

            fSecurity.RemoveAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
            File.SetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts", fSecurity);

            removeLCProxyBtn.Text = "Completed";
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
                    installLCProxyBtn.Invoke((MethodInvoker)delegate {
                        label2.Text = "";
                        installLCProxyBtn.Text = "Install";
                        installLCProxyBtn.Enabled = true;
                    });
                    client.Dispose();
                }
            } else
            {
                MessageBox.Show("The installation cannot continue.", "Missing JDK", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        void installLCP() {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey key = hklm.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment\\1.8");
            if (key != null)
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://assets.lunarproxy.me/server.cer", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCPcert.cer"));
                    String javaHome = (string)key.GetValue("JavaHome");

                    string partName = "zulu";
                    DirectoryInfo dirSearch = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.lunarclient\\jre");
                    FileSystemInfo[] filesAndDirs = dirSearch.GetFileSystemInfos("*" + partName + "*");
                    foreach (FileSystemInfo foundFile in filesAndDirs)
                    {
                        string fullName = foundFile.FullName;
                        Process.Start(javaHome + "\\bin\\keytool.exe", "-keystore \"" + fullName + "\\lib\\security\\cacerts\" -trustcacerts -importcert -alias lcproxy -storepass changeit -file \"" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCPcert.cer") + "\" -noprompt");
                    }

                    client.Dispose();
                }

                if (!File.Exists("C:\\Windows\\System32\\drivers\\etc\\hosts")) File.Create("C:\\Windows\\System32\\drivers\\etc\\hosts");

                FileSecurity fSecurity = File.GetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts");

                fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

                File.SetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts", fSecurity);

                String hostsFile = File.ReadAllText("C:\\Windows\\System32\\drivers\\etc\\hosts");
                if(!hostsFile.Contains("51.195.220.243 assetserver.lunarclientprod.com"))
                {
                    hostsFile += "\n51.195.220.243 assetserver.lunarclientprod.com";
                }
                File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", hostsFile);

                fSecurity.RemoveAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl("C:\\Windows\\System32\\drivers\\etc\\hosts", fSecurity);

                installLCProxyBtn.Text = "Completed";
            } else
            {
                new Thread(noJava).Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            installLCProxyBtn.Text = "Installing...";
            installLCProxyBtn.Enabled = false;
            installLCProxyBtn.ForeColor = System.Drawing.Color.White;

            removeLCProxyBtn.Enabled = true;
            removeLCProxyBtn.Text = "Remove LCProxy";
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

        private void button2_Click(object sender, EventArgs e)
        {
            removeLCProxyBtn.Text = "Removing...";
            removeLCProxyBtn.Enabled = false;
            removeLCProxyBtn.ForeColor = System.Drawing.Color.White;

            installLCProxyBtn.Enabled = true;
            installLCProxyBtn.Text = "Install LCProxy";
            removeLCP();
        }
    }
}
