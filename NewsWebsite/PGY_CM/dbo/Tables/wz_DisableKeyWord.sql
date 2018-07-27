CREATE TABLE [dbo].[wz_DisableKeyWord] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [KeyWord] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_wz_DisableKeyWord] PRIMARY KEY CLUSTERED ([ID] ASC)
);

