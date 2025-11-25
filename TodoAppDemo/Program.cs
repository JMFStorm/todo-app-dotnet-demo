using Microsoft.EntityFrameworkCore;

using TodoAppDemo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlite("Data Source=todos.db"));
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
db.Database.EnsureCreated();

app.MapTodoEndpoints();
app.Run();
