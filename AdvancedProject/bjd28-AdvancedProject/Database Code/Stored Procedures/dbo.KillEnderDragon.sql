CREATE PROCEDURE [dbo].[KillEnderDragon]
	@KillerName varchar(15)
AS
	Declare @PlayerID int
	--Get Player ID that matched the name
	Select @PlayerID = PlayerID from Player where Name = @KillerName

	Declare @MapID int
	--Get MapID of active Map
	Select @MapID = MapID from Map where isActive = 1

	--Set boolean to true
	Update PlayerMapStats set EnderDragonKilled = 1 where PlayerID = @PlayerID AND MapID = @MapID
RETURN 0