using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using back_end.Authentication;
using back_end.DTO;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace back_end.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IAuthenticationManager _authManager;
		private readonly IConfiguration _configuration;

		public AuthenticationController(IMapper mapper, UserManager<User> userManager,
								IAuthenticationManager authManager, IConfiguration configuration)
		{
			_mapper = mapper;
			_userManager = userManager;
			_authManager = authManager;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserForRegistrationDTO
																	userForRegistration)
		{
			User user = _mapper.Map<User>(userForRegistration);
			IdentityResult result = await _userManager.CreateAsync(user, userForRegistration.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}
			await _userManager.AddToRoleAsync(user, userForRegistration.Role); //Roles : many / Role : one
			return StatusCode(((int)HttpStatusCode.Created)); //201
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserForAuthenticationDTO user)
		{
			User _user = await _authManager.ValidateUser(user);
			if (_user == null)
			{
				return Unauthorized();  // 401 Unauthorized
			}
			// Create TOKEN
			IEnumerable<Claim> claims = await _authManager.GetClaims(_user);
			string accessToken = _authManager.CreateAccessToken(claims);
			string refreshToken = _authManager.CreateRefreshToken();

			// Save RefreshToken To DB
			_user.RefreshToken = refreshToken;
			string refreshTokenExpiryTime = _configuration.GetSection("JwtSettings:refreshTokenExpires").Value;
			_user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(refreshTokenExpiryTime));
			await _userManager.UpdateAsync(_user);

			IList<string> roles = await _userManager.GetRolesAsync(_user);
			var userInfor = new
			{
				Id = _user.Id,
				Name = _user.Name,
				Address = _user.Address,
				UserName = _user.UserName,
				Email = _user.Email,
				PhoneNumber = _user.PhoneNumber,
				Role = roles[0]
			};

			return Ok(new
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				User = userInfor
			});
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh(TokenAPI tokenAPI)
		{
			if (tokenAPI == null)
			{
				return BadRequest("Invalid client request");
			}

			string accessToken = tokenAPI.AccessToken;
			string refreshToken = tokenAPI.RefreshToken;
			ClaimsPrincipal principal = _authManager.GetPrincipalFromExpiredToken(accessToken);
			string username = principal.Identity.Name;

			User _user = await _userManager.FindByNameAsync(username);
			if (_user == null || _user.RefreshToken != refreshToken ||
				_user.RefreshTokenExpiryTime <= DateTime.Now)
			{
				return BadRequest("Invalid client request or refresh token expired");
			}

			string newAccessToken = _authManager.CreateAccessToken(principal.Claims);
			string newRefreshToken = _authManager.CreateRefreshToken();
			_user.RefreshToken = newRefreshToken; // Not change ExpireDay, it's only changed when Re-Login or Revoke
			await _userManager.UpdateAsync(_user);

			IList<string> roles = await _userManager.GetRolesAsync(_user);
			var userInfor = new
			{
				Id = _user.Id,
				Name = _user.Name,
				Address = _user.Address,
				UserName = _user.UserName,
				Email = _user.Email,
				PhoneNumber = _user.PhoneNumber,
				Role = roles[0]
			};
			return Ok(new
			{
				AccessToken = newAccessToken,
				RefreshToken = newRefreshToken,
				User = userInfor
			});
		}

		[HttpPost("revoke")]
		[Authorize]
		public async Task<IActionResult> Revoke()
		{
			string username = User.Identity.Name;
			User _user = await _userManager.FindByNameAsync(username);
			if (_user == null)
			{
				return BadRequest("Revoke refresh token fail");
			}

			_user.RefreshToken = null;
			_user.RefreshTokenExpiryTime = new DateTime();
			await _userManager.UpdateAsync(_user);
			return NoContent();
		}
	}
}