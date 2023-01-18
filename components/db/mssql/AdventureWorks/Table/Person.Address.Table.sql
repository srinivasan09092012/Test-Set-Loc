/****** Object:  Table [Person].[Address]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [Person].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[AddressLine1] [nvarchar](60) NOT NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[City] [nvarchar](30) NOT NULL,
	[StateProvinceID] [int] NOT NULL,
	[PostalCode] [nvarchar](15) NOT NULL,
	[SpatialLocation] [geography] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Address_AddressID] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Index [AK_Address_rowguid]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[Address]') AND name = N'AK_Address_rowguid')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Address_rowguid] ON [Person].[Address]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[Address]') AND name = N'IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode] ON [Person].[Address]
(
	[AddressLine1] ASC,
	[AddressLine2] ASC,
	[City] ASC,
	[StateProvinceID] ASC,
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_StateProvinceID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Person].[Address]') AND name = N'IX_Address_StateProvinceID')
CREATE NONCLUSTERED INDEX [IX_Address_StateProvinceID] ON [Person].[Address]
(
	[StateProvinceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_Address_rowguid]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Address] ADD  CONSTRAINT [DF_Address_rowguid]  DEFAULT (newid()) FOR [rowguid]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Person].[DF_Address_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Person].[Address] ADD  CONSTRAINT [DF_Address_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Address_StateProvince_StateProvinceID]') AND parent_object_id = OBJECT_ID(N'[Person].[Address]'))
ALTER TABLE [Person].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_StateProvince_StateProvinceID] FOREIGN KEY([StateProvinceID])
REFERENCES [Person].[StateProvince] ([StateProvinceID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Person].[FK_Address_StateProvince_StateProvinceID]') AND parent_object_id = OBJECT_ID(N'[Person].[Address]'))
ALTER TABLE [Person].[Address] CHECK CONSTRAINT [FK_Address_StateProvince_StateProvinceID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'AddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for Address records.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'AddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'AddressLine1'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'First street address line.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'AddressLine1'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'AddressLine2'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Second street address line.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'AddressLine2'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'City'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the city.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'City'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique identification number for the state or province. Foreign key to StateProvince table.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'StateProvinceID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'PostalCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Postal code for the street address.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'PostalCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'SpatialLocation'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Latitude and longitude of this address.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'SpatialLocation'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'CONSTRAINT',N'DF_Address_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of NEWID()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'CONSTRAINT',@level2name=N'DF_Address_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'CONSTRAINT',N'DF_Address_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'CONSTRAINT',@level2name=N'DF_Address_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'CONSTRAINT',N'PK_Address_AddressID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'CONSTRAINT',@level2name=N'PK_Address_AddressID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'INDEX',N'AK_Address_rowguid'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index. Used to support replication samples.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'INDEX',@level2name=N'AK_Address_rowguid'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'INDEX',N'IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'INDEX',@level2name=N'IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'INDEX',N'IX_Address_StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'INDEX',@level2name=N'IX_Address_StateProvinceID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Street address information for customers, employees, and vendors.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Person', N'TABLE',N'Address', N'CONSTRAINT',N'FK_Address_StateProvince_StateProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing StateProvince.StateProvinceID.' , @level0type=N'SCHEMA',@level0name=N'Person', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'CONSTRAINT',@level2name=N'FK_Address_StateProvince_StateProvinceID'
GO
