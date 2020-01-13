using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.DTO
{
    public class UserMediaEdgeResponseDTO : InstagramResponseBaseDTO
    {
        [JsonProperty("data")]
        public List<MediaEdgeItem> _Data { get; set; }


        public class MediaEdgeItem
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("caption")]
            public string Caption { get; set; }
        }
    }
}
