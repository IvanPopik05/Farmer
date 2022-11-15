using System.Threading.Tasks;

namespace Commands
{
    public class CommandActionState : Command
    {
        public Command Command { get; }

        public CommandActionState(Command command) => 
            Command = command;

        protected override async Task AsyncExecuter()
        {
            Command.Execute();
            await Task.Delay(20);   
        }
    }
}