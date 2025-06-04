'use client';

import { useEffect, useState } from 'react';
import { useRouter, useParams } from 'next/navigation';
import useSWR from 'swr';
import api from '@/utils/api';
import { BookingDto, UserDto, MeetingRoomDto } from '@/types/dto';

const fetcher = (url: string) => api.get(url).then((res) => res.data);

export default function EditBookingPage() {
  const router = useRouter();
  const { id } = useParams();

  const { data: booking, isLoading, error } = useSWR<BookingDto>(`/bookings/${id}`, fetcher);
  const { data: users } = useSWR<UserDto[]>('/users', fetcher);
  const { data: rooms } = useSWR<MeetingRoomDto[]>('/meeting-rooms', fetcher);


  const [roomBookingsUrl, setRoomBookingsUrl] = useState<string | null>(null);
  const { data: roomBookings } = useSWR<BookingDto[]>(
    roomBookingsUrl,
    fetcher,
    { revalidateOnFocus: false }
  );

  const [formData, setFormData] = useState({
    title: '',
    description: '',
    startTime: '',
    endTime: '',
    organizerId: 0,
    meetingRoomId: 0,
  });

  useEffect(() => {
    if (booking) {
      setFormData({
        title: booking.title || '',
        description: booking.description || '',
        startTime: booking.startTime,
        endTime: booking.endTime,
        organizerId: booking.organizerId,
        meetingRoomId: booking.meetingRoomId,
      });
      setRoomBookingsUrl(`/bookings?meetingRoomId=${booking.meetingRoomId}`);
    }
  }, [booking]);

  useEffect(() => {
    if (formData.meetingRoomId) {
      setRoomBookingsUrl(`/bookings?meetingRoomId=${formData.meetingRoomId}`);
    } else {
      setRoomBookingsUrl(null);
    }
  }, [formData.meetingRoomId]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };


  function isOverlapping(a: {start: string, end: string}, b: {start: string, end: string}) {
    return new Date(a.start) < new Date(b.end) && new Date(b.start) < new Date(a.end);
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!booking) return;

    const room = rooms?.find(r => r.id === +formData.meetingRoomId);
    if (!room) {
      alert('Выберите переговорную комнату');
      return;
    }


    if (new Date(formData.startTime) >= new Date(formData.endTime)) {
      alert('Время начала должно быть меньше времени окончания');
      return;
    }


    function timeStringToMinutes(t: string) {
      const [h, m] = t.split(':').map(Number);
      return h * 60 + m;
    }


    const bookingStartMinutes = timeStringToMinutes(formData.startTime.slice(11, 16));
    const bookingEndMinutes = timeStringToMinutes(formData.endTime.slice(11, 16));
    const roomStartMinutes = timeStringToMinutes(room.workStartTime);
    const roomEndMinutes = timeStringToMinutes(room.workEndTime);

    if (bookingStartMinutes < roomStartMinutes || bookingEndMinutes > roomEndMinutes) {
      alert(`Время бронирования должно быть в рабочем интервале комнаты: с ${room.workStartTime} до ${room.workEndTime}`);
      return;
    }


    if (roomBookings) {
      const overlap = roomBookings.some(bk =>
        bk.id !== booking.id &&
        isOverlapping(
          { start: formData.startTime, end: formData.endTime },
          { start: bk.startTime, end: bk.endTime }
        )
      );
      if (overlap) {
        alert('Время бронирования пересекается с другим бронированием в этой комнате');
        return;
      }
    }

    const payload: BookingDto = {
      id: booking.id,
      title: formData.title,
      description: formData.description,
      startTime: formData.startTime,
      endTime: formData.endTime,
      createdAt: booking.createdAt,
      organizerId: +formData.organizerId,
      meetingRoomId: +formData.meetingRoomId,
      organizer: undefined,
      meetingRoom: undefined,
    };

    try {
      await api.put(`/bookings/${booking.id}`, payload);
      alert('Бронирование обновлено!');
      router.push('/bookings');
    } catch (err: any) {
      console.error('Ошибка при обновлении:', err);
      alert(`Ошибка: ${err.response?.data?.message || err.message}`);
    }
  };

  if (isLoading || !users || !rooms) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка загрузки: {error.message}</div>;
  if (!booking) return <div>Бронирование не найдено</div>;

  return (
    <main className="max-w-xl mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Редактировать бронирование</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block font-medium">Заголовок</label>
          <input
            type="text"
            name="title"
            value={formData.title}
            onChange={handleChange}
            className="w-full border px-3 py-2 rounded"
            required
          />
        </div>

        <div>
          <label className="block font-medium">Описание</label>
          <textarea
            name="description"
            value={formData.description}
            onChange={handleChange}
            className="w-full border px-3 py-2 rounded"
          />
        </div>

        <div>
          <label className="block font-medium">Начало</label>
          <input
            type="datetime-local"
            name="startTime"
            value={formData.startTime.slice(0, 16)}
            onChange={handleChange}
            className="w-full border px-3 py-2 rounded"
            required
          />
        </div>

        <div>
          <label className="block font-medium">Окончание</label>
          <input
            type="datetime-local"
            name="endTime"
            value={formData.endTime.slice(0, 16)}
            onChange={handleChange}
            className="w-full border px-3 py-2 rounded"
            required
          />
        </div>

        <div>
          <label className="block font-medium">Организатор</label>
          <select
            name="organizerId"
            value={formData.organizerId}
            onChange={handleChange}
            className="w-full border px-3 py-2 rounded"
            required
          >
            <option value="">-- Выберите организатора --</option>
            {users.map((user) => (
              <option key={user.id} value={user.id}>
                {user.name} ({user.email})
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block font-medium">Переговорная</label>
          <select
            name="meetingRoomId"
            value={formData.meetingRoomId}
            onChange={handleChange}
            className="w-full border px-3 py-2 rounded"
            required
          >
            <option value="">-- Выберите комнату --</option>
            {rooms.map((room) => (
              <option key={room.id} value={room.id}>
                {room.name} (Рабочее время: {room.workStartTime} - {room.workEndTime})
              </option>
            ))}
          </select>
        </div>

        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          Сохранить
        </button>
      </form>
    </main>
  );
}
