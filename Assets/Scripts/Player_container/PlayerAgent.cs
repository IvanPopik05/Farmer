using System;
using Cell_container;
using Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Player_container
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerAgent : MonoBehaviour
    {
        [SerializeField] private AgentConfig _agentConfig;
        
        private NavMeshAgent _agent;
        private PlayerAnimator _playerAnimator;

        private Vector3 _currentPoint;
        
        public event Action PulledPlant;
        public event Action PlantedPlant;
        public event Action WentToTheToilet;
        public event Action WentToTheRest;
        
        public void Initialize(PlayerAnimator playerAnimator)
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerAnimator = playerAnimator;
        }
        
        public void GoToThePlace(Vector3 point)
        {
            _agent.isStopped = false;
            
            _currentPoint = point;
            _agent.SetDestination(_currentPoint);
            
            _playerAnimator.PlayIsWalking(true);
            _agent.speed = _agentConfig.WalkingSpeed;
        }
        
        public void RunToThePlant(Cell cell)
        {
            _agent.isStopped = false;
            _currentPoint = cell.transform.position;
            
            _agent.SetDestination(_currentPoint);
            _agent.speed = _agentConfig.RunningSpeed;
            _playerAnimator.PlayIsRunning(true);
        }

        public void UpdateDistance(ActionType actionType)
        {
            Debug.DrawLine(_agent.transform.position, _currentPoint,Color.yellow);
            float distance = (_agent.transform.position - _currentPoint).sqrMagnitude;

            switch (actionType)
            {
                case ActionType.RunToPlantAPlant:
                    if (distance < 1.5f && !_agent.isStopped) 
                        PlantedPlant?.Invoke();
                    break;
                case ActionType.RunToPullPlant:
                    if (distance < 1.5f && !_agent.isStopped) 
                        PulledPlant?.Invoke();
                    break;
                case ActionType.GoToToilet:
                    if (distance < 0.2f && !_agent.isStopped) 
                        WentToTheToilet?.Invoke();
                    break;
                case ActionType.GoToRest:
                    if (distance < 0.2f && !_agent.isStopped) 
                        WentToTheRest?.Invoke();
                    break;
            }
        }
        
        public void IsStopAgent(bool isStop)
        {
            if (_agent.isActiveAndEnabled) 
                _agent.isStopped = isStop;
        }
    }
}