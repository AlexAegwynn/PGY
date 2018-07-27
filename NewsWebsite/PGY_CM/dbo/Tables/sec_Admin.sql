CREATE TABLE [dbo].[sec_Admin] (
    [AdminID]   INT           IDENTITY (1, 1) NOT NULL,
    [AdminName] VARCHAR (100) NOT NULL,
    [Password]  VARCHAR (500) NOT NULL,
    [Email]     VARCHAR (500) NOT NULL,
    [Addtime]   BIGINT        NOT NULL,
    [IsEnable]  TINYINT       CONSTRAINT [DF_sec_Admin_IsEnable] DEFAULT ((0)) NOT NULL,
    [IsRight]   TINYINT       CONSTRAINT [DF_sec_Admin_IsRight] DEFAULT ((0)) NOT NULL,
    [RealName]  VARCHAR (500) CONSTRAINT [DF_sec_Admin_RealName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_sec_Admin] PRIMARY KEY CLUSTERED ([AdminID] ASC)
);

