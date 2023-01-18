/****** Object:  Table [Production].[Location]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[Location]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[Location](
	[LocationID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[CostRate] [smallmoney] NOT NULL,
	[Availability] [decimal](8, 2) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Location_LocationID] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Location_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[Location]') AND name = N'AK_Location_Name')
CREATE UNIQUE NONCLUSTERED INDEX [AK_Location_Name] ON [Production].[Location]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Location_CostRate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Location] ADD  CONSTRAINT [DF_Location_CostRate]  DEFAULT ((0.00)) FOR [CostRate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Location_Availability]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Location] ADD  CONSTRAINT [DF_Location_Availability]  DEFAULT ((0.00)) FOR [Availability]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_Location_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[Location] ADD  CONSTRAINT [DF_Location_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Location_Availability]') AND parent_object_id = OBJECT_ID(N'[Production].[Location]'))
ALTER TABLE [Production].[Location]  WITH CHECK ADD  CONSTRAINT [CK_Location_Availability] CHECK  (([Availability]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Location_Availability]') AND parent_object_id = OBJECT_ID(N'[Production].[Location]'))
ALTER TABLE [Production].[Location] CHECK CONSTRAINT [CK_Location_Availability]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Location_CostRate]') AND parent_object_id = OBJECT_ID(N'[Production].[Location]'))
ALTER TABLE [Production].[Location]  WITH CHECK ADD  CONSTRAINT [CK_Location_CostRate] CHECK  (([CostRate]>=(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_Location_CostRate]') AND parent_object_id = OBJECT_ID(N'[Production].[Location]'))
ALTER TABLE [Production].[Location] CHECK CONSTRAINT [CK_Location_CostRate]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'COLUMN',N'LocationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for Location records.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'COLUMN',@level2name=N'LocationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'COLUMN',N'Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Location description.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'COLUMN',@level2name=N'Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'COLUMN',N'CostRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Standard hourly cost of the manufacturing location.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'COLUMN',@level2name=N'CostRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'CONSTRAINT',N'DF_Location_CostRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.0' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'CONSTRAINT',@level2name=N'DF_Location_CostRate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'COLUMN',N'Availability'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Work capacity (in hours) of the manufacturing location.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'COLUMN',@level2name=N'Availability'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'CONSTRAINT',N'DF_Location_Availability'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of 0.00' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'CONSTRAINT',@level2name=N'DF_Location_Availability'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'CONSTRAINT',N'DF_Location_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'CONSTRAINT',@level2name=N'DF_Location_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'CONSTRAINT',N'PK_Location_LocationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'CONSTRAINT',@level2name=N'PK_Location_LocationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'INDEX',N'AK_Location_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'INDEX',@level2name=N'AK_Location_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product inventory and manufacturing locations.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'CONSTRAINT',N'CK_Location_Availability'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Availability] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'CONSTRAINT',@level2name=N'CK_Location_Availability'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'Location', N'CONSTRAINT',N'CK_Location_CostRate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [CostRate] >= (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Location', @level2type=N'CONSTRAINT',@level2name=N'CK_Location_CostRate'
GO
