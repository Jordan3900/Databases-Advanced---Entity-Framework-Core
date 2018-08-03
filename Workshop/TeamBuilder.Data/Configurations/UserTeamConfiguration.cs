using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(x => new { x.UserId, x.TeamId });

            //builder.HasOne(x => x.User)
            //    .WithMany(x => x.CreatedUserTeams)
            //    .HasForeignKey(x => x.UserId);

            //builder.HasOne(x => x.Team)
            //    .WithMany(x => x.UserTeams)
            //    .HasForeignKey(x => x.TeamId);
        }
    }
}
