CREATE TABLE [dbo].[wz_Regular] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [ArticleID]   BIGINT        NOT NULL,
    [AdminID]     INT           NOT NULL,
    [ReleaseTime] BIGINT        NOT NULL,
    [State]       INT           NOT NULL,
    [Reason]      VARCHAR (MAX) NOT NULL,
    [Type]        VARCHAR (200) NOT NULL,
    [MainaMap]    VARCHAR (500) CONSTRAINT [DF_wz_Regular_MainaMap] DEFAULT ('') NOT NULL,
    [IsAuto]      TINYINT       CONSTRAINT [DF_wz_Regular_IsType] DEFAULT ((0)) NOT NULL,
    [ReleaseType] TINYINT       CONSTRAINT [DF_wz_MakeAtlas_ReleaseType_1] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_wz_Regular] PRIMARY KEY CLUSTERED ([ID] ASC)
);

