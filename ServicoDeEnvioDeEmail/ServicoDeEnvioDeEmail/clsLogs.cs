using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEnvioDeEmail
{
    public class clsLogs
    {
        public StreamWriter arquivoLog;
        DateTime time = DateTime.Now;
        string format = "dd-MM-yyyy  HH:mm:sss";
        public EventsWillWrite evtWriter = new EventsWillWrite();

        public void WriteEventLogs(string log, bool aplicationOpened)
        {
            try
            {
                var caminhoEventLog = ConfigurationSettings.AppSettings["CAMINHO EVENT LOG"];
                if (arquivoLog is null)
                {
                    arquivoLog = new StreamWriter(caminhoEventLog, true);
                }

                if (aplicationOpened == true)
                {
                    arquivoLog.WriteLine(time.ToString(format) + " " + log);
                    //Limpa o buffer com o método Flush
                    arquivoLog.Flush();
                }
                if (aplicationOpened == false)
                {
                    arquivoLog.WriteLine(time.ToString(format) + " " + log);
                    //Limpa o buffer com o método Flush
                    arquivoLog.Flush();
                    //Fecho o arquivo com o método                    
                    arquivoLog.Close();
                }
            }
            catch (Exception e)
            {
                evtWriter.GetFileEvents().WriteEventLogs("WriteEventLogs:Exception:" + e.Message, true);
                EventLog.WriteEntry("WriteEventLogs:Exception:", e.Message);
            }
        }
    }

    public class EventsWillWrite
    {

        public static clsLogs evtLog = new clsLogs();
        /// <summary>
        /// método criado para retornar objeto da classe clsLogs que só pode ser instanciado 1 vez.
        /// </summary>
        /// <returns></returns>
        public clsLogs GetFileEvents()
        {
            return EventsWillWrite.evtLog;
        }

    }
}
