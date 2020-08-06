using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private IronDoor _ironDoor;
    [SerializeField]
    private Animator _animator;

    private int collidingCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanPressPlate(collision))
        {
            collidingCount++;
            _ironDoor.OpenDoor();

            _animator.SetBool("IsPressed", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CanPressPlate(collision))
        {
            collidingCount--;

            if (collidingCount == 0)
            {
                _ironDoor.CloseDoor();
                _animator.SetBool("IsPressed", false);
            }
        }
    }

    private bool CanPressPlate(Collider2D collision)
        => collision.CompareTag("Player") || collision.CompareTag("Crate");
}