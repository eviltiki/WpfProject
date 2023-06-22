DROP DATABASE ShopDB
GO

CREATE DATABASE ShopDB
GO

USE ShopDB
GO

DROP VIEW Товары
GO
DROP TABLE Товар
GO

CREATE TABLE Товар
( Код INT IDENTITY PRIMARY KEY 
, Название VARCHAR(50) NOT NULL
, Цена MONEY NOT NULL CHECK(Цена > 0)
, Дата DATETIME NOT NULL
)
GO

CREATE VIEW Товары
AS
SELECT Название, Цена, Дата 
  FROM Товар
GO

INSERT INTO Товары(Название, Цена, Дата)
  VALUES('Телефон', 2700, '20230525 10:34:09')
GO
INSERT INTO Товары(Название, Цена, Дата)
  VALUES('Ноутбук', 2150, '20230602 14:12:01')
GO
INSERT INTO Товары(Название, Цена, Дата)
  VALUES('Калькулятор', 52, '20230617 17:54:00')
GO