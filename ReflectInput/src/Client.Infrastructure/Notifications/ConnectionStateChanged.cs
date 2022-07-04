using FSH.WebApi.Shared.Notifications;

namespace ReflectInput.Client.Infrastructure.Notifications;
public record ConnectionStateChanged(ConnectionState State, string? Message) : INotificationMessage;