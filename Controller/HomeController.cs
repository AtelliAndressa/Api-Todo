using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Controller
{
    /// <summary>
    /// Herdar do ControllerBase já traz mais recursos embutidos.
    /// </summary>
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Retorna uma lista de tarefas.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
            => Ok(context.Todos.ToList()); 
        
        /// <summary>
        /// Retorna um todo do db pelo id passado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            return Ok(todo);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody] TodoModel model, [FromServices] AppDbContext context)
        {
            context.Todos.Add(model);
            context.SaveChanges();

            return Created($"/{model.Id}", model);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put([FromRoute] int id, TodoModel model, [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            
            todo.Title = model.Title;
            todo.Done = model.Done;

            context.Todos.Update(todo);
            context.SaveChanges();
            return Ok(todo);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            context.Todos.Remove(todo);
            context.SaveChanges();
            return Ok(todo);
        }            
    }
}
