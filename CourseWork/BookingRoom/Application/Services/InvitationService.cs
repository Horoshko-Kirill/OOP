using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.InvitationRepository;
using DataAccess.UnitOfWork;
using Domain.Models;

namespace Application.Services
{
    public class InvitationService : IInvitationService
    {

        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InvitationService(IInvitationRepository invitationRepository, IUnitOfWork unitOfWork)
        {
            _invitationRepository = invitationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateInvitation(Invitation invitation)
        {
            await _invitationRepository.AddAsync(invitation);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteInvitation(Invitation invitation)
        {
            await _invitationRepository.DeleteAsync(invitation);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsInvitations(Expression<Func<Invitation, bool>> predicate)
        {
            return await _invitationRepository.ExistsAsync(predicate);
        }

        public async Task<IEnumerable<Invitation>> FindInvitations(Expression<Func<Invitation, bool>> predicate)
        {
            return await _invitationRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Invitation>> GetAllInvitation()
        {
            return await _invitationRepository.GetAllWithIncludesAsync(); ;
        }

        public async Task<Invitation> GetByIdInvitation(int id)
        {
            return await _invitationRepository.GetByIdWithIncludesAsync(id);
        }

        public async Task UpdateInvitation(Invitation invitation)
        {
            await _invitationRepository.UpdateAsync(invitation);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Invitation>> GetByBooking(int bookingId)
        {
            return await _invitationRepository.GetByBookingAsync(bookingId);
        }

        public async Task<IEnumerable<Invitation>> GetInvitations(int userId, InvitationStatus? status = null)
        {
            return await _invitationRepository.GetUserInvitationsAsync(userId, status);
        }
    }
}
