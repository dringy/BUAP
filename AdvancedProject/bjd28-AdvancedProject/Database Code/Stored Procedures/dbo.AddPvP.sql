CREATE PROCEDURE [dbo].[AddPvP]
	@VictimName varchar(15),
	@KillerName varchar(15)
AS

	Declare @CurrentMapID int
	--Get the currently active map id
	Select @CurrentMapID = MapID from Map where isActive = 1

	Declare @VictimID int
	--Get the vcitim player ID using the victim player name
	Select @VictimID = PlayerID from Player where Name = @VictimName

	Declare @VictimMapID int
	--Get the victim map id using the victim id and the map id
	Select @VictimMapID = PlayerMapID from PlayerMapStats where PlayerID = @VictimID And MaPId = @CurrentMapID

	Declare @KillerID int
	--Get the killer player ID using the killer player name
	Select @KillerID = PlayerID from Player where Name = @KillerName

	Declare @KillerMapID int
	--Get the killer map id using the killer id and the map id
	Select @KillerMapID = PlayerMapID from PlayerMapStats where PlayerID = @KillerID And MaPId = @CurrentMapID

	Declare @CurrentDate DateTime = CURRENT_TIMESTAMP --Store current date time

	--Insert PVP row using KillerMapID, VictimMapID and the current date time
	Insert into PvP values(@KillerMapID, @VictimMapID, @CurrentDate)

RETURN 0