import './globals.css';
import Link from 'next/link';
import { ReactNode } from 'react';

export default function RootLayout({ children }: { children: ReactNode }) {
  return (
    <html lang="ru">
      <body>
        <header className="p-4 bg-gray-200 flex gap-4">
          <Link href="/">🏠 Главная</Link>
          <Link href="/bookings">📅 Бронирования</Link>
          <Link href="/meeting-rooms">🏢 Комнаты</Link>
          <Link href="/equipment">💻 Оборудование</Link>
          <Link href="/users">👥 Пользователи</Link>
          <Link href="/participants">👥 Участники встречи</Link> {/* Пример ссылки на участников встречи с id=1 */}
        </header>
        <main className="p-6">{children}</main>
      </body>
    </html>
  );
}
