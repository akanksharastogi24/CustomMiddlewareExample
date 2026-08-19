using CustomMiddlewareExample.Models;

namespace CustomMiddlewareExample.Data
{
    public class InMemoryTodoStore
    {
        private readonly List<TodoItem> _todos = new()
    {
        new(1, "Configure logging", true),
        new(2, "Test middleware pipeline", false)
    };

        public IReadOnlyList<TodoItem> GetAll() => _todos.AsReadOnly();

        public TodoItem? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);

        public TodoItem Add(string title)
        {
            var newItem = new TodoItem(_todos.Count + 1, title, false);
            _todos.Add(newItem);
            return newItem;
        }
    }
}
