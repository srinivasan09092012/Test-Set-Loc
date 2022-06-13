namespace HPP.OneClick.XAML.Migration.DevContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BuildQueueContext : DbContext
    {
        public BuildQueueContext()
            : base("name=OneClickDevContext")
        {
        }

        public virtual DbSet<BUILD_QUEUE> BUILD_QUEUE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.SOLUTION_NAME_FULL_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.BUILD_ID)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.BUILD_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.CHANGESET_ID)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.TRIGGERED_BY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.TRIGGERED_BY_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.BUILD_PARAMS_JSON)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.OPERATOR_ID)
                .IsUnicode(false);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.LAST_MODIFIED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.CREATED_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.BUILD_START_TS)
                .HasPrecision(6);

            modelBuilder.Entity<BUILD_QUEUE>()
                .Property(e => e.BUILD_END_TS)
                .HasPrecision(6);
        }
    }
}
