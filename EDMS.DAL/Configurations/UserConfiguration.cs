using EveryDaily.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany<Report>(x => x.Reports).
                WithOne(x => x.User).
                HasPrincipalKey(x => x.Id).
                HasForeignKey(x => x.UserId);
        }
    }
}
