using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.BookingRepository;
using DataAccess.UnitOfWork;
using Domain.Models;

namespace Application.Services
{
    public class BookingService : IBookingService
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;


        public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBooking(Booking booking)
        {
            await _bookingRepository.AddAsync(booking);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBooking(Booking booking)
        {
            await _bookingRepository.DeleteAsync(booking);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsBookings(Expression<Func<Booking, bool>> predicate)
        {
            return await _bookingRepository.ExistsAsync(predicate);
        }

        public async Task<IEnumerable<Booking>> FindBookings(Expression<Func<Booking, bool>> predicate)
        {
            return await _bookingRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Booking>> GetAllBooking()
        {
            return await _bookingRepository.GetAllWithIncludesAsync();
        }

        public async Task<Booking> GetByIdBooking(int id)
        {
            return await _bookingRepository.GetByIdWithIncludesAsync(id);
        }


        public async Task UpdateBooking(Booking booking)
        {
            await _bookingRepository.UpdateAsync(booking);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Booking>> GetUserBookings(int userId, bool includePast)
        {
            return await _bookingRepository.GetUserBookingsAsync(userId, includePast);
        }

        public async Task<bool> IsRoomBooked(int roomId, DateTime start, DateTime end)
        {
            return await _bookingRepository.IsRoomBookedAsync(roomId, start, end);
        }


    }
}
