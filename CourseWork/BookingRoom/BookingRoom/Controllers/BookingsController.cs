using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService _bookingService;
        private readonly IMeetingRoomService _meetingRoomService; // <-- добавили поле
        private readonly IMapper _mapper;

        public BookingsController(
        IBookingService bookingService,
        IMeetingRoomService meetingRoomService,   // <-- внедрение через конструктор
        IMapper mapper)
        {
            _bookingService = bookingService;
            _meetingRoomService = meetingRoomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetAll()
        {
            var bookings = await _bookingService.GetAllBooking();
            return Ok(_mapper.Map<IEnumerable<BookingReadDto>>(bookings));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingReadDto>> GetById(int id)
        {
            var booking = await _bookingService.GetByIdBooking(id);
            if (booking == null) return NotFound();
            return Ok(_mapper.Map<BookingReadDto>(booking));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetByUser(
            int userId,
            [FromQuery] bool includePast = false)
        {
            var bookings = await _bookingService.GetUserBookings(userId, includePast);
            return Ok(_mapper.Map<IEnumerable<BookingReadDto>>(bookings));
        }

        [HttpGet("check-availability")]
        public async Task<ActionResult<bool>> CheckRoomAvailability(
        [FromQuery] int roomId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
        {
            var isBooked = await _bookingService.IsRoomBooked(roomId, start, end);
            return Ok(!isBooked);
        }

        [HttpPost]
        public async Task<ActionResult<BookingReadDto>> Create([FromBody] BookingWriteDto dto)
        {
            // 1. Проверка корректности времени начала и окончания
            if (dto.StartTime >= dto.EndTime)
            {
                return BadRequest("Время начала бронирования должно быть меньше времени окончания.");
            }

            // 2. Получаем переговорную комнату по Id
            var room = await _meetingRoomService.GetByIdMeetingRoom(dto.MeetingRoomId);
            if (room == null)
            {
                return BadRequest("Переговорная комната не найдена.");
            }

            // 3. Проверяем рабочее время комнаты
            // Предполагается, что WorkStartTime и WorkEndTime — строки формата "HH:mm"
            var roomStartTime = room.WorkStartTime; // TimeSpan
            var roomEndTime = room.WorkEndTime;     // TimeSpan

            var bookingStartTime = dto.StartTime.TimeOfDay;
            var bookingEndTime = dto.EndTime.TimeOfDay;


            if (bookingStartTime < roomStartTime || bookingEndTime > roomEndTime)
            {
                return BadRequest($"Время бронирования должно быть в рабочем интервале комнаты: с {roomStartTime} до {roomEndTime}.");
            }

            // 4. Проверяем пересечение с уже существующими бронированиями
            bool isRoomBooked = await _bookingService.IsRoomBooked(dto.MeetingRoomId, dto.StartTime, dto.EndTime);
            if (isRoomBooked)
            {
                return Conflict("В указанное время уже есть бронирование.");
            }

            // 5. Создаем бронирование
            var booking = new Booking(
                id: 0,
                title: dto.Title,
                description: dto.Description,
                startTime: dto.StartTime,
                endTime: dto.EndTime,
                createdAt: DateTime.UtcNow,
                organizerId: dto.OrganizerId,
                meetingRoomid: dto.MeetingRoomId);

            await _bookingService.CreateBooking(booking);
            var createdBooking = await _bookingService.GetByIdBooking(booking.Id);

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, _mapper.Map<BookingReadDto>(createdBooking));
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingWriteDto dto)
        {
            var booking = await _bookingService.GetByIdBooking(id);
            if (booking == null) return NotFound();

            booking.Title = dto.Title;
            booking.Description = dto.Description;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.MeetingRoomid = dto.MeetingRoomId;
            booking.OrganizerId = dto.OrganizerId;


            await _bookingService.UpdateBooking(booking);

            var updatedBooking = await _bookingService.GetByIdBooking(id);

            return Ok(_mapper.Map<BookingReadDto>(updatedBooking));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetByIdBooking(id);
            if (booking == null) return NotFound();

            await _bookingService.DeleteBooking(booking);
            return NoContent();
        }
    }
}
