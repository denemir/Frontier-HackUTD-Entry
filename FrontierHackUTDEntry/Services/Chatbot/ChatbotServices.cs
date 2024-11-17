using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FrontierHackUTDEntry.Services.Chatbot
{
    public class ChatbotServices : IChatbotServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "<api-key>";

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

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);
            StringBuilder fullResponse = new StringBuilder();

            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();

                // Ensure the line starts with "data:" and isn't null or empty
                if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("data:"))
                {
                    // Strip "data:" prefix
                    var jsonPart = line.Substring(5).Trim();

                    // Skip parsing if the part is an empty string
                    if (string.IsNullOrEmpty(jsonPart))
                        continue;

                    try
                    {
                        // Attempt to parse the JSON part
                        var parsedPart = JsonConvert.DeserializeObject<dynamic>(jsonPart);
                        var parsed = parsedPart?.choices?[0]?.delta?.content;
                        if (parsed != null)
                        {
                            fullResponse.Append(parsed.ToString());
                        }
                    }
                    catch (JsonReaderException ex)
                    {
                        Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                        // Log or handle parsing errors appropriately
                    }
                }
            }

            return fullResponse.ToString();
        }
    }
}
