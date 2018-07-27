CREATE TABLE [dbo].[wz_GoodsInfo] (
    [ID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [Month]    INT           NOT NULL,
    [IID]      BIGINT        NOT NULL,
    [NumIID]   BIGINT        NOT NULL,
    [Title]    VARCHAR (500) NOT NULL,
    [Num]      INT           NOT NULL,
    [CJNum]    INT           NOT NULL,
    [ClickUrl] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_wz_GoodsInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_GoodsInfo]
    ON [dbo].[wz_GoodsInfo]([IID] ASC, [Month] ASC);

