using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    class TeamEventConfiguration : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.HasKey(x => new { x.EventId, x.TeamId });

            //builder.HasOne(et => et.Event)
            //    .WithMany(e => e.ParticipatingEventTeams)
            //    .HasForeignKey(e => e.EventId);

            //builder.HasOne(x => x.Team)
            //    .WithMany(x => x.TeamEvents)
            //    .HasForeignKey(x => x.TeamId);
        }
    }
}
