using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Messaging;

namespace MauiAccelerator.Core.Messenger;

[ExcludeFromCodeCoverage]
public class MessengerService : IMessengerService
{
    public void Register<TMessage>(object subscriber, MessageHandler<object, TMessage> handler) where TMessage : class
    {
        WeakReferenceMessenger.Default.Register(subscriber, handler);
    }
    
    public void Unregister<TMessage>(object subscriber) where TMessage : class
    {
        WeakReferenceMessenger.Default.Unregister<TMessage>(subscriber);
    }

    public void Send<TMessage>(TMessage message) where TMessage : class
    {
        WeakReferenceMessenger.Default.Send(message);
    }
}