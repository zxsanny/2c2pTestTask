IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Transactions')
BEGIN
	CREATE DATABASE [Transactions]
END
GO

If NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = 'TransactionsAdmin')
BEGIN
	CREATE LOGIN TransactionsAdmin WITH PASSWORD = 'VkH3j9_8-DZfjqMV'
END
GO

If NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = 'TransactionsWeb')
BEGIN
	CREATE LOGIN TransactionsWeb WITH PASSWORD = 'AS!dML4Z93+@y@^K'
END
GO

Use [Transactions]
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'TransactionsAdmin')
BEGIN
    CREATE USER [TransactionsAdmin] FOR LOGIN [TransactionsAdmin]
    EXEC sp_addrolemember N'db_owner', N'TransactionsAdmin'
END
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'TransactionsWeb')
BEGIN
	CREATE USER [TransactionsWeb] FOR LOGIN [TransactionsWeb]
    EXEC sp_addrolemember N'db_datareader', N'TransactionsWeb'
	EXEC sp_addrolemember N'db_datawriter', N'TransactionsWeb'
END
GO
