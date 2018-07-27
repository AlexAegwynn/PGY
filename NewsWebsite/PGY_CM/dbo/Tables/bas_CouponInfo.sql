CREATE TABLE [dbo].[bas_CouponInfo] (
    [ID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [CouponMoney]    DECIMAL (18, 2) NOT NULL,
    [CouponReq]      DECIMAL (18, 2) CONSTRAINT [DF_bas_CouponInfo_CouponReq] DEFAULT ((0)) NOT NULL,
    [CouponInfo]     VARCHAR (500)   NOT NULL,
    [CouponUrl]      VARCHAR (500)   NOT NULL,
    [PicUrl]         VARCHAR (500)   CONSTRAINT [DF_bas_CouponInfo_PicUrl] DEFAULT ('') NOT NULL,
    [CatID]          INT             CONSTRAINT [DF_bas_CouponInfo_CatID] DEFAULT ((1)) NOT NULL,
    [SortNum]        INT             CONSTRAINT [DF_bas_CouponInfo_SortNum] DEFAULT ((0)) NOT NULL,
    [IsGood]         TINYINT         CONSTRAINT [DF_bas_CouponInfo_IsGood] DEFAULT ((1)) NOT NULL,
    [GoodSortNum]    INT             CONSTRAINT [DF_bas_CouponInfo_GoodSortNum] DEFAULT ((0)) NOT NULL,
    [IsExclusive]    TINYINT         CONSTRAINT [DF_bas_CouponInfo_IsExclusive] DEFAULT ((1)) NOT NULL,
    [IsSup]          TINYINT         CONSTRAINT [DF_bas_CouponInfo_IsSup] DEFAULT ((0)) NOT NULL,
    [StartTime]      BIGINT          CONSTRAINT [DF_bas_CouponInfo_StartTime] DEFAULT ((0)) NOT NULL,
    [EndTime]        BIGINT          CONSTRAINT [DF_bas_CouponInfo_EndTime] DEFAULT ((0)) NOT NULL,
    [Source]         TINYINT         CONSTRAINT [DF_bas_CouponInfo_Source] DEFAULT ((0)) NOT NULL,
    [NumIID]         BIGINT          CONSTRAINT [DF_bas_CouponInfo_NumIID] DEFAULT ((0)) NOT NULL,
    [ItemTitle]      VARCHAR (500)   CONSTRAINT [DF_bas_CouponInfo_ItemTitle] DEFAULT ('') NOT NULL,
    [ZkFinalPrice]   DECIMAL (18, 2) CONSTRAINT [DF_bas_CouponInfo_ZkFinalPrice] DEFAULT ((0)) NOT NULL,
    [CommissionRate] DECIMAL (18, 2) CONSTRAINT [DF_bas_CouponInfo_CommissionRate] DEFAULT ((0)) NOT NULL,
    [SalesCount]     INT             CONSTRAINT [DF_bas_CouponInfo_SalesCount] DEFAULT ((0)) NOT NULL,
    [SellerID]       BIGINT          CONSTRAINT [DF_bas_CouponInfo_SellerID] DEFAULT ((0)) NOT NULL,
    [Nick]           VARCHAR (500)   CONSTRAINT [DF_bas_CouponInfo_Nick] DEFAULT ('') NOT NULL,
    [ShopName]       VARCHAR (500)   CONSTRAINT [DF_bas_CouponInfo_ShopName] DEFAULT ('') NOT NULL,
    [CatTBID]        BIGINT          CONSTRAINT [DF_bas_CouponInfo_CatTBID] DEFAULT ((0)) NOT NULL,
    [UpdateTime]     DATETIME        CONSTRAINT [DF_bas_CouponInfo_UpdateTime] DEFAULT (getdate()) NOT NULL,
    [IsHigh]         TINYINT         CONSTRAINT [DF_bas_CouponInfo_IsHigh] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_bas_CouponInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bas_CouponInfo]
    ON [dbo].[bas_CouponInfo]([NumIID] ASC);

