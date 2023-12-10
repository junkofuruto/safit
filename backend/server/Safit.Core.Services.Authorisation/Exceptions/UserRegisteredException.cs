namespace Safit.Core.Services.Authorisation.Exceptions;

public sealed class UserRegisteredException : Exception
{
    public UserRegisteredException() : base("User already registered") { }
}

