using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Models;

namespace DataAccess.Repository.BookingRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {

        Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId, bool includePast);
        Task<bool> IsRoomBookedAsync(int roomId, DateTime start, DateTime end);
        Task<Booking> GetByIdWithIncludesAsync(int id);
        Task<IEnumerable<Booking>> GetAllWithIncludesAsync();

    }
}
