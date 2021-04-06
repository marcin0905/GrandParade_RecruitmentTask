using System.ComponentModel.DataAnnotations;
using CustomerService.Application.Enum.Application;
using Newtonsoft.Json;

namespace CustomerService.Application.Dto.Customer
{
    public class MrGreenCustomerDto : BaseCustomerDto
    {
        public MrGreenCustomerDto() : base(ApplicationType.MrGreen)
        {
        }

        [JsonProperty("personalNumber")]
        [Required]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Personal number has invalid format. (XXXXX-XXX)")]
        public string PersonalNumber { get; set; }
    }
}