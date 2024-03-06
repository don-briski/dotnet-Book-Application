namespace lifeCycleServices;

public class TransientService : ITransientService
{
    private readonly Guid _guid;
    public TransientService()
    {
        _guid = Guid.NewGuid();
    }

    public Guid GetGuid()
    {
        return _guid;
    }
}
