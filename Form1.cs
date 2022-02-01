using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEbbrowserTest
{
    public partial class Form1 : Form
    {

        private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";

        public Form1()
        {
            InitializeComponent();

            //if (!WBEmulator.IsBrowserEmulationSet())
            //{
            //    MessageBox.Show("latest not set");
            //    //  WBEmulator.SetBrowserEmulationVersion();
            //}
            using (WebBrowser wb = new WebBrowser())
            {
                string Version = wb.Version.ToString();
                MessageBox.Show("Web Browser Version " +Version);
            }
            textBox1.Text = "https://www.bing.com/";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter URL");
            }
            else
            {
                try
                {
                    var uri = new Uri(textBox1.Text);
                    webBrowser1.Navigate(uri);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter a valid URL");
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                var appName = Process.GetCurrentProcess().ProcessName + ".exe";
                SetIEversion.SetIEKeyforWebBrowserControl(appName, comboBox1.SelectedItem.ToString());
               // MessageBox.Show("Web Browser Emulation set to " + comboBox1.SelectedItem.ToString());

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
