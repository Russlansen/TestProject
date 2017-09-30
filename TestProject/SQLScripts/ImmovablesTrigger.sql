CREATE TRIGGER Immovables_trigger
ON Immovables AFTER INSERT, UPDATE, DELETE AS 
IF NOT EXISTS (SELECT * FROM deleted)
BEGIN
	DECLARE @MsgInserted VARCHAR(MAX)
	SELECT @MsgInserted = (SELECT Type FROM inserted)
	INSERT INTO Logs(Message, Time, Category) VALUES('��������� ' + @MsgInserted, SYSDATETIME (), 'Immovables')
END
ELSE IF EXISTS (SELECT * FROM inserted)
BEGIN
DECLARE @MsgUpdateOLD VARCHAR(MAX)
DECLARE @MsgUpdateNEW VARCHAR(MAX)
SELECT @MsgUpdateOLD = (SELECT Type FROM deleted)
SELECT @MsgUpdateNEW = (SELECT Type FROM inserted)
INSERT INTO Logs(Message, Time, Category) VALUES('�������� ' + @MsgUpdateOLD + ' �� ' + @MsgUpdateNEW, SYSDATETIME (), 'Immovables')
END
ELSE
BEGIN
	DECLARE @MsgDelete VARCHAR(MAX)
	SELECT @MsgDelete = (SELECT Type FROM deleted)
	INSERT INTO Logs(Message, Time, Category) VALUES('������� ' + @MsgDelete, SYSDATETIME (), 'Immovables')
END