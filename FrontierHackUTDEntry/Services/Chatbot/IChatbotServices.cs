using FrontierHackUTDEntry.Services.Chatbot;

public interface IChatbotServices
{
    Task<string> SendMessageToApi(string userMessage);
}
