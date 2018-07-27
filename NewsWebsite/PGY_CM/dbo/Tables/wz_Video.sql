CREATE TABLE [dbo].[wz_Video] (
    [VideoID]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (500) NOT NULL,
    [CatName]   VARCHAR (500) NOT NULL,
    [NumIID]    BIGINT        CONSTRAINT [DF_wz_Video_NumIID] DEFAULT ((0)) NOT NULL,
    [Time]      BIGINT        NOT NULL,
    [DownTime]  BIGINT        NOT NULL,
    [Size]      INT           NOT NULL,
    [IsType]    INT           NOT NULL,
    [PicUrl]    VARCHAR (500) NOT NULL,
    [Origin]    VARCHAR (500) NOT NULL,
    [OriginUrl] VARCHAR (500) NOT NULL,
    [VideoUrl]  VARCHAR (500) NOT NULL,
    [Count]     INT           NOT NULL,
    CONSTRAINT [PK_wz_Video] PRIMARY KEY CLUSTERED ([VideoID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_Video]
    ON [dbo].[wz_Video]([OriginUrl] ASC);

