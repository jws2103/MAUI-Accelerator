using MauiAccelerator.Features.Common.Pages;

namespace MauiAccelerator.Features.Todo.Pages;

public partial class TodoListPage : BaseContentPage
{
    public TodoListPage(TodoListPageViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}