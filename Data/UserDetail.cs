namespace Local_WRP.Data
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateUTC { get; set; } = DateTime.UtcNow;
    }
}
