using System.Net.Mail;

namespace CoreEmlakApp.Areas.User.MailHelper
{
    public class ResetPassword
    {
        public static void PasswordSendMail(string link)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            message.From=new MailAddress("system@mail.com");

            message.To.Add("stateproje114@mail.com");
            message.Subject = "Password Updateing Request";
            message.Body = "<h2> Click link for update your password</h2>";
            message.Body += $"<a href='{link}'> Password  renewal link";

            message.IsBodyHtml = true;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("stateproje114@mail.com", "stateproje123");
            smtpClient.Send(message);
        }
    }
}
