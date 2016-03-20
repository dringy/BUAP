CREATE TABLE [dbo].[PvP] (
    [KillerMapID] INT      NOT NULL,
    [VictimMapID] INT      NOT NULL,
    [PvPDate]     DATETIME NOT NULL,
    CONSTRAINT [FK_PvP_PlayerMapStats1] FOREIGN KEY ([KillerMapID]) REFERENCES [dbo].[PlayerMapStats] ([PlayerMapID]),
    CONSTRAINT [FK_PvP_PlayerMapStats2] FOREIGN KEY ([VictimMapID]) REFERENCES [dbo].[PlayerMapStats] ([PlayerMapID])
);

