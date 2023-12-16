namespace Safit.Core.Domain.Repository.Exceptions;

public class EntityModificationUnallowedException : Exception
{
    public EntityModificationUnallowedException() : base("Modification unallowed") { }
}