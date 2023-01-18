/****** Object:  Table [Production].[ProductReview]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[ProductReview]') AND type in (N'U'))
BEGIN
CREATE TABLE [Production].[ProductReview](
	[ProductReviewID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ReviewerName] [dbo].[Name] NOT NULL,
	[ReviewDate] [datetime] NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Rating] [int] NOT NULL,
	[Comments] [nvarchar](3850) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReview_ProductReviewID] PRIMARY KEY CLUSTERED 
(
	[ProductReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductReview_ProductID_Name]    ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Production].[ProductReview]') AND name = N'IX_ProductReview_ProductID_Name')
CREATE NONCLUSTERED INDEX [IX_ProductReview_ProductID_Name] ON [Production].[ProductReview]
(
	[ProductID] ASC,
	[ReviewerName] ASC
)
INCLUDE ( 	[Comments]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     ******/
IF not EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[Production].[ProductReview]'))
CREATE FULLTEXT INDEX ON [Production].[ProductReview]
KEY INDEX [PK_ProductReview_ProductReviewID]ON ([AW2016FullTextCatalog], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductReview_ReviewDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductReview] ADD  CONSTRAINT [DF_ProductReview_ReviewDate]  DEFAULT (getdate()) FOR [ReviewDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[DF_ProductReview_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Production].[ProductReview] ADD  CONSTRAINT [DF_ProductReview_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductReview_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductReview]'))
ALTER TABLE [Production].[ProductReview]  WITH CHECK ADD  CONSTRAINT [FK_ProductReview_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [Production].[Product] ([ProductID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Production].[FK_ProductReview_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductReview]'))
ALTER TABLE [Production].[ProductReview] CHECK CONSTRAINT [FK_ProductReview_Product_ProductID]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_ProductReview_Rating]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductReview]'))
ALTER TABLE [Production].[ProductReview]  WITH CHECK ADD  CONSTRAINT [CK_ProductReview_Rating] CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[Production].[CK_ProductReview_Rating]') AND parent_object_id = OBJECT_ID(N'[Production].[ProductReview]'))
ALTER TABLE [Production].[ProductReview] CHECK CONSTRAINT [CK_ProductReview_Rating]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'ProductReviewID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key for ProductReview records.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'ProductReviewID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product identification number. Foreign key to Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'ReviewerName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the reviewer.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'ReviewerName'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'ReviewDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date review was submitted.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'ReviewDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'CONSTRAINT',N'DF_ProductReview_ReviewDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductReview_ReviewDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'EmailAddress'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reviewer''s e-mail address.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'Rating'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product rating given by the reviewer. Scale is 1 to 5 with 5 as the highest rating.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'Rating'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'Comments'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reviewer''s comments' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'Comments'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'CONSTRAINT',N'DF_ProductReview_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'CONSTRAINT',@level2name=N'DF_ProductReview_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'CONSTRAINT',N'PK_ProductReview_ProductReviewID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'CONSTRAINT',@level2name=N'PK_ProductReview_ProductReviewID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'INDEX',N'IX_ProductReview_ProductID_Name'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nonclustered index.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'INDEX',@level2name=N'IX_ProductReview_ProductID_Name'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer reviews of products they have purchased.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'CONSTRAINT',N'FK_ProductReview_Product_ProductID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Product.ProductID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'CONSTRAINT',@level2name=N'FK_ProductReview_Product_ProductID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Production', N'TABLE',N'ProductReview', N'CONSTRAINT',N'CK_ProductReview_Rating'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check constraint [Rating] BETWEEN (1) AND (5)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReview', @level2type=N'CONSTRAINT',@level2name=N'CK_ProductReview_Rating'
GO
