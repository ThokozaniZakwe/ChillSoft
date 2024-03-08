using Chillisoft.Models;

namespace ChilliSoft.Models.ViewModels
{
    public class ItemAndStatusesViewModel
    {
        public MeetingItem MeetingItem { get; set; }
        public MeetingItemStatus MeetingItemStatus { get; set; }

        public Meeting Meeting { get; set; }
    }
}
