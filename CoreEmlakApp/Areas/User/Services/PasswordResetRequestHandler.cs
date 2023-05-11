namespace CoreEmlakApp.Areas.User.Services
{
    public class PasswordResetRequestHandler
    {
        private RabbitMQHelper _rabbitMQHelper;

        public PasswordResetRequestHandler(RabbitMQHelper rabbitMQHelper)
        {
            _rabbitMQHelper = rabbitMQHelper;
        }

        public void StartHandling()
        {
            _rabbitMQHelper.ConsumePasswordResetRequest((email, passwordResetLink) =>
            {
                MailHelper.ResetPassword.PasswordSendMail(passwordResetLink);
            });
        }
    }
}
