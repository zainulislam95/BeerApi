-- SQL script to create Customers table and insert sample data

CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(200) NOT NULL,
    LastName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(320) NULL,
    Phone NVARCHAR(50) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

INSERT INTO Customers (FirstName, LastName, Email, Phone)
VALUES
('Alice', 'Anderson', 'alice@example.com', '+123456789'),
('Bob', 'Brown', 'bob@example.com', '+1987654321');

-- Note: You may want to alter Id to be IDENTITY in production:
-- ALTER TABLE Customers DROP CONSTRAINT PK_Customers; -- if exists
-- ALTER TABLE Customers ADD Id INT IDENTITY(1,1) PRIMARY KEY;

-- Alternatively use EF Core Migrations to create and seed this table.
