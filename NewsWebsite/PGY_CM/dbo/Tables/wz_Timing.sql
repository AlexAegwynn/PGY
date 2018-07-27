CREATE TABLE [dbo].[wz_Timing] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [AppID]    BIGINT        NOT NULL,
    [TimeSlot] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_wz_Timing] PRIMARY KEY CLUSTERED ([ID] ASC)
);

