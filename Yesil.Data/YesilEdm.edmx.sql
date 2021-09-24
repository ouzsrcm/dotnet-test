
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/22/2016 10:48:13
-- Generated from EDMX file: E:\Projects\Yesil.Web\Yesil.Data\YesilEdm.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Yesil];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerAddress_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerAddress] DROP CONSTRAINT [FK_CustomerAddress_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerCredential_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerCredential] DROP CONSTRAINT [FK_CustomerCredential_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_FacilityImage_Facility]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FacilityImage] DROP CONSTRAINT [FK_FacilityImage_Facility];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_OrderState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_OrderState];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderCard_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderCard] DROP CONSTRAINT [FK_OrderCard_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProduct_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProduct] DROP CONSTRAINT [FK_OrderProduct_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProduct_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProduct] DROP CONSTRAINT [FK_OrderProduct_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_PagePlugin_Page]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PagePlugin] DROP CONSTRAINT [FK_PagePlugin_Page];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCart_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCart] DROP CONSTRAINT [FK_ShoppingCart_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCart_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCart] DROP CONSTRAINT [FK_ShoppingCart_Product];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[BankAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BankAccount];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Company];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[CustomerAddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerAddress];
GO
IF OBJECT_ID(N'[dbo].[CustomerCredential]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerCredential];
GO
IF OBJECT_ID(N'[dbo].[Facility]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Facility];
GO
IF OBJECT_ID(N'[dbo].[FacilityImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FacilityImage];
GO
IF OBJECT_ID(N'[dbo].[Faq]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Faq];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[OrderCard]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderCard];
GO
IF OBJECT_ID(N'[dbo].[OrderProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderProduct];
GO
IF OBJECT_ID(N'[dbo].[OrderState]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderState];
GO
IF OBJECT_ID(N'[dbo].[Page]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Page];
GO
IF OBJECT_ID(N'[dbo].[PagePlugin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PagePlugin];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCart];
GO
IF OBJECT_ID(N'[dbo].[SocialAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialAccount];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [AdminId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'BankAccounts'
CREATE TABLE [dbo].[BankAccounts] (
    [BankAccountId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(300)  NULL,
    [Image] nvarchar(300)  NULL,
    [Number] nvarchar(300)  NULL,
    [Description] nvarchar(300)  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(1000)  NULL,
    [Details] varchar(max)  NULL,
    [Keyword] nvarchar(2000)  NULL,
    [Description] nvarchar(2000)  NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [CompanyId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(500)  NULL,
    [Address] nvarchar(500)  NULL,
    [Email] nvarchar(500)  NULL,
    [Phone1] nvarchar(20)  NULL,
    [Phone2] nvarchar(20)  NULL,
    [Website] nvarchar(100)  NULL,
    [BussinesHours] nvarchar(500)  NULL,
    [Fax] nvarchar(20)  NULL,
    [Description] nvarchar(500)  NULL,
    [IsDefault] bit  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerId] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(100)  NULL,
    [Lastname] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [Phone1] nvarchar(100)  NULL,
    [Phone2] nvarchar(100)  NULL
);
GO

-- Creating table 'CustomerAddresses'
CREATE TABLE [dbo].[CustomerAddresses] (
    [CustomerAddressId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NULL,
    [Title] nvarchar(100)  NULL,
    [Phone1] nvarchar(20)  NULL,
    [Phone2] nvarchar(20)  NULL,
    [Phone3] nvarchar(20)  NULL,
    [Email] nvarchar(100)  NULL,
    [Address] nvarchar(300)  NULL,
    [City] nvarchar(300)  NULL,
    [PostalCode] nvarchar(30)  NULL
);
GO

-- Creating table 'CustomerCredentials'
CREATE TABLE [dbo].[CustomerCredentials] (
    [CustomerCredentialId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NULL,
    [Email] nvarchar(100)  NULL,
    [Password] nvarchar(100)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Facilities'
CREATE TABLE [dbo].[Facilities] (
    [FacilityId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(1000)  NULL,
    [Keyword] nvarchar(1000)  NULL,
    [Description] nvarchar(1000)  NULL,
    [Details] varchar(max)  NULL,
    [Image] nvarchar(1000)  NULL,
    [Address] nvarchar(1000)  NULL
);
GO

-- Creating table 'FacilityImages'
CREATE TABLE [dbo].[FacilityImages] (
    [FacilityImageId] int IDENTITY(1,1) NOT NULL,
    [FacilityId] int  NULL,
    [Image] nvarchar(1000)  NULL
);
GO

-- Creating table 'Faqs'
CREATE TABLE [dbo].[Faqs] (
    [FaqId] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(1000)  NULL,
    [Answer] varchar(max)  NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [NewsId] int IDENTITY(1,1) NOT NULL,
    [Keyword] nvarchar(300)  NULL,
    [Description] nvarchar(1000)  NULL,
    [Title] nvarchar(1000)  NULL,
    [Details] varchar(max)  NULL,
    [Image] nvarchar(1000)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NULL,
    [DeliveryAddressId] int  NULL,
    [BillingAddressId] int  NULL,
    [OrderStateId] int  NULL,
    [Description] varchar(max)  NULL,
    [PaymentType] nvarchar(100)  NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'OrderCards'
CREATE TABLE [dbo].[OrderCards] (
    [OrderCardId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NULL,
    [Date] datetime  NULL,
    [HolderName] nvarchar(100)  NULL,
    [Number] nvarchar(20)  NULL,
    [ExpirationYear] int  NULL,
    [ExpirationMonth] int  NULL,
    [Ccv] int  NULL
);
GO

-- Creating table 'OrderProducts'
CREATE TABLE [dbo].[OrderProducts] (
    [OrderProductId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NULL,
    [ProductId] int  NULL,
    [Price] decimal(19,4)  NULL,
    [Quantity] int  NULL,
    [Date] datetime  NULL,
    [Description] nvarchar(1000)  NULL
);
GO

-- Creating table 'OrderStates'
CREATE TABLE [dbo].[OrderStates] (
    [OrderStateId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(1000)  NULL
);
GO

-- Creating table 'Pages'
CREATE TABLE [dbo].[Pages] (
    [PageId] int IDENTITY(1,1) NOT NULL,
    [Keyword] nvarchar(2000)  NULL,
    [Description] nvarchar(2000)  NULL,
    [Name] nvarchar(100)  NULL,
    [Title] nvarchar(2000)  NULL,
    [Details] varchar(max)  NULL
);
GO

-- Creating table 'PagePlugins'
CREATE TABLE [dbo].[PagePlugins] (
    [PagePluginId] int IDENTITY(1,1) NOT NULL,
    [PageId] int  NULL,
    [PluginType] nvarchar(5)  NULL,
    [Details] varchar(max)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NULL,
    [Title] nvarchar(2000)  NULL,
    [Keyword] nvarchar(2000)  NULL,
    [Description] nvarchar(2000)  NULL,
    [Details] varchar(max)  NULL,
    [Image] nvarchar(1000)  NULL,
    [Tags] nvarchar(2000)  NULL,
    [Price] decimal(19,4)  NULL,
    [Discount] decimal(19,4)  NULL,
    [CampaignPrice] decimal(19,4)  NULL,
    [IsCampaign] bit  NULL
);
GO

-- Creating table 'ShoppingCarts'
CREATE TABLE [dbo].[ShoppingCarts] (
    [ShoppingCartId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NULL,
    [ProductId] int  NULL,
    [SoldPrice] decimal(19,4)  NULL,
    [Date] datetime  NULL,
    [Quantity] int  NULL
);
GO

-- Creating table 'SocialAccounts'
CREATE TABLE [dbo].[SocialAccounts] (
    [SocialAccountId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(500)  NULL,
    [Url] nvarchar(500)  NULL,
    [Icon] nvarchar(100)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AdminId] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([AdminId] ASC);
GO

-- Creating primary key on [BankAccountId] in table 'BankAccounts'
ALTER TABLE [dbo].[BankAccounts]
ADD CONSTRAINT [PK_BankAccounts]
    PRIMARY KEY CLUSTERED ([BankAccountId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [CompanyId] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([CompanyId] ASC);
GO

-- Creating primary key on [CustomerId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- Creating primary key on [CustomerAddressId] in table 'CustomerAddresses'
ALTER TABLE [dbo].[CustomerAddresses]
ADD CONSTRAINT [PK_CustomerAddresses]
    PRIMARY KEY CLUSTERED ([CustomerAddressId] ASC);
GO

-- Creating primary key on [CustomerCredentialId] in table 'CustomerCredentials'
ALTER TABLE [dbo].[CustomerCredentials]
ADD CONSTRAINT [PK_CustomerCredentials]
    PRIMARY KEY CLUSTERED ([CustomerCredentialId] ASC);
GO

-- Creating primary key on [FacilityId] in table 'Facilities'
ALTER TABLE [dbo].[Facilities]
ADD CONSTRAINT [PK_Facilities]
    PRIMARY KEY CLUSTERED ([FacilityId] ASC);
GO

-- Creating primary key on [FacilityImageId] in table 'FacilityImages'
ALTER TABLE [dbo].[FacilityImages]
ADD CONSTRAINT [PK_FacilityImages]
    PRIMARY KEY CLUSTERED ([FacilityImageId] ASC);
GO

-- Creating primary key on [FaqId] in table 'Faqs'
ALTER TABLE [dbo].[Faqs]
ADD CONSTRAINT [PK_Faqs]
    PRIMARY KEY CLUSTERED ([FaqId] ASC);
GO

-- Creating primary key on [NewsId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([NewsId] ASC);
GO

-- Creating primary key on [OrderId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [OrderCardId] in table 'OrderCards'
ALTER TABLE [dbo].[OrderCards]
ADD CONSTRAINT [PK_OrderCards]
    PRIMARY KEY CLUSTERED ([OrderCardId] ASC);
GO

-- Creating primary key on [OrderProductId] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [PK_OrderProducts]
    PRIMARY KEY CLUSTERED ([OrderProductId] ASC);
GO

-- Creating primary key on [OrderStateId] in table 'OrderStates'
ALTER TABLE [dbo].[OrderStates]
ADD CONSTRAINT [PK_OrderStates]
    PRIMARY KEY CLUSTERED ([OrderStateId] ASC);
GO

-- Creating primary key on [PageId] in table 'Pages'
ALTER TABLE [dbo].[Pages]
ADD CONSTRAINT [PK_Pages]
    PRIMARY KEY CLUSTERED ([PageId] ASC);
GO

-- Creating primary key on [PagePluginId] in table 'PagePlugins'
ALTER TABLE [dbo].[PagePlugins]
ADD CONSTRAINT [PK_PagePlugins]
    PRIMARY KEY CLUSTERED ([PagePluginId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [ShoppingCartId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [PK_ShoppingCarts]
    PRIMARY KEY CLUSTERED ([ShoppingCartId] ASC);
GO

-- Creating primary key on [SocialAccountId] in table 'SocialAccounts'
ALTER TABLE [dbo].[SocialAccounts]
ADD CONSTRAINT [PK_SocialAccounts]
    PRIMARY KEY CLUSTERED ([SocialAccountId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Category]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Category'
CREATE INDEX [IX_FK_Product_Category]
ON [dbo].[Products]
    ([CategoryId]);
GO

-- Creating foreign key on [CustomerId] in table 'CustomerAddresses'
ALTER TABLE [dbo].[CustomerAddresses]
ADD CONSTRAINT [FK_CustomerAddress_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerAddress_Customer'
CREATE INDEX [IX_FK_CustomerAddress_Customer]
ON [dbo].[CustomerAddresses]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerId] in table 'CustomerCredentials'
ALTER TABLE [dbo].[CustomerCredentials]
ADD CONSTRAINT [FK_CustomerCredential_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerCredential_Customer'
CREATE INDEX [IX_FK_CustomerCredential_Customer]
ON [dbo].[CustomerCredentials]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Customer'
CREATE INDEX [IX_FK_Order_Customer]
ON [dbo].[Orders]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK_ShoppingCart_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCart_Customer'
CREATE INDEX [IX_FK_ShoppingCart_Customer]
ON [dbo].[ShoppingCarts]
    ([CustomerId]);
GO

-- Creating foreign key on [FacilityId] in table 'FacilityImages'
ALTER TABLE [dbo].[FacilityImages]
ADD CONSTRAINT [FK_FacilityImage_Facility]
    FOREIGN KEY ([FacilityId])
    REFERENCES [dbo].[Facilities]
        ([FacilityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FacilityImage_Facility'
CREATE INDEX [IX_FK_FacilityImage_Facility]
ON [dbo].[FacilityImages]
    ([FacilityId]);
GO

-- Creating foreign key on [OrderStateId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_OrderState]
    FOREIGN KEY ([OrderStateId])
    REFERENCES [dbo].[OrderStates]
        ([OrderStateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_OrderState'
CREATE INDEX [IX_FK_Order_OrderState]
ON [dbo].[Orders]
    ([OrderStateId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderCards'
ALTER TABLE [dbo].[OrderCards]
ADD CONSTRAINT [FK_OrderCard_Order]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCard_Order'
CREATE INDEX [IX_FK_OrderCard_Order]
ON [dbo].[OrderCards]
    ([OrderId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProduct_Order]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProduct_Order'
CREATE INDEX [IX_FK_OrderProduct_Order]
ON [dbo].[OrderProducts]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProduct_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProduct_Product'
CREATE INDEX [IX_FK_OrderProduct_Product]
ON [dbo].[OrderProducts]
    ([ProductId]);
GO

-- Creating foreign key on [PageId] in table 'PagePlugins'
ALTER TABLE [dbo].[PagePlugins]
ADD CONSTRAINT [FK_PagePlugin_Page]
    FOREIGN KEY ([PageId])
    REFERENCES [dbo].[Pages]
        ([PageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PagePlugin_Page'
CREATE INDEX [IX_FK_PagePlugin_Page]
ON [dbo].[PagePlugins]
    ([PageId]);
GO

-- Creating foreign key on [ProductId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK_ShoppingCart_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCart_Product'
CREATE INDEX [IX_FK_ShoppingCart_Product]
ON [dbo].[ShoppingCarts]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------