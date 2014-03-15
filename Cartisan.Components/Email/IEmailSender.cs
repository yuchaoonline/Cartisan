namespace Cartisan.Components.Email {
    public interface IEmailSender {
        void SendMail(Cartisan.Components.Email.Email email);
    }
}