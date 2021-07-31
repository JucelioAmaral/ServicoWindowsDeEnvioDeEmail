using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace EnvioEmail
{
    public partial class frmEnvioDeEmail : Form
    {
        DateTime time = DateTime.Now;
        string format = "dd-MM-yyyy  HH:mm:sss";
        public Process MyProcess;
        SqlConnection con = null;
        public EventosEscrita evtWriter = new EventosEscrita();
        string nameProcess;
        string[] eachNameProcess;
        clsCheckEmails checkEmail = new clsCheckEmails();

        [Obsolete]
        public frmEnvioDeEmail()
        {
            try
            {
            InitializeComponent();
                //----------------------------------Iniciando aplicação externa -------------------------   
                var appExterno = ConfigurationSettings.AppSettings["APP EXTERNO"];
                Process MyProcess = new Process();
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //MyProcess.StartInfo.FileName = @"C:\versao 19_MVC\QualPatio.exe";
                MyProcess.StartInfo.FileName = appExterno;
                Process.Start(MyProcess.StartInfo);
                nameProcess = MyProcess.StartInfo.FileName.ToString();
                eachNameProcess = nameProcess.Split('\\');
                evtWriter.GetFileEvents().WriteEventLogs("Iniciado processo externo:" + eachNameProcess[2].ToString(), true);
                //Process.Start(@"C:\versao19_MVC\QualPatio.exe");


                //----------------------------------Iniciando -------------------------   
                evtWriter.GetFileEvents().WriteEventLogs("Serviço iniciado.", true);
                ThreadStart start = new ThreadStart(checkEmail.VerificarEmailsPendentes);
                Thread thread = new Thread(start);
                thread.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine("frmEnvioDeEmail:Exception:" + e.Message.ToString());
            }
            finally
            {

            }
        }
    }
}
