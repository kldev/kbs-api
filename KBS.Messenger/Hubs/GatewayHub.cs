using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KBS.Messenger.Hubs {
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GatewayHub : Hub<IGatewayClient> {
        private readonly IConfiguration _configuration;
        private ILogger<GatewayHub> _logger;

        public GatewayHub(IConfiguration configuration, ILogger<GatewayHub> logger) {
            this._configuration = configuration;
            this._logger = logger;
        }
        public async Task Proxy(string path, string payload, string method, string token, string guid) {
            // TODO: 
            var url = this._configuration["Kbs:Api"];
            var result = string.Empty;

            try {
                using var http = new HttpClient ( );
                _logger.LogInformation ($"Call api: {url}{path}");
                http.BaseAddress = new Uri (url);
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", token);

                if (string.IsNullOrEmpty (method) || method.ToLower ( ) == "get") {

                    result = await http.GetStringAsync (path);
                }
                else if (method.ToLower ( ) == "post") {

                    var postResult = await http.PostAsync (path, new StringContent (payload));

                    if (postResult.StatusCode == HttpStatusCode.OK) {
                        result = await postResult.Content.ReadAsStringAsync ( );
                    }
                }
            }
            catch (Exception ex) {
                _logger.LogError ($"Error calling api: {ex.Message}");
            }


            await Clients.Caller.ResponseProxy (guid, result);
        }
    }



}

