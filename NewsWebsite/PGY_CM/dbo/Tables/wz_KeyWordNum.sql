CREATE TABLE [dbo].[wz_KeyWordNum] (
    [ID]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [NumIID]  BIGINT        NOT NULL,
    [KeyWord] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_wz_KeyWordNum] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_KeyWordNum]
    ON [dbo].[wz_KeyWordNum]([KeyWord] ASC, [NumIID] ASC);

