'use client';

import { useState } from 'react';
import useSWR from 'swr';
import api from '@/utils/api';
import { ParticipantDto, ParticipantStatus } from '@/types/dto';

const fetcher = (url: string) => api.get(url).then(res => res.data);

function statusToString(status: ParticipantStatus) {
  switch (status) {
    case ParticipantStatus.Pending:
      return 'Pending';
    case ParticipantStatus.Accepted:
      return 'Accepted';
    case ParticipantStatus.Declined:
      return 'Declined';
    default:
      return 'Unknown';
  }
}

interface UserDto {
  id: number;
  name: string;
  email: string;
}

interface BookingDto {
  id: number;
  name: string; // Или другой идентификатор комнаты
}

export default function ParticipantsPage() {
  const { data: participants, error, mutate } = useSWR<ParticipantDto[]>('/participants', fetcher);
  const { data: users } = useSWR<UserDto[]>('/users', fetcher);
  const { data: bookings } = useSWR<BookingDto[]>('/bookings', fetcher);

  // Локальные стейты для формы
  const [form, setForm] = useState({
    status: ParticipantStatus.Pending,
    createdAt: new Date().toISOString().slice(0, 16), // YYYY-MM-DDTHH:mm
    bookingId: '',
    userId: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setForm(prev => ({
      ...prev,
      [name]: name === 'status' ? Number(value) : value,
    }));
  };

  const addParticipant = async () => {
    if (!form.bookingId || !form.userId || !form.createdAt) {
      alert('Пожалуйста, заполните все обязательные поля (bookingId, userId, createdAt)');
      return;
    }

    try {
      const newParticipant: Omit<ParticipantDto, 'id'> = {
        status: form.status,
        createdAt: new Date(form.createdAt).toISOString(),
        bookingId: Number(form.bookingId),
        userId: Number(form.userId),
      };

      await api.post('/participants', newParticipant);
      alert('Участник добавлен');
      setForm({
        status: ParticipantStatus.Pending,
        createdAt: new Date().toISOString().slice(0, 16),
        bookingId: '',
        userId: '',
      });
      mutate(); // Обновить список участников
    } catch (e) {
      alert('Ошибка при добавлении участника');
      console.error(e);
    }
  };

  if (error) return <div className="text-red-600 text-center mt-10">Ошибка загрузки данных</div>;
  if (!participants || !users || !bookings) return <div className="text-center mt-10">Загрузка...</div>;

  return (
    <main className="max-w-5xl mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Все участники</h1>
      <table className="min-w-full border-collapse border border-gray-300">
        <thead>
          <tr className="bg-gray-100">
            <th className="border border-gray-300 px-4 py-2">ID</th>
            <th className="border border-gray-300 px-4 py-2">User ID</th>
            <th className="border border-gray-300 px-4 py-2">Status</th>
            <th className="border border-gray-300 px-4 py-2">Created At</th>
            <th className="border border-gray-300 px-4 py-2">Booking ID</th>
            <th className="border border-gray-300 px-4 py-2">User Info</th>
          </tr>
        </thead>
        <tbody>
          {participants.map(p => (
            <tr key={p.id} className="hover:bg-gray-50">
              <td className="border border-gray-300 px-4 py-2 text-center">{p.id}</td>
              <td className="border border-gray-300 px-4 py-2 text-center">{p.userId}</td>
              <td className="border border-gray-300 px-4 py-2 text-center">{statusToString(p.status)}</td>
              <td className="border border-gray-300 px-4 py-2 text-center">{new Date(p.createdAt).toLocaleString()}</td>
              <td className="border border-gray-300 px-4 py-2 text-center">{p.bookingId}</td>
              <td className="border border-gray-300 px-4 py-2">
                {p.user ? (
                  <>
                    <div><b>Name:</b> {p.user.name}</div>
                    <div><b>Email:</b> {p.user.email}</div>
                  </>
                ) : (
                  <span className="text-gray-500">Нет данных</span>
                )}
              </td>
            </tr>
          ))}
          {participants.length === 0 && (
            <tr>
              <td colSpan={6} className="text-center p-4 text-gray-500">Нет участников</td>
            </tr>
          )}
        </tbody>
      </table>

      {/* Форма добавления */}
      <section className="mt-10 bg-white p-6 rounded shadow-md max-w-md mx-auto">
        <h2 className="text-2xl font-semibold mb-4">Добавить участника</h2>
        <div className="space-y-4">

          <div>
            <label className="block mb-1 font-medium">Пользователь</label>
            <select
              name="userId"
              value={form.userId}
              onChange={handleChange}
              className="w-full border p-2 rounded"
            >
              <option value="">Выберите пользователя</option>
              {users.map(user => (
                <option key={user.id} value={user.id}>
                  {user.name} ({user.email})
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block mb-1 font-medium">Комната</label>
            <select
              name="bookingId"
              value={form.bookingId}
              onChange={handleChange}
              className="w-full border p-2 rounded"
            >
              <option value="">Выберите встречу</option>
              {bookings.map(booking => (
                <option key={booking.id} value={booking.id}>
                  {booking.name || `Встреча #${booking.id}`}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block mb-1 font-medium">Статус</label>
            <select
              name="status"
              value={form.status}
              onChange={handleChange}
              className="w-full border p-2 rounded"
            >
              <option value={ParticipantStatus.Pending}>Pending</option>
              <option value={ParticipantStatus.Accepted}>Accepted</option>
              <option value={ParticipantStatus.Declined}>Declined</option>
            </select>
          </div>

          <div>
            <label className="block mb-1 font-medium">Дата создания</label>
            <input
              type="datetime-local"
              name="createdAt"
              value={form.createdAt}
              onChange={handleChange}
              className="w-full border p-2 rounded"
            />
          </div>

          <button
            onClick={addParticipant}
            className="w-full bg-green-600 text-white py-2 rounded hover:bg-green-700"
          >
            Добавить участника
          </button>
        </div>
      </section>
    </main>
  );
}
