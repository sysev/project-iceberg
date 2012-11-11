USE [GFDB]
GO

/****** Object:  Table [dbo].[MaterialAvailability]    Script Date: 11/11/2012 07:57:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MaterialAvailability](
	[MaterialNumber] [varchar](20) NOT NULL,
	[QuanityInStock] [int] NOT NULL,
	[RollWeight] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaterialNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

