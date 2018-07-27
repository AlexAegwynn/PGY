CREATE TABLE [dbo].[bas_AppIDCat] (
    [ID]       BIGINT        NOT NULL,
    [Title]    VARCHAR (500) NOT NULL,
    [DomainID] INT           NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bas_AppIDCat]
    ON [dbo].[bas_AppIDCat]([ID] ASC, [DomainID] ASC);

