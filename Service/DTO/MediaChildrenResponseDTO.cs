using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.DTO
{
    public class MediaChildrenResponseDTO : InstagramResponseBaseDTO.Paging
    {
        [JsonProperty("data")]
        public List<ChildItem> Data { get; set; }

        public class ChildItem
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }
    }
}
