using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace EnvioEmail
{
    public class clsUtil
    {

        public EventosEscrita evtWriter = new EventosEscrita();

        public void EndProcessExternal()
        {
            evtWriter.GetFileEvents().WriteEventLogs("Finalizando processo externo...", true);
            var nameProcess = ConfigurationSettings.AppSettings["NOME APP EXTERNO"];

            // Get all processes running on the local computer.
            Process[] localAll = Process.GetProcesses();

            try
            {
                foreach (Process pr in localAll)
                {
                    if (pr.ProcessName == nameProcess)
                    {
                        pr.Kill();
                        pr.WaitForExit();
                        evtWriter.GetFileEvents().WriteEventLogs("Finalizado processo:" + pr.ProcessName.ToString(), true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("EndProcessExternal:Exception:", e.Message);
                evtWriter.GetFileEvents().WriteEventLogs("EndProcessExternal:Exception:"+ e.Message, true);
            }
        }
    }
}
