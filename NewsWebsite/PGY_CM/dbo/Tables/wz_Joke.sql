CREATE TABLE [dbo].[wz_Joke] (
    [JokeID]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [Conten]  VARCHAR (500) NOT NULL,
    [CatName] VARCHAR (50)  NOT NULL,
    [Count]   INT           NOT NULL,
    CONSTRAINT [PK_wz_Joke] PRIMARY KEY CLUSTERED ([JokeID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_wz_Joke]
    ON [dbo].[wz_Joke]([Conten] ASC);

