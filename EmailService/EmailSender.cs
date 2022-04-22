using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void Send(Message message)
        {
            var obj = new MimeMessage();
            obj.From.Add(new MailboxAddress(_emailConfig.Username));
            obj.To.AddRange(message.To);
            obj.Subject = message.Subject;
            obj.Body = new TextPart(TextFormat.Html)
            {
                Text = message.Content
            };

            SecureSocketOptions secureSocket = SecureSocketOptions.None;
            using (var client = new SmtpClient())
            {
                switch (_emailConfig.Security)
                {
                    case "STARTTLS":
                        secureSocket = SecureSocketOptions.StartTls;
                        break;
                    case "TLS":
                        secureSocket = SecureSocketOptions.SslOnConnect;
                        break;
                    default:
                        break;
                }
                client.Connect(_emailConfig.Host, _emailConfig.Port, secureSocket);
                //Remove any OAuth functionality
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);
                client.Send(obj);
                client.Disconnect(true);
            }

        }
    }
}
