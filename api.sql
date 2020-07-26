CREATE DATABASE API
GO

USER API
GO

CREATE TABLE Users (
    ID int IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(255) NULL,
    LastName varchar(255) NULL,
    Email varchar(255),
    Password varchar(255) NULL,
    CityID INT NULL,
    StateID INT NULL,
    CPF varchar(255) NULL,
    CNPJ varchar(255) NULL,
    RoleID INT,
    AcceptReleases INT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP NULL,
    DeletedAt TIMESTAMP NULL
)
GO

CREATE TABLE UserRoles (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Role VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP NULL,
    DeletedAt TIMESTAMP NULL
)
GO

CREATE TABLE States (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    StateID INT NOT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP NULL,
    DeletedAt TIMESTAMP NULL
)
GO

CREATE TABLE Cities (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    UF VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP NULL,
    DeletedAt TIMESTAMP NULL
)
GO

CREATE TABLE Logs (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Action VARCHAR(255) NOT NULL,
    UserID INT NULL,
    IP VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP NULL,
    DeletedAt TIMESTAMP NULL
)
GO

INSERT INTO UserRoles (Role) VALUES ("User");