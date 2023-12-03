namespace Safit.Core.Services.Authorisation.Exceptions;

public sealed class UserAlreadyRegisteredException : Exception
{
    public UserAlreadyRegisteredException() : base("User already registered") { }
}

