export default function HomePage() {
  return (
    <main className="flex flex-col items-center justify-center min-h-screen p-8 bg-gradient-to-br from-indigo-50 via-white to-indigo-100">
      <h1 className="text-5xl font-extrabold text-indigo-900 mb-6 text-center">
        Система бронирования переговорных комнат
      </h1>
      <p className="max-w-2xl text-center text-indigo-700 text-lg leading-relaxed">
        Добро пожаловать в современную и удобную систему управления бронированиями переговорных комнат, оборудования и приглашений. 
        Здесь вы сможете легко планировать встречи, управлять ресурсами и повышать эффективность работы вашей команды.
      </p>
      <p className="mt-8 text-center text-indigo-600 italic max-w-xl">
        Этот проект разработан с использованием Next.js, React и современных технологий, чтобы обеспечить быстрый и приятный пользовательский опыт.
      </p>

      {/* Встроенный Google Calendar */}
      <div className="mt-10 w-full max-w-4xl h-[600px] shadow-lg rounded-lg overflow-hidden">
        <iframe
          src="https://calendar.google.com/calendar/embed?src=your_calendar_id%40group.calendar.google.com&ctz=Europe%2FMoscow"
          style={{ border: 0 }}
          width="100%"
          height="100%"
          frameBorder="0"
          scrolling="no"
          title="Google Calendar"
        ></iframe>
      </div>
    </main>
  );
}
