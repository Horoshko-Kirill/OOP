'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import api from '@/utils/api';

export default function NewEquipmentPage() {
  const router = useRouter();
  const [name, setName] = useState('');
  const [meetingRoomId, setMeetingRoomId] = useState(1);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    await api.post('/equipment', {
      name,
      description: '',
      meetingRoomId,
    });

    router.push('/equipment');
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <h1 className="text-2xl font-bold">Новое оборудование</h1>
      <input value={name} onChange={e => setName(e.target.value)} placeholder="Название" className="border p-2 w-full" />
      <input type="number" value={meetingRoomId} onChange={e => setMeetingRoomId(Number(e.target.value))} placeholder="ID комнаты" className="border p-2 w-full" />
      <button type="submit" className="bg-green-600 text-white p-2">Создать</button>
    </form>
  );
}
