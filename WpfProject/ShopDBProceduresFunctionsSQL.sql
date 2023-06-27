USE ShopDB
GO
 
DROP PROC prGetTrackerTable
GO
DROP TRIGGER trgProductIU
GO
DROP PROC prAddProduct
GO
DROP PROC prUpdateProduct
GO
DROP PROC prDeleteProduct
GO
DROP TABLE UpdateTrackerTable
GO

CREATE TABLE UpdateTrackerTable
( Id INT
, Name VARCHAR(50) 
, Price MONEY
, Date DATETIME 
)
GO

CREATE PROC prAddProduct
  @Name VARCHAR(50)
, @Price MONEY
, @Date DATETIME
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'INSERT INTO Product(Name, Price, Date)
               VALUES(@name, @price, @date)'

EXEC sp_executesql @sql, N'@name VARCHAR(50), @price MONEY, @date DATETIME', @Name, @Price, @Date
SELECT (ident_current('Product')) AS 'Product Id'
GO

CREATE PROC prUpdateProduct
  @Id INT
, @Name VARCHAR(50)
, @Price MONEY
, @Date DATETIME
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'UPDATE Product
               SET Name = @name, Price = @price, Date = @date
               WHERE Id = @code'

EXEC sp_executesql @sql, N'@code INT, @name VARCHAR(50), @price MONEY, @date DATETIME', 
  @Id, @Name, @Price, @Date
GO

CREATE PROC prDeleteProduct
  @Id INT
AS
DELETE Product
  WHERE Id = @Id
GO

CREATE TRIGGER trgProductIU ON Product
  AFTER INSERT, UPDATE
AS
  INSERT INTO UpdateTrackerTable 
    SELECT * FROM INSERTED
GO

CREATE PROC prGetTrackerTable
  @Date DATETIME
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'SELECT * FROM UpdateTrackerTable WHERE Date >= @appStartTime'

EXEC sp_executesql @sql, N'@appStartTime DATETIME', @Date 
DELETE FROM UpdateTrackerTable
GO
