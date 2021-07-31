using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEnvioDeEmail
{
    public class clsUtil
    {
        public EventsWillWrite evtWriter = new EventsWillWrite();
        /// <summary>
        /// finaliza o processo que o serviço startou
        /// </summary>
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
                evtWriter.GetFileEvents().WriteEventLogs("Serviço de envio de email foi finalizado.", false);
            }
            catch (Exception e)
            {
                Console.WriteLine("EndProcessExternal:Exception:", e.Message);
                evtWriter.GetFileEvents().WriteEventLogs("EndProcessExternal:Exception:" + e.Message, true);
            }
            finally
            {
                evtWriter = null;
            }
        }

    }
}
