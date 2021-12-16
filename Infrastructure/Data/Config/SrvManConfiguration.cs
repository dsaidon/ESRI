using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//how we want our migration to be created -->config our entities
namespace Infrastructure.Data.Config
{
    class SrvManConfiguration : IEntityTypeConfiguration<SrvMan>
    {
        public void Configure(EntityTypeBuilder<SrvMan> builder)
        {
            builder.Property(p => p.PhoneNo).IsRequired();
            builder.Property(p => p.FirstNm).IsRequired();
            builder.Property(p => p.LastNm).IsRequired();
            builder.HasOne(p => p.SrvManTp).WithMany().HasForeignKey(p => p.SrvManTpId);
        }
    }
}
