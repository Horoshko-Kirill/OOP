using System.Linq.Expressions;
using Domain.Models;

namespace Application.Services
{
    public interface IParticipantService
    {
        Task CreateParticipant(Participant participant);
        Task DeleteParticipant(Participant participant);
        Task<bool> ExistsParticipants(Expression<Func<Participant, bool>> predicate);
        Task<IEnumerable<Participant>> FindParticipants(Expression<Func<Participant, bool>> predicate);
        Task<IEnumerable<Participant>> GetAllParticipant();
        Task<IEnumerable<Participant>> GetByBooking(int bookingId);
        Task<Participant> GetByIdParticipant(int id);
        Task<bool> IsUserParticipant(int userId, int bookingId);
        Task UpdateParticipant(Participant participant);
    }
}