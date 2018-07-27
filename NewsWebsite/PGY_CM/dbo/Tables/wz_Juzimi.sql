CREATE TABLE [dbo].[wz_Juzimi] (
    [JuzimiID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Conten]   VARCHAR (MAX) NOT NULL,
    [CatName]  VARCHAR (50)  NOT NULL,
    [Count]    INT           NOT NULL,
    CONSTRAINT [PK_wz_Juzimi] PRIMARY KEY CLUSTERED ([JuzimiID] ASC)
);

