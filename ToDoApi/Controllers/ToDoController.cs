using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class ToDoController : ControllerBase
    {
        private readonly List<ToDo> _tempToDos = new List<ToDo>
        {
            new ToDo { Title = "title1" },
            new ToDo { Title = "title2" },
            new ToDo { Title = "title3" }
        };

        [HttpGet(Name = "GetToDos")]
        public ActionResult<IEnumerable<ToDo>> Get()
        {
            return Ok(_tempToDos);
        }
    }
}
