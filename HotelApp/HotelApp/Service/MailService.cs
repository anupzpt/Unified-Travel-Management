using System.Net.Mail;
using System.Net;

namespace HotelApp.Service
{
    public class MailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        public MailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, string logoPath, string FullName)
        {
            var message = new MailMessage();
            message.To.Add(toEmail);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.From = new MailAddress(_smtpUser);

            // Load the HTML template
            string htmlTemplate = File.ReadAllText("Views/Tour/EmailView.cshtml");
            htmlTemplate = htmlTemplate.Replace("{{UserName}}", FullName);
            htmlTemplate = htmlTemplate.Replace("{{BodyContent}}", body);

            // Create an alternate view for the email with embedded images
            var alternateView = AlternateView.CreateAlternateViewFromString(htmlTemplate, null, "text/html");
            LinkedResource logo = new LinkedResource(logoPath)
            {
                ContentId = "logo"
            };
            alternateView.LinkedResources.Add(logo);
            message.AlternateViews.Add(alternateView);

            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true; // Enable SSL
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                await client.SendMailAsync(message);
            }
        }
    }
}
