/*
 * Maded by BloddyRose 
 * Thanks to youtube-dl
 * Just a software to download youtube videos
 */
#region Main

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
        #region GetVideo

        private void getLink_Click(object sender, EventArgs e)
        {


            try
            {
                descriptionBox.Clear();


                myProcess.StartInfo.FileName = @".\tools\youtube-dl.exe";

                myProcess.StartInfo.Arguments = " --abort-on-error " + linkInput.Text;

                myProcess.Start();
                descriptionBox.Text = myProcess.StandardOutput.ReadToEnd().ToString();

                myProcess.WaitForExit();
                string files_folder = Directory.CreateDirectory("download").FullName;

                string folder = Environment.CurrentDirectory;


                string[] items = System.IO.Directory.GetFiles(folder, "*.mp4", System.IO.SearchOption.TopDirectoryOnly);
                foreach (string filePath in items)
                {
                    string newFile = System.IO.Path.Combine(files_folder, System.IO.Path.GetFileName(filePath));
                    if (File.Exists(newFile))
                    {
                        MessageBox.Show("File already exists!!\n Overwriting!", "Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // File.Delete(file);
                        descriptionBox.Text = $"File {Path.GetFileName(filePath)} moved in {newFile}\n";
                        File.Delete(filePath);
                        continue;
                    }

                    File.Move(filePath, newFile);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr happend " + ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Download Youtube-dl

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                string foldername = "tools";

                string output = Path.Combine(path, foldername);

                if (Directory.Exists(output))
                {
                }
                else
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
                    progressbar.Value = 0;
                    panel1.Visible = false;
                    panel1.Hide();
                    // any other code to process the file
                };
                webClient.DownloadFileAsync(new Uri("https://youtube-dl.org/downloads/latest/youtube-dl.exe"),
                    @".\tools\youtube-dl.exe");
                if (progressbar.Value == 50)
                {
                    descriptionBox.Clear();
                    descriptionBox.Text = "Almost Downloaded";
                }
                else if (progressbar.Value == 1)
                {
                    descriptionBox.Clear();
                    descriptionBox.Text = "Starting Download Down will be a progress bar";
                }
                else if (progressbar.Value == 100)
                {
                    descriptionBox.Clear();
                    descriptionBox.Text = "Finished Download";
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show($"Erorr Happend : {Ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion
        #region Update Youtube-dl

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
        #endregion
        #region Log Area
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                descriptionBox.Clear();

                string path = Directory.GetCurrentDirectory();
                IEnumerable<string> files = Directory.EnumerateFiles(path, "*.mp4", SearchOption.AllDirectories).Select(Path.GetFileName);
                foreach (string file in files)
                {
                    descriptionBox.Text = $"New file downloaded : {file}\n";
                }

                string files_folder = Directory.CreateDirectory("download").FullName;

                string folder = Environment.CurrentDirectory;


                string[] items = System.IO.Directory.GetFiles(folder, "*.mp4", System.IO.SearchOption.TopDirectoryOnly);
                foreach (string filePath in items)
                {
                    string newFile = System.IO.Path.Combine(files_folder, System.IO.Path.GetFileName(filePath));
                    if (File.Exists(newFile))
                    {
                        MessageBox.Show("File already exists!!\n Overwriting!", "Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // File.Delete(file);
                        descriptionBox.Text = $"File {Path.GetFileName(filePath)} moved in {newFile}\n";
                        File.Delete(filePath);
                        continue;
                    }

                    File.Move(filePath, newFile);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Others
        private void Form1_Load(object sender, EventArgs e)
        {
            descriptionBox.Clear();
            panel1.Visible = false;
            panel1.Hide();
            Application.EnableVisualStyles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            descriptionBox.Clear();
            Dispose();
            Application.Exit();
        }
        #endregion
        #region Download Converter
        private void button4_Click(object sender, EventArgs e)
        {
            string url = @"https://github.com/BloddyRose/Converter/releases/download/v2.0/ConverterSetup.msi";
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
                    progressbar.Value = 0;
                    panel1.Visible = false;
                    panel1.Hide();
                };
                webClient.DownloadFileAsync(new Uri(url),
                    @".\tools\ConverterSetup.msi");
                if (progressbar.Value == 50)
                {
                    descriptionBox.Clear();
                    descriptionBox.Text = "Almost Downloaded";
                }
                else if (progressbar.Value == 1)
                {
                    descriptionBox.Clear();
                    descriptionBox.Text = "Starting Download Down will be a progress bar";
                }
                else if (progressbar.Value == 100)
                {
                    descriptionBox.Clear();
                    descriptionBox.Text = "Finished Download";
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show($"Erorr Happend : {Ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region OpenExplorer On Download Folder

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Directory.CreateDirectory("download");
            string path = Directory.GetCurrentDirectory();
            string folder = "download";

            string full_path = Path.Combine(path, folder);
            Process.Start("explorer.exe", full_path);
        }
        #endregion
    }
}
#endregion 