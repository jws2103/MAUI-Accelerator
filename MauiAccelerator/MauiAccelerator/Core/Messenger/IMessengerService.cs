using CommunityToolkit.Mvvm.Messaging;

namespace MauiAccelerator.Core.Messenger;

public interface IMessengerService
{
    void Register<TMessage>(object subscriber, MessageHandler<object, TMessage> handler) where TMessage : class;

    void Unregister<TMessage>(object subscriber) where TMessage : class;

    void Send<TMessage>(TMessage message) where TMessage : class;
}