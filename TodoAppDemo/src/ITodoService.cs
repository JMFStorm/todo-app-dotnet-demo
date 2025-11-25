namespace TodoAppDemo;

public interface ITodoService
{
    Task<List<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> CreateAsync(Todo newTodo);
    Task<bool> UpdateAsync(int id, Todo updateTodo);
    Task<bool> DeleteAsync(int id);
}