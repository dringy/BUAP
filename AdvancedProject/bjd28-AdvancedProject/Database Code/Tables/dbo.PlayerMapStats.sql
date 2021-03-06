﻿CREATE TABLE [dbo].[PlayerMapStats] (
    [MapID]               INT NOT NULL,
    [PlayerID]            INT NOT NULL,
    [PlayerMapID]         INT IDENTITY (1, 1) NOT NULL,
    [CaveSpiderKills]     INT NOT NULL,
    [EndermanKills]       INT NOT NULL,
    [SpiderKills]         INT NOT NULL,
    [WolfKills]           INT NOT NULL,
    [ZombiePigmanKills]   INT NOT NULL,
    [BlazeKills]          INT NOT NULL,
    [CreeperKills]        INT NOT NULL,
    [GhastKills]          INT NOT NULL,
    [MagmaCubeKills]      INT NOT NULL,
    [SilverfishKills]     INT NOT NULL,
    [SkeletonKills]       INT NOT NULL,
    [SlimeKills]          INT NOT NULL,
    [WitchKills]          INT NOT NULL,
    [WitherSkeletonKills] INT NOT NULL,
    [ZombieKills]         INT NOT NULL,
    [WitherKills]         INT NOT NULL,
    [EnderDragonKilled]   BIT NOT NULL,
    CONSTRAINT [PK_PlayerMapStats] PRIMARY KEY CLUSTERED ([PlayerMapID] ASC),
    CONSTRAINT [FK_PlayerMapStats_Map] FOREIGN KEY ([MapID]) REFERENCES [dbo].[Map] ([MapID]),
    CONSTRAINT [FK_PlayerMapStats_Player] FOREIGN KEY ([PlayerID]) REFERENCES [dbo].[Player] ([PlayerID])
);

