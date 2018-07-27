CREATE TABLE [dbo].[bas_Mosaic] (
    [ID]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (500) NOT NULL,
    [DomainID]  INT           NOT NULL,
    [CreatTime] BIGINT        CONSTRAINT [DF_bas_Mosaic_CreatTime] DEFAULT ((0)) NOT NULL,
    [IsType]    TINYINT       NOT NULL,
    [NumIDStr]  VARCHAR (MAX) CONSTRAINT [DF_bas_Mosaic_NumIDStr] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Mosaic] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bas_Mosaic]
    ON [dbo].[bas_Mosaic]([Title] ASC);

