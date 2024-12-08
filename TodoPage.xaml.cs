namespace WeatherApp;

public partial class TodoPage : ContentPage
{
    public TodoPage()
    {
        InitializeComponent();
        LoadTodos();
    }

    private void LoadTodos()
    {
        TodoListView.ItemsSource = TodoStorage.GetAllTodos();
    }

    private void OnAddTodoClicked(object sender, EventArgs e)
    {
        string newTodo = NewTodoEntry.Text?.Trim();
        if (!string.IsNullOrEmpty(newTodo))
        {
            TodoStorage.AddTodo(newTodo);
            NewTodoEntry.Text = "";
        }
    }
    private void OnDeleteTodoClicked(object sender, EventArgs e)
    {
        string todoDelete = (string)((Button)sender).CommandParameter;
        TodoStorage.RemoveTodo(todoDelete);
    }
}