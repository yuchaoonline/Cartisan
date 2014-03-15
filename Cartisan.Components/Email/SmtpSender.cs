using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Cartisan.Components.Email {
    public class SmtpSender: IEmailSender {
        public void SendMail(Cartisan.Components.Email.Email email) {
            using (var smtpClient = new SmtpClient()) {
                var templatePath = string.Format("{0}\\{1}", ConfigurationManager.AppSettings["Email.Template"], email.Template);

                SmtpClientSetting(smtpClient);

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email.From"], ConfigurationManager.AppSettings["Email.DisplayName"]);

                mailMessage.Subject = email.Subject;

                foreach (var to in email.To) {
                    mailMessage.To.Add(to);
                }

                foreach (var cc in email.CC) {
                    mailMessage.CC.Add(cc);
                }

                AddTxtView(email, mailMessage, templatePath);

                var gifs = Directory.GetFiles(templatePath, "*.gif");
                var jpgs = Directory.GetFiles(templatePath, "*.jpg");
                var pngs = Directory.GetFiles(templatePath, "*.png");

                string htmlTemplatePath = string.Format("{0}\\{1}.html", templatePath, email.Template);
                string htmlTemplate;

                htmlTemplate = ReadFile(htmlTemplatePath);
                htmlTemplate = ReplaceTemplate(email, htmlTemplate);

                htmlTemplate = gifs.Aggregate(htmlTemplate,
                    (current, gif) => {
                        var fileName = gif.Replace(templatePath + "\\", "");
                        return current.Replace(fileName, string.Format("cid:{0}", fileName));
                    });

                htmlTemplate = jpgs.Aggregate(htmlTemplate,
                    (current, jpg) => {
                        var fileName = jpg.Replace(templatePath + "\\", "");
                        return current.Replace(fileName, string.Format("cid:{0}", fileName));
                    });

                htmlTemplate = pngs.Aggregate(htmlTemplate,
                    (current, png) => {
                        var fileName = png.Replace(templatePath + "\\", "");
                        return current.Replace(fileName, string.Format("cid:{0}", fileName));
                    });

                var htmlBody = AlternateView.CreateAlternateViewFromString(htmlTemplate, null, "text/html");

                gifs.ToList().ForEach(gif => htmlBody.LinkedResources.Add(new LinkedResource(gif) {
                    ContentId = gif.Replace(templatePath + "\\", "")
                }));

                jpgs.ToList().ForEach(jpg => htmlBody.LinkedResources.Add(new LinkedResource(jpg) {
                    ContentId = jpg.Replace(templatePath + "\\", "")
                }));

                pngs.ToList().ForEach(png => htmlBody.LinkedResources.Add(new LinkedResource(png) {
                    ContentId = png.Replace(templatePath + "\\", "")
                }));

                mailMessage.AlternateViews.Add(htmlBody);

                smtpClient.Send(mailMessage);
            }
        }

        private static void AddTxtView(Cartisan.Components.Email.Email email, MailMessage mailMessage, string templatePath) {
            var txtTemplatePath = string.Format("{0}\\{1}.txt", templatePath, email.Template);
            if (File.Exists(txtTemplatePath)) {
                string txtTemplate;

                txtTemplate = ReadFile(txtTemplatePath);

                txtTemplate = ReplaceTemplate(email, txtTemplate);

                mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(txtTemplate, null, "text/html"));
            }
        }

        private static void SmtpClientSetting(SmtpClient smtpClient) {
            smtpClient.Host = ConfigurationManager.AppSettings["Email.ServerName"];
            smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["Email.ServerPort"]);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email.UserName"],
                ConfigurationManager.AppSettings["Email.Password"]);
        }

        private static string ReplaceTemplate(Cartisan.Components.Email.Email email, string txtTemplate) {
            txtTemplate = email.Params.Aggregate(txtTemplate,
                (current, param) => current.Replace(string.Format("${{{0}}}", param.Key), param.Value));
            return txtTemplate;
        }

        private static string ReadFile(string templatePath) {
            string txtTemplate;
            using (var reader = new StreamReader(templatePath)) {
                txtTemplate = reader.ReadToEnd();
            }
            return txtTemplate;
        }
    }
}