using GM.Store.Server.Helper;

namespace GM.Store.Server.Models
{
    public class Category
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(LongToStringJsonConverter))]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? MenuItem { get; set; }
        public int? OrderIndex { get; set; }
    }
}
