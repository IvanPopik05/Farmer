using System.Threading.Tasks;

namespace Commands
{
    public abstract class Command
    {
        public bool IsExecuting { get; private set; }

        public async void Execute()
        {
            IsExecuting = true;
            await AsyncExecuter();
            IsExecuting = false;
        }

        protected abstract Task AsyncExecuter();
    }
}