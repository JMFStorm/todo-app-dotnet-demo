using Microsoft.EntityFrameworkCore;
using TodoAppDemo;

namespace TodoAppDemoTests;

public class TodoServiceTests
{
    private readonly TodoDbContext _context;
    private readonly TodoService _service;

    public TodoServiceTests()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new TodoDbContext(options);
        _service = new TodoService(_context);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllTodos()
    {
        _context.Todos.AddRange(
            new Todo { Title = "First", IsDone = false },
            new Todo { Title = "Second", IsDone = true }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, t => t.Title == "First");
         Assert.Contains(result, t => t.Title == "Second");
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsTodo()
    {
        var todo = new Todo { Title = "Test" };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetByIdAsync(todo.Id);

        // Assert
        Assert.Equal(todo.Id, result?.Id);
        Assert.Equal("Test", result?.Title);
    }

        [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        var todo = new Todo { Title = "Test" };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsTodoAndReturnsIt()
    {
        var newTodo = new Todo { Title = "New Todo", IsDone = false };

        // Act
        var result = await _service.CreateAsync(newTodo);

        // Assert
        Assert.True(result.Id > 0);
        Assert.Equal("New Todo", result.Title);
        var currentItems = await _context.Todos.ToListAsync();
        Assert.Single(currentItems);
    }

    [Fact]
    public async Task UpdateAsync_ExistingId_UpdatesAndReturnsTrue()
    {
        var todo = new Todo { Title = "Old", IsDone = false };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        var update = new Todo { Title = "Updated", IsDone = true };

        // Act
        var success = await _service.UpdateAsync(todo.Id, update);

        // Assert
        Assert.True(success);
        var updated = await _context.Todos.FindAsync(todo.Id);
        Assert.Equal("Updated", updated!.Title);
        Assert.True(updated.IsDone);
    }

    [Fact]
    public async Task DeleteAsync_ExistingId_ReturnsTrue()
    {
        var todo = new Todo { Title = "Delete me" };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        // Act
        var success = await _service.DeleteAsync(todo.Id);

        // Assert
        Assert.True(success);
        Assert.Empty(_context.Todos);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingId_ReturnsFalse()
    {
        // Act
        var success = await _service.DeleteAsync(999);

        // Assert
        Assert.False(success);
    }
}