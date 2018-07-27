CREATE TABLE [dbo].[wz_Content] (
    [ArticleID]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [AdminID]          INT            NOT NULL,
    [UAdminID]         INT            CONSTRAINT [DF_wz_Content_UAdminID] DEFAULT ((0)) NOT NULL,
    [RAdminID]         INT            CONSTRAINT [DF_wz_Content_RAdminID] DEFAULT ((0)) NOT NULL,
    [Origin]           VARCHAR (200)  CONSTRAINT [DF_wz_Content_Origin] DEFAULT ('') NOT NULL,
    [APPID]            BIGINT         NOT NULL,
    [DomainID]         INT            NOT NULL,
    [Title]            VARCHAR (400)  NOT NULL,
    [Conten]           NVARCHAR (MAX) NOT NULL,
    [Abstract]         VARCHAR (500)  NOT NULL,
    [ImgUrl]           VARCHAR (1000) NOT NULL,
    [OriginUrl]        VARCHAR (500)  NOT NULL,
    [UpCount]          INT            CONSTRAINT [DF_wz_Content_UpCount] DEFAULT ((0)) NOT NULL,
    [ReleaseTime]      BIGINT         CONSTRAINT [DF_wz_Content_ReleaseTime] DEFAULT ((0)) NOT NULL,
    [PreservationTime] BIGINT         CONSTRAINT [DF_wz_Content_PreservationTime] DEFAULT ((0)) NOT NULL,
    [State]            TINYINT        CONSTRAINT [DF_wz_Content_State] DEFAULT ((0)) NOT NULL,
    [IsType]           TINYINT        NOT NULL,
    [OTitle]           VARCHAR (500)  CONSTRAINT [DF_wz_Content_OTitle_1] DEFAULT ('') NOT NULL,
    [KeyWords]         VARCHAR (200)  CONSTRAINT [DF_wz_Content_KeyWords] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_wz_Content] PRIMARY KEY CLUSTERED ([ArticleID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_wz_Content_1]
    ON [dbo].[wz_Content]([Title] ASC);

