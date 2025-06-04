using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Dtos
{
    public sealed record InvitationWriteDto(
    int Id,
    InvitationStatus Status,
    DateTime SentAt,
    DateTime? RespondedAt,
    int BookingId,
    int InvitedUserId,
    int InviterId);
}
