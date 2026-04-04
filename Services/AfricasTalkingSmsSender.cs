using Contracts.Service;
using Entities.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AfricasTalkingSmsSender : ISmsSender
    {
        private readonly SmsSettings _settings;
        private readonly HttpClient _http;

        public AfricasTalkingSmsSender( IOptions<SmsSettings> options,
            HttpClient http)
        {
            _settings = options.Value;
            _http = http;
        }

        public async Task SendAsync(string phoneNumber, string message)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["username"] = _settings.Username,
                ["to"] = phoneNumber,
                ["message"] = message,
                ["from"] = _settings.SenderId
            });

            _http.DefaultRequestHeaders.Clear();
            //_http.DefaultRequestHeaders.Add("apiKey", _settings.ApiKey);
            //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("apiKey", _settings.ApiKey);
            _http.DefaultRequestHeaders.Add("apiKey", _settings.ApiKey);


            var response = await _http.PostAsync(
                $"{_settings.BaseUrl}/version1/messaging",
                content);

            if (!response.IsSuccessStatusCode)
            {//remove this 
                throw new Exception("SMS sending failed");
            }
        }
    }

}
