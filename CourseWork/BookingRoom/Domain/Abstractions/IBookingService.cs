using System.Linq.Expressions;
using Domain.Models;

namespace Application.Services
{
    public interface IBookingService
    {
        Task CreateBooking(Booking booking);
        Task DeleteBooking(Booking booking);
        Task<bool> ExistsBookings(Expression<Func<Booking, bool>> predicate);
        Task<IEnumerable<Booking>> FindBookings(Expression<Func<Booking, bool>> predicate);
        Task<IEnumerable<Booking>> GetAllBooking();
        Task<Booking> GetByIdBooking(int id);
        Task<IEnumerable<Booking>> GetUserBookings(int userId, bool includePast);
        Task<bool> IsRoomBooked(int roomId, DateTime start, DateTime end);
        Task UpdateBooking(Booking booking);

    }
}