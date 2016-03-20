CREATE TABLE [dbo].[Map] (
    [MapID]          INT      IDENTITY (1, 1) NOT NULL,
    [DateOfCreation] DATETIME NOT NULL,
    [isActive]       BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([MapID] ASC)
);

