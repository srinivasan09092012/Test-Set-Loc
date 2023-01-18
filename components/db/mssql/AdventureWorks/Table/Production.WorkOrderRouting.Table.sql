/****** Object:  Table [Production].[WorkOrderRouting]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[WorkOrderRouting](
	[WorkOrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[OperationSequence] [smallint] NOT NULL,
	[LocationID] [smallint] NOT NULL,
	[ScheduledStartDate] [datetime] NOT NULL,
	[ScheduledEndDate] [datetime] NOT NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[ActualResourceHrs] [decimal](9, 4) NULL,
	[PlannedCost] [money] NOT NULL,
	[ActualCost] [money] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence] PRIMARY KEY CLUSTERED 
(
	[WorkOrderID] ASC,
	[ProductID] ASC,
	[OperationSequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_WorkOrderRouting_ProductID]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]') AND name = N'IX_WorkOrderRouting_ProductID')
CREATE NONCLUSTERED INDEX [IX_WorkOrderRouting_ProductID] ON [Production].[WorkOrderRouting]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_WorkOrderRouting_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[WorkOrderRouting] ADD  CONSTRAINT [DF_WorkOrderRouting_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrderRouting_Location_LocationID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrderRouting_Location_LocationID] FOREIGN KEY([LocationID])
REFERENCES [Production].[Location] ([LocationID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrderRouting_Location_LocationID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [FK_WorkOrderRouting_Location_LocationID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrderRouting_WorkOrder_WorkOrderID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrderRouting_WorkOrder_WorkOrderID] FOREIGN KEY([WorkOrderID])
REFERENCES [Production].[WorkOrder] ([WorkOrderID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_WorkOrderRouting_WorkOrder_WorkOrderID]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [FK_WorkOrderRouting_WorkOrder_WorkOrderID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ActualCost]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrderRouting_ActualCost] CHECK  (([ActualCost]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ActualCost]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [CK_WorkOrderRouting_ActualCost]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ActualEndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrderRouting_ActualEndDate] CHECK  (([ActualEndDate]>=[ActualStartDate] OR [ActualEndDate] IS NULL OR [ActualStartDate] IS NULL))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ActualEndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [CK_WorkOrderRouting_ActualEndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ActualResourceHrs]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrderRouting_ActualResourceHrs] CHECK  (([ActualResourceHrs]>=(0.0000)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ActualResourceHrs]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [CK_WorkOrderRouting_ActualResourceHrs]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_PlannedCost]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrderRouting_PlannedCost] CHECK  (([PlannedCost]>(0.00)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_PlannedCost]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [CK_WorkOrderRouting_PlannedCost]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ScheduledEndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting]  WITH CHECK ADD  CONSTRAINT [CK_WorkOrderRouting_ScheduledEndDate] CHECK  (([ScheduledEndDate]>=[ScheduledStartDate]))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_WorkOrderRouting_ScheduledEndDate]') AND parent_object_id = OBJECT_ID(N'[Production].[WorkOrderRouting]'))
ALTER TABLE [Production].[WorkOrderRouting] CHECK CONSTRAINT [CK_WorkOrderRouting_ScheduledEndDate]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'WorkOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to WorkOrder.WorkOrderID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'WorkOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'OperationSequence'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key. Indicates the manufacturing process sequence.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'OperationSequence'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'LocationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Manufacturing location where the part is processed. Foreign key to Location.LocationID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'LocationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ScheduledStartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Planned manufacturing start date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ScheduledStartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ScheduledEndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Planned manufacturing end date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ScheduledEndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ActualStartDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Actual start date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ActualStartDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ActualEndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Actual end date.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ActualEndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ActualResourceHrs'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of manufacturing hours used.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ActualResourceHrs'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'PlannedCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estimated manufacturing cost.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'PlannedCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ActualCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Actual manufacturing cost.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ActualCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'DF_WorkOrderRouting_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'DF_WorkOrderRouting_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'INDEX',N'IX_WorkOrderRouting_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'INDEX',@level2name=N'IX_WorkOrderRouting_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Work order details.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'FK_WorkOrderRouting_Location_LocationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Location.LocationID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'FK_WorkOrderRouting_Location_LocationID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'FK_WorkOrderRouting_WorkOrder_WorkOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing WorkOrder.WorkOrderID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'FK_WorkOrderRouting_WorkOrder_WorkOrderID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'CK_WorkOrderRouting_ActualCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ActualCost] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrderRouting_ActualCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'CK_WorkOrderRouting_ActualEndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ActualEndDate] >= [ActualStartDate] OR [ActualEndDate] IS NULL OR [ActualStartDate] IS NULL' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrderRouting_ActualEndDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'CK_WorkOrderRouting_ActualResourceHrs'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ActualResourceHrs] >= (0.0000)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrderRouting_ActualResourceHrs'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'CK_WorkOrderRouting_PlannedCost'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [PlannedCost] > (0.00)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrderRouting_PlannedCost'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'WorkOrderRouting', N'CONSTRAINT',N'CK_WorkOrderRouting_ScheduledEndDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [ScheduledEndDate] >= [ScheduledStartDate]' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'WorkOrderRouting', @level2type=N'CONSTRAINT',@level2name=N'CK_WorkOrderRouting_ScheduledEndDate'
GO
