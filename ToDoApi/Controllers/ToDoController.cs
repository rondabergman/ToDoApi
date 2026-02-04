using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            //Get and return all the todos
            var todos = await _db.ToDos.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id:int}", Name = "GetToDoById")]
        public async Task<ActionResult<ToDo>> Get(int id)
        {
            //Get and return a single todo based on the id, return not found if id is not valid
            var todo = await _db.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost(Name = "CreateToDo")]
        public async Task<ActionResult<ToDo>> Create([FromBody] ToDo todo)
        {
            //Ensure we have valid data and create a new todo
            if (todo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure EF assigns an Id for new entity
            todo.Id = 0;
            _db.ToDos.Add(todo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetToDoById", new { id = todo.Id }, todo);
        }

        [HttpPut("{id:int}", Name = "UpdateToDo")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDo todo)
        {
            //Ensure we have valid data and a valid id for the update 
            if (todo == null || id != todo.Id)
            {
                return BadRequest();
            }

            var existing = await _db.ToDos.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Update allowed fields only (do not overwrite CreateDate)
            existing.Title = todo.Title;
            existing.Description = todo.Description;
            existing.DueDate = todo.DueDate;
            existing.IsCompleted = todo.IsCompleted;

            _db.Entry(existing).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteToDo")]
        public async Task<IActionResult> Delete(int id)
        {
            //Ensure we have a valid todo id and delete it
            var todo = await _db.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _db.ToDos.Remove(todo);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}