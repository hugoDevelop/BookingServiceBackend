using BookingServiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceBackend.Utils
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var restaurantCompany = new Company
            {
                Id = 1,
                Name = "Restaurant Company",
                AuthMethod = Models.Enums.AuthMethod.Azure,
                AuthUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize",
                AuthTokenUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/token",
                AuthAudience = "api://b863ee15-3508-4086-8f5c-c58438ce4e86",
                AuthIssuer = "https://sts.windows.net/1859b7bf-2192-44b7-9ca5-7af2a00d298d/",
                AuthScope = "api://b863ee15-3508-4086-8f5c-c58438ce4e86/usr-tst",
                AuthClientId = "b863ee15-3508-4086-8f5c-c58438ce4e86",
                Schema = "com.booking.app",
                AuthRedirectUrl = "http://localhost:61770/callback",
                AuthRedirectUrlMobile = "com.booking.app://callback",
                AuthOpenIdConfigUrl = "https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration",
            };

            _modelBuilder.Entity<Company>().HasData(restaurantCompany);

            _modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@userbookingservicetestoutlo.onmicrosoft.com",
                    Rol = 0,
                    CompanyId = 1
                }
            );


        }
    }
}
