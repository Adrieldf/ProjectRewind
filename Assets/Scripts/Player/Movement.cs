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
    //[SerializeField]
    //private Transform _bulletSpawn;

    private bool _isOnTheFloor;
    private bool _isFacingRight = true;

    void Update()
    {
        CheckJump();
    }

    void FixedUpdate()
    {
        GetHorizontalMovement();
    }

    void GetHorizontalMovement()
    {
        var movement = Input.GetAxis("Horizontal");
        _animator.SetBool("isWalking", movement != 0);

        if (movement > 0 && !_isFacingRight || movement < 0 && _isFacingRight)
            Flip();

        _rigidbody.velocity = new Vector2(movement * Time.deltaTime * _speed, _rigidbody.velocity.y);
    }

    void CheckJump()
    {
        var spaceButton = Input.GetButtonDown("Jump");

        if (spaceButton && _isOnTheFloor)
            _rigidbody.velocity = Vector2.up * _jumpVelocity;
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

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}