/****** Object:  Table [Production].[ProductProductPhoto]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[ProductProductPhoto]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[ProductProductPhoto](
	[ProductID] [int] NOT NULL,
	[ProductPhotoID] [int] NOT NULL,
	[Primary] [dbo].[Flag] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductProductPhoto_ProductID_ProductPhotoID] PRIMARY KEY NONCLUSTERED 
(
	[ProductID] ASC,
	[ProductPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductProductPhoto_Primary]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductProductPhoto] ADD  CONSTRAINT [DF_ProductProductPhoto_Primary]  DEFAULT ((0)) FOR [Primary]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductProductPhoto_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductProductPhoto] ADD  CONSTRAINT [DF_ProductProductPhoto_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductProductPhoto_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductProductPhoto]'))
ALTER TABLE [Production].[ProductProductPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductPhoto_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductProductPhoto_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductProductPhoto]'))
ALTER TABLE [Production].[ProductProductPhoto] CHECK CONSTRAINT [FK_ProductProductPhoto_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductProductPhoto_ProductPhoto_ProductPhotoID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductProductPhoto]'))
ALTER TABLE [Production].[ProductProductPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductPhoto_ProductPhoto_ProductPhotoID] FOREIGN KEY([ProductPhotoID])
REFERENCES [Production].[ProductPhoto] ([ProductPhotoID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductProductPhoto_ProductPhoto_ProductPhotoID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductProductPhoto]'))
ALTER TABLE [Production].[ProductProductPhoto] CHECK CONSTRAINT [FK_ProductProductPhoto_ProductPhoto_ProductPhotoID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'COLUMN',N'ProductPhotoID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product photo identification number. Foreign key to ProductPhoto.ProductPhotoID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'COLUMN',@level2name=N'ProductPhotoID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'COLUMN',N'Primary'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = Photo is not the principal image. 1 = Photo is the principal image.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'COLUMN',@level2name=N'Primary'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'CONSTRAINT',N'DF_ProductProductPhoto_Primary'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0 (FALSE)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductProductPhoto_Primary'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'CONSTRAINT',N'DF_ProductProductPhoto_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductProductPhoto_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'CONSTRAINT',N'PK_ProductProductPhoto_ProductID_ProductPhotoID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductProductPhoto_ProductID_ProductPhotoID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping products and product photos.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'CONSTRAINT',N'FK_ProductProductPhoto_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductProductPhoto_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductProductPhoto', N'CONSTRAINT',N'FK_ProductProductPhoto_ProductPhoto_ProductPhotoID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductPhoto.ProductPhotoID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductProductPhoto', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductProductPhoto_ProductPhoto_ProductPhotoID'
GO
