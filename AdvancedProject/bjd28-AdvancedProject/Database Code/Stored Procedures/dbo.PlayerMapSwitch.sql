CREATE PROCEDURE [dbo].[PlayerMapSwitch]
	@PlayerID int,
	@MapID int
AS
	Declare @NumberOfPlayerMapStatRows int
	--Get the number of player map stats for the input player and map
	Select @NumberOfPlayerMapStatRows = Count(*) from PlayerMapStats where PlayerID = @PlayerID AND MapID = @MapID
	if @NumberOfPlayerMapStatRows = 0
	begin
		--If there is no player-map-stats records for the player and map add a new one
		Insert Into PlayerMapStats values(@MapID, @PlayerID, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)
	end
RETURN 0