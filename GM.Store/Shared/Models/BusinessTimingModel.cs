namespace GM.Store.Shared.Models
{
    public class BusinessTimingRequest
    {
        public int ApplicationId { get; set; }
    }
    public class BusinessTimingModel
    {
        public string WeekDay { get; set; }
        public string WorkingTime { get; set; }
        public bool? IsClosed { get; set; }
        public bool IsOpenNow { get; set; }
        public int? LocationId { get; set; }
    }
}
