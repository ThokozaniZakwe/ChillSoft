namespace ChillSoft.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public int MeetingTypeId { get; set; }
        public string MeetingNumber { get; set; }
        public bool IsDeleted { get; set; } = false;

        public MeetingType MeetingType { get; set; }
        public List<MeetingItemStatus> MeetingItemStatuses { get; set; }
    }
}
