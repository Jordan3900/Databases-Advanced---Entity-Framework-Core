using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(x => x.TownId);

            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.Country)
                .WithMany(x => x.Towns)
                .HasForeignKey(x => x.TownId);
        }
    }
}
