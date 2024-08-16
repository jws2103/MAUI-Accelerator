using System.Threading.Tasks;

namespace MauiAccelerator.Core.Extensions;

public static class TaskExtensions
{
    public static void Forget(this Task task)
    {
        task?.ConfigureAwait(false);
    } 
}