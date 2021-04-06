using System;
using System.ComponentModel.DataAnnotations;
using CustomerService.Application.Enum.Application;
using JsonSubTypes;
using Newtonsoft.Json;

namespace CustomerService.Application.Dto.Customer
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ApplicationType))]
    [JsonSubtypes.KnownSubType(typeof(MrGreenCustomerDto), ApplicationType.MrGreen)]
    [JsonSubtypes.KnownSubType(typeof(RedBetCustomerDto), ApplicationType.RedBet)]
    public class BaseCustomerDto
    {
        [JsonConstructor]
        protected BaseCustomerDto() {}

        protected BaseCustomerDto(ApplicationType applicationType)
        {
            ApplicationType = applicationType;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("applicationType")]
        [Required]
        public ApplicationType ApplicationType { get; }

        [JsonProperty("firstName")]
        [Required]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        [Required]
        public string LastName { get; set; }

        [JsonProperty("address")]
        [Required]
        public CustomerAddressDto Address { get; set; }
    }
}