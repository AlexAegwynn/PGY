CREATE TABLE [dbo].[wz_CatStatistic] (
    [ID]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [CatID]             BIGINT          NOT NULL,
    [CatName]           VARCHAR (500)   NOT NULL,
    [StartDate]         BIGINT          NOT NULL,
    [EndDate]           BIGINT          NOT NULL,
    [CommissionRateAve] DECIMAL (18, 2) NOT NULL,
    [PayPctAve]         DECIMAL (18, 2) NOT NULL,
    [Num]               INT             CONSTRAINT [DF_wz_CatStatistic_Num] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_wz_CatStatistic] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_CatStatistic]
    ON [dbo].[wz_CatStatistic]([CatID] ASC, [StartDate] ASC, [EndDate] ASC);

