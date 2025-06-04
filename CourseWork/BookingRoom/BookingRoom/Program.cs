using Application.Services;
using BookingRoom;
using DataAccess;
using DataAccess.Repository;
using DataAccess.Repository.BookingRepository;
using DataAccess.Repository.EquipmentRepository;
using DataAccess.Repository.InvitationRepository;
using DataAccess.Repository.MeetingRoomRepository;
using DataAccess.Repository.ParticipantRepository;
using DataAccess.Repository.UserRepository;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер
builder.Services.AddControllers();

// Настройка Swagger с явным указанием версии
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking Room API",
        Version = "v1",
        Description = "API для управления переговорными комнатами",
        Contact = new OpenApiContact { Name = "Support", Email = "support@example.com" }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") //FRONTEND
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // если нужен вход
    });
});
// Настройка базы данных
var connectionString = builder.Configuration.GetConnectionString("DataContext");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DataContext' not found in configuration");
}

// Включение совместимости с DateTime без указания часового пояса
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

// Регистрация AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Регистрация сервисов и репозиториев
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddScoped<IMeetingRoomService, MeetingRoomService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IMeetingRoomRepository, MeetingRoomRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Настройка конвейера HTTP запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Room API v1");
        c.RoutePrefix = "swagger";
        c.ConfigObject.DisplayRequestDuration = true;
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

// Инициализация базы данных
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();

    try
    {
        if (db.Database.CanConnect())
        {
            // Применяем миграции, если они есть
            db.Database.Migrate();
            Console.WriteLine("Migrations applied successfully.");
        }
        else
        {
            // Создаем БД, если не существует
            db.Database.EnsureCreated();
            Console.WriteLine("Database created successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
    }
}

app.Run();