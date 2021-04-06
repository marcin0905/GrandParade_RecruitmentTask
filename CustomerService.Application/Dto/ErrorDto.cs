using Newtonsoft.Json;

namespace CustomerService.Application.Dto
{
    public class ErrorDto
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message ")]
        public string Message { get; set; }
    }
}