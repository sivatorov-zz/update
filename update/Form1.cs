using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(@"update.inf", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                string s1 = sr.ReadLine();

                FileVersionInfo file_version_info_remote_control_server_on_server = FileVersionInfo.GetVersionInfo(s1);

                string s2 = sr.ReadLine();

                FileVersionInfo file_version_info_remote_control_server_on_computer = FileVersionInfo.GetVersionInfo(Application.StartupPath + @"\" + s2);

                try
                {
                    Process[] prs = Process.GetProcesses();
                    foreach (Process pr in prs)
                    {
                        if (pr.ProcessName.Contains(s2))
                            pr.Kill();
                    }

                }
                catch (Exception)
                {

                }

                if (Convert.ToDouble(file_version_info_remote_control_server_on_server.FileVersion.Replace(".", ",")) > Convert.ToDouble(file_version_info_remote_control_server_on_computer.FileVersion.Replace(".", ",")))
                {
                    File.Copy(s1, Application.StartupPath + @"\" + s2, true);
                }
                Process process_run_program = new Process();
                process_run_program.StartInfo.FileName = Application.StartupPath + @"\" + s2;
                process_run_program.Start();
            }
            catch (Exception ex)
            {

            }
            this.Close();
        }
    }
}
