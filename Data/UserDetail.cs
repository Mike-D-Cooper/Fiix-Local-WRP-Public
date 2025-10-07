namespace Local_WRP.Data
{
    public class UserDetail
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    _name = null;
                }
                else
                {
                    // Trim whitespace and remove control characters
                    _name = new string(value.Trim().Where(c => !char.IsControl(c)).ToArray());
                }
            }
        }

        public DateTime CreatedDateUTC { get; set; } = DateTime.UtcNow;
        public string Language { get; set; } = "en";
    }
}
