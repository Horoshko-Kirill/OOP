'use client';

import useSWR from 'swr';
import api from '@/utils/api';
import { BookingDto } from '@/types/dto';
import Link from 'next/link';
import { useRouter } from 'next/navigation';

const fetcher = (url: string) => api.get(url).then(res => res.data);

export default function BookingsPage() {
  const { data, error, isLoading, mutate } = useSWR<BookingDto[]>('/bookings', fetcher);
  const router = useRouter();

  const handleDelete = async (id: number) => {
    if (!confirm('Вы уверены, что хотите удалить бронирование?')) return;

    try {
      await api.delete(`/bookings/${id}`);
      mutate(); // перезагрузить список
    } catch (err: any) {
      alert(`Ошибка удаления: ${err.response?.data?.message || err.message}`);
    }
  };

  if (isLoading) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка: {error.message}</div>;

  return (
    <main className="max-w-3xl mx-auto p-4">
      <div className="flex justify-between items-center mb-4">
        <h1 className="text-2xl font-bold">Бронирования</h1>
        <Link
          href="/bookings/new/"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          + Новое
        </Link>
      </div>

      <ul className="space-y-3">
        {data?.map((booking) => (
          <li
            key={booking.id}
            className="border p-3 rounded shadow flex justify-between items-start gap-4"
          >
            <div className="flex-1">
              <div className="font-semibold text-lg">{booking.title}</div>
              <div className="text-sm text-gray-600">
                {booking.startTime} → {booking.endTime}
              </div>
              <div className="text-sm mt-1">
                <span className="font-medium">Комната:</span>{' '}
                {booking.meetingRoom?.name ?? '—'}
              </div>
              <div className="text-sm">
                <span className="font-medium">Организатор:</span>{' '}
                {booking.organizer?.name ?? '—'} ({booking.organizer?.email ?? '—'})
              </div>
            </div>

            <div className="flex gap-2">
              <Link
                href={`/bookings/${booking.id}/edit`}
                className="text-blue-600 hover:underline text-xl"
                title="Редактировать"
              >
                ✏️
              </Link>
              <button
                onClick={() => handleDelete(booking.id)}
                className="text-red-600 hover:underline text-xl"
                title="Удалить"
              >
                🗑️
              </button>
            </div>
          </li>
        ))}
      </ul>
    </main>
  );
}
