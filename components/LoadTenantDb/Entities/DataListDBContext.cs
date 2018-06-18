//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
namespace HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities
{
    using System.Configuration;
    using System.Data.Entity;

    public partial class DataListDBContext : DbContext
    {
        public DataListDBContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual IDbSet<DataLists> DataLists { get; set; }

        public virtual IDbSet<DataListAttributeValues> DataListsAttributeValues { get; set; }

        public virtual IDbSet<DataListAttributes> DataListAttributes { get; set; }

        public virtual IDbSet<DataListsItems> DataListsItems { get; set; }

        public virtual IDbSet<DataListsItemsLinks> DataListsItemsLinks { get; set; }

        public virtual IDbSet<DataListsLanguages> DataListsLanguages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataLists>()
                .Property(e => e.ContentId)
                .IsUnicode(false);

            modelBuilder.Entity<DataLists>()
                .Property(e => e.DataListsName)
                .IsUnicode(false);

            modelBuilder.Entity<DataLists>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DataLists>()
                .HasMany(e => e.DataListsAttributeFK1)
                .WithRequired(e => e.DataListsAttributeValueFK1)
                .HasForeignKey(e => e.DataListsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataLists>()
                .HasMany(e => e.DataListsAttributeFK2)
                .WithRequired(e => e.DataListsAttributeValueFK2)
                .HasForeignKey(e => e.DataListsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListAttributes>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<DataListAttributes>()
                .HasMany(e => e.DataListsAttributeValue)
                .WithRequired(e => e.DataListsAttributes)
                .HasForeignKey(e => e.DataListAttributeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsItems>()
                .Property(e => e.DataListsItemKey)
                .IsUnicode(false);

            modelBuilder.Entity<DataListsItems>()
                .HasMany(e => e.DataListsAttributeValues)
                .WithRequired(e => e.DataListsItemFK1)
                .HasForeignKey(e => e.DataListsItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsItems>()
                .HasMany(e => e.DataListsItemAttributeValues)
                .WithRequired(e => e.DataListsItemFK2)
                .HasForeignKey(e => e.DataListsItemValueId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsItems>()
               .HasMany(e => e.DataListAttributes)
               .WithRequired(e => e.DataListsItem)
               .HasForeignKey(e => e.TypeDefaultItemId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsItems>()
                .HasMany(e => e.DataListsItemLinkParent)
                .WithRequired(e => e.DataListsItemParent)
                .HasForeignKey(e => e.ParentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsItems>()
                .HasMany(e => e.DataListsItemLinkChild)
                .WithRequired(e => e.DataListItemChild)
                .HasForeignKey(e => e.ChildId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsItems>()
                .HasMany(e => e.DataListsLanguages)
                .WithRequired(e => e.DataListsItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataListsLanguages>()
                .Property(e => e.LocalId)
                .IsUnicode(false);

            modelBuilder.Entity<DataListsLanguages>()
                .Property(e => e.Description)
                .IsUnicode(false);

            string schema = this.GetSchemaName(string.Empty);
            modelBuilder.HasDefaultSchema(schema);
        }
    }
}
