namespace Safit.Core.Domain.Service.Response;

public class ResponseContract<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Value { get; set; }
}