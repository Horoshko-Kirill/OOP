using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/invitation")]
    public class InvitationController : ControllerBase
    {

        private readonly IInvitationService _invitationService;
        private readonly IMapper _mapper;

        public InvitationController(IInvitationService invitationService, IMapper mapper)
        {
            _invitationService = invitationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvitationReadDto>>> GetAll()
        {
            var invitations = await _invitationService.GetAllInvitation();
            return Ok(_mapper.Map<IEnumerable<InvitationReadDto>>(invitations));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvitationReadDto>> GetById(int id)
        {
            var invitation = await _invitationService.GetByIdInvitation(id);
            if (invitation == null) return NotFound();
            return Ok(_mapper.Map<InvitationReadDto>(invitation));
        }

        [HttpGet("by-booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<InvitationReadDto>>> GetByBooking(int bookingId)
        {
            var invitations = await _invitationService.GetByBooking(bookingId);
            return Ok(_mapper.Map<IEnumerable<InvitationReadDto>>(invitations));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<InvitationReadDto>>> GetByUser(
            int userId,
            [FromQuery] InvitationStatus? status = null)
        {
            var invitations = await _invitationService.GetInvitations(userId, status);
            return Ok(_mapper.Map<IEnumerable<InvitationReadDto>>(invitations));
        }

        [HttpPost]
        public async Task<ActionResult<InvitationReadDto>> Create([FromBody] InvitationWriteDto dto)
        {
            var invitation = new Invitation(
                id: 0,
                status: InvitationStatus.Pending,
                sentAt: DateTime.UtcNow,
                respondedAt: null,
                bookingId: dto.BookingId,
                invitedUserId: dto.InvitedUserId,
                inviterId: dto.InviterId);

            await _invitationService.CreateInvitation(invitation);
            var createdInvitation = await _invitationService.GetByIdInvitation(invitation.Id);
            return CreatedAtAction(nameof(GetById), new { id = invitation.Id }, _mapper.Map<InvitationReadDto>(createdInvitation));
        }

        [HttpPut("{id}/respond")]
        public async Task<IActionResult> RespondToInvitation(
            int id,
            [FromBody] InvitationWriteDto dto)
        {
            var invitation = await _invitationService.GetByIdInvitation(id);
            if (invitation == null) return NotFound();

            invitation.Status = dto.Status;
            invitation.RespondedAt = DateTime.UtcNow;

            await _invitationService.UpdateInvitation(invitation);

            var updatedInvitation = await _invitationService.GetByIdInvitation(id);

            return Ok(_mapper.Map<InvitationReadDto>(updatedInvitation));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var invitation = await _invitationService.GetByIdInvitation(id);
            if (invitation == null) return NotFound();

            await _invitationService.DeleteInvitation(invitation);
            return NoContent();
        }

    }
}
