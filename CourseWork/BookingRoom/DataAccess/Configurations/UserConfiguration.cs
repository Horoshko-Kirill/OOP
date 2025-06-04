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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
           .IsRequired()
           .HasMaxLength(100);

            builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);

            builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            builder.HasIndex(u => u.Email)
           .IsUnique();

            builder.HasMany(u => u.OrganizedBookings)
          .WithOne(b => b.Organizer)
          .HasForeignKey(b => b.OrganizerId)
          .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Participations)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.SentInvitations)
                .WithOne(i => i.Inviter)
                .HasForeignKey(i => i.InviterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReceivedInvitations)
                .WithOne(i => i.InvitedUser)
                .HasForeignKey(i => i.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
