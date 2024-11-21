using System.Net;
using System.Net.Mail;

namespace SelifyApi.Services;

public class EmailService(IConfiguration config)
{
    
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailSettings = config.GetSection("EmailSettings");

        var client = new SmtpClient(emailSettings["SmtpServer"], Convert.ToInt32(emailSettings["Port"]))
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"])
        };

            var mailMessage = new MailMessage()
            {
                Body = message,
                From = new MailAddress(emailSettings["SenderEmail"]!),
                Subject = subject,
                IsBodyHtml = true
            };
            
            mailMessage.To.Add(email);
            
        await client.SendMailAsync(mailMessage);
    }
    
}