using IlifeCycleServices;


namespace lifeCycleServices{
    public class SingletonService : ISingletonService
    {
        private readonly Guid _guid;
        public SingletonService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return _guid;
        }
    }

 
}

