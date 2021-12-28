using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using back_end.Models;
using back_end.DTO;

namespace back_end.Authentication;

public interface IAuthenticationManager
{
    Task<User> ValidateUser(UserForAuthenticationDTO userForAuth);

    Task<IEnumerable<Claim>> GetClaims(User _user);

    string CreateAccessToken(IEnumerable<Claim> claims);

    string CreateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
