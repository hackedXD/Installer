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
        public Form1()
        {
            InitializeComponent();
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

        void installLCP() {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey key = hklm.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment\\1.8");
            if (key != null)
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://assets.lunarproxy.me/server.cer", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCPcert.cer"));
                    String javaHome = (string)key.GetValue("JavaHome");
                    Process.Start(javaHome + "\\bin\\keytool.exe", "-keystore \"" + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.lunarclient\\jre\\zulu16.30.15-ca-fx-jre16.0.1-win_x64\\lib\\security\\cacerts\" -trustcacerts -importcert -alias lcproxy -storepass changeit -file \"" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LCPcert.cer") + "\" -noprompt");
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

                button1.Text = "Complete";
            } else
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
    }
}
