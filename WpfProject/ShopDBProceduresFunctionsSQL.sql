USE ShopDB
GO

DROP PROC prДобавлениеТовара
GO
DROP PROC prОбновлениеТовара
GO
DROP PROC prУдалениеТовара
GO

CREATE PROC prДобавлениеТовара
  @Название VARCHAR(50)
, @Цена MONEY
, @Дата DATE
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'INSERT INTO Товар(Название, Цена, Дата)
               VALUES(@name, @price, @date)'

EXEC sp_executesql @sql, N'@name VARCHAR(50), @price MONEY, @date DATE', @Название, @Цена, @Дата
SELECT (ident_current('Товар')) AS 'Код записи'
GO

CREATE PROC prОбновлениеТовара
  @Код INT
, @Название VARCHAR(50)
, @Цена MONEY
, @Дата DATE
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'UPDATE Товар
               SET Название = @name, Цена = @price, Дата = @date
               WHERE Код = @code'

EXEC sp_executesql @sql, N'@code INT, @name VARCHAR(50), @price MONEY, @date DATE', 
  @Код, @Название, @Цена, @Дата
GO

CREATE PROC prУдалениеТовара
  @Код INT
AS
DELETE Товар
  WHERE Код = @Код
GO