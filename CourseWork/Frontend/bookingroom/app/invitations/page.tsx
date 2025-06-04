'use client';

import useSWR from 'swr';
import api from '@/utils/api';
import { InvitationDto } from '@/types/dto';

const fetcher = (url: string) => api.get(url).then(res => res.data);

export default function InvitationsPage() {
  const { data, isLoading, error } = useSWR<InvitationDto[]>('/invitation', fetcher);

  if (isLoading) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка</div>;

  return (
    <main>
      <h1>Приглашения</h1>
      <ul>
        {data?.map(inv => (
          <li key={inv.id}>
            ID брони: {inv.bookingId} — Статус: {inv.status}
          </li>
        ))}
      </ul>
    </main>
  );
}
