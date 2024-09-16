using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using WizardMakerPrototype.Models;

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

            Covenant  debugCovenant  = null;
            Character debugCharacter = new Character("DEBUG Character Name", CharacterType.Magus, debugCovenant);
            FormForArM5Character tempForm = new FormForArM5Character(debugCharacter);

            Application.Run(tempForm);
        }
    }
}
