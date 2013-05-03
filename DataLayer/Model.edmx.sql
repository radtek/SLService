
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/03/2013 15:35:47
-- Generated from EDMX file: C:\Users\Edwin\Documents\GitHub\SLService\DataLayer\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SLService];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EventLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EventLog'
CREATE TABLE [dbo].[EventLog] (
    [id] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(250)  NULL,
    [UserName] nvarchar(50)  NULL,
    [Date] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'EventLog'
ALTER TABLE [dbo].[EventLog]
ADD CONSTRAINT [PK_EventLog]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------