using System.Collections;
using System.Threading.Tasks;
using Cell_container;
using DG.Tweening;
using Plants;
using Player_container;
using UnityEngine;

namespace Commands
{
    public class RunToPlantAPlantCommand : Command
    {
        private Player _player;
        private Cell _currentCell;
        private BasePlant _currentPlant;
        
        public RunToPlantAPlantCommand(Player player, BasePlant currentPlant, Cell cell)
        {
            _player = player;
            _currentPlant = currentPlant;
            _currentCell = cell;
        }

        private void PlantedPlant()
        {
            _player.PlayerAgent.PlantedPlant -= PlantedPlant;
            _player.StartCoroutine(PlayPlantAPlantCoroutine());
        }

        private IEnumerator PlayPlantAPlantCoroutine()
        {
            _player.PlayerAnimator.PlayIsRunning(false);
            _player.PlayerAnimator.PLayPlantAPlant();
            _player.PlayerAgent.IsStopAgent(true);
            _player.CinemachineSwitcher.SwitchState();
            
            yield return _player.transform.DOLookAt(_currentCell.transform.position, 1f).WaitForCompletion();
            yield return new WaitUntil(() =>  _player.PlayerAnimator.PlantAPlantAnimationCheck());
            yield return new WaitForSeconds(_player.PlayerAnimator.GetCurrentAnimationLength());
            
            _player.PlantAPlant(_currentCell,_currentPlant);
            _player.CinemachineSwitcher.SwitchState();
            _player.IsExecutingCommand = false;
        }

        protected override async Task AsyncExecuter()
        {
            _player.PlayerAgent.PlantedPlant += PlantedPlant;
            _player.PlayerAgent.RunToThePlant(_currentCell);
            while (_player.IsExecutingCommand)
            {
                _player.PlayerAgent.UpdateDistance(ActionType.RunToPlantAPlant);
                await Task.Delay(20);
            }
        }
    }
}