using IlifeCycleServices;


namespace lifeCycleServices
{
    public class ScopeService : IScopeService
    {
        private readonly Guid _guid;
        public ScopeService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return _guid;
        }
    }
}