using ChillSoft.Data;
using ChillSoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChillSoft.Controllers
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
            var currentMeeting = await _context.Meetings.Where(x => !x.IsDeleted).Include(x => x.MeetingType).Include(x => x.MeetingItemStatuses).FirstOrDefaultAsync(x => x.Id == Id);
            ViewBag.MeetingItems = await _context.MeetingItems.Where(item => !item.IsDeleted && item.MeetingId == Id).Include(x => x.MeetingItemStatuses).ToListAsync();
            ViewData["MeetingType"] = await _context.MeetingTypes.Where(x => !x.IsDeleted && x.Id == currentMeeting.MeetingTypeId).FirstOrDefaultAsync();
            return View(currentMeeting);
        }

        [HttpPost]
        public async Task<IActionResult> AddMeetingItem(MeetingItem item)
        {
            if(item == null)
            {
                return RedirectToAction(nameof(Edit), item.Id);
            }

            await _context.MeetingItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), item.MeetingId);
        }
    }
}
