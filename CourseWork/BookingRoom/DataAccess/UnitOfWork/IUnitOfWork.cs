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

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository Users { get; }
        IMeetingRoomRepository MeetingRooms { get; }
        IBookingRepository Bookings { get; }
        IParticipantRepository Participants { get; }
        IInvitationRepository Invitations { get; }  
        IEquipmentRepository Equipment { get; }     
        Task<int> CommitAsync();

    }
}
