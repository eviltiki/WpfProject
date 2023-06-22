DROP DATABASE ShopDB
GO

CREATE DATABASE ShopDB
GO

USE ShopDB
GO

DROP TABLE Товар
GO

CREATE TABLE Товар
( Код INT IDENTITY PRIMARY KEY 
, Название VARCHAR(50) NOT NULL
, Цена MONEY NOT NULL CHECK(Цена > 0)
, Дата DATE NOT NULL
)
GO

INSERT INTO Товары(Название, Цена, Дата)
  VALUES('Телефон', 2700, '2023-05-25')
GO
INSERT INTO Товары(Название, Цена, Дата)
  VALUES('Ноутбук', 2150, '2023-06-02')
GO
INSERT INTO Товары(Название, Цена, Дата)
  VALUES('Калькулятор', 52, '2023-06-17')
GO