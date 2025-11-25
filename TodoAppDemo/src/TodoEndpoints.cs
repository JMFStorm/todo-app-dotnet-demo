namespace TodoAppDemo;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/todos", async (ITodoService service) => await service.GetAllAsync());

        app.MapGet("/api/todos/{id:int}", async (int id, ITodoService service) =>
            await service.GetByIdAsync(id) is Todo todo
                ? Results.Ok(todo)
                : Results.NotFound());

        app.MapPost("/api/todos", async (Todo todo, ITodoService service) =>
        {
            var newTodo = await service.CreateAsync(todo);
            return Results.Created($"/api/todos/{newTodo.Id}", newTodo);
        });

        app.MapPut("/api/todos/{id:int}", async (int id, Todo dto, ITodoService service) =>
            await service.UpdateAsync(id, dto)
                ? Results.Ok()
                : Results.NotFound());

        app.MapDelete("/api/todos/{id:int}", async (int id, ITodoService service) =>
            await service.DeleteAsync(id)
                ? Results.NoContent()
                : Results.NotFound());
    }
}