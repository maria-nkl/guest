using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form6 loginForm = new Form6();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1());
                }
        }
    }
}
