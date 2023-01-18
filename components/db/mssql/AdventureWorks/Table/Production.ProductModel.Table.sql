/****** Object:  Table [Production].[ProductModel]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[ProductModel]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[ProductModel](
	[ProductModelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[CatalogDescription] [xml](CONTENT [Production].[ProductDescriptionSchemaCollection]) NULL,
	[Instructions] [xml](CONTENT [Production].[ManuInstructionsSchemaCollection]) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductModel_ProductModelID] PRIMARY KEY CLUSTERED 
(
	[ProductModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_ProductModel_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[ProductModel]') AND name = N'AK_ProductModel_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_ProductModel_Name] ON [Production].[ProductModel]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_ProductModel_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[ProductModel]') AND name = N'AK_ProductModel_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_ProductModel_rowguid] ON [Production].[ProductModel]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PXML_ProductModel_CatalogDescription]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[ProductModel]') AND name = N'PXML_ProductModel_CatalogDescription')
CREATE PRIMARY XML INDEX [PXML_ProductModel_CatalogDescription] ON [Production].[ProductModel]
(
	[CatalogDescription]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PXML_ProductModel_Instructions]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[ProductModel]') AND name = N'PXML_ProductModel_Instructions')
CREATE PRIMARY XML INDEX [PXML_ProductModel_Instructions] ON [Production].[ProductModel]
(
	[Instructions]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductModel_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductModel] ADD  CONSTRAINT [DF_ProductModel_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductModel_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductModel] ADD  CONSTRAINT [DF_ProductModel_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'COLUMN',N'ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for ProductModel records.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'COLUMN',@level2name=N'ProductModelID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product model description.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'COLUMN',N'CatalogDescription'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Detailed product catalog information in xml format.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'COLUMN',@level2name=N'CatalogDescription'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'COLUMN',N'Instructions'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Manufacturing instructions in xml format.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'COLUMN',@level2name=N'Instructions'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'CONSTRAINT',N'DF_ProductModel_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductModel_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'CONSTRAINT',N'DF_ProductModel_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductModel_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'CONSTRAINT',N'PK_ProductModel_ProductModelID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductModel_ProductModelID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'INDEX',N'AK_ProductModel_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'INDEX',@level2name=N'AK_ProductModel_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'INDEX',N'AK_ProductModel_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'INDEX',@level2name=N'AK_ProductModel_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'INDEX',N'PXML_ProductModel_CatalogDescription'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary XML index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'INDEX',@level2name=N'PXML_ProductModel_CatalogDescription'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', N'INDEX',N'PXML_ProductModel_Instructions'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary XML index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel', @level2type=N'INDEX',@level2name=N'PXML_ProductModel_Instructions'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductModel', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product model classification.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductModel'
GO
