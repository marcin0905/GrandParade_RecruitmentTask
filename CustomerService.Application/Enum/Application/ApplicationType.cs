using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomerService.Application.Enum.Application
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApplicationType
    {
        [EnumMember(Value = "None")]
        None,

        [EnumMember(Value = "MrGreen")]
        MrGreen,

        [EnumMember(Value = "RedBet")]
        RedBet
    }
}