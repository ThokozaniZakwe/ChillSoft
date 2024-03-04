namespace ChillSoft.Models
{
    public class MeetingItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string PersonResponsible { get; set; }
        public int MeetingId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Comments { get; set; }

        public List<MeetingItemStatus> MeetingItemStatuses { get; set; }
    }
}
