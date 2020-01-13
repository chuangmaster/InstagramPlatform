using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.DTO
{
    public class InstagramResponseBaseDTO
    {
        [JsonProperty("paging")]
        public Paging _Paging { get; set; }

        public class Paging
        {
            [JsonProperty("cursors")]
            public Cursors _Cursors { get; set; }

            [JsonProperty("previous")]
            public string Previous { get; set; }

            [JsonProperty("next")]
            public string Next { get; set; }

            public class Cursors
            {
                [JsonProperty("after")]
                public string After { get; set; }

                [JsonProperty("before")]
                public string Before { get; set; }
            }
        }
    }
}
