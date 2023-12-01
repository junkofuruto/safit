namespace Safit.Core.Services.Authorisation.Exceptions;

public class UserOrPasswordInvalidExeption : Exception
{
    public UserOrPasswordInvalidExeption() : base("User or password are invalid") { }
}