CREATE TABLE [dbo].[Player] (
    [PlayerID]       INT          IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (15) NOT NULL,
    [LastOnlineDate] DATETIME     NOT NULL,
    [IsOnline]       BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([PlayerID] ASC)
);

