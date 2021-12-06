CREATE OR ALTER PROCEDURE [dbo].[GetFilteredEmployees]
	-- Add the parameters for the stored procedure here
	@DepartmentId int = null,
	@StatusId int = null

AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @DepartmentId = 0 AND @StatusId = 0
		SELECT 
			e.*,
			d.Id AS DepartmentId,
			d.Name AS DepartmentName,
			s.Id AS StatusId,
			s.Name AS StatusName
		FROM [dbo].[Employees] AS e
			INNER JOIN [dbo].[Departments] AS d ON e.Department = d.Id
			INNER JOIN [dbo].[Statuses] AS s ON e.Status = s.Id

	IF @DepartmentId != 0 AND @StatusId = 0
		SELECT 
			e.*,
			d.Id AS DepartmentId,
			d.Name AS DepartmentName,
			s.Id AS StatusId,
			s.Name AS StatusName
		FROM [dbo].[Employees] AS e
			INNER JOIN [dbo].[Departments] AS d ON e.Department = d.Id
			INNER JOIN [dbo].[Statuses] AS s ON e.Status = s.Id
		WHERE
			e.Department = @DepartmentId

	IF @DepartmentId = 0 AND @StatusId != 0
		SELECT 
			e.*,
			d.Id AS DepartmentId,
			d.Name AS DepartmentName,
			s.Id AS StatusId,
			s.Name AS StatusName
		FROM [dbo].[Employees] AS e
			INNER JOIN [dbo].[Departments] AS d ON e.Department = d.Id
			INNER JOIN [dbo].[Statuses] AS s ON e.Status = s.Id
		WHERE
			e.Status = @StatusId

	IF @DepartmentId !=0 AND @DepartmentId !=0
		SELECT 
			e.*,
			d.Id AS DepartmentId,
			d.Name AS DepartmentName,
			s.Id AS StatusId,
			s.Name AS StatusName
		FROM [dbo].[Employees] AS e
			INNER JOIN [dbo].[Departments] AS d ON e.Department = d.Id
			INNER JOIN [dbo].[Statuses] AS s ON e.Status = s.Id
		WHERE
			e.Department = @DepartmentId
			AND e.Status = @StatusId
END