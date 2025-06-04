using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Dtos
{
    public sealed record ParticipantReadDto(
    int Id,
    ParticipantStatus Status,
    DateTime CreatedAt,
    int BookingId,
    int UserId,
    UserDto User);
}
