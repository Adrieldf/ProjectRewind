using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _jumpVelocity = 7.5f;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _speed = 200;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rewind _rewind;
    [SerializeField]
    private Battery _battery = null;

    private bool _isOnTheFloor;
    private bool _isFacingRight = true;
    private bool _hasBatteryLeft = true;

    void Update()
    {
        CheckJump();

        _animator.SetBool("isOutOfBattery", _battery.BatteryLeft <= 0);
    }

    void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");

        var isMoving = movement != 0 && !_rewind.IsRewinding;

        _animator.SetBool("isWalking", isMoving && _hasBatteryLeft);

        if (isMoving)
            _hasBatteryLeft = _battery.ConsumeBattery();

        MoveHorizontally(_hasBatteryLeft && !_rewind.IsRewinding ? movement : 0f);
    }

    void MoveHorizontally(float movement)
    {
        if (movement > 0 && !_isFacingRight || movement < 0 && _isFacingRight)
            Flip();

        _rigidbody.velocity = new Vector2(movement * Time.deltaTime * _speed, _rigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isOnTheFloor = !_isOnTheFloor && isOnGround(other) ? true : _isOnTheFloor;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isOnGround(other))
            _isOnTheFloor = false;
    }

    private bool isOnGround(Collider2D other) => other.CompareTag("Ground") || other.CompareTag("Crate");

    private void CheckJump()
    {
        var spaceButton = Input.GetButtonDown("Jump");

        if (spaceButton && _isOnTheFloor && _hasBatteryLeft)
        {
            _rigidbody.velocity = Vector2.up * _jumpVelocity;
            _hasBatteryLeft = _battery.ConsumeBattery();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}