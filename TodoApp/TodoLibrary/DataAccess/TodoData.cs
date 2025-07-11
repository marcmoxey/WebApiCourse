using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLibrary.Models;

namespace TodoLibrary.DataAccess;

public  class TodoData
{
    private readonly ISqlDataAccess _sql;

    public TodoData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
    {
       return  _sql.LoadData<TodoModel, dynamic>("dbo.spTodo_GetAllAssigned", new { AssignedTo = assignedTo }, "Default");
    }
}
