
-- Create Database
CREATE DATABASE secure_voting_system;
GO

USE secure_voting_system;
GO

-- Create Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(256) NOT NULL,
    HasVotedLocal BIT DEFAULT 0,
    HasVotedGeneral BIT DEFAULT 0,
    IsAdmin BIT DEFAULT 0
);

-- Create Candidates Table
CREATE TABLE Candidates (
    CandidateId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Position VARCHAR(100) NOT NULL,
    Party VARCHAR(100) NOT NULL,
    ElectionType VARCHAR(20) CHECK (ElectionType IN ('Local', 'General')) NOT NULL
);

-- Create Votes Table
CREATE TABLE Votes (
    VoteId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CandidateId INT NOT NULL,
    ElectionType VARCHAR(20) CHECK (ElectionType IN ('Local', 'General')) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE(),
    VoteDate DATE DEFAULT CAST(GETDATE() AS DATE),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (CandidateId) REFERENCES Candidates(CandidateId)
);

-- Recommended Indexes
CREATE INDEX IX_Votes_UserId_ElectionType ON Votes(UserId, ElectionType);
CREATE INDEX IX_Candidates_ElectionType ON Candidates(ElectionType);

-- Insert Admin User
INSERT INTO Users (Email, PasswordHash, IsAdmin, HasVotedLocal, HasVotedGeneral)
VALUES (
    'muhammad.hassan@admin.com',
    '2c4d4dadfea9c86b541bd5a93c68a6f6438be460c11c7b46e3129244011c01f7',
    1,
    0,
    0
);
INSERT INTO Candidates (Name, Position, Party, ElectionType) VALUES
('hamza khaliq', 'Mayor', 'Green Party', 'Local'),
('shamraiz asif', 'Councillor', 'Unity Party', 'Local'),
('khizer akram', 'mpa', 'Justice League', 'General'),
('hassan asif', 'mna', 'Progressive Front', 'General');