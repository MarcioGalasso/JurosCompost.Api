using Microsoft.Extensions.Configuration;
using SoftPlan.Juros.Domain;
using SoftPlan.Juros.Domain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace SoftPlan.Juros.Test
{
    public class AuthorizationServerTest
    {
        IConfiguration configuration;
        public AuthorizationServerTest()
        {
            Start.BindServices();
            configuration = Kernel.Get<IConfiguration>();
        }

        [Fact]
        public void AuthorizationToken()
        {
            var url = configuration.GetValue<string>("AuthorizationServer:Authority");
            Uri authorizationServerTokenIssuerUri = new Uri(url +"/connect/token");
            string clientId = "Softplan.Juros.Api";
            string clientSecret = "secretSoftplan";
            string scope = "scope.juros";

            //access token request
            string rawJwtToken = RequestTokenToAuthorizationServer(
                 authorizationServerTokenIssuerUri,
                 clientId,
                 scope,
                 clientSecret)
                .GetAwaiter()
                .GetResult();

            AuthorizationServerAnswer authorizationServerToken;
            authorizationServerToken = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizationServerAnswer>(rawJwtToken);
            Assert.NotNull(authorizationServerToken.access_token);

        }

        private async Task<string> RequestTokenToAuthorizationServer(Uri uriAuthorizationServer, string clientId, string scope, string clientSecret)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, uriAuthorizationServer);
                HttpContent httpContent = new FormUrlEncodedContent(
                    new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("scope", scope),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                    });
                tokenRequest.Content = httpContent;
                responseMessage = await client.SendAsync(tokenRequest);
            }
            return await responseMessage.Content.ReadAsStringAsync();
        }

        private class AuthorizationServerAnswer
        {
            public string access_token { get; set; }
            public string expires_in { get; set; }
            public string token_type { get; set; }

        }


    }
   
}
