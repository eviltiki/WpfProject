USE ShopDB
GO
 

DROP PROC prUpdateProductData
GO
DROP PROC prGetProductOnSale
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

CREATE PROC prUpdateProductData
AS
DECLARE @Now DATETIME = GETDATE()
  UPDATE Product SET OnSale = 1 WHERE Date <= @Now AND OnSale = 0
GO

CREATE PROC prGetProductOnSale
AS
  SELECT * FROM Product
    WHERE OnSale = 1

  UPDATE Product SET OnSale = 2 WHERE OnSale = 1
GO
