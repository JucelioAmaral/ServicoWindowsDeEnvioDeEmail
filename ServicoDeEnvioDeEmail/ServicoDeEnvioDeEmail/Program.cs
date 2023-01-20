using ServicoDeEnvioDeEmail.TestesDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServicoDeEnvioDeEmail
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            {
#if DEBUG
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
#else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new ServiceEnviodeEmail()
                };
                ServiceBase.Run(ServicesToRun);
#endif
            }
        }
    }
}