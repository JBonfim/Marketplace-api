USE [master]
GO
CREATE DATABASE [SMarketplaceDB]
 
GO
USE [SMarketplaceDB]
GO
CREATE TABLE [dbo].[product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Price] [float] NOT NULL,
	[Image] [varchar](500) NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [SMarketplaceDB] SET  READ_WRITE 
GO
