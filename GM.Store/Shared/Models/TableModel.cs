namespace GM.Store.Shared.Models
{
    public class TableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TableNumber { get; set; }
        public int ApplicationId { get; set; }
    }
    public class TableOrders
    {
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public string LocalOrderId { get; set; }
        public string TableName { get; set; }
        public int TableNumber { get; set; }
        public string Description { get; set; }
        public int OrderStatus { get; set; }
        public string? ExternalOrderId { get; set; }
        public DateTime OrderDtm { get; set; }
    }
}
