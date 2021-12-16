using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    class FleetCarRequestConfiguration : IEntityTypeConfiguration<FleetCarRequest>
    {
        public void Configure(EntityTypeBuilder<FleetCarRequest> builder)
        {
            builder.Property(p => p.SubAdmissionTm).IsRequired(false);
            builder.Property(p => p.SubReturnTm).IsRequired(false);
            builder.HasOne(p => p.SrcSrv).WithMany()
                .HasForeignKey(p => p.SrcSrvId);
            builder.HasOne(p => p.SrvTp).WithMany()
                .HasForeignKey(p => p.SrvTpId);
            builder.HasOne(p => p.AreaSrc).WithMany()
                .HasForeignKey(p => p.SbcCarAreaId);
            builder.HasOne(p => p.AreaDest).WithMany()
                .HasForeignKey(p => p.DestAreaId);
        }
    }
}
