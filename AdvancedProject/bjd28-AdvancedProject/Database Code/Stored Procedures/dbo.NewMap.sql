CREATE PROCEDURE [dbo].[NewMap]
AS
	--Set all Maps to be inactive
	Update Map set isActive = 0

	Declare @CurrentDate DateTime = CURRENT_TIMESTAMP --Get current date time

	--Create new map, set to active with the current date time
	Insert into Map values(@CurrentDate, 1)

RETURN 0