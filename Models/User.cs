using BookingServiceBackend.Models.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;

namespace BookingServiceBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required Rol Rol { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
