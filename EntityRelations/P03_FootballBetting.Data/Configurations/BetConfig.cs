using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    class BetConfig : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(x => x.BetId);

            builder.HasOne(x => x.Game)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.User)
              .WithMany(x => x.Bets)
              .HasForeignKey(x => x.UserId);

            builder.Property(x => x.Prediction).IsRequired();
        }
    }
}
