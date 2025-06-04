'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import api from '@/utils/api';

export default function NewRoomPage() {
  const router = useRouter();
  const [name, setName] = useState('');
  const [capacity, setCapacity] = useState(5);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    await api.post('/meeting-rooms', {
      name,
      description: '',
      capacity,
      isActive: true,
      workStartTime: '09:00',
      workEndTime: '18:00',
    });

    router.push('/meeting-rooms');
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <h1 className="text-2xl font-bold">Новая переговорная комната</h1>
      <input value={name} onChange={e => setName(e.target.value)} placeholder="Название" className="border p-2 w-full" />
      <input type="number" value={capacity} onChange={e => setCapacity(Number(e.target.value))} placeholder="Вместимость" className="border p-2 w-full" />
      <button type="submit" className="bg-blue-500 text-white p-2">Создать</button>
    </form>
  );
}
