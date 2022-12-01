IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ProductCatalogME')
BEGIN
    CREATE DATABASE ProductCatalogME
END
    
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ProductCatalogMETests')
BEGIN
    CREATE DATABASE ProductCatalogMETests
END
    
GO