USE [GFDB]
GO

/****** Object:  Table [dbo].[AutoReplenishment]    Script Date: 11/11/2012 07:57:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AutoReplenishment](
	[CustomerID] [int] NOT NULL,
	[MaterialNumber] [varchar](20) NOT NULL,
	[AutoReplenishment] [bit] NOT NULL,
	[ReplenishmentThreshold] [int] NOT NULL,
	[ReorderAmount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

