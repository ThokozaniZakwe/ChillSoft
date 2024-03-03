using ChillSoft.Models;
using Microsoft.EntityFrameworkCore;

namespace ChillSoft.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context): base(context)
        {
            
        }

        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingItem> MeetingItems { get; set; }
        public DbSet<MeetingItemStatus> MeetingItemStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().HasOne(m => m.MeetingType)
                .WithMany()
                .HasForeignKey(m => m.MeetingTypeId);

            modelBuilder.Entity<MeetingItemStatus>().HasOne(mis => mis.Meeting)
                .WithMany(m => m.MeetingItemStatuses)
                .HasForeignKey(mis => mis.MeetingId);

            modelBuilder.Entity<MeetingItemStatus>().HasOne(mis => mis.MeetingItem)
                .WithMany(mi => mi.MeetingItemStatuses)
                .HasForeignKey(mis => mis.MeetingItemId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
