USE ShopDB
GO

DROP PROC pr����������������
GO
DROP PROC pr����������������
GO
DROP PROC pr��������������
GO

CREATE PROC pr����������������
  @�������� VARCHAR(50)
, @���� MONEY
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'INSERT INTO �����(��������, ����, ����)
               VALUES(@name, @price, (SELECT CONVERT (datetime, GETDATE())))'

EXEC sp_executesql @sql, N'@name VARCHAR(50), @price MONEY', @��������, @����
SELECT (ident_current('�����')) AS '��� ������'
GO

CREATE PROC pr����������������
  @��� INT
, @�������� VARCHAR(50)
, @���� MONEY
AS
DECLARE @sql NVARCHAR(1000)
SET @sql = N'UPDATE �����
               SET �������� = @name, ���� = @price, ���� = (SELECT CONVERT (datetime, GETDATE()))
               WHERE ��� = @code'

EXEC sp_executesql @sql, N'@code INT, @name VARCHAR(50), @price MONEY', 
  @���, @��������, @����
GO

CREATE PROC pr��������������
  @��� INT
AS
DELETE �����
  WHERE ��� = @���
GO