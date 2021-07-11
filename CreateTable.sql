USE [DBGitHubRepository]
GO

/****** Object: Table [dbo].[Favoritos] Script Date: 11/07/2021 15:53:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Favoritos] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Description] NCHAR (50) NULL,
    [Language]    NCHAR (10) NULL,
    [UpdateLast]  DATETIME   NOT NULL,
    [Owner]       NCHAR (20) NOT NULL,
    [Name]        NCHAR (20) NOT NULL
);