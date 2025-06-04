using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.BookingRepository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        public BookingRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId, bool includePast)
        {
            var query = _dbSet
                .Where(b => b.OrganizerId == userId ||
                            b.Participants.Any(p => p.UserId == userId))
                .Include(b => b.MeetingRoom)
                .Include(b => b.Participants)
                .ThenInclude(p => p.User);

            return includePast
                ? await query.ToListAsync()
                : await query.Where(b => b.StartTime >= DateTime.UtcNow).ToListAsync();
        }

        public async Task<bool> IsRoomBookedAsync(int roomId, DateTime start, DateTime end)
            => await _dbSet.AnyAsync(b =>
                b.MeetingRoomid == roomId &&
                (b.StartTime < end && b.EndTime > start));

        public async Task<Booking> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Organizer)
                .Include(b => b.MeetingRoom)
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task<IEnumerable<Booking>> GetAllWithIncludesAsync()
        {
            return await _context.Bookings
                .Include(b => b.Organizer)
                .Include(b => b.MeetingRoom)
                .ToListAsync();
        }

    }
}
