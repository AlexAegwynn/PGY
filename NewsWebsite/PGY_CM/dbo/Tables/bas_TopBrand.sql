CREATE TABLE [dbo].[bas_TopBrand] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [TopTitle] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_bas_TopBrand] PRIMARY KEY CLUSTERED ([ID] ASC)
);

