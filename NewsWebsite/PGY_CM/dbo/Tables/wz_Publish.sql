CREATE TABLE [dbo].[wz_Publish] (
    [ID]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [APPID]           BIGINT        NOT NULL,
    [IsType]          INT           NOT NULL,
    [Title]           VARCHAR (500) NOT NULL,
    [Audit]           VARCHAR (500) CONSTRAINT [DF__wz_Publis__Audit__02284B6B] DEFAULT ('') NOT NULL,
    [OriginUrl]       VARCHAR (500) CONSTRAINT [DF_wz_Publish_OriginUrl] DEFAULT ('') NOT NULL,
    [ReleaseTime]     BIGINT        NOT NULL,
    [UpdateTime]      BIGINT        CONSTRAINT [DF_wz_Publish_UpdateTime] DEFAULT ((0)) NOT NULL,
    [ReadCount]       BIGINT        NOT NULL,
    [CommentCount]    BIGINT        NOT NULL,
    [FabulousCount]   BIGINT        NOT NULL,
    [CollectionCount] BIGINT        NOT NULL,
    [ShareCount]      BIGINT        NOT NULL,
    CONSTRAINT [PK_wz_Publish] PRIMARY KEY CLUSTERED ([ID] ASC)
);

