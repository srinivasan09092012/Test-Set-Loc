/****** Object:  Table [Production].[ProductModelProductDescriptionCulture]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[ProductModelProductDescriptionCulture](
	[ProductModelID] [int] NOT NULL,
	[ProductDescriptionID] [int] NOT NULL,
	[CultureID] [nchar](6) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID] PRIMARY KEY CLUSTERED 
(
	[ProductModelID] ASC,
	[ProductDescriptionID] ASC,
	[CultureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductModelProductDescriptionCulture_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductModelProductDescriptionCulture] ADD  CONSTRAINT [DF_ProductModelProductDescriptionCulture_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelProductDescriptionCulture_Culture_CultureID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]'))
ALTER TABLE [Production].[ProductModelProductDescriptionCulture]  WITH CHECK ADD  CONSTRAINT [FK_ProductModelProductDescriptionCulture_Culture_CultureID] FOREIGN KEY([CultureID])
REFERENCES [Production].[Culture] ([CultureID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelProductDescriptionCulture_Culture_CultureID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]'))
ALTER TABLE [Production].[ProductModelProductDescriptionCulture] CHECK CONSTRAINT [FK_ProductModelProductDescriptionCulture_Culture_CultureID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]'))
ALTER TABLE [Production].[ProductModelProductDescriptionCulture]  WITH CHECK ADD  CONSTRAINT [FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID] FOREIGN KEY([ProductDescriptionID])
REFERENCES [Production].[ProductDescription] ([ProductDescriptionID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]'))
ALTER TABLE [Production].[ProductModelProductDescriptionCulture] CHECK CONSTRAINT [FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]'))
ALTER TABLE [Production].[ProductModelProductDescriptionCulture]  WITH CHECK ADD  CONSTRAINT [FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID] FOREIGN KEY([ProductModelID])
REFERENCES [Production].[ProductModel] ([ProductModelID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelProductDescriptionCulture]'))
ALTER TABLE [Production].[ProductModelProductDescriptionCulture] CHECK CONSTRAINT [FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'COLUMN',N'ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to ProductModel.ProductModelID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'COLUMN',@level2name=N'ProductModelID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'COLUMN',N'ProductDescriptionID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to ProductDescription.ProductDescriptionID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'COLUMN',@level2name=N'ProductDescriptionID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'COLUMN',N'CultureID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Culture identification number. Foreign key to Culture.CultureID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'COLUMN',@level2name=N'CultureID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'CONSTRAINT',N'DF_ProductModelProductDescriptionCulture_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductModelProductDescriptionCulture_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'CONSTRAINT',N'PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping product descriptions and the language the description is written in.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'CONSTRAINT',N'FK_ProductModelProductDescriptionCulture_Culture_CultureID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Culture.CultureID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductModelProductDescriptionCulture_Culture_CultureID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'CONSTRAINT',N'FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductDescription.ProductDescriptionID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelProductDescriptionCulture', N'CONSTRAINT',N'FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductModel.ProductModelID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelProductDescriptionCulture', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID'
GO
