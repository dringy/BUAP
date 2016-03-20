CREATE PROCEDURE [dbo].[SwitchMap]
@MapID int
AS
	--All Maps isActive fields are set to false
	Update Map set isActive = 0

	--Map with input ID has its isActive field set to true
	Update Map set isActive = 1 where MapID = @MapID

RETURN 0