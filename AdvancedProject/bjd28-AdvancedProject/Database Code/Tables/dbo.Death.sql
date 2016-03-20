CREATE TABLE [dbo].[Death] (
    [DeathID]     INT          IDENTITY (1, 1) NOT NULL,
    [PlayerMapID] INT          NOT NULL,
    [DeathLog]    VARCHAR (90) NOT NULL,
    [DeathDate]   DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([DeathID] ASC),
    CONSTRAINT [FK_Death_PlayerMapStats] FOREIGN KEY ([PlayerMapID]) REFERENCES [dbo].[PlayerMapStats] ([PlayerMapID])
);

