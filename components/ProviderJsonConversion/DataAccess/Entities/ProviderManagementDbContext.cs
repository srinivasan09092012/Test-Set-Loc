//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Data.Entity;

namespace DataAccess.Entities
{
    public partial class ProviderManagementDbContext : DbContextBase
    {
        public ProviderManagementDbContext(IDbSession session, bool contextOwnsSession = false)
            : base(session, contextOwnsSession)
        {
            Database.SetInitializer<ProviderManagementDbContext>(new NullDatabaseInitializer<ProviderManagementDbContext>());
        }

        [InjectionConstructor]
        public ProviderManagementDbContext(IDbSession session)
            : base(session)
        {
            Database.SetInitializer<ProviderManagementDbContext>(new NullDatabaseInitializer<ProviderManagementDbContext>());
        }

        public virtual IDbSet<PROVIDER> PROVIDERs { get; set; }

        public virtual IDbSet<PROVIDER_SERVICE_LOCATION> PROVIDER_SERVICE_LOCATION { get; set; }

        public virtual IDbSet<ADDRESS> ADDRESSes { get; set; }

        public virtual IDbSet<PracticeLocationAddressAssociation> PracticeLocationAddressAssociation { get; set; }

        public virtual IDbSet<PROVIDER_SPECIALTY> PROVIDER_SPECIALTY { get; set; }

        public virtual IDbSet<PROVIDER_TAXONOMY> PROVIDER_TAXONOMY { get; set; }

        public virtual IDbSet<IRSTaxIdAssociation> PROVIDER_IRS_TAX_ID_ASSCN { get; set; }

        public ProviderManagementDbContext Create()
        {
            var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            DbSession session = null;

            if (cs != null)
            {
                session = new DbSession(cs.ProviderName, cs.ConnectionString);
            }
            else
            {
                session = new DbSession(ApplicationConfigurationManager.AppSettings["DefaultConnectionProvider"], ApplicationConfigurationManager.AppSettings["DefaultConnectionString"]);
            }

            return new ProviderManagementDbContext(session, true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = this.GetSchemaName(string.Empty);
            modelBuilder.HasDefaultSchema(schema);

            modelBuilder.Entity<PROVIDER>()
                .HasEntitySetName("PROVIDERs")
                .ToTable("PROVIDER", schema)
                .HasKey(e => e.PROVIDER_ID);

            modelBuilder.Entity<PROVIDER_SERVICE_LOCATION>()
                .HasEntitySetName("PROVIDER_SERVICE_LOCATION")
                .ToTable("PROVIDER_SERVICE_LOCATION", schema);

            modelBuilder.Entity<PracticeLocationAddressAssociation>()
                .HasEntitySetName("PracticeLocationAddressAssociation")
                .ToTable("PRACTICE_LCTN_ADR_ASSCN", schema);

            modelBuilder.Entity<ADDRESS>()
                .HasEntitySetName("ADDRESSes")
                .ToTable("ADDRESS", schema);

            modelBuilder.Entity<PROVIDER_SPECIALTY>()
                .HasEntitySetName("PROVIDER_SPECIALTY")
                .ToTable("PROVIDER_SPECIALTY", schema);

            modelBuilder.Entity<PROVIDER_TAXONOMY>()
                .HasEntitySetName("PROVIDER_TAXONOMY")
                .ToTable("PROVIDER_TAXONOMY", schema);

            modelBuilder.Entity<IRSTaxIdAssociation>()
                .HasEntitySetName("PROVIDER_IRS_TAX_ID_ASSCN")
                .ToTable("PROVIDER_IRS_TAX_ID_ASSCN", schema);

            modelBuilder.Entity<IRSTaxIdentification>()
                .HasEntitySetName("IRS_TAX_IDENTIFICATION")
                .ToTable("IRS_TAX_IDENTIFICATION", schema);
        }
    }
}
