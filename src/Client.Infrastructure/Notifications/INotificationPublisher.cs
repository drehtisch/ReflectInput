using FSH.WebApi.Shared.Notifications;

namespace ReflectInput.Client.Infrastructure.Notifications;
public interface INotificationPublisher
{
    Task PublishAsync(INotificationMessage notification);
}