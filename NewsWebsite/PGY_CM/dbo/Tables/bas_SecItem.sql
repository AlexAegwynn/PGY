CREATE TABLE [dbo].[bas_SecItem] (
    [ID]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [NumIID]       BIGINT          NOT NULL,
    [HcID]         BIGINT          CONSTRAINT [DF_bas_SecItem_HcID] DEFAULT ((0)) NOT NULL,
    [Title]        VARCHAR (500)   NOT NULL,
    [ClickUrl]     VARCHAR (500)   NOT NULL,
    [ReservePrice] DECIMAL (18, 2) NOT NULL,
    [ZkFinalPrice] DECIMAL (18, 2) NOT NULL,
    [PicUrl]       VARCHAR (500)   NOT NULL,
    [StartTime]    BIGINT          NOT NULL,
    [EndTime]      BIGINT          NOT NULL,
    [SortNum]      INT             NOT NULL,
    [PlaceTime]    INT             CONSTRAINT [DF_bas_SecItem_PlaceTime] DEFAULT ((0)) NOT NULL,
    [Source]       TINYINT         CONSTRAINT [DF_bas_SecItem_Source] DEFAULT ((1)) NOT NULL,
    [IsEnable]     TINYINT         CONSTRAINT [DF_bas_SecItem_IsEnable] DEFAULT ((1)) NOT NULL,
    [TotalAmount]  INT             CONSTRAINT [DF_bas_SecItem_CommissionRate] DEFAULT ((0)) NOT NULL,
    [SoldNum]      INT             CONSTRAINT [DF_bas_SecItem_SalesCount] DEFAULT ((0)) NOT NULL,
    [CatName]      VARCHAR (500)   CONSTRAINT [DF_bas_SecItem_CatName_1] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_bas_SecItem] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_bas_SecItem]
    ON [dbo].[bas_SecItem]([NumIID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_bas_SecItem_1]
    ON [dbo].[bas_SecItem]([StartTime] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_bas_SecItem_2]
    ON [dbo].[bas_SecItem]([PlaceTime] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bas_SecItem_3]
    ON [dbo].[bas_SecItem]([StartTime] ASC, [NumIID] ASC, [Source] ASC);

