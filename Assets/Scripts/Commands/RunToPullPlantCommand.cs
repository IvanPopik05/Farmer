using System.Collections;
using System.Threading.Tasks;
using Cell_container;
using DG.Tweening;
using Player_container;
using UnityEngine;

namespace Commands
{
    public class RunToPullPlantCommand : Command
    {
        private Player _player;
        private Cell _currentCell;

        public RunToPullPlantCommand(Player player, Cell cell)
        {
            _player = player;
            _currentCell = cell;
        }

        private void PulledPlant()
        {
            _player.PlayerAgent.PulledPlant += PulledPlant;
            _player.StartCoroutine(PlayPullPlantCoroutine());
        }


        private IEnumerator PlayPullPlantCoroutine()
        {
            _player.PlayerAnimator.PlayIsRunning(false);
            _player.PlayerAnimator.PlayPullPlant();
            _player.PlayerAgent.IsStopAgent(true);
            _player.CinemachineSwitcher.SwitchState();
            
            yield return _player.transform.DOLookAt(_currentCell.transform.position, 1f).WaitForCompletion();
            yield return new WaitUntil(() =>  _player.PlayerAnimator.PullPlantAnimationCheck());
            yield return new WaitForSeconds( _player.PlayerAnimator.GetCurrentAnimationLength());
            
            _player.PullPlant(_currentCell);
            _player.CinemachineSwitcher.SwitchState();
            _player.IsExecutingCommand = false;
        }

        protected override async Task AsyncExecuter()
        {
            _player.PlayerAgent.PulledPlant += PulledPlant;
            _player.PlayerAgent.RunToThePlant(_currentCell);
            while (_player.IsExecutingCommand)
            {
                _player.PlayerAgent.UpdateDistance(ActionType.RunToPullPlant);
                await Task.Delay(20);
            }
        }
    }
}