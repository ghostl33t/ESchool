namespace server.Models.DTOs.SchoolList
{
    public class PatchUpdate
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public long UpdatedById { get; set; }
        public int SchoolType { get; set; }
    }
}
