using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _jumpVelocity = 5;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _speed = 200;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rewind _rewind;

    private bool _isOnTheFloor;
    private bool _isFacingRight = true;

    void Update()
    {
        CheckJump();
    }

    void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");

        var isMoving = movement != 0 || _rewind.IsRewinding;
        _animator.SetBool("isWalking", isMoving);

        MoveHorizontally(movement);
    }

    void MoveHorizontally(float movement)
    {
        if (movement > 0 && !_isFacingRight || movement < 0 && _isFacingRight)
            Flip();

        _rigidbody.velocity = new Vector2(movement * Time.deltaTime * _speed, _rigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isOnTheFloor = !_isOnTheFloor && other.CompareTag("Ground") ? true : _isOnTheFloor;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            _isOnTheFloor = false;
    }

    private void CheckJump()
    {
        var spaceButton = Input.GetButtonDown("Jump");

        if (spaceButton && _isOnTheFloor)
            _rigidbody.velocity = Vector2.up * _jumpVelocity;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}