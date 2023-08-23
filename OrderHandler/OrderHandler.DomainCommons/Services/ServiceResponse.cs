namespace OrderHandler.DomainCommons.Services;

public record ServiceResponse<T>(bool Success, string Message, T? Data)
{
}