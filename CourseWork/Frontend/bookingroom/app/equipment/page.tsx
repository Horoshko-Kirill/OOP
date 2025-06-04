'use client';

import { useState } from 'react';
import useSWR from 'swr';
import api from '@/utils/api';
import { EquipmentDto, MeetingRoomDto } from '@/types/dto';

const fetcher = (url: string) => api.get(url).then(res => res.data);

export default function EquipmentPage() {
  const { data: equipment, error: equipError, isLoading: equipLoading, mutate: mutateEquipment } = useSWR<EquipmentDto[]>('/equipment', fetcher);
  const { data: meetingRooms, error: roomsError, isLoading: roomsLoading } = useSWR<MeetingRoomDto[]>('/meeting-rooms', fetcher);

  const [newEquipment, setNewEquipment] = useState({
    name: '',
    description: '',
    meetingRoomId: 0,
  });

  const [editing, setEditing] = useState<EquipmentDto | null>(null);

  // Проверка валидности при добавлении и редактировании
  const isNewValid = newEquipment.name.trim() !== '' && newEquipment.meetingRoomId !== 0;
  const isEditingValid = editing !== null && editing.name?.trim() !== '' && editing.meetingRoomId !== 0;

  const handleCreate = async () => {
    if (!isNewValid) {
      alert('Введите название и выберите переговорную');
      return;
    }
    try {
      await api.post('/equipment', newEquipment);
      setNewEquipment({ name: '', description: '', meetingRoomId: 0 });
      mutateEquipment(); // обновить список
    } catch {
      alert('Ошибка при добавлении');
    }
  };

  const handleUpdate = async () => {
    if (!editing || !isEditingValid) return;
    try {
      await api.put(`/equipment/${editing.id}`, editing);
      setEditing(null);
      mutateEquipment();
    } catch {
      alert('Ошибка при обновлении');
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('Удалить оборудование?')) return;
    try {
      await api.delete(`/api/equipment/${id}`);
      mutateEquipment();
    } catch {
      alert('Ошибка при удалении');
    }
  };

  if (equipLoading || roomsLoading) return <div>Загрузка...</div>;
  if (equipError || roomsError) return <div>Ошибка загрузки данных</div>;

  return (
    <main className="p-4 max-w-3xl mx-auto">
      <h1 className="text-2xl font-bold mb-6">Оборудование</h1>

      {/* Форма добавления нового оборудования */}
      <section className="mb-8">
        <h2 className="text-xl mb-3">Добавить оборудование</h2>
        <input
          type="text"
          placeholder="Название"
          className="border p-2 mr-2"
          value={newEquipment.name}
          onChange={e => setNewEquipment({ ...newEquipment, name: e.target.value })}
        />
        <input
          type="text"
          placeholder="Описание"
          className="border p-2 mr-2"
          value={newEquipment.description}
          onChange={e => setNewEquipment({ ...newEquipment, description: e.target.value })}
        />
        <select
          className="border p-2 mr-2"
          value={newEquipment.meetingRoomId}
          onChange={e => setNewEquipment({ ...newEquipment, meetingRoomId: Number(e.target.value) })}
        >
          <option value={0}>Выберите переговорную</option>
          {meetingRooms?.map(room => (
            <option key={room.id} value={room.id}>
              {room.name || `Комната #${room.id}`}
            </option>
          ))}
        </select>
        <button
          disabled={!isNewValid}
          onClick={handleCreate}
          className={`px-4 py-2 rounded text-white ${isNewValid ? 'bg-blue-600 hover:bg-blue-700' : 'bg-gray-400 cursor-not-allowed'}`}
        >
          Добавить
        </button>
      </section>

      {/* Список оборудования */}
      <section>
        <h2 className="text-xl mb-4">Список оборудования</h2>
        {equipment?.length === 0 && <p>Оборудование не найдено</p>}
        <ul>
          {equipment?.map(equip => (
            <li key={equip.id} className="mb-4 border-b pb-2">
              {editing?.id === equip.id ? (
                <>
                  <input
                    type="text"
                    className="border p-1 mr-2"
                    value={editing.name || ''}
                    onChange={e => setEditing({ ...editing, name: e.target.value })}
                  />
                  <input
                    type="text"
                    className="border p-1 mr-2"
                    value={editing.description || ''}
                    onChange={e => setEditing({ ...editing, description: e.target.value })}
                  />
                  <select
                    className="border p-1 mr-2"
                    value={editing.meetingRoomId}
                    onChange={e => setEditing({ ...editing, meetingRoomId: Number(e.target.value) })}
                  >
                    <option value={0}>Выберите переговорную</option>
                    {meetingRooms?.map(room => (
                      <option key={room.id} value={room.id}>
                        {room.name || `Комната #${room.id}`}
                      </option>
                    ))}
                  </select>
                  <button
                    disabled={!isEditingValid}
                    onClick={handleUpdate}
                    className={`px-3 py-1 rounded text-white ${isEditingValid ? 'bg-green-600 hover:bg-green-700' : 'bg-gray-400 cursor-not-allowed'}`}
                  >
                    Сохранить
                  </button>
                  <button
                    onClick={() => setEditing(null)}
                    className="ml-2 px-3 py-1 rounded border border-gray-400"
                  >
                    Отмена
                  </button>
                </>
              ) : (
                <>
                  <span className="font-semibold">{equip.name || '(Без названия)'}</span> — {equip.description || '-'} (Комната #{equip.meetingRoomId})
                  <button
                    onClick={() => setEditing(equip)}
                    className="ml-4 text-blue-600 hover:underline"
                  >
                    Редактировать
                  </button>
                  <button
                    onClick={() => handleDelete(equip.id)}
                    className="ml-2 text-red-600 hover:underline"
                  >
                    Удалить
                  </button>
                </>
              )}
            </li>
          ))}
        </ul>
      </section>
    </main>
  );
}
