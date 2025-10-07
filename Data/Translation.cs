namespace Local_WRP.Data
{
    public class Translation
    {
        public int Id { get; set; }
        public string FromPhrase { get; set; }
        public string ToPhrase { get; set; }
        public DateTime CreatedDateUTC { get; set; } = DateTime.UtcNow;
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}
