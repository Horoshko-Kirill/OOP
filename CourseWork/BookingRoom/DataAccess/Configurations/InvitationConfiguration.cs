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
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("Invitations");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(i => i.SentAt)
                .IsRequired()
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            builder.Property(i => i.RespondedAt)
                .IsRequired(false);

            // Внешние ключи
            builder.HasOne(i => i.Booking)
                .WithMany(b => b.Invitations)
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.InvitedUser)
                .WithMany(u => u.ReceivedInvitations)
                .HasForeignKey(i => i.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Inviter)
                .WithMany(u => u.SentInvitations)
                .HasForeignKey(i => i.InviterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Уникальный индекс для пары BookingId-InvitedUserId
            builder.HasIndex(i => new { i.BookingId, i.InvitedUserId })
                .IsUnique();
        }
    }
}
