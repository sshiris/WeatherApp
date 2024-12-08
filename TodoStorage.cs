namespace WeatherApp;

public static class TodoStorage
{
    private static List<string> todos = new();

    public static void AddTodo(string todo)
    {
        todos.Add(todo);
    }
    public static void RemoveTodo(string todo)
    {
        todos.Remove(todo);
    }
    public static List<string> GetAllTodos()
    {
        return todos;
    }
}