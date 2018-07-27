CREATE TABLE [dbo].[wz_Title] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [SiteName]     VARCHAR (500) NULL,
    [TitleType]    VARCHAR (200) NULL,
    [Title]        VARCHAR (500) NULL,
    [Author]       VARCHAR (100) NULL,
    [CreateTime]   DATETIME      NULL,
    [ReadCount]    INT           NULL,
    [CommentCount] INT           NULL,
    [Count]        INT           NULL,
    [AddTime]      BIGINT        CONSTRAINT [DF_wz_Title_UpdateMobileDate_1] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_wz_Title] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_Title]
    ON [dbo].[wz_Title]([Title] ASC);

