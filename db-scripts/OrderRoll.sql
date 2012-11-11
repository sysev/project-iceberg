USE [GFDB]
GO

/****** Object:  Table [dbo].[OrderRoll]    Script Date: 11/11/2012 07:57:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[OrderRoll](
	[CutomerID] [int] NOT NULL,
	[GlatfelterOrderNumber] [varchar](20) NOT NULL,
	[CustomerPONumber] [varchar](20) NOT NULL,
	[RollNumber] [varchar](20) NOT NULL,
	[MaterialNumber] [varchar](20) NOT NULL,
	[CustomerPartNumber] [varchar](20) NOT NULL,
	[RollWeight] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ShippedDate] [datetime] NOT NULL,
	[ScheduleDeliveryDate] [datetime] NOT NULL,
	[Carrier] [varchar](20) NOT NULL,
	[ReceivedDate] [datetime] NOT NULL,
	[UsedDate] [datetime] NOT NULL,
	[LastPhysicalDate] [datetime] NOT NULL,
	[ShipmentNumber] [varchar](20) NOT NULL,
	[DeliveryNumber] [varchar](20) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

