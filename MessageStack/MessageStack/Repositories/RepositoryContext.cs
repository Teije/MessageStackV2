namespace MessageStack.Repositories
{
    public class RepositoryContext
    {
        private RepositoryContext()
        {
        }

        private static Context.MessageStackContext UniqueInstance;

        public static Context.MessageStackContext GetInstance()
        {
            UniqueInstance = UniqueInstance ?? new Context.MessageStackContext();

            return UniqueInstance;
        }
    }
}