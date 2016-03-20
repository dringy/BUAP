CREATE PROCEDURE [dbo].[PlayerLogin]
	@PlayerName varchar(15)
AS
	Declare @CurrentDate DateTime = CURRENT_TIMESTAMP --Gets current date time
	Declare @NumberOfInstance int = -1
	Declare @PlayerID int = -1
	Declare @MapID int = -1
	--Get the mapID of the currently active map
	Select @MapID = MapID from Map where isActive = 1
	--Get the number of players that have the input player name
	Select @NumberOfInstance = COUNT(*) From Player where Name = @PlayerName
	if @NumberOfInstance = 0
	Begin
		--If there is no players with that name Insert a new player using the input player name and set the m to online
		Insert Into Player values(@PlayerName, @CurrentDate, 1)
		--Get the playerID of the row you just created
		Select @PlayerID = PlayerID from Player where Name = @PlayerName
		--Insert a new row into the player map stats using the playerID and the active MapID
		Insert Into PlayerMapStats values(@MapID, @PlayerID, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)
	End
	Else
	Begin
		--If the player already has a record then get their PlayerID using their PlayerName
		Select @PlayerID = PlayerID from Player where Name = @PlayerName
		--Get the number of player map stats records the player has for that map
		Select @NumberOfInstance = COUNT(*) From PlayerMapStats where PlayerID = @PlayerID AND MapID = @MapID
		if @NumberOfInstance = 0
		Begin
			--If they have 0 player map stats for this map then add a new one using the active map id and the player's id
			Insert Into PlayerMapStats values(@MapID, @PlayerID, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)
		end
		--Set the player to be online and update their last online date
		Update Player set IsOnline = 1, LastOnlineDate = @CurrentDate WHERE PlayerID = @PlayerID
	End

RETURN 0