using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FrontierHackUTDEntry.Services.Chatbot
{
    public class ChatbotServices : IChatbotServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "0b64b29e-f81e-43ca-8c77-b81eb9079d74";

        public ChatbotServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendMessageToApi(string userMessage)
        {
            var apiUrl = "https://api.sambanova.ai/v1/chat/completions";
            var requestBody = new
            {
                stream = true,
                model = "Meta-Llama-3.1-8B-Instruct",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant" },
                new { role = "user", content = userMessage }
            }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
