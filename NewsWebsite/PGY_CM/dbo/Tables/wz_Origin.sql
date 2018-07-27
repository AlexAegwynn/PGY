CREATE TABLE [dbo].[wz_Origin] (
    [OriginID]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (500) NOT NULL,
    [Conten]    VARCHAR (MAX) NOT NULL,
    [OriginUrl] VARCHAR (500) NOT NULL,
    [Count]     INT           NOT NULL,
    CONSTRAINT [PK_wz_Origin] PRIMARY KEY CLUSTERED ([OriginID] ASC)
);

