USE [TodoDB]
GO

/****** Object: Table [dbo].[Todos] Script Date: 8/28/2025 8:50:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Todos] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Task]       NVARCHAR (50) NOT NULL,
    [AssignedTo] INT           NOT NULL,
    [IsComplete] BIT           NOT NULL
);


