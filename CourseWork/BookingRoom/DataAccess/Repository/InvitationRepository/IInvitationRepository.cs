using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Models;

namespace DataAccess.Repository.InvitationRepository
{
    public interface IInvitationRepository : IRepository<Invitation>
    {

        Task<IEnumerable<Invitation>> GetByBookingAsync(int bookingId);
        Task<IEnumerable<Invitation>> GetUserInvitationsAsync(int userId, InvitationStatus? status = null);

        Task<IEnumerable<Invitation>> GetAllWithIncludesAsync();

        Task<Invitation> GetByIdWithIncludesAsync(int id);

    }
}
