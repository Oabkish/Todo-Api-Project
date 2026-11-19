using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems
            .OrderBy(t => t.Id)
            .ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem> AddAsync(TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> UpdateAsync(int id, TodoItem updated)
    {
        var existing = await _context.TodoItems.FindAsync(id);
        if (existing is null)
        {
            return false;
        }

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.IsComplete = updated.IsComplete;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.TodoItems.FindAsync(id);
        if (existing is null)
        {
            return false;
        }

        _context.TodoItems.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}