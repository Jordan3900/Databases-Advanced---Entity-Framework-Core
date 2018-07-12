using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.TeamId);
            builder.Property(x => x.Name)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(x => x.Initials)
                .HasColumnType("CHAR(3)")
                .IsRequired().IsUnicode();

            builder.HasOne(x => x.PrimaryKitColor)
                .WithMany(x => x.PrimaryKitTeams)
                .HasForeignKey(x => x.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SecondaryKitColor)
              .WithMany(x => x.SecondaryKitTeams)
              .HasForeignKey(x => x.SecondaryKitColorId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Town)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.TownId);
        }
    }
}
