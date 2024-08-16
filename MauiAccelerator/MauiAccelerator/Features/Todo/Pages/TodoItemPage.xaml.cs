using MauiAccelerator.Features.Common.Pages;

namespace MauiAccelerator.Features.Todo.Pages;

public partial class TodoItemPage : BaseContentPage
{
    public TodoItemPage(TodoItemPageViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}