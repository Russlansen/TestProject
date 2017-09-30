CREATE TRIGGER Pictures_trigger
ON Pictures AFTER INSERT, UPDATE, DELETE AS 
IF NOT EXISTS (SELECT * FROM deleted)
BEGIN
	DECLARE @MsgInserted VARCHAR(MAX)
	SELECT @MsgInserted = (SELECT Title FROM inserted)
	INSERT INTO Logs(Message, Time, Category) VALUES('Добавлено ' + @MsgInserted, SYSDATETIME (), 'Pictures')
END
ELSE IF EXISTS (SELECT * FROM inserted)
BEGIN
DECLARE @MsgUpdateOLD VARCHAR(MAX)
DECLARE @MsgUpdateNEW VARCHAR(MAX)
SELECT @MsgUpdateOLD = (SELECT Title FROM deleted)
SELECT @MsgUpdateNEW = (SELECT Title FROM inserted)
INSERT INTO Logs(Message, Time, Category) VALUES('Изменено ' + @MsgUpdateOLD + ' на ' + @MsgUpdateNEW, SYSDATETIME (), 'Pictures')
END
ELSE
BEGIN
	DECLARE @MsgDelete VARCHAR(MAX)
	SELECT @MsgDelete = (SELECT Title FROM deleted)
	INSERT INTO Logs(Message, Time, Category) VALUES('Удалено ' + @MsgDelete, SYSDATETIME (), 'Pictures')
END