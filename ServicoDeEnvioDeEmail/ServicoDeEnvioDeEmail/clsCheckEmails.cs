using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace ServicoDeEnvioDeEmail
{
    public class clsCheckEmails
    {
        SqlConnection con = null;
        DateTime time = DateTime.Now;
        string format = "yyyy-MM-dd HH:mm:ss.fff";
        public EventsWillWrite evtWriter = new EventsWillWrite();

        public void VerificarEmailsPendentes()
        {
            try
            {
                evtWriter.GetFileEvents().WriteEventLogs("Verificação de emails pendentes iniciada.", true);

                while (true)
                {
                    Thread.Sleep(5000);

                    clsConnection getConn = new clsConnection();
                    con = getConn.getConexaoBD();
                    con.Open();
                    string sqlString = "";
                    sqlString = "SELECT top 100 * FROM tblEnviarEmail WHERE status = 'N'";
                    SqlCommand cmd = new SqlCommand(sqlString, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // ENVIO DE EMAIL PELO GMAIL COMENTADO DEVIDO NÃO ESTAR MAIS SEND POSSÍVEL O ENVIO CONFORME O GMAIL: "A partir de 30 de maio de 2022, o Google não autorizará mais o uso de apps ou dispositivos de terceiros que exigem apenas nome de usuário e senha para fazer login na Conta do Google . Essa mudança tem como objetivo proteger sua conta." 
                        //if (EnviaEmailDoGmail(reader["EmailOrigem"].ToString(),
                        //    reader["EmailDestino"].ToString(),
                        //    reader["NomeOrigem"].ToString(),
                        //    reader["NomeDestino"].ToString(),
                        //    reader["Assunto"].ToString(),
                        //    reader["Mensagem"].ToString()) == true)
                        //{
                        AtualizaBancoDeDados(Convert.ToInt32(reader["Id"].ToString()));
                        //}
                    }
                    reader.Close();
                    con.Close();
                    sqlString = "";
                }
            }
            catch (Exception e)
            {
                evtWriter.GetFileEvents().WriteEventLogs("VerificarEmailsPendentes:Exception:" + e.Message, true);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Envia Email para Gmail
        /// </summary>
        /// <param name="emailOrigem"></param>
        /// <param name="emailDestino"></param>
        /// <param name="nomeOrigem"></param>
        /// <param name="nomeDestino"></param>
        /// <param name="assunto"></param>
        /// <param name="mensagememail"></param>
        /// <returns></returns>
        public bool EnviaEmailDoGmail(string emailOrigem,
            string emailDestino,
            string nomeOrigem,
            string nomeDestino,
            string assunto,
            string mensagememail)
        {
            try
            {
                //evtWriter.GetFileEvents().WriteEventLogs("Enviando email...", true);
                var senhaEmail = ConfigurationSettings.AppSettings["SENHA"];
                var hostEmail = ConfigurationSettings.AppSettings["HOST"];
                var port = ConfigurationSettings.AppSettings["PORT"];
                int portnumber = Int32.Parse(port);
                MailAddress origem = new MailAddress(emailOrigem, nomeOrigem);
                MailAddress destino = new MailAddress(emailDestino, nomeDestino);

                // cria uma mensagem
                MailMessage mensagem = new MailMessage(origem, destino);
                mensagem.Subject = assunto;
                mensagem.Body = assunto;

                SmtpClient smtp = new SmtpClient(hostEmail, portnumber);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(origem.Address, senhaEmail);
                smtp.Send(mensagem);
                Console.WriteLine("Mensagem enviada para  " + origem.Address + " às " + DateTime.Now.ToString() + ".");
                evtWriter.GetFileEvents().WriteEventLogs("Email enviado para " + origem.Address.ToString(), true);

                return true;
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("EnviaEmailparaGmail:Exception:", e.Message.ToString());
                evtWriter.GetFileEvents().WriteEventLogs("EnviaEmailparaGmail:Exception:" + e.Message, true);
                return false;
            }
            finally
            {

            }
        }      

        /// <summary>
        /// Atualiza o Banco de DeDados
        /// </summary>
        /// <param name="id"></param>
        public void AtualizaBancoDeDados(int id)
        {

            try
            {
                evtWriter.GetFileEvents().WriteEventLogs("Atualizando banco de dados...", true);
                clsConnection getConn = new clsConnection();
                con = getConn.getConexaoBD();
                con.Open();
                string sqlString = "";
                sqlString = "UPDATE tblEnviarEmail SET status = 'S', DataHora='" + time.ToString(format) + "' WHERE Id = @id";
                SqlCommand cmdupdate = new SqlCommand(sqlString, con);
                cmdupdate.Parameters.Add(new SqlParameter("@Id", id));
                cmdupdate.ExecuteNonQuery();
                con.Close();
                sqlString = "";
                evtWriter.GetFileEvents().WriteEventLogs("Banco atualizado.", true);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("AtualizarEmail:Exception:", e.Message.ToString());
                evtWriter.GetFileEvents().WriteEventLogs("AtualizaBancoDeDados:Exception:" + e.Message, true);
            }
            finally
            {

            }
        }
    }
}
