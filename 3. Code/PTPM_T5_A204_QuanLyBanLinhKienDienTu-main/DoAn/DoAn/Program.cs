﻿ using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoAn
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new FRM.FrmSystem().Show();
            Application.Run();
        }
    }
}
