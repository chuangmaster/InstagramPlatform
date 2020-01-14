using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.DTO
{
    public class RefreshTokenResponseDTO : ExchangeTokenResponseDTO
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
