CREATE PROCEDURE [dbo].[spTodos_Delete]
	@AssignedTo int,
	@TodoId int
AS
BEGIN
	DELETE FROM dbo.Todos
	where Id = @TodoId and AssignedTo = @AssignedTo
END
