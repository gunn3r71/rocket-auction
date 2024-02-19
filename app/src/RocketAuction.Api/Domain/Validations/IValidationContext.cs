namespace RocketAuction.Api.Domain.Validations;

public interface IValidationContext
{
    bool HasErrors();
    void AddNotification(string key, string message);
    IList<KeyValuePair<string, string>> GetNotifications();
}