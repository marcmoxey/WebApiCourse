CREATE PROCEDURE [dbo].[spTodos_UpdateTask]
	@Task nvarchar(50),
	@AssignedTo int,
	@TodoId int

AS
BEGIN 
	UPDATE dbo.Todos 
	SET Task = @Task
	WHERE Id = @TodoId and AssignedTo = @AssignedTo;
END
