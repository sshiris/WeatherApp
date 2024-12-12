using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp;

public partial class TodoPage : ContentPage
{
    private readonly AppDbContext _dbContext;
    public TodoPage(AppDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
        LoadTodos();
    }

    private void LoadTodos()
    {
        var todos = _dbContext.Todos.ToList();
        TodoListView.ItemsSource = todos;
    }

    private async void OnAddTodoClicked(object sender, EventArgs e)
    {
        string? newTodoDescription = NewTodoEntry.Text?.Trim();
        if (!string.IsNullOrEmpty(newTodoDescription))
        {
            var newTodo = new Todo { Description = newTodoDescription, IsCompleted = false };

            await _dbContext.Todos.AddAsync(newTodo);
            await _dbContext.SaveChangesAsync();

            NewTodoEntry.Text = "";
            LoadTodos();

        }
    }
    private async void OnDeleteTodoClicked(object sender, EventArgs e)
    {
        var todoToDelete = (Todo)((Button)sender).BindingContext;
        if (todoToDelete != null)
        {
            _dbContext.Todos.Remove(todoToDelete);
            await _dbContext.SaveChangesAsync();
            LoadTodos();
        }
    }
}