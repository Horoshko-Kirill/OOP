'use client';

import useSWR from 'swr';
import api from '@/utils/api';
import { MeetingRoomDto } from '@/types/dto';
import { useState } from 'react';

const fetcher = (url: string) => api.get(url).then(res => res.data);

type RoomForm = Omit<MeetingRoomDto, 'id'>;

function formatTimeWithSeconds(time: string) {
  return time.length === 5 ? `${time}:00` : time;
}

export default function RoomsPage() {
  const { data, mutate, isLoading, error } = useSWR<MeetingRoomDto[]>('/meeting-rooms', fetcher);
  const [editingRoomId, setEditingRoomId] = useState<number | null>(null);
  const [form, setForm] = useState<RoomForm>({
    name: '',
    description: '',
    capacity: 0,
    isActive: true,
    workStartTime: '09:00',
    workEndTime: '18:00',
  });


  const validateForm = (): boolean => {
    if (!form.name?.trim()) {
      alert('Название комнаты не может быть пустым');
      return false;
    }

    if (!Number.isInteger(form.capacity) || form.capacity <= 0) {
      alert('Вместимость должна быть целым числом больше 0');
      return false;
    }

    const timeRegex = /^([01]\d|2[0-3]):([0-5]\d)(:[0-5]\d)?$/;

    if (!timeRegex.test(form.workStartTime)) {
      alert('Неверный формат времени начала работы');
      return false;
    }
    if (!timeRegex.test(form.workEndTime)) {
      alert('Неверный формат времени окончания работы');
      return false;
    }

    const toSeconds = (time: string) => {
      const parts = time.split(':').map(Number);
      return parts[0] * 3600 + parts[1] * 60 + (parts[2] || 0);
    };

    if (toSeconds(form.workStartTime) >= toSeconds(form.workEndTime)) {
      alert('Время начала работы должно быть меньше времени окончания');
      return false;
    }

    return true;
  };

  const handleSubmit = async () => {
    if (!validateForm()) return;

    const payload = {
      ...form,
      workStartTime: formatTimeWithSeconds(form.workStartTime),
      workEndTime: formatTimeWithSeconds(form.workEndTime),
    };

    try {
      if (editingRoomId !== null) {
        await api.put(`/meeting-rooms/${editingRoomId}`, {
          ...payload,
          id: editingRoomId,
        });
      } else {
        await api.post('/meeting-rooms', payload);
      }

      setForm({
        name: '',
        description: '',
        capacity: 0,
        isActive: true,
        workStartTime: '09:00',
        workEndTime: '18:00',
      });
      setEditingRoomId(null);
      await mutate(undefined, { revalidate: true });
    } catch (err) {
      console.error('Ошибка при сохранении комнаты', err);
      alert('Ошибка при сохранении комнаты. Проверьте консоль.');
    }
  };

  const handleDelete = async (id: number) => {
    if (confirm('Удалить комнату?')) {
      try {
        await api.delete(`/meeting-rooms/${id}`);
        await mutate();
      } catch (err) {
        console.error('Ошибка при удалении комнаты', err);
        alert('Ошибка при удалении комнаты. Проверьте консоль.');
      }
    }
  };

  const startEdit = (room: MeetingRoomDto) => {
    setEditingRoomId(room.id);
    setForm({
      name: room.name || '',
      description: room.description || '',
      capacity: room.capacity,
      isActive: room.isActive,
      workStartTime: room.workStartTime.slice(0, 5),
      workEndTime: room.workEndTime.slice(0, 5),
    });
  };

  if (isLoading) return <div className="flex justify-center items-center min-h-screen">Загрузка...</div>;
  if (error) return <div className="flex justify-center items-center min-h-screen text-red-600">Ошибка загрузки</div>;

  return (
    <main className="min-h-screen bg-gray-100 py-10 px-4">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-3xl font-bold mb-6 text-center text-blue-700">Управление переговорными комнатами</h1>

        <ul className="space-y-4 mb-10">
          {data?.map(room => (
            <li key={room.id} className="bg-white border rounded p-4 shadow-sm hover:shadow-md transition">
              <div className="font-bold text-lg">{room.name || 'Без названия'}</div>
              <div className="text-sm text-gray-600 mb-2">{room.description}</div>
              <div className="text-sm">Вместимость: {room.capacity}</div>
              <div className="text-sm">Активна: {room.isActive ? 'Да' : 'Нет'}</div>
              <div className="text-sm">Время работы: {room.workStartTime} – {room.workEndTime}</div>
              <div className="mt-2 space-x-3">
                <button
                  onClick={() => startEdit(room)}
                  className="text-blue-600 hover:underline"
                >
                  Редактировать
                </button>
                <button
                  onClick={() => handleDelete(room.id)}
                  className="text-red-600 hover:underline"
                >
                  Удалить
                </button>
              </div>
            </li>
          ))}
        </ul>

        <div className="bg-white p-6 rounded shadow-md">
          <h2 className="text-xl font-semibold mb-4">
            {editingRoomId !== null ? 'Редактировать комнату' : 'Добавить новую комнату'}
          </h2>

          <form onSubmit={e => { e.preventDefault(); handleSubmit(); }} className="space-y-4">
            <input
              value={form.name}
              onChange={e => setForm({ ...form, name: e.target.value })}
              placeholder="Название"
              className="w-full p-2 border rounded"
            />
            <textarea
              value={form.description}
              onChange={e => setForm({ ...form, description: e.target.value })}
              placeholder="Описание"
              className="w-full p-2 border rounded"
            />
            <input
              type="number"
              value={form.capacity}
              onChange={e => setForm({ ...form, capacity: Number(e.target.value) })}
              placeholder="Вместимость"
              className="w-full p-2 border rounded"
              required
              min={1}
            />
            <label className="flex items-center gap-2">
              <input
                type="checkbox"
                checked={form.isActive}
                onChange={e => setForm({ ...form, isActive: e.target.checked })}
              />
              Активна
            </label>
            <div className="flex gap-4">
              <label className="flex flex-col flex-1">
                Время начала:
                <input
                  type="time"
                  value={form.workStartTime}
                  onChange={e => setForm({ ...form, workStartTime: e.target.value })}
                  className="p-2 border rounded"
                  required
                />
              </label>
              <label className="flex flex-col flex-1">
                Время окончания:
                <input
                  type="time"
                  value={form.workEndTime}
                  onChange={e => setForm({ ...form, workEndTime: e.target.value })}
                  className="p-2 border rounded"
                  required
                />
              </label>
            </div>
            <button type="submit" className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded transition">
              {editingRoomId !== null ? 'Сохранить изменения' : 'Добавить комнату'}
            </button>
          </form>
        </div>
      </div>
    </main>
  );
}
