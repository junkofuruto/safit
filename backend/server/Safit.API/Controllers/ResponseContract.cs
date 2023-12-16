﻿namespace Safit.API.Controllers;

public sealed class ResponseContract<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Value { get; set; }

    private ResponseContract() {}

    public static ResponseContract<T> Create(T value)
    {
        return new ResponseContract<T>()
        {
            Success = true,
            Message = string.Empty,
            Value = value
        };
    }

    public static ResponseContract<T> Create(Exception exception)
    {
        var response = new ResponseContract<T>()
        {
            Success = false,
            Message = $"{exception.Source}: {exception.Message}"
        };
        if (exception.InnerException != null) response.Message = exception.InnerException.Message;
        return response;
    }
}