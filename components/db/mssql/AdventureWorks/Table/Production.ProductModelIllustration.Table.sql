/****** Object:  Table [Production].[ProductModelIllustration]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[ProductModelIllustration]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[ProductModelIllustration](
	[ProductModelID] [int] NOT NULL,
	[IllustrationID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductModelIllustration_ProductModelID_IllustrationID] PRIMARY KEY CLUSTERED 
(
	[ProductModelID] ASC,
	[IllustrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductModelIllustration_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductModelIllustration] ADD  CONSTRAINT [DF_ProductModelIllustration_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelIllustration_Illustration_IllustrationID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelIllustration]'))
ALTER TABLE [Production].[ProductModelIllustration]  WITH CHECK ADD  CONSTRAINT [FK_ProductModelIllustration_Illustration_IllustrationID] FOREIGN KEY([IllustrationID])
REFERENCES [Production].[Illustration] ([IllustrationID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelIllustration_Illustration_IllustrationID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelIllustration]'))
ALTER TABLE [Production].[ProductModelIllustration] CHECK CONSTRAINT [FK_ProductModelIllustration_Illustration_IllustrationID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelIllustration_ProductModel_ProductModelID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelIllustration]'))
ALTER TABLE [Production].[ProductModelIllustration]  WITH CHECK ADD  CONSTRAINT [FK_ProductModelIllustration_ProductModel_ProductModelID] FOREIGN KEY([ProductModelID])
REFERENCES [Production].[ProductModel] ([ProductModelID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductModelIllustration_ProductModel_ProductModelID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductModelIllustration]'))
ALTER TABLE [Production].[ProductModelIllustration] CHECK CONSTRAINT [FK_ProductModelIllustration_ProductModel_ProductModelID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'COLUMN',N'ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to ProductModel.ProductModelID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'COLUMN',@level2name=N'ProductModelID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'COLUMN',N'IllustrationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Illustration.IllustrationID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'COLUMN',@level2name=N'IllustrationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'CONSTRAINT',N'DF_ProductModelIllustration_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductModelIllustration_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'CONSTRAINT',N'PK_ProductModelIllustration_ProductModelID_IllustrationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductModelIllustration_ProductModelID_IllustrationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping product models and illustrations.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'CONSTRAINT',N'FK_ProductModelIllustration_Illustration_IllustrationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Illustration.IllustrationID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductModelIllustration_Illustration_IllustrationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModelIllustration', N'CONSTRAINT',N'FK_ProductModelIllustration_ProductModel_ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductModel.ProductModelID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModelIllustration', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductModelIllustration_ProductModel_ProductModelID'
GO
