using BookingServiceBackend.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookingServiceBackend.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required AuthMethod AuthMethod { get; set; }
        public string? AuthUrl { get; set; }
        public string? AuthClientId { get; set; }
        public string? AuthClientSecret { get; set; }
        public string? Schema { get; set; }
        public string? AuthOpenIdConfigUrl { get; set; }
        public string? AuthRedirectUrl { get; set; }
        public string? AuthRedirectUrlMobile { get; set; }
        public string? AuthScope { get; set; }
        public string? AuthAudience { get; set; }
        public string? AuthIssuer { get; set; }
        public string? AuthTokenUrl { get; set; }
    }
}
