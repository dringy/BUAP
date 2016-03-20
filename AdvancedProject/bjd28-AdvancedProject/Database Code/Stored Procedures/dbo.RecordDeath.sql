CREATE PROCEDURE [dbo].[RecordDeath]
	@PlayerName varchar(15),
	@Deathlog varchar(90)
AS
	Declare @PlayerID int
	--Get the playerID using the input PlayerName
	Select @PlayerID = PlayerID from Player where Name = @PlayerName

	Declare @MapID int
	--Get the currently active map ID
	Select @MapID = MapID from Map where isActive = 1

	Declare @PlayerMapID int
	--Using the playerID and the Active MapID get the PlayerMapID from the PlayerMapStats database table
	Select @PlayerMapID = PlayerMapID from PlayerMapStats where PlayerID = @PlayerID AND MapID = @MapID

	Declare @CurrentDate DateTime = CURRENT_TIMESTAMP --Get current date time

	--Using the PlayerMapID and the input death log and the current date insert a new death into the database
	insert into Death values(@PlayerMapID, @Deathlog, @CurrentDate)

RETURN 0