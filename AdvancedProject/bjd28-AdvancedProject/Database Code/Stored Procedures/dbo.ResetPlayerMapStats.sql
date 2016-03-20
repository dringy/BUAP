CREATE PROCEDURE [dbo].[ResetPlayerMapStats]
	@PlayerMapID int,
	@isPlayerOnMap bit
AS
	--Delete all ocurances of PvP where the input PlayerMapID is a match of either the victim of the killer
	Delete from PvP where KillerMapID = @PlayerMapID OR VictimMapID = @PlayerMapID

	--Delete all deaths from player 
	Delete from Death where PlayerMapID = @PlayerMapID

	if (@isPlayerOnMap = 0)
	begin
		--If the player is not on the map delete their PlayerMapStats record
		Delete from PlayerMapStats where PlayerMapID = @PlayerMapID
	end

	else
	begin
		--If the player is on the map simply update their PlayerMapStats records so they appear newly created
		Update PlayerMapStats Set CaveSpiderKills = 0, EndermanKills = 0, 
		SpiderKills = 0, WolfKills = 0, ZombiePigmanKills = 0, BlazeKills = 0,
		CreeperKills = 0, GhastKills = 0, MagmaCubeKills = 0, SilverfishKills = 0,
		SkeletonKills = 0, SlimeKills = 0, WitchKills = 0, WitherSkeletonKills = 0,
		ZombieKills = 0, WitherKills = 0, EnderDragonKilled = 0 where PlayerMapID =
		@PlayerMapID
	end

RETURN 0