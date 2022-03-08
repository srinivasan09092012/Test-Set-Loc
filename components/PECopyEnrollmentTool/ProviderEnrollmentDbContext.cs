using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Microsoft.Practices.Unity;
using System.Data.Entity;

namespace PECopyEnrollment
{
    public partial class ProviderEnrollmentDbContext : DbContextBase
    {
        public ProviderEnrollmentDbContext(IDbSession databaseSession, bool contextOwnsSession = false)
            : base(databaseSession, contextOwnsSession)
        {
            Database.SetInitializer<ProviderEnrollmentDbContext>(new NullDatabaseInitializer<ProviderEnrollmentDbContext>());
        }

        [InjectionConstructor]
        public ProviderEnrollmentDbContext(IDbSession databaseSession)
            : base(databaseSession, false)
        {
            Database.SetInitializer<ProviderEnrollmentDbContext>(new NullDatabaseInitializer<ProviderEnrollmentDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = this.GetSchemaName("UA3_PROVIDER_ENROLL");
            modelBuilder.HasDefaultSchema(schema);
        }
    }
}
