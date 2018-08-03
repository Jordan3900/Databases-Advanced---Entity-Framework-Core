namespace TeamBuilder.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Username).IsRequired();

            builder.Property(u => u.Password).IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();

            //builder.HasMany(u => u.CreatedEvents)
            //  .WithOne(x => x.Creator)
            //  .HasForeignKey(x => x.CreatorId)
            //  .OnDelete(DeleteBehavior.Restrict);

            ////builder.HasMany(u => u.CreatedUserTeams)
            ////    .WithOne(x => x.User)
            ////    .HasForeignKey(x => x.UserId);

            //builder.HasMany(x => x.ReceivedInvitations)
            //    .WithOne(x => x.InvitedUser)
            //    .HasForeignKey(x => x.InvitedUserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //public ICollection<Event> CreatedEvents { get; set; }

            //public ICollection<UserTeam> UserTeams { get; set; }

            //public ICollection<Team> CreatorTeams { get; set; }

            //public ICollection<Invitation> ReceivedInvitations { get; set; }

            builder.HasMany(x => x.ReceivedInvitations)
                .WithOne(x => x.InvitedUser)
                .HasForeignKey(x => x.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UserTeams)
             .WithOne(x => x.User)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CreatorTeams)
                   .WithOne(x => x.Creator)
                   .HasForeignKey(x => x.CretorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CreatedEvents)
                 .WithOne(x => x.Creator)
                 .HasForeignKey(x => x.CreatorId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
