namespace Mango.Service.AuthAPI.Models
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Audience { get; set; }
    }
}