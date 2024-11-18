using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FrontierHackUTDEntry.Services.Chatbot
{
    public class ChatbotServices : IChatbotServices
    {
        //public ICustomerService _customerService;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "<api-key>";
        private string prompt = "You are a Frontier Internet support chat bot. The user's network consists of the following attributes:\r\n\"acct_id, extenders, wireless_clients_count, wired_clients_count, rx_avg_bps, tx_avg_bps, rx_p95_bps, tx_p95_bps, rx_max_bps, tx_max_bps, rssi_mean, rssi_median, rssi_max, rssi_min, network_speed, city, state, whole_home_wifi, wifi_security, wifi_security_plus, premium_tech_pro, identity_protection, family_identity_protection, total_shield, youtube_tv\"\r\nHere are the values in the same order:\r\n'00019f1459bdfbdae7d3d172c4a8b5bf01955e83ea18febafd8ecb2ecaef0704', '0', '18', '1', '413874.95263157896000000000', '4854.06842105263100000000', '1161072.900000000100000', '13906.526315789473000', '1782036.600000000300000', '48182.668421052630000', '-58.40', '-60.00', '-39.00', '-82.00', '1000.0M', 'WESLEY CHAPEL', 'FL', '1', '1', '0', '0', '0', '0', '0', '0'\r\nThese are statistics about the network, or products that they have purchased.\r\nYour initial message will be as follows:\r\n\"Hi! How can I be of assistance?\r\n1. My internet is too slow.\r\n2. I'm paying too much.\r\n3. My physical hardware broke.\r\nOr feel free to ask me anything else!\"\r\nIf the user answers for option 1, use the user's network information to diagnose a solution in 2-3 sentences that is easy for a non-technical user to understand.\r\nIf the user answers for option 2, they may choose from Fiber 500, 1 gig, 2 gig, 5 gig, and 7 gig. If they aren't fully using their bandwidth, suggest saving money by downgrading instead of canceling the plan. Otherwise, it is up to you to come up with a good answer.\r\nIf the user answers for option 3, walk them through how they may fix it.\r\nIf the user doesn't answer for any of these options, you may decide what is best for the conversation. Please make sure that there are new line characters between the list of options and the sentences, as well as indenting the options to make it easier to read as well.";

        public ChatbotServices(HttpClient httpClient/*, CustomerService customerService*/)
        {
            _httpClient = httpClient;
            //_customerService = customerService;
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
            new { role = "system", content = prompt },
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

        //public async void SetPromptFromAcctId(string acctId)
        //{
        //    var customerData = await _customerService.GetCustomerByIdAsync(acctId);
        //    prompt = "You are a Frontier Internet support chat bot. The user's network consists of the following attributes:\r\n\"acct_id, extenders, wireless_clients_count, wired_clients_count, rx_avg_bps, tx_avg_bps, rx_p95_bps, tx_p95_bps, rx_max_bps, tx_max_bps, rssi_mean, rssi_median, rssi_max, rssi_min, network_speed, city, state, whole_home_wifi, wifi_security, wifi_security_plus, premium_tech_pro, identity_protection, family_identity_protection, total_shield, youtube_tv\"\r\n\nThese are statistics about the network, or products that they have purchased.\r\nYour initial message will be as follows:\r\n\"Hi! How can I be of assistance?\r\n1. My internet is too slow.\r\n2. I'm paying too much.\r\n3. My physical hardware broke.\r\nOr feel free to ask me anything else!\"\r\nIf the user answers for option 1, use the user's network information to diagnose a solution in 2-3 sentences that is easy for a non-technical user to understand.\r\nIf the user answers for option 2, they may choose from Fiber 500, 1 gig, 2 gig, 5 gig, and 7 gig. If they aren't fully using their bandwidth, suggest saving money by downgrading instead of canceling the plan. Otherwise, it is up to you to come up with a good answer.\r\nIf the user answers for option 3, walk them through how they may fix it.\r\nIf the user doesn't answer for any of these options, you may decide what is best for the conversation. Please make sure that there are new line characters between the list of options and the sentences, as well as indenting the options to make it easier to read as well. Because the user is logged in, please use this information to guide your answers: " + customerData.ToString();
        //}
    }
}
