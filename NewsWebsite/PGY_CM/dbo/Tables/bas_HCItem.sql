CREATE TABLE [dbo].[bas_HCItem] (
    [ID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [CatID]          TINYINT         NOT NULL,
    [ActID]          INT             NOT NULL,
    [NumIID]         BIGINT          NOT NULL,
    [Title]          NVARCHAR (500)  NOT NULL,
    [ClickUrl]       VARCHAR (500)   NOT NULL,
    [ReservePrice]   DECIMAL (18, 2) NOT NULL,
    [ZkFinalPrice]   DECIMAL (18, 2) NOT NULL,
    [PicUrl]         VARCHAR (500)   NOT NULL,
    [SortNum]        INT             NOT NULL,
    [CommissionRate] DECIMAL (18, 2) CONSTRAINT [DF_bas_HCItem_CommissionRate] DEFAULT ((0)) NOT NULL,
    [SalesCount]     INT             CONSTRAINT [DF_bas_HCItem_SalesCount] DEFAULT ((0)) NOT NULL,
    [Source]         TINYINT         CONSTRAINT [DF_bas_HCItem_Source] DEFAULT ((1)) NOT NULL,
    [CatName]        VARCHAR (500)   CONSTRAINT [DF_bas_HCItem_CatName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_bas_HCItem] PRIMARY KEY CLUSTERED ([ID] ASC)
);

