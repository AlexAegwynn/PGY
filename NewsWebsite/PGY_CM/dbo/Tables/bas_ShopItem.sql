CREATE TABLE [dbo].[bas_ShopItem] (
    [ID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [HcID]           BIGINT          NOT NULL,
    [NumIID]         BIGINT          NOT NULL,
    [Title]          VARCHAR (500)   NOT NULL,
    [ClickUrl]       VARCHAR (500)   NOT NULL,
    [ReservePrice]   DECIMAL (18, 2) NOT NULL,
    [ZkFinalPrice]   DECIMAL (18, 2) NOT NULL,
    [PicUrl]         VARCHAR (500)   NOT NULL,
    [SortNum]        INT             NOT NULL,
    [CommissionRate] DECIMAL (18, 2) CONSTRAINT [DF_bas_ShopItem_CommissionRate] DEFAULT ((0)) NOT NULL,
    [SalesCount]     INT             CONSTRAINT [DF_bas_ShopItem_SalesCount] DEFAULT ((0)) NOT NULL,
    [Source]         TINYINT         CONSTRAINT [DF_bas_ShopItem_Source] DEFAULT ((1)) NOT NULL,
    [CatName]        VARCHAR (500)   CONSTRAINT [DF_bas_ShopItem_CatName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_bas_ShopItem] PRIMARY KEY CLUSTERED ([ID] ASC)
);

