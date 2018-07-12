using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class HomeworkConfig : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder
                 .Property(p => p.Content)
                 .IsUnicode(false);

            builder
                .ToTable("HomeworkSubmissions");
        }
    }
}
