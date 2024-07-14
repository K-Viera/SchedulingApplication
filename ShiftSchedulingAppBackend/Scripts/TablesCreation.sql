-- Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) NOT NULL UNIQUE,
	[Password] NVARCHAR(255) NOT NULL,
    UserType INT NOT NULL,
    InternalId NVARCHAR(255) NOT NULL
);

-- ShiftTypes Table
CREATE TABLE ShiftTypes (
    ShiftTypeId INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    Price DECIMAL(18, 2) NOT NULL,
    NightPrice DECIMAL(18, 2) NULL,
    HolidayPrice DECIMAL(18, 2) NULL
);

-- Places Table
CREATE TABLE Places (
    PlaceId INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL
);

-- Shifts Table
CREATE TABLE Shifts (
    ShiftId INT PRIMARY KEY IDENTITY(1,1),
    CreationDate DATE NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    PricePerUser DECIMAL(18, 2) NOT NULL,
    ShiftTypeId INT FOREIGN KEY REFERENCES ShiftTypes(ShiftTypeId),
    CreatorUserId INT FOREIGN KEY REFERENCES Users(UserId),
    MaxUsers INT NULL,
    TotalPrice DECIMAL(18, 2) NULL,
    Status INT NOT NULL
);

-- ShiftPlaces Table (Many-to-Many relationship between Shifts and Places)
CREATE TABLE ShiftPlaces (
    ShiftId INT FOREIGN KEY REFERENCES Shifts(ShiftId),
    PlaceId INT FOREIGN KEY REFERENCES Places(PlaceId),
    PRIMARY KEY (ShiftId, PlaceId)
);

-- AppliedUsers Table (Many-to-Many relationship between Shifts and Users for applied users)
CREATE TABLE AppliedUsers (
    ShiftId INT FOREIGN KEY REFERENCES Shifts(ShiftId),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    PRIMARY KEY (ShiftId, UserId)
);

-- ApprovedUsers Table (Many-to-Many relationship between Shifts and Users for approved users)
CREATE TABLE ApprovedUsers (
    ShiftId INT FOREIGN KEY REFERENCES Shifts(ShiftId),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    PRIMARY KEY (ShiftId, UserId)
);