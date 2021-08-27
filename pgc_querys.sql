CREATE DATABASE [pgc]
GO

USE [pgc]
GO

CREATE TABLE [PhoneNumberType] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(80) NOT NULL,

    CONSTRAINT [PK_PhoneNumberType] PRIMARY KEY([Id])
)
GO

CREATE TABLE [PersonPhone] (
    [BusinessEntityID] INT NOT NULL IDENTITY(1, 1),
    [PhoneNumber] NVARCHAR(20) NOT NULL,
    [PhoneNumberTypeID] INT NOT NULL,

    CONSTRAINT [PK_PersonPhone] PRIMARY KEY([BusinessEntityID]),
    CONSTRAINT [FK_PhoneNumberType] FOREIGN KEY([PhoneNumberTypeID]) REFERENCES [PhoneNumberType]([Id])
)
GO
