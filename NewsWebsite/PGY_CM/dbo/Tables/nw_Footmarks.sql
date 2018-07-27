CREATE TABLE [dbo].[nw_Footmarks] (
    [FmID]      INT            IDENTITY (1, 1) NOT NULL,
    [UID]       INT            CONSTRAINT [DF_nw_Footmarks_UID] DEFAULT ((0)) NOT NULL,
    [ArticleID] BIGINT         CONSTRAINT [DF_nw_Footmarks_ArticleID] DEFAULT ((0)) NOT NULL,
    [FmTitel]   NVARCHAR (200) CONSTRAINT [DF_nw_Footmarks_FmTitel] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_nw_Footmarks] PRIMARY KEY CLUSTERED ([FmID] ASC)
);

