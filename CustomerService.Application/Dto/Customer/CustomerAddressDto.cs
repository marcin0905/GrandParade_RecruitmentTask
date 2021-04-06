using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CustomerService.Application.Dto.Customer
{
    public class CustomerAddressDto
    {
        [JsonProperty("street")]
        [Required]
        public string Street { get; set; }

        [JsonProperty("number")]
        [Required]
        public int Number { get; set; }

        [JsonProperty("zipCode")]
        [Required]
        public string ZipCode { get; set; }

        [JsonProperty("city")]
        [Required]
        public string City { get; set; }
    }
}