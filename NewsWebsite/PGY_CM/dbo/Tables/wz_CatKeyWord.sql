CREATE TABLE [dbo].[wz_CatKeyWord] (
    [ID]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [CatID]      BIGINT        NOT NULL,
    [KeyWord]    VARCHAR (200) NOT NULL,
    [Impression] INT           NOT NULL,
    [UpDateTime] BIGINT        NOT NULL,
    CONSTRAINT [PK_wz_CatKeyWord] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_CatKeyWord]
    ON [dbo].[wz_CatKeyWord]([CatID] ASC, [KeyWord] ASC);

