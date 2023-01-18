/****** Object:  Table [Sales].[PersonCreditCard]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[PersonCreditCard]') AND type in (N'U'))
BEGIN
CREATE TABLE [Sales].[PersonCreditCard](
	[BusinessEntityID] [int] NOT NULL,
	[CreditCardID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PersonCreditCard_BusinessEntityID_CreditCardID] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityID] ASC,
	[CreditCardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sales].[DF_PersonCreditCard_ModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [Sales].[PersonCreditCard] ADD  CONSTRAINT [DF_PersonCreditCard_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_PersonCreditCard_CreditCard_CreditCardID]') AND parent_object_id = OBJECT_ID(N'[Sales].[PersonCreditCard]'))
ALTER TABLE [Sales].[PersonCreditCard]  WITH CHECK ADD  CONSTRAINT [FK_PersonCreditCard_CreditCard_CreditCardID] FOREIGN KEY([CreditCardID])
REFERENCES [Sales].[CreditCard] ([CreditCardID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_PersonCreditCard_CreditCard_CreditCardID]') AND parent_object_id = OBJECT_ID(N'[Sales].[PersonCreditCard]'))
ALTER TABLE [Sales].[PersonCreditCard] CHECK CONSTRAINT [FK_PersonCreditCard_CreditCard_CreditCardID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_PersonCreditCard_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[PersonCreditCard]'))
ALTER TABLE [Sales].[PersonCreditCard]  WITH CHECK ADD  CONSTRAINT [FK_PersonCreditCard_Person_BusinessEntityID] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Sales].[FK_PersonCreditCard_Person_BusinessEntityID]') AND parent_object_id = OBJECT_ID(N'[Sales].[PersonCreditCard]'))
ALTER TABLE [Sales].[PersonCreditCard] CHECK CONSTRAINT [FK_PersonCreditCard_Person_BusinessEntityID]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'COLUMN',N'BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Business entity identification number. Foreign key to Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'COLUMN',@level2name=N'BusinessEntityID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'COLUMN',N'CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit card identification number. Foreign key to CreditCard.CreditCardID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'COLUMN',@level2name=N'CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'COLUMN',N'ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and time the record was last updated.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'CONSTRAINT',N'DF_PersonCreditCard_ModifiedDate'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default constraint value of GETDATE()' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'CONSTRAINT',@level2name=N'DF_PersonCreditCard_ModifiedDate'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'CONSTRAINT',N'PK_PersonCreditCard_BusinessEntityID_CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key (clustered) constraint' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'CONSTRAINT',@level2name=N'PK_PersonCreditCard_BusinessEntityID_CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cross-reference table mapping people to their credit card information in the CreditCard table. ' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'CONSTRAINT',N'FK_PersonCreditCard_CreditCard_CreditCardID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing CreditCard.CreditCardID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'CONSTRAINT',@level2name=N'FK_PersonCreditCard_CreditCard_CreditCardID'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'Sales', N'TABLE',N'PersonCreditCard', N'CONSTRAINT',N'FK_PersonCreditCard_Person_BusinessEntityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing Person.BusinessEntityID.' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'PersonCreditCard', @level2type=N'CONSTRAINT',@level2name=N'FK_PersonCreditCard_Person_BusinessEntityID'
GO
