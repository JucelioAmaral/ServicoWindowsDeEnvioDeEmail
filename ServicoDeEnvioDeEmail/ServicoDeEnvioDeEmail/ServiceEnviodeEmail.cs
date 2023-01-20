using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicoDeEnvioDeEmail
{
    public partial class ServiceEnviodeEmail : ServiceBase
    {
        EventsWillWrite evtWriter;
        clsControleProcessamento controle;

        public ServiceEnviodeEmail()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                evtWriter = new EventsWillWrite();
                evtWriter.GetFileEvents().WriteEventLogs("Serviço de envio de email foi iniciado.", true);
                EventLog.WriteEntry("Serviço de envio de email foi iniciado!", EventLogEntryType.Warning);
                controle = new clsControleProcessamento();
                controle.IniciarProcessamento();
            }
            catch (Exception e)
            {
                evtWriter.GetFileEvents().WriteEventLogs("OnStart:Exception:" + e.Message, true);
                EventLog.WriteEntry("OnStart:Exception:", e.Message);
            }
            finally
            {
                evtWriter = null;
            }
        }

        protected override void OnStop()
        {
            try
            {
                evtWriter = new EventsWillWrite();
                evtWriter.GetFileEvents().WriteEventLogs("Serviço de envio de email foi finalizado.", false);
                EventLog.WriteEntry("Serviço de envio de email foi finalizado!", EventLogEntryType.Warning);
                controle.FinalizarProcessamento();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OnStop:Exception:", e.Message);
            }
        }
    }
}
