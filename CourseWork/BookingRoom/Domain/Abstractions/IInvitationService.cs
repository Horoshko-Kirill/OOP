using System.Linq.Expressions;
using Domain.Models;

namespace Application.Services
{
    public interface IInvitationService
    {
        Task CreateInvitation(Invitation invitation);
        Task DeleteInvitation(Invitation invitation);
        Task<bool> ExistsInvitations(Expression<Func<Invitation, bool>> predicate);
        Task<IEnumerable<Invitation>> FindInvitations(Expression<Func<Invitation, bool>> predicate);
        Task<IEnumerable<Invitation>> GetAllInvitation();
        Task<IEnumerable<Invitation>> GetByBooking(int bookingId);
        Task<Invitation> GetByIdInvitation(int id);
        Task<IEnumerable<Invitation>> GetInvitations(int userId, InvitationStatus? status = null);
        Task UpdateInvitation(Invitation invitation);
    }
}