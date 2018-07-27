CREATE TABLE [dbo].[bas_HCActivity] (
    [ActID]     INT           IDENTITY (1, 1) NOT NULL,
    [CatID]     INT           NOT NULL,
    [CatActPic] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_bas_HCActivity] PRIMARY KEY CLUSTERED ([ActID] ASC)
);

