namespace Local_WRP.Data
{
    public class TenantDetail
    {
        public int Id { get; set; }
        public string AppKey { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string TenantName { get; set; }
        public long? LimitToSite { get; set; }
        public string TimeZoneId { get; set; }
        public string Language { get; set; }
    }
}
