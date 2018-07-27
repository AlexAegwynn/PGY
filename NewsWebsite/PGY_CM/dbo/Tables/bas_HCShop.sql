CREATE TABLE [dbo].[bas_HCShop] (
    [HcID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [CatID]      TINYINT        NOT NULL,
    [ShopTitle]  VARCHAR (500)  NOT NULL,
    [SubTitle]   VARCHAR (500)  NOT NULL,
    [ShopBanner] VARCHAR (500)  NOT NULL,
    [ShopLogo]   VARCHAR (500)  NOT NULL,
    [ShopPic]    VARCHAR (500)  NOT NULL,
    [HcPic]      VARCHAR (500)  NOT NULL,
    [CouponInfo] VARCHAR (5000) NOT NULL,
    [IsMain]     TINYINT        NOT NULL,
    [UpdatedPic] BIGINT         NOT NULL,
    [Source]     TINYINT        CONSTRAINT [DF_bas_HCShop_Source] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_bas_HcShopCS] PRIMARY KEY CLUSTERED ([HcID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bas_HCShop]
    ON [dbo].[bas_HCShop]([ShopTitle] ASC);

