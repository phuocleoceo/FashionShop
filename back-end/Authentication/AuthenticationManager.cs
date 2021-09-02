using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using back_end.DTO;
using back_end.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace back_end.Authentication
{
	public class AuthenticationManager : IAuthenticationManager
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;

		public AuthenticationManager(UserManager<User> userManager,
									IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}
		public async Task<User> ValidateUser(UserForAuthenticationDTO userForAuth)
		{
			User _user = await _userManager.FindByNameAsync(userForAuth.UserName);
			if (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password))
			{
				return _user;
			}
			else
			{
				return null;
			}
		}

		// ACCESS TOKEN
		public async Task<IEnumerable<Claim>> GetClaims(User _user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, _user.UserName)
			};
			IList<string> roles = await _userManager.GetRolesAsync(_user);
			foreach (string role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			return claims;
		}

		private SigningCredentials GetSigningCredentials()
		{
			string _configKey = _configuration.GetSection("JwtSettings:secretKey").Value;
			byte[] key = Encoding.UTF8.GetBytes(_configKey);
			SymmetricSecurityKey secret = new SymmetricSecurityKey(key);
			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
														IEnumerable<Claim> claims)
		{
			IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");
			JwtSecurityToken tokenOptions = new JwtSecurityToken
			(
				issuer: jwtSettings.GetSection("validIssuer").Value,
				audience: jwtSettings.GetSection("validAudience").Value,
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
				signingCredentials: signingCredentials
			);
			return tokenOptions;
		}

		public string CreateAccessToken(IEnumerable<Claim> claims)
		{
			SigningCredentials signingCredentials = GetSigningCredentials();
			JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		// REFRESH TOKEN
		public string CreateRefreshToken()
		{
			byte[] randomNumber = new byte[32];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			string _configKey = _configuration.GetSection("JwtSettings:secretKey").Value;
			byte[] key = Encoding.UTF8.GetBytes(_configKey);
			TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
			};

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;
			ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
			JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

			if (jwtSecurityToken == null ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
										StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");
			return principal;
		}
	}
}