CREATE PROCEDURE [dbo].[spTodos_Create]
	@Task nvarchar(50),
	@AssignedTo int

AS
BEGIN 
	INSERT INTO dbo.Todos (Task, AssignedTo)
	VALUES (@Task, @AssignedTo);

	SELECT Id, Task, AssignedTo, IsComplete
	FROM dbo.Todos
	where Id = SCOPE_IDENTITY();  -- Get the last created Id in given scoope 
END