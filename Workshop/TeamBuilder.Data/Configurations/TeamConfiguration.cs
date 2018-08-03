using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            //        public ICollection<TeamEvent> TeamEvents { get; set; }
            //public ICollection<UserTeam> UserTeams { get; set; }
            //public ICollection<User> CreatorTeams { get; set; }
            builder.HasMany(x => x.TeamEvents)
                   .WithOne(x => x.Team)
                   .HasForeignKey(x => x.TeamId);

            builder.HasMany(x => x.UserTeams)
                   .WithOne(x => x.Team)
                   .HasForeignKey(x => x.TeamId);

            builder.HasMany(x => x.Invitations)
                  .WithOne(x => x.Team)
                  .HasForeignKey(x => x.TeamId);
        }
    }
}
