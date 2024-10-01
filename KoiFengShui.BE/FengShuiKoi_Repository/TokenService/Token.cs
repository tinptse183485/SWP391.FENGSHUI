using FengShuiKoi_BO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KoiFengShui.BE.TokenService
{
	public class Token : IToken
	{
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;

		public Token(IConfiguration config)
		{
			_config = config;
			var signingKey = _config["JWT:SigningKey"];
			if (string.IsNullOrEmpty(signingKey))
			{
				throw new InvalidOperationException("JWT:SigningKey is not configured in appsettings.json");
			}
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
		}

		public string CreateToken(Account user)
		{
			var authClaims = new List<Claim>
			{
				new Claim(CustomClaimTypes.UserId, user.UserId),
				new Claim(ClaimTypes.Email, user.Email ?? ""),
				new Claim(CustomClaimTypes.Role, user.Role ?? "")
			};

			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new JwtSecurityToken(
				issuer: _config["JWT:Issuer"],
				audience: _config["JWT:Audience"],
				claims: authClaims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
		}
	}

	public static class CustomClaimTypes
	{
		public const string Role = "Role";
		public const string UserId = "UserId";
	}
}
