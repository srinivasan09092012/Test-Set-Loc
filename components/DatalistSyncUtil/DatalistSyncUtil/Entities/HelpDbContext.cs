//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil.Entities
{
    using HP.HSP.UA3.Core.BAS.CQRS.Base;
    using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
    using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
    using Microsoft.Practices.Unity;
    using System.Configuration;
    using System.Data.Entity;

    public partial class HelpDbContext : DbContextBase, IHelpDbContext
    {
        public HelpDbContext(IDbSession session, bool contextOwnsSession = false)
            : base(session, contextOwnsSession)
        {
        }

        [InjectionConstructor]
        public HelpDbContext(IDbSession session)
            : base(session)
        {
        }

        public virtual IDbSet<HelpMita> HelpMita { get; set; }

        public virtual IDbSet<HelpNode> HelpNode { get; set; }

        public virtual IDbSet<HelpNodeLink> HelpNodeLink { get; set; }

        public virtual IDbSet<HelpNodeLocale> HelpNodeLocale { get; set; }

        public virtual IDbSet<TenantModule> TenantModule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ConfigurationManager.AppSettings["SchemaName"];

            if (string.IsNullOrEmpty(schema))
            {
                schema = "TENANT_WRK";
            }

            modelBuilder.Entity<HelpMita>()
                .HasEntitySetName("HelpMita")
                .ToTable("HELP_MITA", schema);

            modelBuilder.Entity<HelpMita>()
                .Property(e => e.MitaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<HelpMita>()
                .Property(e => e.MitaUrl)
                .IsUnicode(false);

            modelBuilder.Entity<HelpMita>()
                .Property(e => e.OperatorID)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNode>()
                .HasEntitySetName("HelpNode")
                .ToTable("HELP_NODE", schema);

            modelBuilder.Entity<HelpNode>()
                .Property(e => e.HelpNodeNM)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNode>()
                .Property(e => e.HelpNodeTypeCD)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNode>()
                .Property(e => e.OperatorID)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNode>()
                .Property(e => e.HowToHelpNodeNM)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNode>()
                .HasMany(e => e.HelpNodeLink)
                .WithRequired(e => e.HelpNode)
                .HasForeignKey(e => e.ParentNodeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HelpNode>()
                .HasMany(e => e.HelpNodeLink1)
                .WithRequired(e => e.HelpNode1)
                .HasForeignKey(e => e.ChildNodeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HelpNode>()
                .HasMany(e => e.HelpNodeLocale)
                .WithRequired(e => e.HelpNode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HelpNodeLink>()
                .HasEntitySetName("HelpNodeLink")
                .ToTable("HELP_NODE_LINK", schema);

            modelBuilder.Entity<HelpNodeLink>()
                .Property(e => e.OperatorID)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNodeLocale>()
                .Property(e => e.LocaleId)
                .IsUnicode(false);

            modelBuilder.Entity<HelpNodeLocale>()
                .HasEntitySetName("HelpNodeLocale")
                .ToTable("HELP_NODE_LOCALE", schema);

            modelBuilder.Entity<HelpNodeLocale>()
                .Property(e => e.OperatorID)
                .IsUnicode(false);

            modelBuilder.Entity<TenantModule>()
                .HasEntitySetName("TenantModule")
                .ToTable("TENANT_MODULE", schema);

            modelBuilder.Entity<TenantModule>()
                .Property(e => e.OperatorId)
                .IsUnicode(false);
        }
    }
}
