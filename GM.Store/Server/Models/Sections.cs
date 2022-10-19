using GM.Store.Server.Helper;

namespace GM.Store.Server.Models
{
    public class Section
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(LongToStringJsonConverter))]
        public string Id { get; set; }
        public string Title { get; set; }
        public int SectionType { get; set; }
        public List<Table> Tables { get; set; }
    }
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TableNumber { get; set; }
        public int ApplicationId { get; set; }
    }
}
