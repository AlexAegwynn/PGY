CREATE TABLE [dbo].[nw_Favorites] (
    [FaID]      INT            IDENTITY (1, 1) NOT NULL,
    [UID]       INT            CONSTRAINT [DF_nw_Favorites_UID] DEFAULT ((0)) NOT NULL,
    [ArticleID] BIGINT         CONSTRAINT [DF_nw_Favorites_ArticleID] DEFAULT ((0)) NOT NULL,
    [FaTitel]   NVARCHAR (200) CONSTRAINT [DF_nw_Favorites_FaTitel] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_nw_Favorites] PRIMARY KEY CLUSTERED ([FaID] ASC)
);

