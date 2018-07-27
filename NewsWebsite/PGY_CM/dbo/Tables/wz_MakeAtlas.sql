CREATE TABLE [dbo].[wz_MakeAtlas] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [AdminID]       INT           NOT NULL,
    [APPID]         BIGINT        NOT NULL,
    [DomainID]      INT           NOT NULL,
    [ReleaseTime]   BIGINT        NOT NULL,
    [State]         INT           NOT NULL,
    [Reason]        VARCHAR (MAX) NOT NULL,
    [Number]        VARCHAR (100) NOT NULL,
    [KeyWord]       VARCHAR (MAX) NOT NULL,
    [IsCycle]       TINYINT       CONSTRAINT [DF_wz_MakeAtlas_IsCycle] DEFAULT ((0)) NOT NULL,
    [Cycle]         VARCHAR (200) CONSTRAINT [DF_wz_MakeAtlas_Cycle] DEFAULT ('') NOT NULL,
    [Category]      VARCHAR (200) CONSTRAINT [DF_wz_MakeAtlas_Category] DEFAULT ('') NOT NULL,
    [CategorySmall] VARCHAR (200) CONSTRAINT [DF_wz_MakeAtlas_CategorySmall] DEFAULT ('') NOT NULL,
    [CatIDStr]      VARCHAR (MAX) CONSTRAINT [DF_wz_MakeAtlas_CatIDStr_1] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_wz_MakeAtlas] PRIMARY KEY CLUSTERED ([ID] ASC)
);

