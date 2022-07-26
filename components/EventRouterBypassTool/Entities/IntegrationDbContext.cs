//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Common;
using EventRouterByPassTool.Entities.PublishedEvents;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System.Data.Entity;

namespace EventRouterByPassTool.Entities
{
    public partial class IntegrationDbContext : DbContextBase, IIntegrationDbContext
    {
        #region Constructor
        public IntegrationDbContext(IDbSession session, bool contextOwnSession = false) : base(session, contextOwnSession)
        {
            Database.SetInitializer<IntegrationDbContext>(new NullDatabaseInitializer<IntegrationDbContext>());
        }

        public IntegrationDbContext(IDbSession session) : base(session)
        {
            Database.SetInitializer<IntegrationDbContext>(new NullDatabaseInitializer<IntegrationDbContext>());
        }
        #endregion

        #region Properties
        public virtual IDbSet<IntegrationModule> Modules { get; set; }
        
        public virtual IDbSet<ModuleEvent> ModuleEvents { get; set; }

        public virtual IDbSet<ModuleEventSubscription> ModuleEventSubscription { get; set; }

        public virtual IDbSet<EventSubscriptionError> EventSubscriptionErrors { get; set; }

        public virtual IDbSet<ExternalReq> ExternalReqs { get; set; }

        public virtual IDbSet<ModuleEventSubscriptionFilter> ModuleEventSubscriptionFilter { get; set; }
        #endregion

        #region Protected Method
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = this.GetSchemaName(string.Empty);

            modelBuilder.Entity<IntegrationModule>()
                .HasEntitySetName(EventRouterBypassConstants.DatabaseRelated.ModuleEntitySet)
                .ToTable(EventRouterBypassConstants.DatabaseRelated.Module, schema)
                .HasKey(e => e.ModuleID);

            modelBuilder.Entity<ModuleEvent>()
                .HasEntitySetName(EventRouterBypassConstants.DatabaseRelated.ModuleEventEntitySet)
                .ToTable(EventRouterBypassConstants.DatabaseRelated.ModuleEvents, schema)
                .HasKey(e => e.ModuleEventID);

            modelBuilder.Entity<ModuleEventSubscription>()
                .HasEntitySetName(EventRouterBypassConstants.DatabaseRelated.ModuleEventSubscriptions)
                .ToTable(EventRouterBypassConstants.DatabaseRelated.ModuleEventSubscriptionTable, schema)
                .HasKey(e => e.EventSubscriptionID);

            modelBuilder.Entity<EventSubscriptionError>()
                .HasEntitySetName(EventRouterBypassConstants.DatabaseRelated.EventSubscriptionErrorEntitySet)
                .ToTable(EventRouterBypassConstants.DatabaseRelated.EventSubscriptionsErrorTable, schema)
                .HasKey(e => e.ID);

            modelBuilder.Entity<EventSubscriptionError>()
                .Property(e => e.ErrorMessage)
                .IsUnicode(false);

            modelBuilder.Entity<ExternalReq>()
                .HasEntitySetName(EventRouterBypassConstants.DatabaseRelated.ExternalReqEntity)
                .ToTable(EventRouterBypassConstants.DatabaseRelated.ExternalReqTable, schema);

            modelBuilder.Entity<ModuleEventSubscriptionFilter>()
               .HasEntitySetName(EventRouterBypassConstants.DatabaseRelated.ModuleEventSubscriptionFilter)
               .ToTable(EventRouterBypassConstants.DatabaseRelated.ModuleEventSubscriptionFilterTable, schema)
               .HasKey(e => e.EventSubscriptionFilterID);
        }
        #endregion
    }
}
