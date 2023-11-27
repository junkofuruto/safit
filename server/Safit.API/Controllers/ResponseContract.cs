namespace Safit.API.Controllers;

public sealed class ResponseContract<T> where T : ResponseBase
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Value { get; set; }
}