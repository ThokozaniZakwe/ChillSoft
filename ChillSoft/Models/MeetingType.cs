namespace ChillSoft.Models
{
    public class MeetingType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
