using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace EnvioEmail
{
    public class clsLogs
    {
        public StreamWriter arquivoLog;
        DateTime time = DateTime.Now;
        string format = "dd-MM-yyyy  HH:mm:sss";

        public void WriteEventLogs(string log, bool aplicationOpened)
        {
            try
            {
                var caminhoEventLog = ConfigurationSettings.AppSettings["CAMINHO EVENT LOG"];
                //arquivoLog = new StreamWriter(@"C:\testeLog.txt", true);
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
                    //Fecho o arquivo com o método Close
                    arquivoLog.Close();
                }
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("WriteEventLogs:Exception:", e.Message);
            }
        }
    }

    public class EventosEscrita
    {

        public static clsLogs evtLog = new clsLogs();

        public clsLogs GetFileEvents()
        {
            return EventosEscrita.evtLog;
        }
    }
}
