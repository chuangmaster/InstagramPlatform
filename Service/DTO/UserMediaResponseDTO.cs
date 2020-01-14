using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Service.Enum;

namespace Service.DTO
{
    public class UserMediaResponseDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("media_type")]
        public MediaTypeEnum MediaType { get; set; }

        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// The Media's thumbnail image URL. Only available on VIDEO Media.
        /// </summary>
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// The Media's permanent URL.
        /// </summary>
        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }
}
