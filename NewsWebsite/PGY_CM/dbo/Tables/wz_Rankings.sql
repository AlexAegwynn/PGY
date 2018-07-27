CREATE TABLE [dbo].[wz_Rankings] (
    [ID]                  BIGINT          IDENTITY (1, 1) NOT NULL,
    [Category]            VARCHAR (500)   CONSTRAINT [DF_wz_Rankings_Category] DEFAULT ('') NOT NULL,
    [CategorySmall]       VARCHAR (500)   CONSTRAINT [DF_wz_Rankings_CategorySmall] DEFAULT ('') NOT NULL,
    [RankingsID]          INT             NOT NULL,
    [KeyWord]             VARCHAR (500)   NOT NULL,
    [KeyWordUrl]          VARCHAR (500)   NOT NULL,
    [AttentionNum]        DECIMAL (18, 2) NOT NULL,
    [UpOrDownSeat]        INT             NOT NULL,
    [UpOrDownSeatState]   TINYINT         NOT NULL,
    [UpOrDownExtent]      NVARCHAR (500)  NOT NULL,
    [UpOrDownExtentState] TINYINT         NOT NULL,
    [CreateTime]          BIGINT          NOT NULL,
    [UpdateTime]          BIGINT          NOT NULL,
    [IsType]              TINYINT         NOT NULL,
    CONSTRAINT [PK_wz_Rankings] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_Rankings]
    ON [dbo].[wz_Rankings]([IsType] ASC, [KeyWord] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'wz_Rankings', @level2type = N'COLUMN', @level2name = N'ID';

