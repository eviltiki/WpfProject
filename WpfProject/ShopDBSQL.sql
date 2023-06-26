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
, Date DATE NOT NULL
)
GO

INSERT INTO Product(Name, Price, Date)
  VALUES('Телефон', 2700, '2023-05-25')
GO
INSERT INTO Product(Name, Price, Date)
  VALUES('Ноутбук', 2150, '2023-06-02')
GO
INSERT INTO Product(Name, Price, Date)
  VALUES('Калькулятор', 52, '2023-06-17')
GO
