using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEnvioDeEmail
{
    public class clsConnection
    {
        public EventsWillWrite evtWriter = new EventsWillWrite();

        public SqlConnection getConexaoBD()
        {
            try
            {
                var conexaoBD = ConfigurationSettings.AppSettings["CONEXAOBD"];
                //obtem a string de conexão do App.Config e retorna uma nova conexao
                SqlConnection sqlConn = new SqlConnection("Data Source=" + conexaoBD + ";Initial Catalog=DBEnviaEmail;User Id=jra; Password=jra");
                return sqlConn;
            }
            catch (Exception e)
            {
                evtWriter.GetFileEvents().WriteEventLogs("getConexaoBD:Exception:" + e.Message, true);
                EventLog.WriteEntry("OnStart:Exception:", e.Message);
                return null;
            }
            finally
            {
                //evtWriter = null;
            }
        }
    }
}
