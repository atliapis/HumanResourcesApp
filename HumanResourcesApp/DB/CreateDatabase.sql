IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'HumanResources')
    BEGIN
        CREATE DATABASE [HumanResources]
    END
