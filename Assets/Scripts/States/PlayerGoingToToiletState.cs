using System.Collections;
using DG.Tweening;
using Player_container;
using UnityEngine;

namespace States
{
    public class PlayerGoingToToiletState : IState
    {
        private Player _player;
        private Toilet _toilet;
        private PlayerStateMachine _stateMachine;

        public PlayerGoingToToiletState(PlayerStateMachine stateMachine, Player player, Toilet toilet)
        {
            _player = player;
            _toilet = toilet;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _player.PlayerAgent.GoToThePlace(_toilet.transform.position);
            _player.PlayerAgent.WentToTheToilet += WentToTheToilet;
        }

        private void WentToTheToilet()
        {
            _player.PlayerAgent.WentToTheToilet -= WentToTheToilet;
            _player.StartCoroutine(InTheToiletCoroutine());
        }


        private IEnumerator InTheToiletCoroutine()
        {
            _player.PlayerAgent.IsStopAgent(true);
            _player.PlayerAnimator.PlayIsWalking(false);
            _player.PlayerAnimator.PlayIdle();
            
            _toilet.CloseTheDoor();
            yield return new WaitForSeconds(5f);
            _toilet.OpenTheDoor();
            _stateMachine.Enter<PlayerGoingToRestState>();
            yield return new WaitForSeconds(2f);
            _toilet.CloseTheDoor();
        }

        public void Exit() => 
            _player.PlayerAgent.WentToTheToilet -= WentToTheToilet;

        public void Update() => 
            _player.PlayerAgent.UpdateDistance(ActionType.GoToToilet);
    }
}