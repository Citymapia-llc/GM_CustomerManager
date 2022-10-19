using GM.Store.Server.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GM.Store.Server.Provider
{
    public interface IJwtAuthProvider
    {
        string GenerateTokens(string username, Claim[] claims, DateTime now);
        //(ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
        //JWTClaimModel ReadJwtToken(string token);
    }

    /// <summary>
    /// Jwt Auth Provider class
    /// </summary>
    public class JwtAuthProvider : IJwtAuthProvider
    {
        private readonly ServiceConfiguration _serviceConfig;
        private readonly byte[] _secret;

        /// <summary>
        /// contructor creates
        /// </summary>
        /// <param name="serviceConfig"></param>
        public JwtAuthProvider(ServiceConfiguration serviceConfig)
        {
            _serviceConfig = serviceConfig;
            _secret = Encoding.ASCII.GetBytes(_serviceConfig.Jwt.Secret);
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="claims"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public string GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _serviceConfig.Jwt.ValidIssuer,
                shouldAddAudienceClaim ? _serviceConfig.Jwt.ValidAudience : string.Empty,
                claims,
                expires: now.AddDays(_serviceConfig.Jwt.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }
        //public JWTClaimModel ReadJwtToken(string token)
        //{
        //    try
        //    {
        //        var model = new JWTClaimModel();
        //        var handler = new JwtSecurityTokenHandler();
        //        var jsonToken = handler.ReadToken(token);
        //        var tokenS = jsonToken as JwtSecurityToken;
        //        model.CurrentUserTypeId = tokenS.Claims.First(claim => claim.Type == "UserTypeId").Value.ParseToInt();
        //        model.CurrentUserType = tokenS.Claims.First(claim => claim.Type == "UserType").Value;
        //        return model;
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }

        //}

        /// <summary>
        /// To Decode Jwt Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
 

        //private static string GenerateRefreshTokenString()
        //{
        //    var randomNumber = new byte[32];
        //    using var randomNumberGenerator = RandomNumberGenerator.Create();
        //    randomNumberGenerator.GetBytes(randomNumber);
        //    return Convert.ToBase64String(randomNumber);
        //}
    }

    /// <summary>
    /// Result Object
    /// </summary>
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
    }
    public class JWTClaimModel
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserType { get; set; }
        public int CurrentUserTypeId { get; set; }
        public int PersonId { get; set; }
        public int BusinessId { get; set; }
        public int GroupId { get; set; }
        public string BusinessToken { get; set; }
        public int LanguageId { get; set; }
        public int TimeZoneOffSet { get; set; }
    }
}
