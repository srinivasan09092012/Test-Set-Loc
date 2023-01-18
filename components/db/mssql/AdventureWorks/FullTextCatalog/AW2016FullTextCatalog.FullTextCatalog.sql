/****** Object:  FullTextCatalog [AW2016FullTextCatalog]    ******/
IF NOT EXISTS (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'AW2016FullTextCatalog')
CREATE FULLTEXT CATALOG [AW2016FullTextCatalog] AS DEFAULT
GO
