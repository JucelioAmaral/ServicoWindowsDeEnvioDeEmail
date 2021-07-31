using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace EnvioEmail
{
    class clsConnection
    {
        public SqlConnection getConexaoBD()
        {
            var conexaoBD = ConfigurationSettings.AppSettings["CONEXAOBD"];

            //obtem a string de conexão do App.Config e retorna uma nova conexao
            //ou Data Source=127.0.0.1
            SqlConnection sqlConn = new SqlConnection("Data Source=" + conexaoBD + ";Initial Catalog=DBEnviaEmail;User Id=jra; Password=jra");
            return sqlConn;
        }
    }
}
