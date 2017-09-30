CREATE TRIGGER Pets_trigger
ON Pets AFTER INSERT, UPDATE, DELETE AS 
IF NOT EXISTS (SELECT * FROM deleted)
BEGIN
	DECLARE @MsgInserted VARCHAR(MAX)
	SELECT @MsgInserted = (SELECT Name FROM inserted)
	INSERT INTO Logs(Message, Time, Category) VALUES('Добавлено ' + @MsgInserted, SYSDATETIME (), 'Pets')
END
ELSE IF EXISTS (SELECT * FROM inserted)
BEGIN
DECLARE @MsgUpdateOLD VARCHAR(MAX)
DECLARE @MsgUpdateNEW VARCHAR(MAX)
SELECT @MsgUpdateOLD = (SELECT Name FROM deleted)
SELECT @MsgUpdateNEW = (SELECT Name FROM inserted)
INSERT INTO Logs(Message, Time, Category) VALUES('Изменено ' + @MsgUpdateOLD + ' на ' + @MsgUpdateNEW, SYSDATETIME (), 'Pets')
END
ELSE
BEGIN
	DECLARE @MsgDelete VARCHAR(MAX)
	SELECT @MsgDelete = (SELECT Name FROM deleted)
	INSERT INTO Logs(Message, Time, Category) VALUES('Удалено ' + @MsgDelete, SYSDATETIME (), 'Pets')
END