using CustomMiddlewareExample.Data;
using CustomMiddlewareExample.Models;
namespace CustomMiddlewareExample.Endpoints;
public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/todos");

        // 1. Standard GET (200 OK)
        group.MapGet("/", (InMemoryTodoStore store) =>
            Results.Ok(store.GetAll()));

        // 2. GET by ID (200 OK or 404 NotFound)
        group.MapGet("/{id:int}", (int id, InMemoryTodoStore store) =>
        {
            var item = store.GetById(id);
            return item is not null
                ? Results.Ok(item)
                : Results.NotFound(new { Message = $"Todo {id} not found." });
        });

        // 3. POST with validation (201 Created or 400 BadRequest)
        group.MapPost("/", (CreateTodoDto dto, InMemoryTodoStore store) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return Results.BadRequest(new { Error = "Title cannot be empty." });
            }

            var newItem = store.Add(dto.Title);
            return Results.Created($"/api/todos/{newItem.Id}", newItem);
        });

        // 4. Latency test (Simulated 500ms delay)
        group.MapGet("/slow", async () =>
        {
            await Task.Delay(500);
            return Results.Ok(new { Status = "Completed after delay" });
        });

        // 5. Exception test (500 Internal Server Error)
        group.MapGet("/fail", () =>
        {
            throw new InvalidOperationException("Simulated unexpected database failure.");
        });

        return routes;
    }
}