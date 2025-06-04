using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            builder.Property(b => b.StartTime)
                .IsRequired();

            builder.Property(b => b.EndTime)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            // Внешние ключи
            builder.HasOne(b => b.Organizer)
                .WithMany(u => u.OrganizedBookings)
                .HasForeignKey(b => b.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.MeetingRoom)
                .WithMany(mr => mr.Bookings)
                .HasForeignKey(b => b.MeetingRoomid)
                .OnDelete(DeleteBehavior.Restrict);

            // Навигационные свойства
            builder.HasMany(b => b.Participants)
                .WithOne(p => p.Booking)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Invitations)
                .WithOne(i => i.Booking)
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индекс для быстрого поиска по датам
            builder.HasIndex(b => new { b.StartTime, b.EndTime });
        }
    }
}
