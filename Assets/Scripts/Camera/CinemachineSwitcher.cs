using UnityEngine;

namespace Camera
{
    public class CinemachineSwitcher : MonoBehaviour
    {
        private const string CropPlantingView = "CropPlantingViewCamera";
        private const string CropView = "CropViewCamera";
        
        [SerializeField] private Animator _stateAnimator;

        private bool _isCropViewCamera = true;
        private void Start() => 
            SwitchState();
        
        public void SwitchState()
        {
            _stateAnimator.Play(_isCropViewCamera ? CropView : CropPlantingView);
            _isCropViewCamera = !_isCropViewCamera;
        }
    }
}