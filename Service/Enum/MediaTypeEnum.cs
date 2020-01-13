using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Service.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaTypeEnum
    {
        IMAGE = 1,
        VIDEO,
        CAROUSEL_ALBUM
    }
}