CREATE TABLE [dbo].[Web_AtlasWeb] (
    [ID]        INT    IDENTITY (1, 1) NOT NULL,
    [WID]       INT    NOT NULL,
    [ArticleID] BIGINT NOT NULL,
    CONSTRAINT [PK_Web_AtlasWeb] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Web_AtlasWeb]
    ON [dbo].[Web_AtlasWeb]([ArticleID] ASC, [WID] ASC);

