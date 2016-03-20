CREATE PROCEDURE [dbo].[IncrementWitherSkeletonKills]
	@KillerName varchar(15)
AS
	Declare @PlayerID int
	--Get Player ID that matched the name
	Select @PlayerID = PlayerID from Player where Name = @KillerName

	Declare @MapID int
	--Get MapID of active Map
	Select @MapID = MapID from Map where isActive = 1

	--Increment mob kill by 1
	Update PlayerMapStats set WitherSkeletonKills = WitherSkeletonKills + 1 where PlayerID = @PlayerID AND MapID = @MapID
RETURN 0