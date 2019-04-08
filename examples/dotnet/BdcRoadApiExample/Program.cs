using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BdcRoadApiExample
{
    class Program
    {
        private static async Task Main()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://i.centr.by/oauth2");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }


            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "bdc_road_api_resource_owner",

                UserName = "USERNAME",
                Password = "PASSWORD"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }


            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://i.centr.by/bdc-road-api/v1.0/roads/road-position?lat=53&lon=28");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
        }
    }
}
