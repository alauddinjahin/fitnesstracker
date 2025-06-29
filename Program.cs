using _216678_FitnessTracker.Screens;
using _216678_FitnessTracker.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _216678_FitnessTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Set DataDirectory manually to your real folder
            //string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string appDataPath = Path.Combine(projectDirectory, @"..\..\App_Data");
            //appDataPath = Path.GetFullPath(appDataPath); // normalize path
            //AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (FtAuth.IsAuthenticate())
            {
                Application.Run(new frmDashboard());
            }
            else
            {
                Application.Run(new frmLogin());
            }
            
        }
    }
}
