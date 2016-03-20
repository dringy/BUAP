CREATE PROCEDURE [dbo].[PlayerLogout]
	@PlayerName varchar(15)
AS
	Declare @CurrentDate DateTime = CURRENT_TIMESTAMP --Gets current date time
	--Set player to be offline and update their LastOnlineDate
	UPDATE Player set IsOnline = 0, LastOnlineDate = @CurrentDate WHERE Name = @PlayerName
RETURN 0