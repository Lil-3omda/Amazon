namespace Amazon_API.Helpers
{
    public static class OrderStatuses
    {
        public const string Pending = "Pending";
        public const string Processing = "Processing";
        public const string Shipped = "Shipped";
        public const string Delivered = "Delivered";
        public const string Cancelled = "Cancelled";
        public const string Failed = "Failed";

        public static readonly List<string> All = new()
        {
            Pending,
            Processing,
            Shipped,
            Delivered,
            Cancelled,
            Failed
        };
    }
}
