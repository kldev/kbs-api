using Newtonsoft.Json;

namespace KBS.Web.Infrastructure
{
    [JsonObject("jwtConfig")]
    public class JwtConfig
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}