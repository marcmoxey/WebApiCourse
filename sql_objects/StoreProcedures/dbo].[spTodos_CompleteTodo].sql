CREATE PROCEDURE [dbo].[spTodos_CompleteTodo]
	@AssignedTo int,
	@TodoId int

AS
BEGIN 
	UPDATE dbo.Todos 
	SET IsComplete = 1
	WHERE Id = @TodoId and AssignedTo = @AssignedTo;
END
