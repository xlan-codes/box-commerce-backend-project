namespace WebAPI.Models
{
    public class OIDCSettings
    {
        public const string SettingsSection = "OIDC";
        public string Url { get; set; }
        public string RedirectUrl { get; set; }
        public string ClientId { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
    }

}
