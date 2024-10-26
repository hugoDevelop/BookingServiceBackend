using BookingServiceBackend.Attributes;
using BookingServiceBackend.Data;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace BookingServiceBackend.Middleware
{
    public class TokenValidationMiddleware : IMiddleware
    {
        private static readonly JsonWebTokenHandler jsonWebTokenHandler = new JsonWebTokenHandler();
        private readonly BookingContext _context;

        public TokenValidationMiddleware(BookingContext context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.GetEndpoint()?.Metadata.GetMetadata<Anonimo>() != null)
            {
                await next(context);
                return;
            }
            //get the token from the header
            var tokenParts = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ");
            var token = tokenParts != null && tokenParts.Length > 0 ? tokenParts[tokenParts.Length - 1] : null;
            //if token is null or empty
            if (token == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token is missing");
                return;
            }
            //extract the email of the token from unique_name
            var email = jsonWebTokenHandler.ReadJsonWebToken(token).Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

            //then, using context, retrieve the user, later the company, and finally, the company settings
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("User not found");
                return;
            }
            var company = _context.Companies.FirstOrDefault(c => c.Id == user.CompanyId);
            if (company == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Company not found");
                return;
            }

            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                company.AuthOpenIdConfigUrl,
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());

            var openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);

            var emisoresPermitidosString = _context.Companies.Where(e => e.Id == company.Id).Select(e => e.AuthIssuer).FirstOrDefault();

            if (string.IsNullOrEmpty(emisoresPermitidosString))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Configuración de emisores no encontrada");
                return;
            }

            // Separar la cadena de emisores en un array, eliminando espacios en blanco adicionales
            var emisoresPermitidos = emisoresPermitidosString.Split(',')
                .Select(emisor => emisor.Trim()) // Elimina espacios en blanco al principio y al final de cada emisor
                .Where(emisor => !emisor.Contains(',')) // Excluir elementos que contengan comas
                .ToArray();

            // Configurar la validación de tokens
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidAudiences = new[] { company.AuthAudience },
                ValidIssuers = emisoresPermitidos,
                IssuerSigningKeys = openIdConfig.SigningKeys,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
            };

            if (jsonWebTokenHandler.ValidateToken(token, tokenValidationParameters).IsValid)
            {
                context.Items["email"] = email;
                context.Items["companyID"] = company.Id;
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
            }
        }
    }
}
