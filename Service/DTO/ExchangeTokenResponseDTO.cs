using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class ExchangeTokenResponseDTO
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
