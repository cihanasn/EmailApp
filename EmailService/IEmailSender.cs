namespace EmailService
{
    public interface IEmailSender
    {
        void Send(Message message);
    }
}
