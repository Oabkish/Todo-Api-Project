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
}