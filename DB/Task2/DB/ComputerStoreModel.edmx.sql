
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/07/2023 18:47:11
-- Generated from EDMX file: D:\Ostap\SSWU\Homeworks\DB\Task2\DB\ComputerStoreModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ComputerStorePartDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Price] decimal(18,0)  NOT NULL,
    [SerialNum] nvarchar(max)  NOT NULL,
    [DateOfManufaturer] datetime  NOT NULL,
    [CategoryId] int  NOT NULL,
    [ManufacturerId] int  NOT NULL,
    [ItemParams_Id] int  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'ManufacturerSet'
CREATE TABLE [dbo].[ManufacturerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [WEB_Site_Link] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ItemParamsSet'
CREATE TABLE [dbo].[ItemParamsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Height] float  NOT NULL,
    [Width] float  NOT NULL,
    [Depth] float  NOT NULL,
    [Weight] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ManufacturerSet'
ALTER TABLE [dbo].[ManufacturerSet]
ADD CONSTRAINT [PK_ManufacturerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemParamsSet'
ALTER TABLE [dbo].[ItemParamsSet]
ADD CONSTRAINT [PK_ItemParamsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_CategoryItem]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[CategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryItem'
CREATE INDEX [IX_FK_CategoryItem]
ON [dbo].[ItemSet]
    ([CategoryId]);
GO

-- Creating foreign key on [ItemParams_Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_ItemItemParams]
    FOREIGN KEY ([ItemParams_Id])
    REFERENCES [dbo].[ItemParamsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemItemParams'
CREATE INDEX [IX_FK_ItemItemParams]
ON [dbo].[ItemSet]
    ([ItemParams_Id]);
GO

-- Creating foreign key on [ManufacturerId] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_ManufacturerItem]
    FOREIGN KEY ([ManufacturerId])
    REFERENCES [dbo].[ManufacturerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManufacturerItem'
CREATE INDEX [IX_FK_ManufacturerItem]
ON [dbo].[ItemSet]
    ([ManufacturerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------