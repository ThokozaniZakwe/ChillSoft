namespace ChillSoft.Models
{
    public class MeetingItemStatus
    {
        public int Id { get; set; }
        public int MeetingItemId { get; set; }
        public int MeetingId { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public string ResponsiblePerson { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Meeting Meeting { get; set; }
        public MeetingItem MeetingItem { get; set; }
    }
}
