using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TodoLibrary.Models;

namespace TodoLibrary.DataAccess;

public class TodoData
{
    private readonly ISqlDataAccess _sql;

    public TodoData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
    {
        return _sql.LoadData<TodoModel, dynamic>
            ("dbo.spTodos_GetAllAssigned",
            new { AssignedTo = assignedTo },
            "Default");
    }

    public async Task<TodoModel> GetOneAssigned(int assignedTo, int todoId) // if casing match would not need to do = 'Assigned = assignedTo' can be AssignedTo
    {
        var results = await _sql.LoadData<TodoModel, dynamic>
            ("dbo.spTodos_GetOneAssigned",
            new { AssignedTo = assignedTo, TodoId = todoId },
            "Default");

        return results.FirstOrDefault();
    }
}
