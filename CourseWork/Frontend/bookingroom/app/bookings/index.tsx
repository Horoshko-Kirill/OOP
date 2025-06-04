import useSWR from 'swr';
import api from '@/utils/api';
import { BookingDto } from '@/types/dto';

const fetcher = (url: string) => api.get(url).then(res => res.data);

export default function BookingsPage() {
  const { data, error, isLoading } = useSWR<BookingDto[]>('/bookings', fetcher);

  if (isLoading) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка загрузки</div>;

  return (
    <div>
      <h1>Бронирования</h1>
      <ul>
        {data?.map(booking => (
          <li key={booking.id}>
            <strong>{booking.title}</strong> – с {booking.startTime} по {booking.endTime}
          </li>
        ))}
      </ul>
    </div>
  );
}
