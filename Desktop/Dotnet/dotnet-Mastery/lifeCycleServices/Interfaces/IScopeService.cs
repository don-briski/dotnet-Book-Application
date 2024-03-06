using System.Reflection.Metadata.Ecma335;

namespace IlifeCycleServices;

public interface IScopeService
{
    Guid GetGuid();
}

public interface ISingletonService
{
    Guid GetGuid();
}

public interface ITransientService
{
    Guid GetGuid();
}

