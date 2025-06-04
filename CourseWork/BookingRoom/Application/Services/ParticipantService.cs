using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.ParticipantRepository;
using DataAccess.UnitOfWork;
using Domain.Models;

namespace Application.Services
{
    public class ParticipantService : IParticipantService
    {

        private readonly IParticipantRepository _participantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipantService(IParticipantRepository participantRepository, IUnitOfWork unitOfWork)
        {
            _participantRepository = participantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateParticipant(Participant participant)
        {
            await _participantRepository.AddAsync(participant);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteParticipant(Participant participant)
        {
            await _participantRepository.DeleteAsync(participant);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsParticipants(Expression<Func<Participant, bool>> predicate)
        {
            return await _participantRepository.ExistsAsync(predicate);
        }

        public async Task<IEnumerable<Participant>> FindParticipants(Expression<Func<Participant, bool>> predicate)
        {
            return await _participantRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Participant>> GetAllParticipant()
        {
            return await _participantRepository.GetAllWithIncludesAsync();
        }

        public async Task<Participant> GetByIdParticipant(int id)
        {
            return await _participantRepository.GetByIdWithIncludesAsync(id);
        }

        public async Task UpdateParticipant(Participant participant)
        {
            await _participantRepository.UpdateAsync(participant);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Participant>> GetByBooking(int bookingId)
        {
            return await _participantRepository.GetByBookingAsync(bookingId);
        }

        public async Task<bool> IsUserParticipant(int userId, int bookingId)
        {
            return await _participantRepository.IsUserParticipantAsync(userId, bookingId);
        }
    }
}
