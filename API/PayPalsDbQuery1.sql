CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
	TotalExpenses INT NOT NULL Default 0,
	IsAdmin BIT NOT NULL DEFAULT 0
);


CREATE TABLE Groups (
    GroupId INT PRIMARY KEY IDENTITY,
    GroupName NVARCHAR(50) NOT NULL,
    CreatorId INT NOT NULL,
	TotalExpenses INT NOT NULL Default 0,
    FOREIGN KEY (CreatorId) REFERENCES Users(UserId)
);


CREATE TABLE GroupMembers (
    GroupId INT NOT NULL,
    UserId INT NOT NULL,
    PRIMARY KEY (GroupId, UserId),
    FOREIGN KEY (GroupId) REFERENCES Groups(GroupId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


CREATE TABLE Expenses (
    ExpenseId INT PRIMARY KEY IDENTITY,
    GroupId INT NOT NULL,
    PayerId INT NOT NULL,
    ExpenseDate DATETIME NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
	ApprovalsReceived INT DEFAULT(0),
	TotalMembers INT NOT NULL,
    Description NVARCHAR(100) NOT NULL,
    FOREIGN KEY (GroupId) REFERENCES Groups(GroupId),
    FOREIGN KEY (PayerId) REFERENCES Users(UserId)
);


CREATE TABLE ExpenseUsers (
    ExpenseId INT NOT NULL,
    UserId INT NOT NULL,
    PRIMARY KEY (ExpenseId, UserId),
    FOREIGN KEY (ExpenseId) REFERENCES Expenses(ExpenseId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);



CREATE TABLE Approvals (
    ApprovalId INT PRIMARY KEY IDENTITY,
    ExpenseId INT NOT NULL,
    ApproverId INT NOT NULL,
    ApprovalStatus INT NOT NULL,
    FOREIGN KEY (ExpenseId) REFERENCES Expenses(ExpenseId),
    FOREIGN KEY (ApproverId) REFERENCES Users(UserId)
);


CREATE TABLE ExpenseApprovals (
    ExpenseId INT NOT NULL,
    UserId INT NOT NULL,
    IsApproved BIT NOT NULL DEFAULT 0,
    PRIMARY KEY (ExpenseId, UserId),
    FOREIGN KEY (ExpenseId) REFERENCES Expenses(ExpenseId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);