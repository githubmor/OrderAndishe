
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/26/2016 18:42:57
-- Generated from EDMX file: D:\desktop\OrdersApp\OrdersAndisheh\OrdersAndisheh\DL\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [D:\DESKTOP\ORDERSAPP\ORDERSANDISHEH\ORDERSANDISHEH\BIN\DEBUG\ORDERDB.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductBazres]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_ProductBazres];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetailCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetailCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetailDriver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetailDriver];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_ProductOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPallet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_ProductPallet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bazres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bazres];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Driver]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Driver];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[OrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetail];
GO
IF OBJECT_ID(N'[dbo].[Pallet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pallet];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bazres'
CREATE TABLE [dbo].[Bazres] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL
);
GO

-- Creating table 'Customer'
CREATE TABLE [dbo].[Customer] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Driver'
CREATE TABLE [dbo].[Driver] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nam] nvarchar(max)  NOT NULL,
    [Tel1] nvarchar(50)  NULL,
    [Tel2] nvarchar(50)  NULL,
    [Pelak] nvarchar(10)  NULL,
    [Car] nvarchar(20)  NULL,
    [Ton] tinyint  NULL,
    [Tol] tinyint  NULL
);
GO

-- Creating table 'Order'
CREATE TABLE [dbo].[Order] (
    [Id] int  NOT NULL,
    [Tarikh] nchar(10)  NULL,
    [Accepted] bit  NOT NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'OrderDetail'
CREATE TABLE [dbo].[OrderDetail] (
    [Id] int  NOT NULL,
    [Tedad] int  NOT NULL,
    [TahvilForosh] int  NOT NULL,
    [Description] varchar(max)  NULL,
    [ProductId] smallint  NOT NULL,
    [CustomerId] smallint  NOT NULL,
    [DriverId] int  NOT NULL,
    [OrderId] int  NOT NULL,
    [HasOracle] bit  NOT NULL,
    [ItemType] tinyint  NOT NULL
);
GO

-- Creating table 'Pallet'
CREATE TABLE [dbo].[Pallet] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NULL,
    [Vazn] tinyint  NULL
);
GO

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Code] nchar(8)  NOT NULL,
    [Nam] nvarchar(100)  NOT NULL,
    [Weight] float  NULL,
    [FaniCode] nchar(15)  NULL,
    [CodeJense] nchar(20)  NULL,
    [TedadDarPallet] smallint  NULL,
    [TedadDarSabad] smallint  NULL,
    [PalletId] tinyint  NOT NULL,
    [Bazres_Id] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Bazres'
ALTER TABLE [dbo].[Bazres]
ADD CONSTRAINT [PK_Bazres]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customer'
ALTER TABLE [dbo].[Customer]
ADD CONSTRAINT [PK_Customer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Driver'
ALTER TABLE [dbo].[Driver]
ADD CONSTRAINT [PK_Driver]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [PK_Order]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [PK_OrderDetail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pallet'
ALTER TABLE [dbo].[Pallet]
ADD CONSTRAINT [PK_Pallet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Bazres_Id] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_ProductBazres]
    FOREIGN KEY ([Bazres_Id])
    REFERENCES [dbo].[Bazres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductBazres'
CREATE INDEX [IX_FK_ProductBazres]
ON [dbo].[Product]
    ([Bazres_Id]);
GO

-- Creating foreign key on [CustomerId] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [FK_OrderDetailCustomer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetailCustomer'
CREATE INDEX [IX_FK_OrderDetailCustomer]
ON [dbo].[OrderDetail]
    ([CustomerId]);
GO

-- Creating foreign key on [DriverId] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [FK_OrderDetailDriver]
    FOREIGN KEY ([DriverId])
    REFERENCES [dbo].[Driver]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetailDriver'
CREATE INDEX [IX_FK_OrderDetailDriver]
ON [dbo].[OrderDetail]
    ([DriverId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [FK_OrderOrderDetail]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Order]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderDetail'
CREATE INDEX [IX_FK_OrderOrderDetail]
ON [dbo].[OrderDetail]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderDetail'
ALTER TABLE [dbo].[OrderDetail]
ADD CONSTRAINT [FK_ProductOrderDetail]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrderDetail'
CREATE INDEX [IX_FK_ProductOrderDetail]
ON [dbo].[OrderDetail]
    ([ProductId]);
GO

-- Creating foreign key on [PalletId] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_ProductPallet]
    FOREIGN KEY ([PalletId])
    REFERENCES [dbo].[Pallet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPallet'
CREATE INDEX [IX_FK_ProductPallet]
ON [dbo].[Product]
    ([PalletId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------