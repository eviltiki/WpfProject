DROP DATABASE ShopDB
GO

CREATE DATABASE ShopDB
GO

USE ShopDB
GO

DROP TABLE �����
GO

CREATE TABLE �����
( ��� INT IDENTITY PRIMARY KEY 
, �������� VARCHAR(50) NOT NULL
, ���� MONEY NOT NULL CHECK(���� > 0)
, ���� DATE NOT NULL
)
GO

INSERT INTO ������(��������, ����, ����)
  VALUES('�������', 2700, '2023-05-25')
GO
INSERT INTO ������(��������, ����, ����)
  VALUES('�������', 2150, '2023-06-02')
GO
INSERT INTO ������(��������, ����, ����)
  VALUES('�����������', 52, '2023-06-17')
GO