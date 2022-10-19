using GM.Store.Server.Helper;

namespace GM.Store.Server.Models
{
    public class SmsTemplate
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(LongToStringJsonConverter))]
        public string Id { get; set; }
        public string? Template { get; set; }
        public string? Title { get; set; }
    }
}
