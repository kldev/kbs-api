using System;
using Newtonsoft.Json;

namespace KBS.Web.Infrastructure {
    [JsonObject ("CorsConfig")]
    public class CorsConfig {
        [JsonProperty ("Urls")]
        public string Urls { get; set; } = "";

        [JsonIgnore]
        public string[] Allowed {
            get {
                if (!string.IsNullOrEmpty ((Urls))) {
                    return Urls.Split (",", StringSplitOptions.RemoveEmptyEntries);
                }

                return new string[] { };
            }
        }
    }
}
