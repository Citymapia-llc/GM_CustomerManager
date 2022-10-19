using GM.Store.Server.Helper;

namespace GM.Store.Server.Models
{
    public class Sync
    {
       
        public DateTime LastUpdatedTime { get; set; }
    }
    public class CartSync
    {
        public string? LocalId { get; set; }
        public int OrderId { get; set; }
    }
}
