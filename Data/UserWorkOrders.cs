namespace Local_WRP.Data
{
    public class UserWorkOrders
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long WorkOrderId { get; set; }
        public int UserId { get; set; }
    }
}
