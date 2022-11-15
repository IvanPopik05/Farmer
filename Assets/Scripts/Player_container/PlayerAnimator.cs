using UnityEngine;

namespace Player_container
{
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly int WalkHash = Animator.StringToHash("Walk");
        private readonly int RunHash = Animator.StringToHash("Run");
        private readonly int PlantAPlantHash = Animator.StringToHash("PlantAPlant");
        private readonly int PullPlantHash = Animator.StringToHash("PullPlant");
        private readonly int IdleHash = Animator.StringToHash("Idle");
        private readonly int SitDownHash = Animator.StringToHash("Sitting");

        private readonly string PlantAPlantName = "PlantAPlant";
        private readonly string PullPlantName = "PullPlant";

        
        [SerializeField] private Animator _animator;

        public void PlayIdle() => _animator.SetTrigger(IdleHash);
        
        public void PlaySitDown() => _animator.SetTrigger(SitDownHash);

        public void PLayPlantAPlant() => _animator.SetTrigger(PlantAPlantHash);

        public void PlayPullPlant() => _animator.SetTrigger(PullPlantHash);

        public float GetCurrentAnimationLength() => 
            _animator.GetCurrentAnimatorStateInfo(0).length;

        public bool PullPlantAnimationCheck() => 
            _animator.GetCurrentAnimatorStateInfo(0).IsName(PullPlantName);
        
        public bool PlantAPlantAnimationCheck() => 
            _animator.GetCurrentAnimatorStateInfo(0).IsName(PlantAPlantName);

        public void PlayIsWalking(bool isActive)
        {
            if(isActive)
                PlayIsRunning(false);
            
            _animator.SetBool(WalkHash,isActive);
        }

        public void PlayIsRunning(bool isActive)
        {
            if(isActive)
                PlayIsWalking(false);
            
            _animator.SetBool(RunHash,isActive);
        }
    }
}