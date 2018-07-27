CREATE TABLE [dbo].[Web_ArticleWeb] (
    [ID]        INT    IDENTITY (1, 1) NOT NULL,
    [WID]       INT    NOT NULL,
    [ArticleID] BIGINT NOT NULL,
    CONSTRAINT [PK_Web_ArticleWeb] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Web_ArticleWeb]
    ON [dbo].[Web_ArticleWeb]([ArticleID] ASC, [WID] ASC);

