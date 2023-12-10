namespace Safit.Core.Services.Authorisation.Exceptions;

public class InvalidCredentialsExeption : Exception
{
    public InvalidCredentialsExeption() : base("User or password are invalid") { }
}