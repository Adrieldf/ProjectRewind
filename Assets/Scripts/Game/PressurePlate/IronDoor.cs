using UnityEngine;

public class IronDoor : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Collider2D _collider;

    private bool _isOpen;

    public void OpenDoor()
    {
        if (_isOpen)
            return;

        _isOpen = true;
        _collider.enabled = false;
        _animator.SetBool("IsOpen", true);
    }

    public void CloseDoor()
    {
        if (!_isOpen)
            return;

        _isOpen = false;
        _collider.enabled = true;
        _animator.SetBool("IsOpen", false);
    }
}