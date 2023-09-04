using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using ASPIspit.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASPIspit.Api.Core
{
    public class JwtManager
    {
        private readonly AspIspitContext _context;
        //private readonly JwtSettings _settings;

        public JwtManager(AspIspitContext context)
        {
            _context = context;
        }

        public string MakeToken(string username, string password)
        {
            var user = _context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            //var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            //if (!valid)
            //{
            //    throw new UnauthorizedAccessException();
            //}

            //var useCases = _context.UserUseCase.Where(x => x.UserId == user.UserId).Select(x => x.UseCaseId);

            var actor = new JwtUser
            {
                Id = user.Id,
                UseCaseIds = user.UserUseCases.Select(x => x.UseCaseId).ToList(),
                Identity = user.Username,
                Username = user.Username
            };

            var issuer = "AspIspit";
            var secret = "1233asidhjakldjkas12";

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, issuer),
                new Claim("UseCases", JsonConvert.SerializeObject(actor.UseCaseIds)),
                new Claim("Username", user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(10000),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
