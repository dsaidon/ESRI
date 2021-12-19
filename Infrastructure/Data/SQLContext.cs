using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace Infrastructure.Data
{
    public class SQLContext : DbContext
    {

        public SQLContext(): base()
        {
        }

        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {
        }
        public DbSet<Area> Area { get; set; }
        public DbSet<CallsTaskInfo> CallsTaskInfo { get; set; }
        public DbSet<ClosingReason> ClosingReason { get; set; }
        public DbSet<CreadirCardTp> CreadirCardTp { get; set; }
        public DbSet<FaultTp> FaultTp { get; set; }
        public DbSet<FleetCarRequest> FleetCarRequest { get; set; }
        public DbSet<Fnc> Fnc { get; set; }
        public DbSet<RequestStsTp> RequestStsTp { get; set; }
        public DbSet<SiteParam> SiteParams { get; set; }
        public DbSet<SrcSrv> SrcSrv { get; set; }
        public DbSet<SrvMan> SrvMan { get; set; }
        public DbSet<SrvManAttn> SrvManAttn { get; set; }
        public DbSet<SrvManAttnTp> SrvManAttnTp { get; set; }
        public DbSet<SrvManExecSts> SrvManExecSts { get; set; }
        public DbSet<SrvManExecStsTp> SrvManExecStsTp { get; set; }
        public DbSet<SrvManShiftTp> SrvManShiftTp { get; set; }
        public DbSet<SrvManTp> SrvManTp { get; set; }
        public DbSet<SrvTp> SrvTp { get; set; }
        public DbSet<XYLocation> XYLocation { get; set; }
        public DbSet<SrvManClaim> SrvManClaims { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<LockedSrvMan> LockedSrvMans { get; set; }
        public DbSet<LockedIp> LockedIps { get; set; }
        public DbSet<Proc> Process { get; set; }
        public DbSet<StepType> StepType { get; set; }

        //we need to do is tell our store context that there's configurations to look for
        //we need to override one of the methods inside the context.
        //this method is responsible for creating that migration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}