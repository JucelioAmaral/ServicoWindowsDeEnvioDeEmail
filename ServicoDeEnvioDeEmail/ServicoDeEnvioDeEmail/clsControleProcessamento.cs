using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicoDeEnvioDeEmail
{
    public class clsControleProcessamento
    {
        public EventsWillWrite evtWriter = new EventsWillWrite();
        Thread threadVerificacao;
        private static List<Thread> listaThreads = new List<Thread>();

        public void IniciarProcessamento()
        {
            try
            {                
                evtWriter.GetFileEvents().WriteEventLogs("IniciarProcessamento:Iniciando Processamento.", true);
                clsCheckEmails checkEmails = new clsCheckEmails();
                ThreadStart start = new ThreadStart(checkEmails.VerificarEmailsPendentes);
                threadVerificacao = new Thread(start);
                threadVerificacao.Start();

                listaThreads.Add(threadVerificacao);
            }
            catch (Exception ex)
            {
                evtWriter.GetFileEvents().WriteEventLogs("IniciarProcessamento:Exception:" + ex.Message, true);
                EventLog.WriteEntry("IniciarProcessamento:Exception:", ex.Message);
            }
        }

        public void FinalizarProcessamento()
        {
            try
            {
                foreach (Thread thread in listaThreads)
                {
                    thread.Abort();
                }
                evtWriter.GetFileEvents().WriteEventLogs("FinalizarProcessamento:Processos finalizados.", true);
            }
            catch (Exception ex)
            {
                evtWriter.GetFileEvents().WriteEventLogs("FinalizarProcessamento:Exception:" + ex.Message, true);
                EventLog.WriteEntry("FinalizarProcessamento:Exception:", ex.Message);
            }
        }
    }
}
