using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.Models;
using Fitweb.Infrastructure.Identity.External.Facebook.Settings;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.External.Facebook.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInformationUrl = "https://graph.facebook.com/me?fields={0}&access_token={1}";

        private readonly FacebookSettings _facebookSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuthService(FacebookSettings facebookSettings, IHttpClientFactory httpClientFactory)
        {
            _facebookSettings = facebookSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FacebookUserInformationResult> GetUserInformationAsync(string accessToken)
        {
            var formattedUrl = string.Format(UserInformationUrl, string.Join(",", _facebookSettings.Fields), accessToken);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            if (!result.IsSuccessStatusCode)
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUserInformationResult>(responseAsString);
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(TokenValidationUrl, accessToken,
                _facebookSettings.AppId, _facebookSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            if (!result.IsSuccessStatusCode)
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }
    }
}
