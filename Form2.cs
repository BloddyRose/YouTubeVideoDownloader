using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace YouTubeVideoDownloader
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/BloddyRose/Converter/releases/download/2.0/Converter.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
