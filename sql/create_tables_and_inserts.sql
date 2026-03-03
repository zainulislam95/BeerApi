-- SQL script to create Products and Articles tables and insert sample data
-- Run this on your SQL Server (adjust types/lengths as needed)

CREATE TABLE Products (
    Id INT PRIMARY KEY,
    BrandName NVARCHAR(200) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    DescriptionText NVARCHAR(2000) NULL
);

CREATE TABLE Articles (
    Id INT PRIMARY KEY,
    ProductId INT NOT NULL,
    ShortDescription NVARCHAR(500) NULL,
    Price DECIMAL(18,2) NOT NULL,
    Unit NVARCHAR(100) NULL,
    PricePerUnit DECIMAL(18,2) NULL,
    PricePerUnitText NVARCHAR(200) NULL,
    Image NVARCHAR(500) NULL,
    CONSTRAINT FK_Articles_Product FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Sample data
INSERT INTO Products (Id, BrandName, Name, DescriptionText) VALUES
(1, 'BrandA', 'Beer A', 'Tasty beer A'),
(2, 'BrandB', 'Beer B', 'Tasty beer B');

INSERT INTO Articles (Id, ProductId, ShortDescription, Price, Unit, PricePerUnit, PricePerUnitText, Image) VALUES
(11, 1, 'Box', 5.00, '6x330ml', 1.50, '1.50 €/Liter', '/img/a.png'),
(12, 1, 'Bottle', 2.50, '330ml', 2.00, '2.00 €/Liter', '/img/a-bottle.png'),
(21, 2, 'Can', 3.00, '500ml', 2.50, '2.50 €/Liter', '/img/b.png');

-- You can run this script to set up a local DB. Adjust identity/autoincrement as needed.

-- Optional: make Ids identity columns
-- ALTER TABLE Products DROP CONSTRAINT PK_Products; -- if created
-- ALTER TABLE Products ADD Id INT IDENTITY(1,1) PRIMARY KEY;

-- ALTER TABLE Articles DROP CONSTRAINT PK_Articles; -- if created
-- ALTER TABLE Articles ADD Id INT IDENTITY(1,1) PRIMARY KEY;

-- Note: If you plan to use EF Core Migrations instead of raw SQL, you can scaffold migrations in your project.
