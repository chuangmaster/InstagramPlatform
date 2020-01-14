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

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        private DateTime ExpireDate
        {
            get
            {
                if (this.ExpiresIn == 0)
                {
                    return CreateDate.AddDays(1);
                }
                else
                {
                    return CreateDate.AddSeconds(this.ExpiresIn);
                }
            }
        }

        private DateTime CreateDate;

        public ExchangeTokenResponseDTO()
        {
            this.CreateDate = DateTime.UtcNow;
        }
    }
}
