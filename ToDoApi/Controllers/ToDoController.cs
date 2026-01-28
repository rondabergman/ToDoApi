using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _db;

        public ToDoController(ToDoDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetToDos")]
        public async Task<ActionResult<IEnumerable<ToDo>>> Get()
        {
            var todos = await _db.ToDos.ToListAsync();
            return Ok(todos);
        }
    }
}
