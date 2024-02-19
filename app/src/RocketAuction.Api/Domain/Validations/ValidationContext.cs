namespace RocketAuction.Api.Domain.Validations;

public class ValidationContext : IValidationContext
{
    private IList<KeyValuePair<string, string>> Notifications { get; } = new List<KeyValuePair<string, string>>();

    public bool HasErrors() => Notifications.Count > 0;

    public void AddNotification(string key, string message)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key), "Chave não pode ser nula.");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message), "Mensagem não pode ser nula.");
        
        Notifications.Add(new KeyValuePair<string, string>(key, message));
    }

    public IList<KeyValuePair<string, string>> GetNotifications() =>
        Notifications;
}