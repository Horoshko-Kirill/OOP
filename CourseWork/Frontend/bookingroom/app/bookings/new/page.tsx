'use client';

import { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import useSWR from 'swr';
import api from '@/utils/api';
import { UserDto, MeetingRoomDto } from '@/types/dto';

const fetcher = (url: string) => api.get(url).then(res => res.data);

export default function NewBookingPage() {
  const router = useRouter();

  const { data: users } = useSWR<UserDto[]>('/users', fetcher);
  const { data: rooms } = useSWR<MeetingRoomDto[]>('/meeting-rooms', fetcher);

  const [formData, setFormData] = useState({
    title: '',
    description: '',
    startTime: '',
    endTime: '',
    organizerId: 0,
    meetingRoomId: 0,
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: name === 'organizerId' || name === 'meetingRoomId' ? Number(value) : value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await api.post('/bookings', {
        title: formData.title,
        description: formData.description,
        startTime: formData.startTime,
        endTime: formData.endTime,
        organizerId: formData.organizerId,
        meetingRoomId: formData.meetingRoomId,
      });

      alert('Бронирование создано!');
      router.push('/bookings');
    } catch (err: any) {
      console.error('Ошибка при создании:', err);
      alert(`Ошибка: ${err.response?.data?.message || err.message}`);
    }
  };

  return (
    <main className="max-w-xl mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Создать бронирование</h1>
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
            value={formData.startTime}
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
            value={formData.endTime}
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
            <option value="">Выберите организатора</option>
            {users?.map(user => (
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
            <option value="">Выберите комнату</option>
            {rooms?.map(room => (
              <option key={room.id} value={room.id}>
                {room.name}
              </option>
            ))}
          </select>
        </div>
        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          Создать
        </button>
      </form>
    </main>
  );
}
