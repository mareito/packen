using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using score.Data;
using score.Dto;
using score.Services.UserService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace score.Services.SecurityService
{

    /// <summary>
    /// Class that implements ISecurityService interface
    /// </summary>
    public class SecurityService : ISecurityService
    {

        private readonly ApplicationContext context;
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public SecurityService(ApplicationContext context, IUserService userService, IConfiguration configuration)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userService = userService ?? throw new ArgumentNullException(nameof(context));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateDefaultUser()
        {

            var usuario = userService.getUserByEmail("test");

            if (usuario is null)
            {
                EncriptPassword("test", out var password, out var salt);

                usuario = new UserDto {
                    Id = 0,
                    Email = "test",
                    Name = "Mario Ramos",
                    Password = password,
                    Salt = salt                    
                };

                userService.SaveUser(usuario);
            }

        }       

        private void EncriptPassword(string clave, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentNullException(nameof(clave));

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        public bool VerifiedPassword(string clave, byte[] claveBd, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentNullException(nameof(clave));

            if (claveBd.Length != 64)
                throw new ArgumentException(nameof(claveBd));

            if (salt.Length != 128)
                throw new ArgumentException(nameof(salt));

            using (var hmac = new HMACSHA512(salt))
            {
                var claveHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));
                for (int i = 0; i < claveHash.Length; i++)
                {
                    if (claveHash[i] != claveBd[i])
                        return false;
                }
            }
            return true;
        }


        public string getToken(UserDto userDto)
        {          
            var JwtIssuerSigningKey = configuration.GetValue<string>(Constants.JwtConfiguration.IssuerSigningKey);
            var JwtValidIssuer = configuration.GetValue<string>(Constants.JwtConfiguration.ValidIssuer);
            var JwtValidAudience = configuration.GetValue<string>(Constants.JwtConfiguration.ValidAudience);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtIssuerSigningKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {
                new Claim(Constants.CustomClaimsNames.Id,userDto.Id.ToString()),
                new Claim(Constants.CustomClaimsNames.Email,(userDto.Email != null ? userDto.Email : "")),
                new Claim(Constants.CustomClaimsNames.Name, userDto.Name),
                             
            };

            var payload = new JwtPayload(
                issuer: JwtValidIssuer,
                audience: JwtValidAudience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddYears(1)
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool ValidToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                getClaim(token,"Id",out var value);
                var IdUsuario = int.Parse(value);
                var usuario = userService.getUserById(IdUsuario);
                if (usuario is null)
                {
                    return false;
                }

                var tokens = userService.getTokensUser(usuario.Id);
                var encontrado = false;
                foreach (var tokenDb in tokens)
                {
                    var strToken = Encoding.UTF8.GetString(tokenDb.token);

                    if (string.Equals(token, strToken))
                    {
                        encontrado = true;
                    }                                              
                }

                if (!encontrado)
                {
                    return false;
                }

                var validationParameters = GetValidationParameters();
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public void getClaim(string token, string claim, out string value)
        {
            var decodedToken = new JwtSecurityToken(token);
            var claims = decodedToken.Claims.ToList();
            var claimId = claims.FirstOrDefault(c => c.Type.Equals(claim, StringComparison.InvariantCultureIgnoreCase));
            if (claimId is null)
                throw new NullReferenceException();
            value = claimId.Value;           
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var tokenSecret = Encoding.UTF8.GetBytes(
                configuration.GetValue<string>(Constants.JwtConfiguration.IssuerSigningKey)
                );
          
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,

                ValidIssuer = configuration.GetValue<string>(Constants.JwtConfiguration.ValidIssuer),
                ValidAudience = configuration.GetValue<string>(Constants.JwtConfiguration.ValidAudience),
                IssuerSigningKey = new SymmetricSecurityKey(tokenSecret)
            };
        }

    }
}
