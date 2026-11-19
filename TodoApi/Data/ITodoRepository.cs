using TodoApi.Models;

namespace TodoApi.Data;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> AddAsync(TodoItem item);
    Task<bool> UpdateAsync(int id, TodoItem updated);
    Task<bool> DeleteAsync(int id);
}
