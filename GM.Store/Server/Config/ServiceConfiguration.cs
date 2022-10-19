namespace GM.Store.Server.Config
{
    public class ServiceConfiguration
    {
        public int AppId { get; set; }
        public string? BaseApiUrl { get; set; }
        public string? ApplicationSecret { get; set; }
        public JwtTokenConfig Jwt { get; set; }
        public SmsConfig Sms { get; set; }
    }
    public class JwtTokenConfig
    {
        /// <summary>
        /// Secret Key
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// Valid Issuer
        /// </summary>
        public string ValidIssuer { get; set; }
        /// <summary>
        /// Valid Audience
        /// </summary>
        public string ValidAudience { get; set; }
        /// <summary>
        /// AccessToken Expirattion time
        /// </summary>
        public int AccessTokenExpiration { get; set; }
        // public int RefreshTokenExpiration { get; set; }
    }
    public class SmsConfig
    {
        public string SenderID { get; set; }
        public string ApiUrl { get; set; }
        public string Secret { get; set; }
        public string Key { get; set; }
    }
}
