using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace BookingRoom
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();

            // MeetingRoom mappings
            CreateMap<MeetingRoom, MeetingRoomDto>();

            // Equipment mappings
            CreateMap<Equipment, EquipmentDto>();

            // Booking → BookingReadDto (для отображения)
            CreateMap<Booking, BookingReadDto>()
                .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.Organizer))
                .ForMember(dest => dest.MeetingRoom, opt => opt.MapFrom(src => src.MeetingRoom));

            // BookingWriteDto → Booking (для создания/обновления)
            CreateMap<BookingWriteDto, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID задаётся отдельно
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // обычно задаётся в контроллере
                .ForMember(dest => dest.Organizer, opt => opt.Ignore()) // будет подгружен по OrganizerId
                .ForMember(dest => dest.MeetingRoom, opt => opt.Ignore()); // будет подгружен по MeetingRoomId

            // Participant mappings
            CreateMap<Participant, ParticipantReadDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            // Invitation mappings
            CreateMap<Invitation, InvitationReadDto>()
                .ForMember(dest => dest.InvitedUser, opt => opt.MapFrom(src => src.InvitedUser))
                .ForMember(dest => dest.Inviter, opt => opt.MapFrom(src => src.Inviter));
        }
    }
}
