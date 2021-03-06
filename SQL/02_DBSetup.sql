USE [Transactions]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO	

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transactions')
BEGIN
	/****** Object:  Table [dbo].[Transcations]    Script Date: 2020-08-20 16:44:57 ******/
	CREATE TABLE [dbo].[Transactions](
		[Id] [nchar](20) NOT NULL,
		[Amount] [numeric](18, 0) NOT NULL,
		[Currency] [nchar](5) NOT NULL,
		[Date] [date] NOT NULL,
		[Status] [smallint] NOT NULL,
	 CONSTRAINT [PK_Transactions_Id] PRIMARY KEY CLUSTERED ([Id] ASC )
	 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
	 ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_Currency] ON [dbo].[Transactions]
	([Currency] ASC)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, 
		DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, 
		OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	
	CREATE NONCLUSTERED INDEX [IX_Date] ON [dbo].[Transactions]
	([Date] ASC)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, 
		DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
		OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='_TransactionsTempUpdate')
BEGIN
	CREATE TABLE [dbo].[_TransactionsTempUpdate](
		[Id] [nchar](20) NOT NULL,
		[Amount] [numeric](18, 0) NOT NULL,
		[Currency] [nchar](5) NOT NULL,
		[Date] [date] NOT NULL,
		[Status] [smallint] NOT NULL,
	 CONSTRAINT [PK_TransactionsTempUpdate_Id] PRIMARY KEY CLUSTERED ([Id] ASC )
	 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
	 ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END