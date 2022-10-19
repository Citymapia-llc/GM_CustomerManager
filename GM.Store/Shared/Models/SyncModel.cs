using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Shared.Models
{
    public class SyncModel
    {
        public string? Id { get; set; }
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastSyncTime { get; set; }
    }
}
