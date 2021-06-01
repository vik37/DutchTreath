namespace Dutch.Services
{
    public interface IMailService
    {
        public void SendMessage(string to, string subject, string body);
        
    }
}