using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardMakerPrototype
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            //MainProgramWindow mainWindow = new MainProgramWindow();
            //Application.Run( mainWindow );

            FormForArM5Character tempForm = new FormForArM5Character();
            Application.Run(tempForm);
        }
    }
}
