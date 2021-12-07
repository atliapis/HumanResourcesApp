BEGIN TRY
    BEGIN TRANSACTION
 
		USE [HumanResources]

        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Statuses' AND xtype='U')
        BEGIN
            CREATE TABLE Statuses (
                Id int PRIMARY KEY, --selected int for faster comparison when filtering
                Name nvarchar(255) NOT NULL UNIQUE
            );
        END

        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Departments' AND xtype='U')
        BEGIN
            CREATE TABLE Departments (
                Id int PRIMARY KEY,
                Name nvarchar(255) NOT NULL UNIQUE
            );
        END

        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
        BEGIN
            CREATE TABLE Employees (
                EmployeeNumber int PRIMARY KEY,
                FirstName nvarchar(255) NOT NULL,
                LastName nvarchar(255) NOT NULL,
                DateOfBirth DATE,
                Email nvarchar(255),
                Department int	FOREIGN KEY REFERENCES Departments(Id),
                Status int	FOREIGN KEY REFERENCES Statuses(Id)
            );
        END
        
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Settings' AND xtype='U')
        BEGIN
            CREATE TABLE Settings (
                Name nvarchar(255) PRIMARY KEY,
                Value int
            );
        END

        CREATE INDEX idx_filter
            ON Employees (Department, Status);

        CREATE INDEX idx_statuses_filter
            ON Statuses (Id);

        CREATE INDEX idx_departments_filter
            ON Departments (Id);

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN --RollBack in case of Error

    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  

    SELECT   
       @ErrorMessage = ERROR_MESSAGE(),  
       @ErrorSeverity = ERROR_SEVERITY(),  
       @ErrorState = ERROR_STATE();  

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);  
END CATCH