using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Models;

namespace DataAccess.Repository.ParticipantRepository
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetByBookingAsync(int bookingId);
        Task<bool> IsUserParticipantAsync(int userId, int bookingId);
        Task<IEnumerable<Participant>> GetAllWithIncludesAsync();

        Task<Participant> GetByIdWithIncludesAsync(int id);
    }
}
