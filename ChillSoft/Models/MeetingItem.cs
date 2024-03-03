namespace ChillSoft.Models
{
    public class MeetingItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<MeetingItemStatus> MeetingItemStatuses { get; set; }
    }
}
