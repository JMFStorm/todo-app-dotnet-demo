using Microsoft.EntityFrameworkCore;

namespace TodoAppDemo;

public class TodoService(TodoDbContext db) : ITodoService
{
    public async Task<List<Todo>> GetAllAsync() => await db.Todos.ToListAsync();

    public async Task<Todo?> GetByIdAsync(int id) => await db.Todos.FindAsync(id);

    public async Task<Todo> CreateAsync(Todo newTodo)
    {
        var todo = new Todo
        { 
            Title = newTodo.Title,
            IsDone = newTodo.IsDone 
        };
        db.Todos.Add(todo);
        await db.SaveChangesAsync();
        return todo;
    }

    public async Task<bool> UpdateAsync(int id, Todo updateTodo)
    {
        var existing = await db.Todos.FindAsync(id);
        if (existing is null)
        {
            return false;
        }
        existing.Title  = updateTodo.Title;
        existing.IsDone = updateTodo.IsDone;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await db.Todos.FirstOrDefaultAsync(t => t.Id == id);
        if (item is null)
        {
            return false;
        }
        db.Todos.Remove(item);
        await db.SaveChangesAsync();
        return true;
    }
}