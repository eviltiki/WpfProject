USE ShopDB
GO

DROP PROC prAddProduct
GO
DROP PROC prUpdateProduct
GO
DROP PROC prDeleteProduct
GO

CREATE PROC prAddProduct
  @Name VARCHAR(50)
, @Price MONEY
, @Date DATE
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'INSERT INTO Product(Name, Price, Date)
               VALUES(@name, @price, @date)'

EXEC sp_executesql @sql, N'@name VARCHAR(50), @price MONEY, @date DATE', @Name, @Price, @Date
SELECT (ident_current('Product')) AS 'Product Id'
GO

CREATE PROC prUpdateProduct
  @Id INT
, @Name VARCHAR(50)
, @Price MONEY
, @Date DATE
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'UPDATE Product
               SET Name = @name, Price = @price, Date = @date
               WHERE Id = @code'

EXEC sp_executesql @sql, N'@code INT, @name VARCHAR(50), @price MONEY, @date DATE', 
  @Id, @Name, @Price, @Date
GO

CREATE PROC prDeleteProduct
  @Id INT
AS
DELETE Product
  WHERE Id = @Id
GO