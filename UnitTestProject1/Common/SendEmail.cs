using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using OHSConnect.Common;
using OHSConnect.CodeBindings;
using NUnit.Framework;

namespace OHSConnect.Common
{
    class SendEmail
    {
        public static string To, Cc, Subj;

        private static string PopulateBody(string projectName, string passCount, string failCount, string skipCount, string errorCount, string totalCount)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(System.Configuration.ConfigurationManager.AppSettings["ReportHTMLPath"]))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("${ReportHeader}", projectName);
            body = body.Replace("${PassedTestCases}", passCount);
            body = body.Replace("${FailedTestCases}", failCount);
            body = body.Replace("${SkippedTestCases}", skipCount);
            body = body.Replace("${ErrorTestCases}", errorCount);
            body = body.Replace("${TotalTestCases}", totalCount);
            return body;
        }


        public static bool email_send(string filepath, string ErrorFilePath, string SystemMachineName, string[] EmailCollection, string projectName)
        {
            ExcelReport.ReadData Readdata = new ExcelReport.ReadData();
            try
            {
                //Console.WriteLine("In email_send method");
                List<String> to = new List<string>(); List<String> cc = new List<string>(); List<String> bcc = new List<string>(); List<String> Attachment = new List<string>();

                //Set Email To path, It will take path from App.config if available, otherwise default loction will be Input file path
                To = string.Concat((ConfigurationManager.AppSettings["ToEmail"].ToString() == "") ? Readdata.ProcessOnCollection(EmailCollection, "EmailTo") : ConfigurationManager.AppSettings["ToEmail"].ToString());

                //Set Email CC path, It will take path from App.config if available, otherwise default loction will be Input file path
                Cc = string.Concat((ConfigurationManager.AppSettings["CcEmail"].ToString() == "") ? Readdata.ProcessOnCollection(EmailCollection, "EmailCc") : ConfigurationManager.AppSettings["CcEmail"].ToString());

                //Set Email Subject path, It will take path from App.config if available, otherwise default loction will be Input file path
                Subj = string.Concat((ConfigurationManager.AppSettings["SubjectEmail"].ToString() == "") ? Readdata.ProcessOnCollection(EmailCollection, "EmailSubject") : ConfigurationManager.AppSettings["SubjectEmail"].ToString());

                //string To = ConfigurationManager.AppSettings["EmailTo"].ToString();

                //To = Readdata.ProcessOnCollection(EmailCollection, "EmailTo");

                //string Cc = ConfigurationManager.AppSettings["EmailCc"];
                // Cc = Readdata.ProcessOnCollection(EmailCollection, "EmailCC");




                //string Subj = ConfigurationManager.AppSettings["EmailSubject"];
                // Subj = Readdata.ProcessOnCollection(EmailCollection, "EmailSubject");

                string Message = string.Empty;

                //= "Hello," + Environment.NewLine + "Please find attached excel sheet to view automation test results. " + Environment.NewLine
                //      + Environment.NewLine + "You can find more details -  " + SystemMachineName + " in " + Environment.CurrentDirectory
                //     + Environment.NewLine + Environment.NewLine + "Thanks." + Environment.NewLine + "ProCare Transportation and Language Services";

                System.Text.StringBuilder body = new System.Text.StringBuilder();
                body.Clear();
                body.Length = 0;
                string bodyText = PopulateBody(projectName, TestSuit.pass.ToString(), TestSuit.fail.ToString(), TestSuit.skip.ToString(), TestSuit.error.ToString(), (TestSuit.pass + TestSuit.fail + TestSuit.skip + TestSuit.error).ToString());
                body.Append(bodyText);

                //body.Append("<table><tr><td colspan='3' class='Content'> <h3 align='center' id='reportHeader'> ${ReportHeader} </h3></td></tr>");
                // body.Append("");
                /*body.Append("<table border='0' class='subtable' cellpadding='0' cellspacing='0' style='background-color: White; font-family:Calibri;'>");
                body.Append("<tr><td colspan='12'>Hello,</td></tr>" + "\r\n");
                body.Append("<tr style='height: 6px'><td colspan='12'><br></td></tr>" + "\r\n");
                body.Append("<tr><td colspan='12'>Please find attached excel sheet to view automation test results.</td></tr>" + "\r\n");
                body.Append("<tr><td colspan='12'>You can find more details -  " + SystemMachineName + " in " + Environment.CurrentDirectory + "</td></tr>" + "\r\n");
                body.Append("<tr style='height: 6px'><td colspan='12'><br></td></tr>" + "\r\n");
                body.Append("<tr><td colspan='12'>Thanks, </td></tr>" + "\r\n");
                body.Append("<tr style='height: 6px'><td colspan='12'><br></td></tr>" + "\r\n");
                body.Append("<tr><td colspan='12' style='color:#D50000; font-size:14px; font-style:italic; font-family: Times New Roman;'>\"Tailors Mark\"</td></tr></table>" + "\r\n");*/

                Message = body.ToString();



                if (!string.IsNullOrEmpty(To))
                {
                    string[] ToMuliId = To.Split(';');
                    foreach (string ToEMailId in ToMuliId)
                    {
                        if (ToEMailId != "")
                            to.Add(ToEMailId);
                    }
                }

                if (!string.IsNullOrEmpty(Cc))
                {
                    string[] CCId = Cc.Split(';');
                    foreach (string CCEmail in CCId)
                    {
                        if (CCEmail != "")
                            cc.Add(CCEmail);
                    }
                }

                if (!string.IsNullOrEmpty(filepath))
                {
                    string[] mailAttachments = filepath.Split(';');
                    foreach (string mailAttach in mailAttachments)
                    {
                        if (mailAttach != "")
                            Attachment.Add(mailAttach);
                    }
                }

                if (!string.IsNullOrEmpty(ErrorFilePath))
                {
                    string[] mailAttachments = ErrorFilePath.Split(';');
                    foreach (string mailAttach in mailAttachments)
                    {
                        if (mailAttach != "")
                            Attachment.Add(mailAttach);
                    }
                }

                bool success;
                string ErrMsg = "";
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage = SetMailParameters(to, ConfigurationManager.AppSettings["FromEmail"].ToString(), Subj, Message, mailMessage);
                    mailMessage = SetCopyParameters(cc, bcc, mailMessage);
                    mailMessage = SetAttachmentPath(Attachment, mailMessage);
                    mailMessage.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();

                    string UserName = ConfigurationManager.AppSettings["Username"];
                    string Host = ConfigurationManager.AppSettings["EmailHost"];
                    bool Ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailSsl"]);
                    string pwd = ConfigurationManager.AppSettings["EmailPwd"];
                    string Port = ConfigurationManager.AppSettings["EmailPort"];

                    client = AuthenticateCredentials(Host, Convert.ToInt16(Port), Ssl, UserName, pwd, client);

                    //  Console.WriteLine("About to send");
                    client.Send(mailMessage);
                    success = true;
                    Console.WriteLine("Sent");
                }
                catch (Exception Ex)
                {
                    TestSuit.TakeScreenShot("Fail");
                    logger.WriteLog(Ex);
                    success = false;
                    ErrMsg = Ex.Message;
                }
                return success;
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                return false;
            }
        }

        #region Email Supporting Methods

        public static SmtpClient AuthenticateCredentials(string server, int port, bool requireSsl, string userName, string password, SmtpClient smtpClient)
        {
            SmtpClient client;
            try
            {
                smtpClient.Host = server;
                smtpClient.Port = port;
                smtpClient.EnableSsl = requireSsl;
                if ((userName.Length > 0) && (password.Length > 0))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(userName, password);
                }
                else
                {
                    //   Console.WriteLine("failed");
                }
                client = smtpClient;
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                throw;
            }
            return client;
        }


        public static MailMessage SetAttachmentPath(List<string> attachmentPath, MailMessage mailMessage)
        {
            MailMessage message;
            try
            {
                Console.WriteLine("In setattachment path.");
                if ((attachmentPath != null) && (attachmentPath.Count > 0))
                {
                    foreach (string str in attachmentPath)
                    {
                        mailMessage.Attachments.Add(new Attachment(str));
                    }
                }
                message = mailMessage;
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                throw;
            }
            return message;
        }


        public static MailMessage SetCopyParameters(List<string> cc, List<string> bcc, MailMessage mailMessage)
        {
            MailMessage message;
            try
            {
                // Console.WriteLine("In SetCopyParameters.");
                if ((cc != null) && (cc.Count > 0))
                {
                    foreach (string str in cc)
                    {
                        mailMessage.CC.Add(new MailAddress(str));
                    }
                }
                if ((bcc != null) && (bcc.Count > 0))
                {
                    foreach (string str in bcc)
                    {
                        mailMessage.Bcc.Add(new MailAddress(str));
                    }
                }
                message = mailMessage;
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                throw;
            }
            return message;
        }


        private static MailMessage SetMailParameters(List<string> to, string from, string subject, string messageBody, MailMessage mailMessage)
        {
            MailMessage message;
            try
            {
                Console.WriteLine("SetMailParameters");
                if ((to == null) || (to.Count <= 0))
                {
                    throw new Exception("The 'To-Address' was not specified");
                }
                foreach (string str in to)
                {
                    mailMessage.To.Add(new MailAddress(str));
                }
                if (string.IsNullOrEmpty(from))
                {
                    throw new Exception("The 'From-Address' was not specified");
                }
                MailAddress address = new MailAddress(from);
                mailMessage.From = address;
                if (!string.IsNullOrEmpty(subject))
                {
                    mailMessage.Subject = subject;
                }
                else
                {
                    mailMessage.Subject = string.Empty;
                }
                if (!string.IsNullOrEmpty(messageBody))
                {
                    mailMessage.Body = messageBody;
                }
                else
                {
                    mailMessage.Body = string.Empty;
                }
                message = mailMessage;
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                throw;
            }
            return message;
        }
        #endregion
    }
}
