CREATE TABLE [dbo].[Web_UpdataAWeb] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [WID]        INT           NOT NULL,
    [TypeID]     TINYINT       NOT NULL,
    [KeyWordStr] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Web_UpdataAWeb] PRIMARY KEY CLUSTERED ([ID] ASC)
);

