namespace Safit.Core.Domain.Repository.Exceptions;

public class EntityNullException : Exception
{
    public EntityNullException() : base("Entity was null") { }
}