using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _repository;

    public TodoController(ITodoRepository repository)
    {
        _repository = repository;
    }

   
    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAll()
    {
        var todos = await _repository.GetAllAsync();
        return Ok(todos);
    }

   
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var todo = await _repository.GetByIdAsync(id);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

  
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item)
    {
        item.Id = 0;
        item.CreatedAt = DateTime.UtcNow;

        var created = await _repository.AddAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

  
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TodoItem updated)
    {
        var success = await _repository.UpdateAsync(id, updated);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

   
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}