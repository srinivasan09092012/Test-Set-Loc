namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DevBuildTypeContext : DbContext
    {
        public DevBuildTypeContext()
            : base("name=OneClickDevContext")
        {
        }

        public virtual IDbSet<BRANCH_RELEASE_MAP> BRANCH_RELEASE_MAP { get; set; }
        public virtual IDbSet<BUILD_TYPE> BUILD_TYPE { get; set; }
        public virtual IDbSet<ENVIRONMENT_RELEASE_MAP> ENVIRONMENT_RELEASE_MAP { get; set; }
        public virtual IDbSet<JOB_CONFIG> JOB_CONFIG { get; set; }
        public virtual IDbSet<MODULE> MODULEs { get; set; }
        public virtual IDbSet<PACKAGE> PACKAGEs { get; set; }
        public virtual IDbSet<SLTN_PKG_DEPENDENCY> SLTN_PKG_DEPENDENCY { get; set; }
        public virtual IDbSet<SOLUTION> SOLUTIONs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRODUCT>()
    .Property(e => e.PRODUCT_NAME)
    .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.JOB_CONFIG)
                .WithRequired(e => e.PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BRANCH_RELEASE_MAP>()
                .Property(e => e.BRANCH_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BRANCH_RELEASE_MAP>()
                .Property(e => e.HPP_RELEASE)
                .IsUnicode(false);

            modelBuilder.Entity<BRANCH_RELEASE_MAP>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<BRANCH_RELEASE_MAP>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BRANCH_RELEASE_MAP>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BUILD_TYPE>()
                .Property(e => e.BUILD_TYPE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_TYPE>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_TYPE>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BUILD_TYPE>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BUILD_TYPE>()
                .HasMany(e => e.JOB_CONFIG)
                .WithRequired(e => e.BUILD_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ENVIRONMENT_RELEASE_MAP>()
                .Property(e => e.BUILD_RELEASE)
                .IsUnicode(false);

            modelBuilder.Entity<ENVIRONMENT_RELEASE_MAP>()
                .Property(e => e.HPP_RELEASE)
                .IsUnicode(false);

            modelBuilder.Entity<ENVIRONMENT_RELEASE_MAP>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<ENVIRONMENT_RELEASE_MAP>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<ENVIRONMENT_RELEASE_MAP>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.SOLUTION_NAME_FULL_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.TARGET_BUILD_ENVRMT)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.APLCTN_RELEASE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.BUILD_PLATFORM)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.BUILD_CONFIG)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.MSBUILD_ARGS)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.DEPLOYMENT_MODEL)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.TESTCASE_FILTER)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.TESTSOURCE_SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.TESTCASE_PLATFORM)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.DEPLOY_PACKAGE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<JOB_CONFIG>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);
                       

            modelBuilder.Entity<JOB_CONFIG>()
              .Property(e => e.COMBINED_BUILD_ARGS)
              .IsUnicode(false);

            modelBuilder.Entity<MODULE>()
                .Property(e => e.MODULE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<MODULE>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<MODULE>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<MODULE>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<MODULE>()
                .HasMany(e => e.JOB_CONFIG)
                .WithRequired(e => e.MODULE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MODULE>()
                .HasMany(e => e.PACKAGEs)
                .WithRequired(e => e.MODULE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MODULE>()
                .HasMany(e => e.SOLUTIONs)
                .WithRequired(e => e.MODULE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .Property(e => e.PACKAGE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<PACKAGE>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PACKAGE>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<PACKAGE>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.SLTN_PKG_DEPENDENCY)
                .WithRequired(e => e.PACKAGE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLTN_PKG_DEPENDENCY>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SLTN_PKG_DEPENDENCY>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<SLTN_PKG_DEPENDENCY>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<SOLUTION>()
                .Property(e => e.SOLUTION_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SOLUTION>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SOLUTION>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<SOLUTION>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<SOLUTION>()
                .HasMany(e => e.SLTN_PKG_DEPENDENCY)
                .WithRequired(e => e.SOLUTION)
                .WillCascadeOnDelete(false);
        }
    }
}
