using System.ComponentModel.DataAnnotations;
using CustomerService.Application.Enum.Application;
using Newtonsoft.Json;

namespace CustomerService.Application.Dto.Customer
{
    public class RedBetCustomerDto : BaseCustomerDto
    {
        public RedBetCustomerDto() : base(ApplicationType.RedBet)
        {
        }

        [JsonProperty("favoriteFootballTeam")]
        [Required]
        public string FavoriteFootballTeam { get; set; }
    }
}