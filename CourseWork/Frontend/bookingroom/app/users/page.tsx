'use client';

import useSWR from 'swr';
import api from '@/utils/api';
import { UserDto, UserRole } from '@/types/dto';
import { useState } from 'react';

const fetcher = (url: string) => api.get(url).then(res => res.data);

type UserForm = Omit<UserDto, 'id'> & { password?: string };

export default function UsersPage() {
  const { data, mutate, isLoading, error } = useSWR<UserDto[]>('/users', fetcher);
  const [editingUserId, setEditingUserId] = useState<number | null>(null);
  const [form, setForm] = useState<UserForm>({
    name: '',
    email: '',
    password: '',
    role: UserRole.User,
  });

  const handleSubmit = async () => {
    try {
      // Копируем форму в payload
      const payload = { ...form };

      // Если редактируем и пароль пустой — удаляем поле, чтобы не менять пароль
      if (editingUserId !== null && (!payload.password || payload.password.trim() === '')) {
        delete payload.password;
      }

      // Для PUT-запроса не передаем поле id в теле, пусть сервер сам определяет по URL
      if (editingUserId !== null) {
        await api.put(`/users/${editingUserId}`, payload);
      } else {
        // При создании пользователя пароль обязателен (проверка есть в форме)
        await api.post('/users', payload);
      }

      // Сброс формы и состояния редактирования
      setForm({
        name: '',
        email: '',
        password: '',
        role: UserRole.User,
      });
      setEditingUserId(null);

      // Обновляем данные SWR
      await mutate(undefined, { revalidate: true });
    } catch (err: any) {
      console.error('Ошибка при сохранении пользователя', err.response?.data || err.message || err);
      alert('Ошибка при сохранении пользователя. Проверьте консоль.');
    }
  };

  const startEdit = (user: UserDto) => {
    setEditingUserId(user.id);
    setForm({
      name: user.name || '',
      email: user.email || '',
      password: '',
      role: user.role,
    });
  };

  if (isLoading)
    return (
      <div className="flex justify-center items-center min-h-screen">
        Загрузка...
      </div>
    );

  if (error)
    return (
      <div className="flex justify-center items-center min-h-screen text-red-600">
        Ошибка загрузки
      </div>
    );

  return (
    <main className="min-h-screen bg-gray-100 py-10 px-4">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-3xl font-bold mb-6 text-center text-blue-700">
          Управление пользователями
        </h1>

        <ul className="space-y-4 mb-10">
          {data?.map(user => (
            <li
              key={user.id}
              className="bg-white border rounded p-4 shadow-sm hover:shadow-md transition"
            >
              <div className="font-bold text-lg">{user.name || 'Без имени'}</div>
              <div className="text-sm text-gray-600 mb-2">{user.email || 'Без email'}</div>
              <div className="text-sm">
                Роль: {user.role === UserRole.Admin ? 'Администратор' : 'Пользователь'}
              </div>
              <div className="mt-2">
                <button
                  onClick={() => startEdit(user)}
                  className="text-blue-600 hover:underline"
                >
                  Редактировать
                </button>
              </div>
            </li>
          ))}
        </ul>

        <div className="bg-white p-6 rounded shadow-md">
          <h2 className="text-xl font-semibold mb-4">
            {editingUserId !== null ? 'Редактировать пользователя' : 'Добавить нового пользователя'}
          </h2>

          <form
            onSubmit={e => {
              e.preventDefault();
              handleSubmit();
            }}
            className="space-y-4"
          >
            <input
              type="text"
              value={form.name}
              onChange={e => setForm({ ...form, name: e.target.value })}
              placeholder="Имя"
              className="w-full p-2 border rounded"
              required
            />
            <input
              type="email"
              value={form.email}
              onChange={e => setForm({ ...form, email: e.target.value })}
              placeholder="Email"
              className="w-full p-2 border rounded"
              required
            />
            <input
              type="password"
              value={form.password}
              onChange={e => setForm({ ...form, password: e.target.value })}
              placeholder={
                editingUserId !== null
                  ? 'Новый пароль'
                  : 'Пароль'
              }
              className="w-full p-2 border rounded"
              {...(editingUserId === null ? { required: true } : {})}
            />
            <select
              value={form.role}
              onChange={e => setForm({ ...form, role: Number(e.target.value) })}
              className="w-full p-2 border rounded"
            >
              <option value={UserRole.Admin}>Администратор</option>
              <option value={UserRole.User}>Пользователь</option>
            </select>

            <button
              type="submit"
              className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded transition"
            >
              {editingUserId !== null ? 'Сохранить изменения' : 'Добавить пользователя'}
            </button>
          </form>
        </div>
      </div>
    </main>
  );
}
