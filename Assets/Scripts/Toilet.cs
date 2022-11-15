using UnityEngine;

public class Toilet : MonoBehaviour
{
    private readonly int OpenTheDoorHash = Animator.StringToHash("OpenDoor");
    private readonly int CloseTheDoorHash = Animator.StringToHash("CloseDoor");
        
    [SerializeField] private Animator _animator;
    public void OpenTheDoor() => 
        _animator.SetTrigger(OpenTheDoorHash);
        
    public void CloseTheDoor() => 
        _animator.SetTrigger(CloseTheDoorHash);
}