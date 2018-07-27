CREATE TABLE [dbo].[bas_CatTB] (
    [ID]           BIGINT        NOT NULL,
    [CatID]        INT           NOT NULL,
    [CatCouponID]  INT           NOT NULL,
    [Title]        VARCHAR (500) NOT NULL,
    [IsGoodCoupon] INT           CONSTRAINT [DF_bas_CatTB_IsGoodCoupon] DEFAULT ((0)) NOT NULL
);

