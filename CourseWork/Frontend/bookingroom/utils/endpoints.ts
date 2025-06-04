export const endpoints = {
  bookings: '/bookings',
  bookingByUser: (userId: number) => `/bookings/by-user/${userId}`,
  bookingById: (id: number) => `/bookings/${id}`,
};
