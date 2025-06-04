using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.InvitationRepository
{
    public class InvitationRepository : Repository<Invitation>, IInvitationRepository
    {

        public InvitationRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Invitation>> GetByBookingAsync(int bookingId)
            => await _dbSet
                .Where(i => i.BookingId == bookingId)
                .Include(i => i.InvitedUser)
                .Include(i => i.Inviter)
                .ToListAsync();

        public async Task<IEnumerable<Invitation>> GetUserInvitationsAsync(int userId, InvitationStatus? status = null)
        {
            var query = _dbSet
                .Where(i => i.InvitedUserId == userId)
                .Include(i => i.Booking)
                    .ThenInclude(b => b.MeetingRoom)
                .Include(i => i.Inviter);

            if (status != null)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invitation, User>)query.Where(i => i.Status == status);

            return await query.ToListAsync();
        }

        public async Task<Invitation> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Invitations
                .Include(i => i.InvitedUser)
                .Include(i => i.Inviter)
                .Include(i => i.Booking)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invitation>> GetAllWithIncludesAsync()
        {
            return await _context.Invitations
                .Include(i => i.InvitedUser)
                .Include(i => i.Inviter)
                .Include(i => i.Booking)
                .ToListAsync();
        }

    }
}
