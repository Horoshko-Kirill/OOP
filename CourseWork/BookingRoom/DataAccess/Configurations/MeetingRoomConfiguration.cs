using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class MeetingRoomConfiguration : IEntityTypeConfiguration<MeetingRoom>
    {
        public void Configure(EntityTypeBuilder<MeetingRoom> builder)
        {
            builder.ToTable("MeetingRooms");

            builder.HasKey(mr => mr.Id);

            builder.Property(mr => mr.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(mr => mr.Description)
                .HasMaxLength(500);

            builder.Property(mr => mr.Capacity)
                .IsRequired();


            builder.Property(mr => mr.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(mr => mr.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            // Навигационные свойства
            builder.HasMany(mr => mr.Bookings)
                .WithOne(b => b.MeetingRoom)
                .HasForeignKey(b => b.MeetingRoomid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(mr => mr.Equipments)
                .WithOne(e => e.MeetingRoom)
                .HasForeignKey(e => e.MeetingRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
