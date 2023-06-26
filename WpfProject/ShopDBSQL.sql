DROP DATABASE ShopDB
GO

CREATE DATABASE ShopDB
GO

USE ShopDB
GO

DROP TABLE Product
GO

CREATE TABLE Product
( Id INT IDENTITY PRIMARY KEY 
, Name VARCHAR(50) NOT NULL
, Price MONEY NOT NULL CHECK(Price > 0)
, Date DATETIME NOT NULL
)
GO

INSERT INTO Product(Name, Price, Date)
  VALUES('Телефон', 2700, '25-05-2023 19:40')
GO
INSERT INTO Product(Name, Price, Date)
  VALUES('Ноутбук', 2700, '09-06-2023 17:20')
GO
INSERT INTO Product(Name, Price, Date)
  VALUES('Калькулятор', 2700, '25-06-2023 14:17')
GO

INSERT INTO Product(Name, Price, Date)
  VALUES('Калькулятор', 2700,'25-06-2023 14:17')
GO

SELECT * FROM Product