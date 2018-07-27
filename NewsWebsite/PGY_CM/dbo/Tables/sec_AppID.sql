CREATE TABLE [dbo].[sec_AppID] (
    [APPID]               BIGINT        NOT NULL,
    [APPName]             VARCHAR (100) CONSTRAINT [DF_sec_AppID_APPName] DEFAULT ('') NOT NULL,
    [Name]                VARCHAR (100) CONSTRAINT [DF_sec_AppID_Name] DEFAULT ('') NOT NULL,
    [Source]              VARCHAR (100) NOT NULL,
    [PID]                 VARCHAR (200) CONSTRAINT [DF_sec_AppID_PID] DEFAULT ('') NOT NULL,
    [LastDayFans]         INT           CONSTRAINT [DF_sec_AppID_LastDayFans] DEFAULT ((0)) NOT NULL,
    [ContentQuality]      INT           CONSTRAINT [DF_sec_AppID_ContentQuality] DEFAULT ((0)) NOT NULL,
    [ActivePerformance]   INT           CONSTRAINT [DF_sec_AppID_ActivePerformance] DEFAULT ((0)) NOT NULL,
    [UserLove]            INT           CONSTRAINT [DF_sec_AppID_UserLove] DEFAULT ((0)) NOT NULL,
    [DomainConcentration] INT           CONSTRAINT [DF_sec_AppID_DomainConcentration] DEFAULT ((0)) NOT NULL,
    [OriginalAbility]     INT           CONSTRAINT [DF_sec_AppID_OriginalAbility] DEFAULT ((0)) NOT NULL,
    [APPIndex]            INT           CONSTRAINT [DF_sec_AppID_APPIndex] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_sec_AppID] PRIMARY KEY CLUSTERED ([APPID] ASC)
);

