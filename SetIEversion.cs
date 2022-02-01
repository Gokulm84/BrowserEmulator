using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEbbrowserTest
{
    public static class SetIEversion
    {
        public static void SetIEKeyforWebBrowserControl(string appName, string version)
        {
            RegistryKey Regkey = null;
            try
            {
                // For 64 bit machine
                if (Environment.Is64BitOperatingSystem)
                {
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                }
                else  //For 32 bit machine
                {
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                }

                // If the path is not correct or
                // if the user haven't priviledges to access the registry
                if (Regkey == null)
                {
                    MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));
                
                // Check if key is already present
                if (FindAppkey == ((int)(BrowserEmulationVersion)Enum.Parse(typeof(BrowserEmulationVersion), version)).ToString())
                {
                    MessageBox.Show("Required Application Settings Present");
                    Regkey.Close();
                    return;
                }

                // If a key is not present add the key, Key value  (decimal)
               
                    int regValue = ((int)(BrowserEmulationRegistryVersion)Enum.Parse(typeof(BrowserEmulationRegistryVersion), version));
                   // string hexValue = regValue.ToString("X");
                    MessageBox.Show("About to set Registry value to  - " + regValue);
                   Regkey.SetValue(appName, unchecked(regValue), RegistryValueKind.DWord);
                

                // Check for the key after adding
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                if (FindAppkey == ((int)(BrowserEmulationVersion)Enum.Parse(typeof(BrowserEmulationVersion), version)).ToString())
                {
                    MessageBox.Show("Application Settings Applied Successfully");
                }
                else
                {
                    MessageBox.Show("Application Settings Failed, Ref: " + FindAppkey);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Settings Failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the Registry
                if (Regkey != null)
                    Regkey.Close();
            }
        }
    }
}
