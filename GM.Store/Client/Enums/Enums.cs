namespace GM.Store.Client.Enums
{
    public enum Gender
    {
        Male = 1,
        Female,
        Other
    }
    public enum PaymentMethods
    {
        Cash = 1,
        Card,
        Credit
    }

    public enum KOTStatus
    {
        Pending = 1,
        Confirmed = 2,
        Preparing = 3,
        Completed = 4,
        Cancelled = 5,
        Transfered = 6,
    }
    public enum TableChairs
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
    }
    public enum SectionType
    {
        Dining = 1,
        TakeAway = 2,
    }
}
