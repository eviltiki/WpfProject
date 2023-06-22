DROP DATABASE ShopDB
GO

CREATE DATABASE ShopDB
GO

USE ShopDB
GO

DROP VIEW ������
GO
DROP TABLE �����
GO

CREATE TABLE �����
( ��� INT IDENTITY PRIMARY KEY 
, �������� VARCHAR(50) NOT NULL
, ���� MONEY NOT NULL CHECK(���� > 0)
, ���� DATETIME NOT NULL
)
GO

CREATE VIEW ������
AS
SELECT ��������, ����, ���� 
  FROM �����
GO

INSERT INTO ������(��������, ����, ����)
  VALUES('�������', 2700, '20230525 10:34:09')
GO
INSERT INTO ������(��������, ����, ����)
  VALUES('�������', 2150, '20230602 14:12:01')
GO
INSERT INTO ������(��������, ����, ����)
  VALUES('�����������', 52, '20230617 17:54:00')
GO