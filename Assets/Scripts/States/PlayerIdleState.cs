namespace States
{
    public class PlayerIdleState : IState
    {
        private PlayerStateMachine _stateMachine;
        
        public PlayerIdleState(PlayerStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter() { }

        public void Exit() { }

        public void Update() { }
    }
}