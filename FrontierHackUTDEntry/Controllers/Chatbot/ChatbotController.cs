namespace FrontierHackUTDEntry.Controllers.Chatbot
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using FrontierHackUTDEntry.Services.Chatbot;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotServices _chatbotService;

        public ChatbotController(IChatbotServices chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userMessage)
        {
            var responseContent = await _chatbotService.SendMessageToApi(userMessage);
            return Ok(responseContent);
        }

        //[HttpPost]
        //public void SetPromptFromAcctId(string acctId)
        //{
        //    _chatbotService.SetPromptFromAcctId(acctId);
        //}
    }


}
