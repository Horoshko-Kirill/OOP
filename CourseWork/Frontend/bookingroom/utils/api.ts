import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7103/api', // ✅ используй HTTPS и порт 7103
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true, // если планируешь авторизацию с куками
});

export default api;
