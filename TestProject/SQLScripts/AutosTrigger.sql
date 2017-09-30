CREATE TRIGGER Autos_trigger
ON Autos AFTER INSERT, UPDATE, DELETE AS 
IF NOT EXISTS (SELECT * FROM deleted)
BEGIN
	DECLARE @MsgInserted VARCHAR(MAX)
	SELECT @MsgInserted = (SELECT Model FROM inserted)
	INSERT INTO Logs(Message, Time, Category) VALUES('��������� ' + @MsgInserted, SYSDATETIME (), 'Autos')
END
ELSE IF EXISTS (SELECT * FROM inserted)
BEGIN
DECLARE @MsgUpdateOLD VARCHAR(MAX)
DECLARE @MsgUpdateNEW VARCHAR(MAX)
SELECT @MsgUpdateOLD = (SELECT Model FROM deleted)
SELECT @MsgUpdateNEW = (SELECT Model FROM inserted)
INSERT INTO Logs(Message, Time, Category) VALUES('�������� ' + @MsgUpdateOLD + ' �� ' + @MsgUpdateNEW, SYSDATETIME (), 'Autos')
END
ELSE
BEGIN
	DECLARE @MsgDelete VARCHAR(MAX)
	SELECT @MsgDelete = (SELECT Model FROM deleted)
	INSERT INTO Logs(Message, Time, Category) VALUES('������� ' + @MsgDelete, SYSDATETIME (), 'Autos')
END