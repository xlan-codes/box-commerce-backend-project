using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CmsExportAuthorizationAttribute : TypeFilterAttribute
{
    public CmsExportAuthorizationAttribute() : base(typeof(CmsExportAuthorizationFilter))
    {
    }

    private class CmsExportAuthorizationFilter : IAuthorizationFilter
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OIDCSettings _settings;

        public CmsExportAuthorizationFilter( IHttpClientFactory httpClientFactory, IOptions<OIDCSettings> settings)
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Query["authorization"];


            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var client = _httpClientFactory.CreateClient();

            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
                {

                    var json = Task.Run(async () => await client.GetStringAsync(string.Concat(_settings.Url +"/.well-known/openid-configuration/jwks")));
                    List<SecurityKey> keys = new List<SecurityKey>();
                    keys.Add(new JsonWebKeySet(json.Result).Keys.First());
                    return keys;
                },
                ValidIssuer = _settings.Url,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero

            };

            try
            {
                _jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}