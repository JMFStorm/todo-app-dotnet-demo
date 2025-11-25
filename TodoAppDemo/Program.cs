using Microsoft.EntityFrameworkCore;

using TodoAppDemo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlite("Data Source=todos.db"));
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
db.Database.EnsureCreated();

app.MapGet("/", () => "Hello World!");
app.MapTodoEndpoints();
app.Run();
