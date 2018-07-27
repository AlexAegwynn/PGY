CREATE TABLE [dbo].[nw_Users] (
    [UID]      INT           IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (50) CONSTRAINT [DF_nw_Users_UserName] DEFAULT ('') NOT NULL,
    [Password] VARCHAR (50)  CONSTRAINT [DF_nw_Users_Password] DEFAULT ('') NOT NULL,
    [Email]    VARCHAR (50)  CONSTRAINT [DF_nw_Users_Email] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_nw_Users] PRIMARY KEY CLUSTERED ([UID] ASC)
);

