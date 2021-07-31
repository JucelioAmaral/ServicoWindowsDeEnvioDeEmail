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

        public ServiceEnviodeEmail()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            clsCheckEmails checkEmails;
            EventsWillWrite evtWriter;
            string nameProcess;
            string[] eachNameProcess;

            evtWriter = new EventsWillWrite();
            try
            {
                //System.Diagnostics.Process processo = new System.Diagnostics.Process();
                //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                //startInfo.FileName = @"C:\versao 19_MVC\QualPatio.exe";
                //System.Diagnostics.Process.Start(startInfo);

                ////----------------------------------Iniciando aplicação externa -------------------------
                //var appExterno = ConfigurationSettings.AppSettings["APP EXTERNO"];
                //Process MyProcess = new Process();
                //MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                ////MyProcess.StartInfo.FileName = @"C:\versao 19_MVC\QualPatio.exe";
                //MyProcess.StartInfo.FileName = appExterno;
                //Process.Start(MyProcess.StartInfo);
                //nameProcess = MyProcess.StartInfo.FileName.ToString();
                //eachNameProcess = nameProcess.Split('\\');
                //evtWriter.GetFileEvents().WriteEventLogs("Iniciado processo externo:" + eachNameProcess[2].ToString(), true);


                ////----------------------------------Iniciando -------------------------   
                checkEmails = new clsCheckEmails();             

                evtWriter.GetFileEvents().WriteEventLogs("Serviço de envio de email foi iniciado.", true);
                EventLog.WriteEntry("Serviço de envio de email foi iniciado!", EventLogEntryType.Warning);
                                
                ThreadStart start = new ThreadStart(checkEmails.VerificarEmailsPendentes);
                Thread thread = new Thread(start);
                thread.Start();
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
                //evtWriter.GetFileEvents().WriteEventLogs("Serviço de envio de email foi finalizado.", false);
                EventLog.WriteEntry("Serviço de envio de email foi finalizado!", EventLogEntryType.Warning);
                //clsUtil endprocess = new clsUtil();
                //endprocess.EndProcessExternal();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OnStop:Exception:", e.Message);
            }
        }
    }
}
