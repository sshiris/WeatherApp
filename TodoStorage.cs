using System.Collections.ObjectModel;

namespace WeatherApp;

public static class TodoStorage
{
    private static ObservableCollection<string> todos = new();

    public static void AddTodo(string todo)
    {
        todos.Add(todo);
    }
    public static void RemoveTodo(string todo)
    {
        todos.Remove(todo);
    }
    public static ObservableCollection<string> GetAllTodos()
    {
        return todos;
    }
}