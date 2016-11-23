using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogitechProfilePlus.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form MainForm = new frmMain();
                Application.Run();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }

        }
    }
}
