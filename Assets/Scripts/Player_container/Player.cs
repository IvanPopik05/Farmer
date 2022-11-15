using System;
using System.Collections.Generic;
using Camera;
using Cell_container;
using Commands;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using Plants;
using States;
using UnityEngine;
using UnityEngine.EventSystems;
using Tree = Plants.Tree;

namespace Player_container
{
    public enum ActionType
    {
        RunToPlantAPlant,
        RunToPullPlant,
        GoToToilet,
        GoToRest
    }

    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerAgent _playerAgent;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CinemachineSwitcher _cinemachineSwitcher;
        [SerializeField] private Transform _restZone;
        [SerializeField] private Toilet _toilet;

        private int _amountOfExperience;
        private int _amountOfCarrot;
        
        private PlayerStateMachine _stateMachine;
        private UnityEngine.Camera _mainCamera;
        
        private Dictionary<Cell, BasePlant> _plantedPlantsDictionary;
        private CommandActionState _activeCommandActionState;
        private Queue<CommandActionState> _commandQueue = new Queue<CommandActionState>();

        public event Action<int> GotAmountOfCarrot;
        public event Action<int> GotAmountOfExperience;

        public bool IsExecutingCommand { get; set; }
        public PlayerAnimator PlayerAnimator => _playerAnimator;
        public PlayerAgent PlayerAgent => _playerAgent;
        public CinemachineSwitcher CinemachineSwitcher => _cinemachineSwitcher;

        public void Initialize()
        {
            _plantedPlantsDictionary = new Dictionary<Cell,BasePlant>();
            
            _mainCamera = UnityEngine.Camera.main;
            _playerAgent.Initialize(_playerAnimator);

            _stateMachine = new PlayerStateMachine(this,_toilet, _restZone);
            _stateMachine.Enter<PlayerGoingToRestState>();
        }

        public void Update()
        {
            MouseClickOnAComponent();

            if (_commandQueue.Count > 0 && (_activeCommandActionState == null || !IsExecutingCommand))
                UsingCommandsInAQueue();

            if (IsActiveCommandStateQueue() && CheckingTheStateGoToTheToilet())
                _stateMachine.Enter<PlayerGoingToRestState>();

            _stateMachine.Update();
        }

        private void UsingCommandsInAQueue()
        {
            _stateMachine.Enter<PlayerIdleState>();
            _activeCommandActionState = _commandQueue.Dequeue();
            IsExecutingCommand = true;
            _activeCommandActionState.Execute();
        }

        private void MouseClickOnAComponent()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    TryToPlantOrPullThePlant(hit);
                    TryToGoToTheToilet(hit);
                }
            }
        }

        private void TryToGoToTheToilet(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Toilet toilet) && IsActiveCommandStateQueue())
            {
                toilet.OpenTheDoor();
                _stateMachine.Enter<PlayerGoingToToiletState>();
            }
        }

        private void TryToPlantOrPullThePlant(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Cell cell)
                && CheckingTheStateGoToTheToilet())
            {
                TryPlantAPlant(cell);
                TryPullPlant(cell);
            }
        }

        private bool CheckingTheStateGoToTheToilet() => 
            _stateMachine.CurrentState.GetType() != typeof(PlayerGoingToToiletState);

        private bool IsActiveCommandStateQueue() => 
            _commandQueue.Count <= 0  && (_activeCommandActionState == null || !IsExecutingCommand);

        private void TryPlantAPlant(Cell cell)
        {
            if (cell.IsFree)
            {
                cell.ChoseAPlant += ChoseAPlant;
                cell.HarvestSelection();
            }
        }

        private void TryPullPlant(Cell cell)
        {
            if (!cell.IsFree && cell.IsRipened)
            {
                if (cell.Plant.GetType() != typeof(Tree))
                    _commandQueue.Enqueue(new CommandActionState(new RunToPullPlantCommand(this, cell)));
                
                cell.DeactivatePanel();
            }
        }

        private void ChoseAPlant(BasePlant plant, Cell cell)
        {
            cell.ChoseAPlant -= ChoseAPlant;
            _commandQueue.Enqueue(new CommandActionState(new RunToPlantAPlantCommand(this,cell.Plant,cell)));
        }

        public void PlantAPlant(Cell cell, BasePlant plant)
        {
            BasePlant newPlant = Instantiate(plant, cell.transform.position, Quaternion.identity,cell.transform);
            newPlant.Initialize(cell);
            cell.AddPlant(newPlant);

            if(!_plantedPlantsDictionary.ContainsKey(cell))
                _plantedPlantsDictionary.Add(cell, newPlant);
            cell.ShowDisplayPlantProcess();
        }

        public void PullPlant(Cell cell)
        {
            if(_plantedPlantsDictionary.TryGetValue(cell, out BasePlant plant))
            {
                if (plant.GetType() == typeof(Carrot))
                {
                    _amountOfCarrot += 1;
                    GotAmountOfCarrot?.Invoke(_amountOfCarrot);
                }
                
                _amountOfExperience += cell.PlantConfig.Experience;
                GotAmountOfExperience?.Invoke(_amountOfExperience);

                if (plant.GetType() == typeof(Tree)) 
                    return;
                
                cell.DeactivatePanel();
                _plantedPlantsDictionary.Remove(cell);
                cell.MakePlace();
            }
        }
    }
}