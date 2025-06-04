using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.ParticipantRepository
{
    public class ParticipantRepository : Repository<Participant>, IParticipantRepository
    {

        public ParticipantRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Participant>> GetByBookingAsync(int bookingId)
            => await _dbSet.Where(p => p.BookingId == bookingId).ToListAsync();

        public async Task<bool> IsUserParticipantAsync(int userId, int bookingId)
            => await _dbSet.AnyAsync(p => p.UserId == userId && p.BookingId == bookingId);

        public async Task<Participant> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Participants
                .Include(p => p.User)
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Participant>> GetAllWithIncludesAsync()
        {
            return await _context.Participants
                .Include(p => p.User)
                .Include(p => p.Booking)
                .ToListAsync();
        }


    }
}
