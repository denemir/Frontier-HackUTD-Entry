public interface IChatbotServices
{
    Task<string> SendMessageToApi(string userMessage);
}
