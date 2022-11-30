namespace server.Models.DTOs.UsersDTO
{
    public class Delete
    {
        public long Id { get; set; }
        public DateTime? DeletedDate { get; set; } = DateTime.Now;
        public int? Deleted { get; set; }
    }
}
