CREATE TRIGGER tr_AfterDeleteMeeting
ON Meetings
FOR DELETE
AS
BEGIN
    INSERT INTO Logs (ID, TableName, Action, UserId, CreatedDate, Status)
    SELECT
        NEWID() AS ID, 
        'Meetings' AS TableName, 
        'DELETE - ' + Deleted.Title AS Action, 
        Deleted.UserId,
        GETDATE() AS CreatedDate,
        Deleted.Status AS Status
    FROM Deleted;
END;
