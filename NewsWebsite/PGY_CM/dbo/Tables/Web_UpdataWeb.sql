CREATE TABLE [dbo].[Web_UpdataWeb] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [WID]            INT           NOT NULL,
    [CatID]          BIGINT        NOT NULL,
    [KeyWordStr]     VARCHAR (MAX) NOT NULL,
    [IsEnable]       TINYINT       CONSTRAINT [DF_Web_UpdataWeb_IsEnable] DEFAULT ((0)) NOT NULL,
    [CommissionRate] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Web_UpdataWeb] PRIMARY KEY CLUSTERED ([ID] ASC)
);

