using System.Collections;
using DG.Tweening;
using Player_container;
using UnityEngine;

namespace States
{
    public class PlayerGoingToRestState : IState
    {
        private Player _player;
        private Transform _restZone;
        private PlayerStateMachine _stateMachine;
        
        public PlayerGoingToRestState(PlayerStateMachine stateMachine, Player player, Transform restZone)
        {
            _player = player;
            _restZone = restZone;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _player.PlayerAgent.GoToThePlace(_restZone.position);
            _player.PlayerAgent.WentToTheRest += WentToTheRest;
        }

        private void WentToTheRest()
        {
            _player.PlayerAgent.WentToTheRest -= WentToTheRest;
            _player.StartCoroutine(RestCoroutine());
        }


        private IEnumerator RestCoroutine()
        {
            _player.PlayerAgent.IsStopAgent(true);
            _player.PlayerAnimator.PlayIsWalking(false);

            yield return _player.transform.DORotate(new Vector3(0, 70, 0), 1f);
            _player.PlayerAnimator.PlaySitDown();
        }

        public void Exit() => 
            _player.PlayerAgent.WentToTheRest -= WentToTheRest;

        public void Update() => 
            _player.PlayerAgent.UpdateDistance(ActionType.GoToRest);
    }
}