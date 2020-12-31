using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
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
            //Alterar de volta para o Form1 (Login) quando já estiver funcional
            //E quando estiver funcional, ir ao codigo Load do form Menu e retirar a atribuição manual de valores ao newForm.currentuser
            Application.Run(new Cliente.Forms.Menu());
        }
    }
}
