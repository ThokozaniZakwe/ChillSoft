using Chillisoft.Data;
using Chillisoft.Models;
using ChilliSoft.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chillisoft.Controllers
{
    public class MeetingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(_context.Meetings.Where(x => !x.IsDeleted).Include(x => x.MeetingType).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<MeetingType> meetingTypes = await _context.MeetingTypes.Where(m => !m.IsDeleted).ToListAsync();
            var lastMeeting = await _context.Meetings.Where(x => !x.IsDeleted).OrderBy(x => x.Id).LastOrDefaultAsync();
            ViewBag.MT = new SelectList(meetingTypes, "Id", "Description");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Meeting newMeeting)
        {
            List<MeetingType> meetingTypes = await _context.MeetingTypes.Where(m => !m.IsDeleted).ToListAsync();
            ViewBag.MT = new SelectList(meetingTypes, "Id", "Description");

            if (newMeeting == null)
            {
                return View();
            }

            //if(ModelState.IsValid)
            //{
            if (newMeeting.MeetingTypeId == 0)
            {
                ModelState.Clear();
                ModelState.AddModelError("Meeting", "Please select Meeting Type");
                return View();
            }
            if (newMeeting.MeetinngDate == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("Meeting", "Please select Meeting Date");
                return View();
            }
            var existingMeeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == newMeeting.Id);
            if (existingMeeting != null)
            {
                ModelState.AddModelError("Meeting", "The Meeting already exists!");
                return View();
            }

            var lastMeeting = await _context.Meetings.Where(x => !x.IsDeleted).OrderBy(x => x.Id).LastOrDefaultAsync();
            var meetingType = _context.MeetingTypes.Where(x => !x.IsDeleted && x.Id == newMeeting.MeetingTypeId).FirstOrDefault();
            if (lastMeeting != null && meetingType != null)
            {
                newMeeting.MeetingNumber = meetingType.Description.Substring(0, 1) + lastMeeting.Id.ToString();
            }
            else
            {
                newMeeting.MeetingNumber = meetingType.Description.Substring(0, 1) + "0";
            }

            var addedMeeting = await _context.Meetings.AddAsync(newMeeting);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { Id = addedMeeting.Entity.Id });
            //}
            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var currentMeeting = await _context.Meetings.Where(x => !x.IsDeleted).Include(x => x.MeetingType).FirstOrDefaultAsync(x => x.Id == Id);
            ViewBag.MeetingItems = await _context.MeetingItems.Where(item => !item.IsDeleted && item.MeetingId == Id).Include(x => x.MeetingItemStatuses).ToListAsync();
            var previousMeetingItems = await _context.MeetingItems.Where(item => !item.IsDeleted && item.MeetingId != Id).ToListAsync();
            ViewBag.PreviousItems = new SelectList(previousMeetingItems, "Id", "Description");
            ViewData["MeetingType"] = await _context.MeetingTypes.Where(x => !x.IsDeleted && x.Id == currentMeeting.MeetingTypeId).FirstOrDefaultAsync();
            ViewData["meetingStatuses"] = await _context.MeetingItemStatuses.Where(x => !x.IsDeleted).ToListAsync();
            ItemAndStatusesViewModel meetingVM = new ItemAndStatusesViewModel
            {
                Meeting = currentMeeting,
                MeetingItem = new MeetingItem(),
                MeetingItemStatus = new MeetingItemStatus()
            };
            return View(meetingVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddMeetingItem(ItemAndStatusesViewModel vm)
        {
            MeetingItem item = vm.MeetingItem;
            item.MeetingId = vm.Meeting.Id;
            MeetingItemStatus meetingItemStatus = vm.MeetingItemStatus;
            if(item == null)
            {
                return RedirectToAction(nameof(Edit), item.Id);
            }

            //var meetingItemStatus = new MeetingItemStatus
            //{
            //    MeetingId = item.MeetingId,
            //    Status = "new",
            //    Action = "New Action",
            //    //MeetingItemId = item.Id,
            //    ResponsiblePerson = item.PersonResponsible
            //};
            

            var newMeetingItem = await _context.MeetingItems.AddAsync(item);


            await _context.SaveChangesAsync();

            //meetingItemStatus.MeetingItemId = newMeetingItem.Entity.Id;
            meetingItemStatus.MeetingItemId = newMeetingItem.Entity.Id;
            meetingItemStatus.MeetingId = vm.Meeting.Id;
            meetingItemStatus.ResponsiblePerson = newMeetingItem.Entity.PersonResponsible;
            await _context.MeetingItemStatuses.AddAsync(meetingItemStatus);

            await _context.SaveChangesAsync();

            //int Id = item.MeetingId;

            return RedirectToAction(nameof(Edit), new { Id = item.MeetingId });
        }

        [HttpPost]
        public async Task<IActionResult> EditMeetingItem(ItemAndStatusesViewModel vm)//(MeetingItem item)
        {
            MeetingItem item = vm.MeetingItem;
            var dbItem = await _context.MeetingItems.Where(i => !i.IsDeleted && i.Id == item.Id).FirstOrDefaultAsync();
            if(dbItem == null)
            {
                return RedirectToAction(nameof(Edit), item.MeetingId);
            }

            dbItem.Description = item.Description;
            dbItem.Comments = item.Comments;
            dbItem.PersonResponsible = item.PersonResponsible;
            dbItem.DueDate = item.DueDate;

            

            await _context.SaveChangesAsync();

            MeetingItemStatus status = vm.MeetingItemStatus;
            var dbMeetingStatus = await _context.MeetingItemStatuses.Where(x => !x.IsDeleted && x.Id == status.Id).FirstOrDefaultAsync();
            if (dbMeetingStatus == null)
            {
                status.MeetingItemId = dbItem.Id;
                status.MeetingId = dbItem.MeetingId;
                status.ResponsiblePerson = dbItem.PersonResponsible;
                await _context.MeetingItemStatuses.AddAsync(status);
            }
            else
            {
                dbMeetingStatus.Action = status.Action;
                dbMeetingStatus.Status = status.Status;
                dbMeetingStatus.ResponsiblePerson = dbItem.PersonResponsible;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { Id = dbItem.MeetingId });
        }

        public async Task<IActionResult> AddPreviousMeetingItem(ItemAndStatusesViewModel vm)
        {
            var previousItem = vm.MeetingItem;
            var currentMeeting = vm.Meeting;

            var prevDbItem = await _context.MeetingItems.Where(x => !x.IsDeleted && x.Id == previousItem.Id).FirstOrDefaultAsync();
            prevDbItem.MeetingId = currentMeeting.Id;
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Edit), new { Id = currentMeeting.Id });
        }
    }
}
