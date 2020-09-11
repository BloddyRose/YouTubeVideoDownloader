using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace YouTubeVideoDownloader
{
    public partial class Form1 : Form
    {
        private readonly Process myProcess = new Process();

        public Form1()
        {
            InitializeComponent();
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            descriptionBox.Text = "";
        }

        private void getLink_Click(object sender, EventArgs e)
        {


            try
            {
                descriptionBox.Text = "";
                string path = Directory.GetCurrentDirectory();
                string foldername = "download";



                if (!Directory.Exists(path + foldername))
                {
                    Directory.CreateDirectory(path + foldername);
                }

                string outout = Path.Combine(path, foldername);


                myProcess.StartInfo.FileName = @".\tools\youtube-dl.exe";

                myProcess.StartInfo.Arguments = $"-o {outout}" + linkInput.Text;

                myProcess.Start();
                descriptionBox.Text = myProcess.StandardOutput.ReadToEnd().ToString();

                myProcess.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                string foldername = "tools";

                string output = Path.Combine(path, foldername);

                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }



                WebClient webClient = new WebClient();
                panel1.Visible = true;
                panel1.Show();
                webClient.DownloadProgressChanged += (s, ea) =>
                {
                    progressbar.Value = ea.ProgressPercentage;
                };
                webClient.DownloadFileCompleted += (s, ea) =>
                {
                    progressbar.Visible = false;
                    panel1.Visible = false;
                    panel1.Hide();
                    // any other code to process the file
                };
                webClient.DownloadFileAsync(new Uri("https://youtube-dl.org/downloads/latest/youtube-dl.exe"),
                    @".\tools\youtube-dl.exe");
            }
            catch (Exception Ex)
            {

                MessageBox.Show($"Erorr Happend : {Ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                descriptionBox.Text = "";
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = @".\tools\youtube-dl.exe";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.StartInfo.Arguments = "--update";
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.Start();

                myProcess.WaitForExit();

                descriptionBox.Text = myProcess.StandardOutput.ReadToEnd().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Happended : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = "";
            string path = Directory.GetCurrentDirectory();
            string folder = "download";

            string download = Path.Combine(path, folder);
            IEnumerable<string> files = Directory.EnumerateFiles(download, "*.mp4", SearchOption.AllDirectories).Select(Path.GetFileName);
            foreach (string file in files)
            {
                descriptionBox.Text = $"New file downloaded in {download} : {file}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
    }
}