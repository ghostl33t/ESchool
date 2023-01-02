namespace server.Models.DTOs.SchoolList
{
    public class PostSchoolList
    {
        public string SerialNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public int SchoolType { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
