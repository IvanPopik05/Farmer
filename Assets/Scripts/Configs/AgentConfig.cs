using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Agent config",menuName = "Configs/Agent",order = 1)]
    public class AgentConfig : ScriptableObject
    {
        [SerializeField,Range(1,20)] private float _walkingSpeed;
        [SerializeField,Range(1,20)] private float _runningSpeed;

        public float WalkingSpeed => _walkingSpeed;
        public float RunningSpeed => _runningSpeed;
    }
}