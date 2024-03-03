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
            ViewData["MeetingTypes"] = new SelectList(await _context.MeetingTypes.Where(m => !m.IsDeleted).ToListAsync());
            return View(_context.Meetings.Where(x => !x.IsDeleted).Include(x => x.MeetingType).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<MeetingType> meetingTypes = _context.MeetingTypes.Where(m => !m.IsDeleted).ToList();
            ViewBag.MT = new SelectList(meetingTypes, "MeetingTypeId", "Description");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Meeting newMeeting)
        {
            if(newMeeting == null)
            {
                return View();
            }

            if(ModelState.IsValid)
            {
                var existingMeeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == newMeeting.Id);
                if(existingMeeting != null)
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

                await _context.Meetings.AddAsync(newMeeting);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
