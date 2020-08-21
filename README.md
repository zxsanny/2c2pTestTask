# 2c2p Test Task Transactions Manager
## Getting started
### Pre requisites:
1. [Visual Studio 2019 with installed .NET Standard 2.0](https://visualstudio.microsoft.com/vs/)
2. [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. [Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
4. [NodeJS](https://nodejs.org/en/)

### Setup DB
Open Management Studio and connect to localhost db
1. Run SQL\01_DBSetup.sql
2. Run SQL\02_DBSetup.sql
3. Make sure that MSSQL server is running in a mixed auth mode: Server properties - Security - authentication should be in 'SQL Server and Windos Authentication mode',
  after possible change restart MSSQL server (using SQL Server Configuration Manager)

### Run application
