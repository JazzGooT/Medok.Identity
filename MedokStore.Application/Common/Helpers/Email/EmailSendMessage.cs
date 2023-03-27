using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MedokStore.Application.Common.Helpers.Email
{
    public class EmailSendMessage
    {
        private readonly static MimeMessage message = new MimeMessage();

        /// <summary>
        /// Method for sending a message to the user's mail
        /// </summary>
        /// <param name="email">Email to which the message will be sent</param>
        /// <param name="name">The name of the user to whom the message will be sent</param>
        /// <param name="code">The code that will be sent</param>
        /// <returns></returns>
        public static string EmailSender(string email, string name, string code)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            var path = "H:\\Project\\Medok\\Medok.Identity\\MedokStore.Application\\Common\\Helpers\\Email\\message.html";

            message.From.Add(new MailboxAddress(configuration["EmailSettings:Name"], configuration["EmailSettings:Email"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = $"Medok Karpatskyj, {name} ";
            message.Body = new BodyBuilder()
            {
                HtmlBody = File.ReadAllText(path).Replace("**username**", name).Replace("**email**", email).Replace("**code**", code)
            }.ToMessageBody();

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(configuration["EmailSettings:EmailHost"], 465, true);
                client.Authenticate(configuration["EmailSettings:Email"], configuration["EmailSettings:Password"]);
                client.Send(message);
                client.Dispose();
            }
            return $"Succeeded";
        }
    }
}
