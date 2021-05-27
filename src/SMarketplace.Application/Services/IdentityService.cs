using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMarketplace.Application.Extensions;
using SMarketplace.Core.Communication;
using SMarketplace.Core.Configuration;
using SMarketplace.Domain.Interfaces;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMarketplace.Application.Services
{
    public class IdentityService : Service, IIdentityService
    {

        private readonly AppSettings _appSettings;
        private readonly AppSettingsUrl _appSettingsUrl;
        private readonly HttpClient _httpClient;

        public IdentityService(IOptions<AppSettings> appSettings, HttpClient httpClient, IOptions<AppSettingsUrl> settings)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
             httpClient.BaseAddress = new Uri(settings.Value.AutenticationUrl);
        }

        public async Task<UserResponseLogin> LoginAsync(UserLogin userLogin)
        {

             var loginContent = GetContent(userLogin);
            _httpClient.AtribuirUserEncoder(userLogin.Username, userLogin.Password);
            var MessagensErro = new List<string>();
            try
            {
                var response = await _httpClient.PostAsync("/api/login", new StringContent(JsonSerializer.Serialize(userLogin)));

                if (!HandleErrosResponse(response))
                {
                    return new UserResponseLogin
                    {
                        ResponseResult = await DeserializeObjetoResponse<ResponseResult>(response)
                    };
                }

                var result = await DeserializeObjetoResponse<UserLoginAuthenticated>(response);

                var userLogado = new UserLogado { Username = userLogin.Username };

                if (result.Success)
                {
                    return GerarJwt(userLogado);
                }

                if (result.ResponseResult != null) 
                    MessagensErro = result.ResponseResult.Errors.Mensagens;
                else
                    MessagensErro.Add(result.Error);
            }
            catch (Exception ex)
            {
               
                MessagensErro.Add(ex.Message);
            }

            return new UserResponseLogin
            {
                ResponseResult = new ResponseResult { Errors = new ResponseErrorMessages { Mensagens = MessagensErro } },
                Erro = ""
            };


        }


        private UserResponseLogin GerarJwt(UserLogado userLogin)
        {
            //var user = await _userManager.FindByEmailAsync(email);
            //var claims = await _userManager.GetClaimsAsync(user);

            ICollection<Claim> claims = new List<Claim>();

            var identityClaims = ObterClaimsUsuario(claims, userLogin);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, userLogin, claims);
        }

        private ClaimsIdentity ObterClaimsUsuario(ICollection<Claim> claims, UserLogado userLogin)
        {
            claims.Add(new Claim("sub", userLogin.Id.ToString())); 
            claims.Add(new Claim("name", userLogin.Username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));



            claims.Add(new Claim("User", "write"));
            claims.Add(new Claim("User", "read"));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserResponseLogin ObterRespostaToken(string encodedToken, UserLogado user, IEnumerable<Claim> claims)
        {
            return new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id.ToString(),
                    Email = user.Username,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                },
                Authenticated = true
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        
    }
}
