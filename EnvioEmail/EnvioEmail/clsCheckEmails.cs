using System;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace EnvioEmail
{
    class clsCheckEmails
    {
        SqlConnection con = null;
        DateTime time = DateTime.Now;
        string format = "yyyy-MM-dd HH:mm:ss.fff";
        public EventosEscrita evtWriter = new EventosEscrita();



        [Obsolete]
        public void VerificarEmailsPendentes()
        {
            try
            {
                evtWriter.GetFileEvents().WriteEventLogs("Verificando Emails Pendentes...", true);
                while (true)
                {
                    Thread.Sleep(5000);

                    clsConnection getConn = new clsConnection();
                    con = getConn.getConexaoBD();
                    SqlCommand cmd = new SqlCommand("SELECT top 100 * FROM tblEnviarEmail WHERE status = 'N'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Envia do Gmail e atualiza no BD só após envio pelo gmail
                        if (EnviaEmailFromGmail(reader["EmailOrigem"].ToString(),
                            reader["EmailDestino"].ToString(),
                            reader["NomeOrigem"].ToString(),
                            reader["NomeDestino"].ToString(),
                            reader["Assunto"].ToString(),
                            reader["Mensagem"].ToString()) == true)
                        {
                            AtualizaBancoDeDados(Convert.ToInt32(reader["Id"].ToString()));
                        }

                        //Envia do Outlook e não atualiza no BD. Feito só para testes de exploração.
                        SendEmailFromOutlook(reader["EmailOrigem"].ToString(),
                            reader["EmailDestino"].ToString(),
                            reader["NomeOrigem"].ToString(),
                            reader["NomeDestino"].ToString(),
                            reader["Assunto"].ToString(),
                            reader["Mensagem"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                evtWriter.GetFileEvents().WriteEventLogs("VerificarEmailsPendentes:Exception:" + ex.Message, true);
                Console.WriteLine("VerificarEmailsPendentes:Exception:" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        private bool SendEmailFromOutlook(string emailOrigem,
            string emailDestino,
            string nomeOrigem,
            string nomeDestino,
            string assunto,
            string mensagememail)
        {
            try
            {
                evtWriter.GetFileEvents().WriteEventLogs("Enviando email...", true);
                Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
                Outlook.MailItem mailItem = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                StringBuilder conteudo = new StringBuilder();
                string txtNome = "Jucélio";
                string txtEmail = nomeOrigem;
                string txtTelefone = "32 9 8888 5555";
                string ddlServico = assunto;
                string txtMensagem = mensagememail;

                conteudo.Append("O Srº(ª) " + "<b>" + txtNome + "</b>" + " entrou em contato pelo site no dia " + DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "hrs" + "<br /><br />");
                conteudo.Append("<b>" + "Nome do contato: " + "</b>" + txtNome + "<br />");
                conteudo.Append("<b>" + "Email: " + "</b>" + txtEmail + "<br />");
                conteudo.Append("<b>" + "Telefone: " + "</b>" + txtTelefone + "<br />");
                conteudo.Append("<b>" + "Tipo do serviço: " + "</b>" + ddlServico + "<br />");
                conteudo.Append("<b>" + "Mensagem: " + "</b>" + txtMensagem + "<br />");

                mailItem.Subject = assunto;
                mailItem.HTMLBody = conteudo.ToString();           
                mailItem.To = "wabtectester@gmail.com";

                // Sobre a linha de cima, Caso queira adicionar email para ir em cópia, basta colocar ponto e virgula (;) e adicionar o outro email que, irá junto.
                //mailItem.To = "wabtectester@gmail.com;email@hotmail.com";

                //mailItem.CC = EmailCC;

                mailItem.Send();
                return true;

            }    
            catch (Exception ex)
            {
                evtWriter.GetFileEvents().WriteEventLogs("SendEmail:Exception:" + ex.Message, true);
                Console.WriteLine("SendEmail:Exception:" + ex.Message.ToString());
                return false;
            }
        }

        [Obsolete]
        public bool EnviaEmailFromGmail(string emailOrigem,
            string emailDestino,
            string nomeOrigem,
            string nomeDestino,
            string assunto,
            string mensagememail)
        {
            evtWriter.GetFileEvents().WriteEventLogs("Enviando email...", true);
            try
            {
                //-------------Teste com corpo de email em HTML------------------
                StringBuilder conteudo = new StringBuilder();
                string txtNome = "Jucelio";
                string txtEmail = "jucelioinfo@gmail.com";
                string txtTelefone = "32 9 8888 5555";
                string ddlServico = "ddlServico";
                string txtMensagem = "vai toma no Cu";

                conteudo.Append("O Srº(ª) " + "<b>" + txtNome + "</b>" + " entrou em contato pelo site no dia " + DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "hrs" + "<br /><br />");
                conteudo.Append("<b>" + "Nome do contato: " + "</b>" + txtNome + "<br />");
                conteudo.Append("<b>" + "Email: " + "</b>" + txtEmail + "<br />");
                conteudo.Append("<b>" + "Telefone: " + "</b>" + txtTelefone + "<br />");
                conteudo.Append("<b>" + "Tipo do serviço: " + "</b>" + ddlServico + "<br />");
                conteudo.Append("<b>" + "Mensagem: " + "</b>" + txtMensagem + "<br />");

                var senhaEmail = ConfigurationSettings.AppSettings["SENHA"];
                var hostEmail = ConfigurationSettings.AppSettings["HOST"];
                var port = ConfigurationSettings.AppSettings["PORT"];
                int portnumber = Int32.Parse(port);
                MailAddress origem = new MailAddress(emailOrigem, nomeOrigem);
                MailAddress destino = new MailAddress(emailDestino, nomeDestino);

                // cria uma mensagem
                MailMessage mensagem = new MailMessage(origem, destino);
                mensagem.Subject = assunto;
                //mensagem.Body = assunto;
                //-------------Teste com corpo de email em HTML------------------
                mensagem.Body = conteudo.ToString();
                mensagem.BodyEncoding = Encoding.Default;
                mensagem.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient smtp = new SmtpClient(hostEmail, portnumber);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(origem.Address, senhaEmail);
                smtp.Send(mensagem);
                evtWriter.GetFileEvents().WriteEventLogs("Email enviado.", true);
                Console.WriteLine("Mensagem enviada para  " + origem.Address + " às " + time.ToString(format) + ".");
                return true;
            }
            catch (Exception ex)
            {
                evtWriter.GetFileEvents().WriteEventLogs("EnviaEmailparaGmail:Exception:" + ex.Message, true);
                Console.WriteLine("EnviaEmailparaGmail:Exception:" + ex.Message.ToString());
                return false;
            }
            finally
            {

            }
        }
        public void AtualizaBancoDeDados(int id)
        {
            evtWriter.GetFileEvents().WriteEventLogs("Atualizando banco de dados...", true);
            try
            {
                clsConnection getConn = new clsConnection();
                con = getConn.getConexaoBD();
                //SqlCommand cmdupdate = new SqlCommand("UPDATE tblEnviarEmail SET status = 'S' WHERE Id = @id", con);
                SqlCommand cmdupdate = new SqlCommand("UPDATE tblEnviarEmail SET status = 'S',DataHora='" + time.ToString(format) + "' WHERE Id = @id", con);

                cmdupdate.Parameters.Add(new SqlParameter("@Id", id));

                con.Open();
                cmdupdate.ExecuteNonQuery();

                //Escrevo no arquivo texto no momento exato que o arquivo for encerrado
                evtWriter.GetFileEvents().WriteEventLogs("Banco atualizado.", true);
            }
            catch (Exception ex)
            {
                evtWriter.GetFileEvents().WriteEventLogs("AtualizaBancoDeDados:Exception:" + ex.Message, true);
                Console.WriteLine("AtualizaBancoDeDados:Exception:" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }
    }
}
