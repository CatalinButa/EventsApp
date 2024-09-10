using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace SoundSphere.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController() { }

        protected int GetLoggedInUserId()
        {
            string? rawToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            string? token = rawToken?.Substring("Bearer ".Length).Trim();
            JwtSecurityToken parserToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string? rawUserId = parserToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            if (rawUserId == null)
            {
                throw new Exception("Invalid user id claim");
            }
            return int.Parse(rawUserId);
        }
    }
}