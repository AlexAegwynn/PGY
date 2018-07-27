CREATE TABLE [dbo].[bas_ItemLog] (
    [ID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [IsType]         TINYINT         NOT NULL,
    [NumIID]         BIGINT          NOT NULL,
    [CreateTime]     BIGINT          NOT NULL,
    [UpdateTime]     BIGINT          NOT NULL,
    [ItemTitle]      VARCHAR (500)   NOT NULL,
    [ClickUrl]       VARCHAR (500)   NOT NULL,
    [PicUrl]         VARCHAR (500)   NOT NULL,
    [ReservePrice]   DECIMAL (18, 2) NOT NULL,
    [ZkFinalPrice]   DECIMAL (18, 2) NOT NULL,
    [CommissionRate] DECIMAL (18, 2) NOT NULL,
    [SalesCount]     INT             NOT NULL,
    [CatID]          TINYINT         NOT NULL,
    [Source]         TINYINT         NOT NULL,
    [SellerID]       BIGINT          NOT NULL,
    [Nick]           VARCHAR (500)   NOT NULL,
    [ShopName]       VARCHAR (500)   NOT NULL,
    [CouponMoney]    DECIMAL (18, 2) NOT NULL,
    [CouponReq]      DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_bas_ItemLog] PRIMARY KEY CLUSTERED ([ID] ASC)
);

