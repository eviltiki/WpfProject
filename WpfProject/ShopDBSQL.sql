DROP DATABASE ShopDB
GO

CREATE DATABASE ShopDB
GO

USE ShopDB
GO
 
DROP SERVICE SBInitiatorService
GO
DROP QUEUE SBInitiatorQueue
GO

ALTER DATABASE ShopDB
  SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE
GO

DROP TABLE Product
GO

CREATE TABLE Product
( Id INT IDENTITY PRIMARY KEY 
, Name VARCHAR(50) NOT NULL
, Price MONEY NOT NULL CHECK(Price > 0)
, Date DATETIME NOT NULL
, OnSale tinyint DEFAULT 0
)
GO

ALTER DATABASE ShopDB SET DISABLE_BROKER;

ALTER DATABASE ShopDB SET ENABLE_BROKER
GO

CREATE QUEUE SBInitiatorQueue
GO

CREATE SERVICE SBInitiatorService
ON QUEUE SBInitiatorQueue
GO

INSERT INTO Product(Name, Price, Date, OnSale)
  VALUES('�������', 2700, '25-05-2023 19:40', 2)
GO
INSERT INTO Product(Name, Price, Date, OnSale)
  VALUES('�������', 2700, '09-06-2023 17:20', 2)
GO
INSERT INTO Product(Name, Price, Date, OnSale)
  VALUES('�����������', 2700, '25-06-2023 14:17', 2)
GO

