using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.BookingRepository;
using DataAccess.Repository.EquipmentRepository;
using DataAccess.Repository.InvitationRepository;
using DataAccess.Repository.MeetingRoomRepository;
using DataAccess.Repository.ParticipantRepository;
using DataAccess.Repository.UserRepository;
using Domain.Models;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            MeetingRooms = new MeetingRoomRepository(context);
            Bookings = new BookingRepository(context);
            Participants = new ParticipantRepository(context);
            Invitations = new InvitationRepository(context);  // Новый
            Equipment = new EquipmentRepository(context);     // Новый
        }

        public IUserRepository Users { get; }
        public IMeetingRoomRepository MeetingRooms { get; }
        public IBookingRepository Bookings { get; }
        public IParticipantRepository Participants { get; }
        public IInvitationRepository Invitations { get; }
        public IEquipmentRepository Equipment { get; }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

    }
}
