using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KINZHAL
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
       {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());*/

           /*Application.EnableVisualStyles();
           Application.Run(new frmShutDown());*/

            Application.SetCompatibleTextRenderingDefault(false);
            frmLogo first = new frmLogo();
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(2);
            first.Show();
            while (end > DateTime.Now) { Application.DoEvents(); }
            first.Close();
            first.Dispose();
            Application.EnableVisualStyles();
            Application.Run(new frmMain());
        }
    }
}
